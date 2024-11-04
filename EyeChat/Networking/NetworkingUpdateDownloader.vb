Imports log4net
Imports Newtonsoft.Json.Linq
Imports System.Net

Namespace Networking
    Public Class NetworkingUpdateDownloader
        Private Shared ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

        Public Async Function DownloadUpdate() As Task(Of String)
            Dim updateUrl As String = Await GetUpdateUrl()
            If String.IsNullOrEmpty(updateUrl) Then
                logger.Error("L'URL de mise à jour est introuvable.")
                Return Nothing
            End If

            Dim downloadPath As String = "update.zip"
            Dim webClient As New WebClient()

            Try
                Await webClient.DownloadFileTaskAsync(New Uri(updateUrl), downloadPath)
                logger.Info($"Mise à jour téléchargée avec succès : {downloadPath}")
                Console.WriteLine($"Mise à jour téléchargée avec succès : {downloadPath}")
                Return downloadPath
            Catch ex As WebException
                logger.Error($"Erreur réseau lors du téléchargement de la mise à jour : {ex.Status} - {ex.Message}")
                Console.WriteLine($"Erreur réseau : {ex.Status} - {ex.Message}")
                Return Nothing
            Catch ex As Exception
                logger.Error($"Erreur inattendue lors du téléchargement de la mise à jour : {ex.Message}")
                Console.WriteLine($"Erreur inattendue : {ex.Message}")
                Return Nothing
            End Try
        End Function

        Private Async Function GetUpdateUrl() As Task(Of String)
            Try
                Dim apiUrl As String = $"https://api.github.com/repos/{GlobalConfig.RepoOwner}/{GlobalConfig.RepoName}/releases/latest"
                Dim webClient As New WebClient()
                webClient.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; UpdateDownloader/1.0)")
                Dim jsonResponse As String = Await webClient.DownloadStringTaskAsync(apiUrl)
                Dim releaseData As JObject = JObject.Parse(jsonResponse)

                Dim updateUrl As String = releaseData("assets")?.FirstOrDefault(Function(asset) asset("name").ToString() = "update.zip")?("browser_download_url")?.ToString()

                If Not String.IsNullOrEmpty(updateUrl) Then
                    logger.Debug($"URL de mise à jour trouvée : {updateUrl}")
                Else
                    logger.Error("URL de mise à jour non trouvée dans les assets de la release.")
                End If

                Return updateUrl
            Catch ex As Exception
                logger.Error($"Erreur lors de la récupération de l'URL de mise à jour : {ex.Message}")
                Return Nothing
            End Try
        End Function
    End Class
End Namespace
