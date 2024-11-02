Imports log4net
Imports Newtonsoft.Json.Linq
Imports NuGet.Versioning
Imports System.Net

Namespace Networking
    Public Class NetworkingUpdateChecker
        Private Shared ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

        Public Async Function CheckForUpdates(channel As String, currentVersion As String) As Task(Of Boolean)
            logger.Info($"Checking for updates on channel: {channel}")
            Dim serverVersion As String = Await GetServerVersion(channel)
            If Not String.IsNullOrEmpty(serverVersion) AndAlso CompareVersions(serverVersion, currentVersion) > 0 Then
                Return True
            End If
            Return False
        End Function

        Private Async Function GetServerVersion(channel As String) As Task(Of String)
            Try
                Dim apiUrl As String = $"https://raw.githubusercontent.com/{GlobalConfig.RepoOwner}/{GlobalConfig.RepoName}/master/releases/{channel}/version.json"
                Dim webClient As New WebClient()
                Dim versionJson As String = Await webClient.DownloadStringTaskAsync(apiUrl)
                Dim versionObject As JObject = JObject.Parse(versionJson)
                Return versionObject("version").ToString()
            Catch ex As Exception
                logger.Error($"Error getting server version: {ex.Message}")
                Return Nothing
            End Try
        End Function

        Private Function CompareVersions(version1 As String, version2 As String) As Integer
            Try
                Dim nugetVersion1 As NuGetVersion = NuGetVersion.Parse(version1)
                Dim nugetVersion2 As NuGetVersion = NuGetVersion.Parse(version2)
                Dim result As Integer = nugetVersion1.CompareTo(nugetVersion2)

                If result > 0 Then
                    logger.Debug("La version " & version1 & " est supérieure à la version " & version2)
                    Console.WriteLine("La version " & version1 & " est supérieure à la version " & version2)
                ElseIf result < 0 Then
                    logger.Debug("La version " & version1 & " est inférieure à la version " & version2)
                    Console.WriteLine("La version " & version1 & " est inférieure à la version " & version2)
                Else
                    logger.Debug("La version " & version1 & " est égale à la version " & version2)
                    Console.WriteLine("La version " & version1 & " est égale à la version " & version2)
                End If

                Return result
            Catch ex As Exception
                logger.Error("Erreur lors de la comparaison des versions : " & ex.Message)
                Return 0
            End Try
        End Function
    End Class
End Namespace
