Imports EyeChat.Models
Imports System.IO
Imports EyeChat.Networking
Imports Microsoft.Win32
Imports ControlzEx.Theming
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports EyeChat.EyeChat
Imports EyeChat.Utilities

Namespace Controls.Settings
    Public Class UserSettingsControl

        Public Sub New()

            InitializeComponent()
            'LoadAvatars()
            'SelectAvatarByIndex(UserSettingsList.UserAvatar)
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
        Private Sub SelectAvatarByIndex(avatarId As String)
            For i As Integer = 0 To cboAvatars.Items.Count - 1
                Dim avatarItem As AvatarItemModels = TryCast(cboAvatars.Items(i), AvatarItemModels)
                If avatarItem IsNot Nothing AndAlso avatarItem.Tag.ToString() = avatarId Then
                    cboAvatars.SelectedIndex = i
                    Exit Sub
                End If
            Next
        End Sub


        Private Sub ColorPicker_DropDownClosed(sender As Object, e As EventArgs)
            Dim colorPicker As MahApps.Metro.Controls.ColorPicker = CType(sender, MahApps.Metro.Controls.ColorPicker)
            SetTheme()
        End Sub
        Private Sub AppColorChanged(sender As Object, e As SelectionChangedEventArgs)
            SetTheme()
        End Sub

        Private Sub AppThemeChanged(sender As Object, e As SelectionChangedEventArgs)
            SetTheme()
        End Sub

        Public Shared Sub SetTheme()
            Try
                ' Récupérer le nom du thème (clair ou sombre)
                Dim themeName As String = If(UserSettingsList IsNot Nothing AndAlso UserSettingsList.AppTheme = "Clair", "Light", "Dark")


                ' Récupérer la couleur sélectionnée à partir de My.Settings.AppColor
                Dim mediaColor As System.Windows.Media.Color = System.Windows.Media.Color.FromArgb(
            UserSettingsList.AppColor.A,
            UserSettingsList.AppColor.R,
            UserSettingsList.AppColor.G,
            UserSettingsList.AppColor.B)

                ' Détecter l'application actuelle
                Dim application = System.Windows.Application.Current

                ' Vérifier si l'application et ses dictionnaires de ressources existent
                If application IsNot Nothing AndAlso application.Resources IsNot Nothing Then
                    ' Nettoyer les thèmes existants en double
                    CleanExistingThemes()

                    ' Regénérer un nouveau thème si nécessaire
                    Dim newTheme As Theme = RuntimeThemeGenerator.Current.GenerateRuntimeTheme(themeName, mediaColor)
                    ' Changer le thème de l'application en utilisant le nouvel objet Theme
                    ThemeManager.Current.ChangeTheme(System.Windows.Application.Current, newTheme)

                    ' Rechercher si un dictionnaire de ressources existe déjà avec ce thème
                    Dim existingResourceDictionary = application.Resources.MergedDictionaries.FirstOrDefault(Function(rd) rd.Source IsNot Nothing AndAlso rd.Source.ToString().Contains("AppTheme"))

                    ' Si un dictionnaire de ressources existe avec le même thème, ne pas ajouter
                    If existingResourceDictionary IsNot Nothing Then
                        logger.Info("Le thème existe déjà, aucun besoin de le regénérer.")
                    Else
                        ' Ajouter le nouveau thème
                        application.Resources.MergedDictionaries.Add(newTheme.Resources)
                        ThemeManager.Current.ChangeTheme(application, newTheme)
                    End If
                End If

            Catch ex As Exception
                ' Gérer l'erreur
                logger.Error($"Erreur lors de la modification du thème : {ex.Message}")
            End Try
        End Sub

        ' Méthode pour nettoyer les thèmes en double dans le ResourceDictionary
        Private Shared Sub CleanExistingThemes()
            Dim application = System.Windows.Application.Current

            If application IsNot Nothing AndAlso application.Resources.MergedDictionaries IsNot Nothing Then
                ' Supprimer les dictionnaires de ressources en double qui contiennent "AppTheme"
                Dim themesToRemove = application.Resources.MergedDictionaries.Where(Function(rd) rd.Source IsNot Nothing AndAlso rd.Source.ToString().Contains("AppTheme")).ToList()

                For Each theme In themesToRemove
                    application.Resources.MergedDictionaries.Remove(theme)
                Next
            End If
        End Sub


    End Class
End Namespace
