Imports EyeChat.EyeChat
Imports EyeChat.Networking
Imports MahApps.Metro.Controls.Dialogs

Namespace Utilities
    Public Class DialogHelper
        Private mainWindow As MainWindow

        ' Constructeur pour passer l'instance de MainWindow
        Public Sub New(mainWin As MainWindow)
            mainWindow = mainWin
        End Sub

        Public Async Function ShowSpeedMessageDialogAsync(ByVal Titre As String, ByVal Message As String, ByVal option1 As String, ByVal option2 As String, ByVal option3 As String) As Task(Of String)
            Try
                Dim dialogSettings As New MetroDialogSettings With {
                .AffirmativeButtonText = option1,
                .NegativeButtonText = option2,
                .FirstAuxiliaryButtonText = option3
            }

                Dim result As MessageDialogResult = Await DialogCoordinator.Instance.ShowMessageAsync(mainWindow, Titre, Message, MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, dialogSettings)

                Select Case result
                    Case MessageDialogResult.Affirmative
                        Return option1
                    Case MessageDialogResult.Negative
                        Return option2
                    Case MessageDialogResult.FirstAuxiliary
                        Return option3
                    Case Else
                        Return Nothing ' Aucune option sélectionnée
                End Select
            Catch ex As Exception
                logger.Error("Erreur lors de l'affichage du message dialog : " & ex.Message)
                Return Nothing
            End Try
        End Function

        Public Sub SendSpeedMessage(ByVal selectedOption As String, ByVal Titre As String)
            Dim message As String = GetSpeedMessage(selectedOption, Titre)
            If message IsNot Nothing Then
                SendManager.SendMessage(message)
            End If
        End Sub

        Private Function GetSpeedMessage(ByVal selectedOption As String, ByVal Titre As String) As String
            Select Case selectedOption
                Case "0"
                    Return $"MSG01{UserSettingsList.UserName}|A Tous|{Environment.UserName}|{Titre} je viens au plus vite|{UserSettingsList.UserAvatar}"
                Case "1"
                    Return $"MSG01{UserSettingsList.UserName}|A Tous|{Environment.UserName}|{Titre} je viens dans 2 minutes|{UserSettingsList.UserAvatar}"
                Case "2"
                    Return $"MSG01{UserSettingsList.UserName}|A Tous|{Environment.UserName}|{Titre} je viens dans 5 minutes|{UserSettingsList.UserAvatar}"
                Case "3"
                    Return $"MSG01{UserSettingsList.UserName}|A Tous|{Environment.UserName}|{Titre} je viens dans 10 minutes|{UserSettingsList.UserAvatar}"
                Case "A"
                    Return $"MSG01{UserSettingsList.UserName}|A Tous|{Environment.UserName}|{Titre} met en attente|{UserSettingsList.UserAvatar}"
                Case Else
                    Return Nothing ' Option non reconnue
            End Select
        End Function
    End Class
End Namespace
