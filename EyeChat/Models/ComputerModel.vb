Imports System.Collections.ObjectModel
Imports System.IO
Imports Newtonsoft.Json
Imports log4net
Imports EyeChat.Utilities

Namespace Models
    Public Class ComputerModel
        Private Shared ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

        <JsonProperty("computer_id")>
        Public Property ComputerID As String

        <JsonProperty("computer_user")>
        Public Property ComputerUser As String

        <JsonProperty("computer_ip")>
        Public Property ComputerIp As String

        ' Méthode pour initialiser la collection avec gestion des erreurs
        Public Shared Sub InitializeComputersList()
            Try
                LoadComputersFromJson()
            Catch ex As Exception
                logger.Error($"Erreur lors de l'initialisation de la collection des ordinateurs : {ex.Message}")
            End Try
        End Sub

        ' Sauvegarde des ordinateurs dans un fichier JSON
        Public Shared Sub SaveComputersToJson()
            Try
                Dim json As String = JsonConvert.SerializeObject(ComputersList, Formatting.Indented)
                File.WriteAllText(AppConfig.ComputerListPath, json)
            Catch ex As Exception
                logger.Error("Erreur lors de la sauvegarde de la collection des ordinateurs : ", ex)
            End Try
        End Sub

        ' Charge les ordinateurs à partir d'un fichier JSON
        Private Shared Sub LoadComputersFromJson()
            Try
                Dim filePath = AppConfig.ComputerListPath
                If File.Exists(filePath) Then
                    Dim json As String = File.ReadAllText(filePath)
                    ComputersList = JsonConvert.DeserializeObject(Of ObservableCollection(Of ComputerModel))(json)
                Else
                    ComputersList = New ObservableCollection(Of ComputerModel)()
                    SaveComputersToJson()
                End If
            Catch ex As Exception
                logger.Error("Erreur lors du chargement de la collection des ordinateurs", ex)
            End Try
        End Sub
    End Class
End Namespace
