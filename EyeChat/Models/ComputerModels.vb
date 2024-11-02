Imports System.Collections.ObjectModel
Imports System.IO
Imports Newtonsoft.Json
Imports log4net
Imports EyeChat.Utilities

Namespace Models
    Public Class ComputerModels

        <JsonProperty("computer_id")>
        Public Property ComputerID As String

        <JsonProperty("computer_user")>
        Public Property ComputerUser As String

        <JsonProperty("computer_ip")>
        Public Property ComputerIp As String

        ' Sauvegarde des ordinateurs dans un fichier JSON
        Public Shared Sub SaveComputersToJson()
            Try
                Dim dossier As String = Path.GetDirectoryName(AppConfig.ComputerListPath)
                If Not Directory.Exists(dossier) Then
                    Directory.CreateDirectory(dossier)
                End If
                Dim json As String = JsonConvert.SerializeObject(ComputersList, Formatting.Indented)
                File.WriteAllText(AppConfig.ComputerListPath, json)
            Catch ex As Exception
                logger.Error("Erreur lors de la sauvegarde de la collection des ordinateurs : ", ex)
            End Try
        End Sub

        ' Charge les ordinateurs à partir d'un fichier JSON
        Public Shared Sub LoadComputersFromJson()
            Try
                Dim filePath = AppConfig.ComputerListPath
                If File.Exists(filePath) Then
                    Dim json As String = File.ReadAllText(filePath)
                    ComputersList = JsonConvert.DeserializeObject(Of ObservableCollection(Of ComputerModels))(json)
                Else
                    ComputersList = New ObservableCollection(Of ComputerModels)()
                    SaveComputersToJson()
                End If
            Catch ex As Exception
                logger.Error("Erreur lors du chargement de la collection des ordinateurs", ex)
                ComputersList = New ObservableCollection(Of ComputerModels)()
                SaveComputersToJson()
            End Try
        End Sub
    End Class
End Namespace
