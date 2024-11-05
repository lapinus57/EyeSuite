Imports System.Text
Imports log4net
Imports EyeChat.EyeChat
Imports MahApps.Metro.SimpleChildWindow
Imports System.Collections.ObjectModel
Imports EyeChat.Networking
Imports EyeChat.Models
Imports EyeChat.Utilities

Namespace Input
    Public Class CommandHandler
        Private Shared ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
        Public Shared Async Function HandleCommandAsync(command As String, mainWindow As MainWindow) As Task
            command = command.TrimEnd()

            If String.IsNullOrEmpty(command) Then Return

            Select Case command
                Case "/DEBUG"
                    logger.Info("Commande /DEBUG exécutée")
                    Try
                        Dim userToAdd As UserModels = UsersListGlobal.FirstOrDefault(Function(user) user.Name = "Marvin")
                        If userToAdd Is Nothing Then
                            UsersListGlobal.Add(New UserModels With {.Name = "Marvin", .Avatar = AppConfig.AvatarPathMarvin, .Status = "Don't Panic"})
                            logger.Debug("Utilisateur Marvin ajouté")
                        End If
                        ''mainWindow.UsersDialogBox.SetCurrentValue(ChildWindow.IsOpenProperty, True)
                    Catch ex As Exception
                        logger.Error("Erreur lors de l'exécution de la commande /DEBUG : " & ex.Message)
                    End Try

                Case "/ENDDEBUG"
                    logger.Info("Commande /ENDDEBUG exécutée")
                    Try
                        Dim userToRemove As UserModels = UsersListGlobal.FirstOrDefault(Function(user) user.Name = "Marvin")
                        If userToRemove IsNot Nothing Then
                            UsersListGlobal.Remove(userToRemove)
                            UserModels.SaveUsersToJson(UsersListGlobal)
                            logger.Debug("Utilisateur Marvin supprimé")
                        End If
                    Catch ex As Exception
                        logger.Error("Erreur lors de l'exécution de la commande /ENDDEBUG : " & ex.Message)
                    End Try

                Case "/LSTCOMPUTER"
                    logger.Info("Commande /LSTCOMPUTER exécutée")
                    Try
                        SendManager.SendMessage("DBG01")
                        logger.Debug("Message DBG01 envoyé")
                    Catch ex As Exception
                        logger.Error("Erreur lors de l'exécution de la commande /LSTCOMPUTER : " & ex.Message)
                    End Try

                Case "/DISPCOMPUTER"
                    logger.Info("Commande /DISPCOMPUTER exécutée")
                    Try
                        Dim computerString As New StringBuilder()
                        computerString.AppendLine("Actuellement il y a :")
                        For Each computer In ComputersListGlobal
                            computerString.AppendLine($"{computer.ComputerUser} {computer.ComputerIp}")
                        Next
                        Dim userToAdd As UserModels = UsersListGlobal.FirstOrDefault(Function(user) user.Name = "Marvin")
                        If userToAdd Is Nothing Then
                            UsersListGlobal.Add(New UserModels With {.Name = "Marvin", .Avatar = AppConfig.AvatarPathMarvin, .Status = "Don't Panic"})
                            logger.Debug("Utilisateur Marvin ajouté")

                        End If
                        MessageModels.AddMessage("Marvin", "Marvin", "Cœur en Or", computerString.ToString(), False, AppConfig.AvatarPathMarvin)
                        logger.Debug("Liste des ordinateurs affichée")
                    Catch ex As Exception
                        logger.Error("Erreur lors de l'affichage des ordinateurs avec la commande /DISPCOMPUTER : " & ex.Message)
                    End Try

                Case "/TESTPATIENT"
                    logger.Info("Commande /TESTPATIENT exécutée")
                    Try
                        Dim testPrenoms As String() = {"Jean", "Marie", "Claire", "Pierre", "Sophie"}
                        Dim testNoms As String() = {"Dubois", "Martin", "Lefebvre", "Dupont", "Moreau"}
                        Dim identifier As String = "PTN01Iel"
                        Dim otherInfo As String = $"RDC|System|{DateTime.Now:yyyy-MM-ddTHH:mm:ss.fff}"
                        Dim random As New Random()

                        For Each optionCode As ExamOptionModels In ExamOptionsListGlobal
                            Dim code As String = optionCode.Name
                            Dim testPrenom As String = testPrenoms(random.Next(testPrenoms.Length))
                            Dim testNom As String = testNoms(random.Next(testNoms.Length))
                            Dim annotation As String = "ODG " & optionCode.Annotation
                            Dim message As String = $"{identifier}|{testNom}|{testPrenom}|{code}|{annotation}|{otherInfo}"
                            SendManager.SendMessage(message)
                        Next
                    Catch ex As Exception
                        logger.Error("Erreur lors de l'exécution de la commande /TESTPATIENT : " & ex.Message)
                    End Try

                Case "/TESTSENDFILE"
                    logger.Info("Commande /TESTSENDFILE exécutée")
                    Try
                        NetworkingFileSender.CreateTextFile("Core", "test.txt", "fichier test")
                        NetworkingFileSender.SendFileOverNetwork("Core", "test.txt")
                    Catch ex As Exception
                        logger.Error("Erreur lors de l'exécution de la commande /TESTSENDFILE : " & ex.Message)
                    End Try

                Case "/TESTMARVIN"
                    logger.Info("Commande /TESTMARVIN exécutée")
                    Try
                        Dim random As New Random()

                        If EggPhrasesList IsNot Nothing AndAlso EggPhrasesList.MarvinPhrases.Count > 0 AndAlso EggPhrasesList.JoyPhrases.Count > 0 Then
                            Dim randomIndexMarvin As Integer = random.Next(EggPhrasesList.MarvinPhrases.Count)
                            Dim randomIndexJoy As Integer = random.Next(EggPhrasesList.JoyPhrases.Count)

                            Dim randomPhraseMarvin As String = EggPhrasesList.MarvinPhrases(randomIndexMarvin)
                            Dim randomPhraseJoy As String = EggPhrasesList.JoyPhrases(randomIndexJoy)

                            EggPhrasesModels.DisplayMarvinAndJoyMessages(randomPhraseMarvin, randomPhraseJoy)
                        End If
                    Catch ex As Exception
                        logger.Error("Erreur lors de l'exécution de la commande /TESTMARVIN : " & ex.Message)
                    End Try

                Case Else
                    logger.Info("Vérification si le message de la Sendbox commence par un code MSG")

                    Dim startsWithCodeMSG As Boolean = False
                    Dim matchedOption As ExamOptionModels = Nothing

                    Try
                        ' Recherche d'un code MSG dans la liste des options d'examen
                        For Each examOption As ExamOptionModels In ExamOptionsListGlobal
                            If command.StartsWith(examOption.CodeMSG) Then
                                startsWithCodeMSG = True
                                matchedOption = examOption
                                Exit For
                            End If
                        Next
                    Catch ex As Exception
                        logger.Error("Erreur lors du parcours de la liste des ExamOptions : " & ex.Message)
                    End Try

                    If startsWithCodeMSG AndAlso matchedOption IsNot Nothing Then
                        ' Le message commence par un code MSG et un code MSG a été trouvé
                        logger.Info("Le message de la Sendbox commence par un code MSG et un code MSG a été trouvé")
                        Dim codeMSG As String = matchedOption.CodeMSG
                        Dim annotation As String = matchedOption.Annotation

                        ' Supprimer les caractères inutiles du code MSG
                        codeMSG = codeMSG.Replace("=", "")

                        ' Extraction des informations du texte du message
                        Dim spaceIndex As Integer = command.IndexOf(" ", codeMSG.Length)
                        Dim patientTitre As String = ""
                        Dim patientNom As String = ""
                        Dim patientPrenom As String = ""

                        If spaceIndex > codeMSG.Length Then
                            PatientStringHelper.ExtractInfoFromInput(command.Substring(spaceIndex + 1), patientTitre, patientNom, patientPrenom)
                        Else
                            PatientStringHelper.ExtractInfoFromInput(command.Substring(codeMSG.Length), patientTitre, patientNom, patientPrenom)
                        End If

                        ' Formatage du message à envoyer
                        Dim text As String = $"PTN01{patientTitre}|{patientNom}|{patientPrenom}|{codeMSG}|{annotation}|RDC|{GlobalConfig.UserSettingsList.UserName}|{DateTime.Now:yyyy-MM-ddTHH:mm:ss.fff}"
                        SendManager.SendMessage(text)
                    Else
                        ' Si le message est un message simple sans code MSG
                        logger.Info("Le message de la Sendbox est un message simple")

                        Try
                            '' Dim selectedUser As UserModels = TryCast(mainWindow.UsersList.SelectedItem, UserModels)
                            ''If selectedUser IsNot Nothing Then
                            ''Dim text As String = $"MSG01{GlobalConfig.UserSettingsList.UserName}|{selectedUser.Name}|{Environment.UserName}|{command}|{GlobalConfig.UserSettingsList.UserAvatar}"
                            ''SendManager.SendMessage(text)
                            ''mainWindow.MessageList.ScrollToEnd()
                            logger.Debug("Message simple envoyé")
                            ''End If

                        Catch ex As Exception
                            logger.Error("Erreur lors de l'envoi du message simple : " & ex.Message)
                        End Try
                    End If

            End Select
        End Function




    End Class

End Namespace
