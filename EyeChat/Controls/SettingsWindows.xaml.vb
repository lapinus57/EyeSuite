Imports EyeChat.EyeChat
Imports EyeChat.Models
Imports EyeChat.Networking
Imports EyeChat.ViewModel
Imports log4net
Imports Microsoft.Win32
Imports System.Collections.ObjectModel
Imports System.IO

Namespace Controls
    Public Class SettingsWindows

        Private _settings As SettingsViewModel

        Public ReadOnly Property Settings As SettingsViewModel
            Get
                If _settings Is Nothing Then
                    _settings = New SettingsViewModel()
                End If
                Return _settings
            End Get
        End Property

        Private Sub AppColorChanged(sender As Object, e As SelectionChangedEventArgs)
            SettingsViewModel.SetTheme()
        End Sub

        Private Sub AppThemeChanged(sender As Object, e As SelectionChangedEventArgs)
            SettingsViewModel.SetTheme()
        End Sub

        Private Sub SettingsWindows_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
            InitializeComponent()
            Dim settings As New SettingsViewModel()
            Me.DataContext = settings

            LoadAvatars()
        End Sub

        Public Sub LoadAvatars()
            Dim avatarFolder As String = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Avatar")

            If Directory.Exists(avatarFolder) Then
                cboAvatars.Items.Clear()

                Dim imageFiles() As String = Directory.GetFiles(avatarFolder, "*.png")

                cboAvatars.Items.Add(New AvatarItemModels() With {
                    .ImagePath = "",
                    .Width = 25,
                    .Height = 25,
                    .Tag = "Importer un avatar"
                })

                For Each imagePath As String In imageFiles
                    Dim fileName As String = Path.GetFileName(imagePath)
                    cboAvatars.Items.Add(New AvatarItemModels() With {
                        .ImagePath = imagePath,
                        .Width = 25,
                        .Height = 25,
                        .Tag = fileName
                    })
                Next
            Else
                ' Gérer l'absence du dossier ici si nécessaire
            End If
        End Sub

        Private Sub CboAvatars_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim selectedItem As AvatarItemModels = TryCast(cboAvatars.SelectedItem, AvatarItemModels)

            If selectedItem Is Nothing Then Return

            If selectedItem.Tag IsNot Nothing AndAlso selectedItem.Tag.ToString() = "Importer un avatar" Then
                SelectImageFile()
            Else
                If selectedItem.Tag IsNot Nothing Then
                    SendManager.SendMessage("USR11" & UserSettingsList.UserName & "|/Avatar/" & selectedItem.Tag.ToString())
                    UserSettingsList.UserAvatar = selectedItem.Tag.ToString()
                    UserSettingsList.SaveUserSettingsToJson(UserSettingsList.UserName)
                End If
            End If
        End Sub

        Private Sub SelectAvatarByIndex(avatarId As String)
            For i As Integer = 0 To cboAvatars.Items.Count - 1
                Dim avatarItem As AvatarItemModels = TryCast(cboAvatars.Items(i), AvatarItemModels)
                If avatarItem IsNot Nothing AndAlso avatarItem.Tag.ToString() = avatarId Then
                    cboAvatars.SelectedIndex = i
                    Exit Sub
                End If
            Next
        End Sub

        Private Sub SelectImageFile()
            Dim openFileDialog As New OpenFileDialog() With {
                .Title = "Sélectionnez une image",
                .Filter = "Fichiers d'image|*.png;*.jpg;*.jpeg;*.bmp;*.gif|Tous les fichiers|*.*"
            }

            Dim result As Boolean? = openFileDialog.ShowDialog()

            If result = True Then
                Dim selectedImagePath As String = openFileDialog.FileName
                Dim image As New BitmapImage(New Uri(selectedImagePath))

                Dim maxWidthPixels As Integer = 1000
                Dim maxHeightPixels As Integer = 1000

                If image.PixelWidth <= maxWidthPixels AndAlso image.PixelHeight <= maxHeightPixels Then
                    Dim destinationFolder As String = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Avatar")

                    If Not Directory.Exists(destinationFolder) Then
                        Directory.CreateDirectory(destinationFolder)
                    End If

                    Dim fileName As String = System.IO.Path.GetFileName(selectedImagePath)
                    Dim destinationPath As String = System.IO.Path.Combine(destinationFolder, fileName)

                    File.Copy(selectedImagePath, destinationPath, True)

                    cboAvatars.Items.Clear()
                    LoadAvatars()
                Else
                    ' Gérer le cas où l'image est trop grande ici si nécessaire
                End If
            End If
        End Sub

        Private Sub ToggleSwitch_Toggled(sender As Object, e As RoutedEventArgs)
            My.Application.MainWindow.UpdateLayout()
        End Sub

        Private Sub ExamDataGrid_CellEditEnding(sender As Object, e As DataGridCellEditEndingEventArgs) Handles ExamDataGrid.CellEditEnding
            Dim editedItem As ExamOptionModels = TryCast(e.Row.Item, ExamOptionModels)

            If editedItem IsNot Nothing Then
                If e.Column.Header.ToString() = "Name" Then
                    Dim editedValue As String = TryCast((TryCast(e.EditingElement, TextBox))?.Text, String)
                    editedItem.Name = editedValue
                ElseIf e.Column.Header.ToString() = "Color" Then
                    Dim colorPicker As MahApps.Metro.Controls.ColorPicker = TryCast(e.EditingElement, MahApps.Metro.Controls.ColorPicker)
                    If colorPicker IsNot Nothing Then
                        editedItem.Color = colorPicker.SelectedColor.ToString()
                    End If
                End If

                Dim examOptionList As List(Of ExamOptionModels) = ExamDataGrid.ItemsSource.Cast(Of ExamOptionModels)().ToList()
                ExamOptionsListGlobal = New ObservableCollection(Of ExamOptionModels)(examOptionList)
                ExamOptionModels.SaveExamOptionsToJson()
            End If
        End Sub

        Private Sub SaveExamChangesButton_Click(sender As Object, e As RoutedEventArgs)
            Dim examOptionList As List(Of ExamOptionModels) = ExamDataGrid.ItemsSource.Cast(Of ExamOptionModels)().ToList()
            ExamOptionsListGlobal = New ObservableCollection(Of ExamOptionModels)(examOptionList)

            ExamOptionModels.SaveExamOptionsToJson()
            NetworkingFileSender.SendFileOverNetwork("Assets/Config", "ExamOptions.json")
            ExamOptionModels.LoadExamOptionFromJson()
        End Sub

        Private Sub SavePlanningChangesButton_Click(sender As Object, e As RoutedEventArgs)
            ''Dim PlanningList As List(Of Planning) = PlanningDataGrid.ItemsSource.Cast(Of Planning)().ToList()
            ''Dim PlanningCollection As New ObservableCollection(Of Planning)(PlanningList)

            ''Planning.SavePlanningToJson(PlanningCollection)
        End Sub

        Private Sub SaveSpeedMessageChangesButton_Click(sender As Object, e As RoutedEventArgs)
            SpeedMessagesListGlobal = SpeedMessageDataGrid.ItemsSource.Cast(Of SpeedMessageModels)().ToList()
            SpeedMessageModels.SaveSpeedMessageToJson()
        End Sub

        Private Sub ExamDataGrid_AddingNewItem(sender As Object, e As AddingNewItemEventArgs) Handles ExamDataGrid.AddingNewItem
            Dim examOptionList As List(Of ExamOptionModels) = ExamDataGrid.ItemsSource.Cast(Of ExamOptionModels)().ToList()
            Dim newExamOption As New ExamOptionModels() With {
                .Index = examOptionList.Count + 1
            }
            e.NewItem = newExamOption
            ExamOptionsListGlobal = New ObservableCollection(Of ExamOptionModels)(examOptionList)
            ExamOptionModels.SaveExamOptionsToJson()
        End Sub

        Private Sub SpeedMessageGrid_AddingNewItem(sender As Object, e As AddingNewItemEventArgs) Handles SpeedMessageDataGrid.AddingNewItem
            Dim SpeedMessageList As List(Of SpeedMessageModels) = SpeedMessageDataGrid.ItemsSource.Cast(Of SpeedMessageModels)().ToList()

            Dim newSpeedMessage As New SpeedMessageModels() With {
                .Index = SpeedMessageList.Count + 1
            }

            e.NewItem = newSpeedMessage

            SpeedMessagesListGlobal = New List(Of SpeedMessageModels)(SpeedMessageList)

            SpeedMessageModels.SaveSpeedMessageToJson()
        End Sub

        Private Sub ColorPicker_DropDownClosed(sender As Object, e As EventArgs)
            Dim colorPicker As MahApps.Metro.Controls.ColorPicker = CType(sender, MahApps.Metro.Controls.ColorPicker)
            SettingsViewModel.SetTheme()
        End Sub

        Private Sub SettingsWindows_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
            SelectAvatarByIndex(UserSettingsList.UserAvatar)
        End Sub
    End Class
End Namespace
