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
                Dim filePath = AppConfig.EggPhrasesPath
                If File.Exists(filePath) Then
                    Dim jsonData As String = File.ReadAllText(filePath)
                    EggPhrasesList = JsonConvert.DeserializeObject(Of EggPhrasesModels)(jsonData)
                Else
                EggPhrasesList = New EggPhrasesModels
                End If
            Catch ex As Exception
                logger.Error("Erreur lors du chargement des phrases d'oeuf : ", ex)
                EggPhrasesList = New EggPhrasesModels
            End Try
        End Sub


        Public Shared Sub DisplayMarvinAndJoyMessages(randomPhraseMarvin As String, randomPhraseJoy As String)
            Try
                ' Chemins vers les avatars


                ' Vérifie si Marvin est déjà présent dans la liste des utilisateurs
                If UserModels.IsNameInList(UsersListGlobal, "Marvin") Then
                    ' Ajoute les messages de Marvin et Joy
                    MessageModels.AddMessage("Marvin", "Marvin", "Cœur en Or", randomPhraseMarvin, False, AppConfig.AvatarPathMarvin)
                    MessageModels.AddMessage("Marvin", "Joy", "Riley", randomPhraseJoy, False, AppConfig.AvatarPathJoy)
                Else
                    ' Ajoute Marvin à la liste des utilisateurs puis ajoute les messages
                    UsersListGlobal.Add(New UserModels With {.Name = "Marvin", .Avatar = AppConfig.AvatarPathMarvin, .Status = "Don't Panic"})
                    MessageModels.AddMessage("Marvin", "Marvin", "Cœur en Or", randomPhraseMarvin, False, AppConfig.AvatarPathMarvin)
                    MessageModels.AddMessage("Marvin", "Joy", "Riley", randomPhraseJoy, False, AppConfig.AvatarPathJoy)
                End If

                ' Journalise l'ajout des messages
                logger.Debug("Messages de Marvin et Joy affichés.")
            Catch ex As Exception
                logger.Error("Erreur lors de l'affichage des messages de Marvin et Joy : " & ex.Message)
            End Try
        End Sub

    End Class
End Namespace
