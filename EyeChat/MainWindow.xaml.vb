
Imports EyeChat.Input
Imports EyeChat.Networking
Imports EyeChat.Utilities

Namespace EyeChat
    Class MainWindow

        Private dialogHelper As DialogHelper
        Public Sub New()

            InitializeComponent()
            CheckAndUpdate()
            ' Instancier la classe DialogHelper
            dialogHelper = New DialogHelper(Me)
        End Sub


        Public Sub initUDP()

            ' Initialisation de SendManager
            SendManager.InitializeSender()

        End Sub

        Public Sub closeUDP()

            ' Fermeture de SendManager
            SendManager.CloseSender()

        End Sub

        ' Appeler la méthode SpeedMessageDialog



        Public Async Sub SpeedMessageDialog(userSend As String, message As String, option1 As String, option2 As String, option3 As String)
            Dim selectedOption As String = Await dialogHelper.ShowSpeedMessageDialogAsync(userSend, message, option1, option2, option3)
            If selectedOption IsNot Nothing Then
                dialogHelper.SendSpeedMessage(selectedOption, userSend)
            End If
        End Sub

        Private Async Sub CheckAndUpdate()
            Dim updateChecker As New NetworkingUpdateChecker()
            Dim isUpdateAvailable As Boolean = Await updateChecker.CheckForUpdates("v0.0.1")
            If isUpdateAvailable Then
                Dim updateDownloader As New NetworkingUpdateDownloader()
                Dim downloadPath As String = Await updateDownloader.DownloadUpdate("beta")
                If Not String.IsNullOrEmpty(downloadPath) Then
                    Dim updateInstaller As New NetworkingUpdateInstaller()
                    updateInstaller.InstallUpdate(downloadPath)
                End If
            End If
        End Sub

        Private Sub MainWindow_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized


        End Sub

        ''Private Async Function SendTextBox_KeyDownAsync(sender As Object, e As KeyEventArgs) As Task Handles SendTextBox.KeyDown
        ''If e.Key = Key.Enter Then
        ''Await CommandHandler.HandleCommandAsync(SendTextBox.Text, Me)
        ''SendTextBox.Clear()
        '' ElseIf e.Key = Key.Tab Then
        '' Try
        ''If Not String.IsNullOrEmpty(_currentSuggestion) Then
        ''SendTextBox.Text = _currentSuggestion
        ''SendTextBox.SelectionStart = SendTextBox.Text.Length
        ''End If
        ''e.Handled = True
        ''  Catch ex As Exception
        ''logger.Error("Erreur lors de l'insertion de la suggestion dans la zone de texte : " & ex.Message)
        ''   End Try
        ''End If
        ''Return Task.CompletedTask
        ''End Function

    End Class
End Namespace

