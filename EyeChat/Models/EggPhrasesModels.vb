Imports System.IO
Imports EyeChat.EyeChat
Imports EyeChat.Utilities
Imports log4net
Imports log4net.Repository.Hierarchy
Imports Newtonsoft.Json

Namespace Models
    Public Class EggPhrasesModels
        Private Shared ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
        Public Property MarvinPhrases As List(Of String)
        Public Property JoyPhrases As List(Of String)


        Public Shared Sub LoadEggPhrasesFromJson()
            Try
                Dim jsonData As String = File.ReadAllText(AppConfig.EggPhrasesPath)
                EggPhrasesList = JsonConvert.DeserializeObject(Of EggPhrasesModels)(jsonData)
            Catch ex As Exception
                logger.Error("Erreur lors du chargement des phrases d'oeuf : ", ex)
            End Try
        End Sub
    End Class
End Namespace
