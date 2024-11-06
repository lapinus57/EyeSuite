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



        Private Sub SettingsWindows_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
            InitializeComponent()
            Dim settings As New SettingsViewModel()
            Me.DataContext = settings

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



        Private Sub SettingsWindows_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        End Sub
    End Class
End Namespace
