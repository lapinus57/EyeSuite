Imports log4net
Imports Newtonsoft.Json.Linq
Imports NuGet.Versioning
Imports System.Net

Namespace Networking
    Public Class NetworkingUpdateChecker
        Private Shared ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

        Public Async Function CheckForUpdates(currentVersion As String) As Task(Of Boolean)
            logger.Info("Checking for updates from the latest GitHub release")
            Dim latestVersion As String = Await GetLatestGitHubReleaseVersion()

            If Not String.IsNullOrEmpty(latestVersion) AndAlso CompareVersions(latestVersion, currentVersion) > 0 Then
                Console.WriteLine("Mise à jour disponible")
                Return True
            End If
            Return False
        End Function

        Private Async Function GetLatestGitHubReleaseVersion() As Task(Of String)
            Try
                ' Appel de l'API pour obtenir la dernière release
                Dim apiUrl As String = $"https://api.github.com/repos/{GlobalConfig.RepoOwner}/{GlobalConfig.RepoName}/releases/latest"
                Dim webClient As New WebClient()
                webClient.Headers.Add("User-Agent", "request")

                Dim jsonResponse As String = Await webClient.DownloadStringTaskAsync(apiUrl)
                Dim releaseObject As JObject = JObject.Parse(jsonResponse)
                Dim latestVersion As String = releaseObject("tag_name").ToString()
                logger.Info($"Latest GitHub release version obtained: {latestVersion}")
                Return latestVersion
            Catch ex As Exception
                logger.Error($"Error retrieving the latest GitHub release version: {ex.Message}")
                Return Nothing
            End Try
        End Function

        Private Function CompareVersions(version1 As String, version2 As String) As Integer
            Try
                ' Nettoie les chaînes de version avant de comparer
                Dim cleanVersion1 As String = CleanVersionString(version1)
                Dim cleanVersion2 As String = CleanVersionString(version2)

                logger.Debug($"Comparing cleaned versions {cleanVersion1} and {cleanVersion2}")

                Dim nugetVersion1 As NuGetVersion = NuGetVersion.Parse(cleanVersion1)
                Dim nugetVersion2 As NuGetVersion = NuGetVersion.Parse(cleanVersion2)
                Dim result As Integer = nugetVersion1.CompareTo(nugetVersion2)

                Select Case result
                    Case > 0
                        logger.Info($"La version {cleanVersion1} est plus récente que {cleanVersion2}.")
                    Case < 0
                        logger.Info($"La version {cleanVersion1} est plus ancienne que {cleanVersion2}.")
                    Case 0
                        logger.Info($"Les versions {cleanVersion1} et {cleanVersion2} sont identiques.")
                End Select

                Return result
            Catch ex As Exception
                logger.Error("Erreur lors de la comparaison des versions : " & ex.Message)
                Return 0
            End Try
        End Function

        Private Function CleanVersionString(version As String) As String
            ' Vérifie si le préfixe "v" est présent et le supprime si c'est le cas
            If version.StartsWith("v") Then
                version = version.Substring(1)
            End If
            Return version
        End Function
    End Class
End Namespace
