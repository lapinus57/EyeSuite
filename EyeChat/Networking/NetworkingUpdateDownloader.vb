Imports log4net
Imports Newtonsoft.Json.Linq
Imports System.Net

Namespace Networking
    Public Class NetworkingUpdateDownloader
        Private Shared ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

        Public Async Function DownloadUpdate(channel As String) As Task(Of String)
            Dim updateUrl As String = Await GetUpdateUrl(channel)
            Dim downloadPath As String = "update.zip"
            Dim webClient As New WebClient()

            Try
                Try
                    Await webClient.DownloadFileTaskAsync(New Uri(updateUrl), downloadPath)
                    logger.Info($"Downloaded update to {downloadPath}")
                    Console.WriteLine($"Mise à jour téléchargée avec succès : {downloadPath}")
                Catch ex As WebException
                    logger.Error($"Network error: {ex.Status} - {ex.Message}")
                    Console.WriteLine($"Erreur réseau lors du téléchargement de la mise à jour : {ex.Status} - {ex.Message}")
                Catch ex As Exception
                    logger.Error($"Unexpected error: {ex.Message}")
                    Console.WriteLine($"Erreur inattendue lors du téléchargement de la mise à jour : {ex.Message}")
                End Try
                logger.Info($"Downloaded update to {downloadPath}")
                Console.WriteLine($"Mise à jour téléchargée avec succès : {downloadPath}")
                Return downloadPath
            Catch ex As Exception
                logger.Error($"Error downloading update: {ex.Message}")
                Return Nothing
            End Try
        End Function

        Private Async Function GetUpdateUrl(channel As String) As Task(Of String)
            Try
                Dim apiUrl As String = $"https://raw.githubusercontent.com/{GlobalConfig.RepoOwner}/{GlobalConfig.RepoName}/master/releases/{channel}/version.json"
                Dim webClient As New WebClient()
                Dim versionJson As String = Await webClient.DownloadStringTaskAsync(apiUrl)
                Dim versionObject As JObject = JObject.Parse(versionJson)
                Dim updateUrl As String = versionObject("updateUrl").ToString()
                logger.Debug($"URL de mise à jour obtenue avec succès : {updateUrl}")
                Console.WriteLine($"URL de mise à jour obtenue avec succès : {updateUrl}")
                Return updateUrl
            Catch ex As Exception
                logger.Error($"Erreur lors de la récupération de l'URL de mise à jour : {ex.Message}")
                Return Nothing
            End Try
        End Function
    End Class
End Namespace
