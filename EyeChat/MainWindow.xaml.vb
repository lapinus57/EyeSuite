
Imports EyeChat.Networking

Namespace EyeChat

    Class MainWindow
        Public Sub New()

            ' Cet appel est requis par le concepteur.
            InitializeComponent()
            Console.WriteLine("Hello World!")
            CheckAndUpdate()
            ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().

        End Sub

        Private Async Sub CheckAndUpdate()
            Dim updateChecker As New NetworkingUpdateChecker()
            Dim isUpdateAvailable As Boolean = Await updateChecker.CheckForUpdates("beta", "0.0.1")
            If isUpdateAvailable Then
                Dim updateDownloader As New NetworkingUpdateDownloader()
                Dim downloadPath As String = Await updateDownloader.DownloadUpdate("beta")
                If Not String.IsNullOrEmpty(downloadPath) Then
                    'Dim updateInstaller As New NetworkingUpdateInstaller()
                    'updateInstaller.InstallUpdate(downloadPath)
                End If
            End If
        End Sub
    End Class
End Namespace

