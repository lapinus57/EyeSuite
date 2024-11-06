Imports System.IO
Imports System.Windows
Imports EyeChat.EyeChat
Imports EyeChat.Models
Imports EyeChat.Networking
Imports EyeChat.Utilities
Imports EyeChat.Views
Imports EyeChat
Imports log4net
Imports log4net.Config
Imports Newtonsoft.Json.Linq

Namespace EyeChat
    Partial Class Application
        Inherits System.Windows.Application

        Protected Overrides Sub OnStartup(e As StartupEventArgs)
            MyBase.OnStartup(e)

            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown


            ' Initialisation des logs
            Try
                If Not Directory.Exists("Logs") Then Directory.CreateDirectory("Logs")
                XmlConfigurator.Configure(New FileInfo("log4net.config"))
            Catch ex As Exception
                MessageBox.Show("Erreur lors de l'initialisation des logs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error)
                Shutdown()
                Return
            End Try

            ' Chargement de la configuration
            Dim configFilePath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Config", "appsettings.json")
            Dim config As JObject = Nothing

            Try
                If Not File.Exists(configFilePath) Then
                    Throw New FileNotFoundException("Le fichier appsettings.json est introuvable.")
                End If
                config = AppConfig.LoadConfigFronFile
                AppConfig.LoadConfig()
            Catch ex As Exception
                logger.Error($"Erreur lors du chargement de la configuration : {ex.Message}")
                MessageBox.Show("Erreur de configuration, l'application va se fermer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error)
                Shutdown()
                Return
            End Try

            ' Configurer les logs
            Try
                ConfigureLogging(config)
            Catch ex As Exception
                logger.Error($"Erreur lors de la configuration des logs : {ex.Message}")
            End Try

            ' Assurer les valeurs essentielles
            EnsureConfigValue(config, "UniqueId", AddressOf UIDManager.GenerateUniqueId)
            EnsureConfigValue(config, "ComputerName", Function() Environment.MachineName)
            EnsureConfigValue(config, "WindowsUser", Function() Environment.UserName)

            ' Sauvegarder les modifications de la configuration
            Try
                SaveConfig(configFilePath, config)
            Catch ex As Exception
                logger.Error($"Erreur lors de la sauvegarde de la configuration : {ex.Message}")
            End Try

            ' Charger les données nécessaires
            Try

                MessageModels.LoadMessagesFromJson()
                PlanningListGlobal = PlanningModels.LoadPlanningFromJson()
                ExamOptionModels.LoadExamOptionFromJson()
                PatientModels.LoadPatientsFromJson()
                ComputerModels.LoadComputersFromJson()
                SpeedMessageModels.LoadSpeedMessages()
                EggPhrasesModels.LoadEggPhrasesFromJson()
            Catch ex As Exception
                logger.Error($"Erreur lors du chargement des données : {ex.Message}")
                MessageBox.Show("Erreur lors du chargement des données nécessaires.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error)
                Shutdown()
                Return
            End Try

            ' Gérer la configuration de Planning
            If Not HandlePlanningConfig(config, configFilePath) Then
                Return
            End If


            ' Initialiser le SendManager
            Try
                SendManager.InitializeSender()
            Catch ex As Exception
                logger.Error($"Erreur lors de l'initialisation du SendManager : {ex.Message}")
                MessageBox.Show("Erreur lors de l'initialisation du SendManager.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error)
                Shutdown()
                Return
            End Try

            ' Créer et afficher la fenêtre principale
            Dim mainWin As New MainWindow()
            Application.Current.MainWindow = mainWin
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose
            mainWin.Show()
        End Sub

        ''' <summary>
        ''' Gère la configuration de Planning.
        ''' </summary>
        Private Function HandlePlanningConfig(config As JObject, configFilePath As String) As Boolean
            Try
                ' Charger la configuration
                Dim planningConfig As JObject = config("Planning")
                If planningConfig Is Nothing Then
                    Throw New Exception("La section 'Planning' est manquante dans la configuration.")
                End If

                ' Récupérer les valeurs de PlanningEnabled et DefaultUser
                Dim planningEnabled As Boolean = planningConfig("PlanningEnabled")?.ToObject(Of Boolean)() OrElse False
                UserConnected = planningConfig("DefaultUser")?.ToString()

                If Not planningEnabled Then
                    ' Si PlanningEnabled est False
                    If String.IsNullOrEmpty(UserConnected) Then
                        ' Ouvrir une fenêtre pour inviter l'utilisateur à entrer un nom
                        If Not ShowInputDefaultUserWindow(config, configFilePath) Then
                            Return False
                        End If
                    End If

                    ' Charger les paramètres utilisateur pour DefaultUser
                    Try
                        UserSettingsModels.LoadUserSettingsFronJson(UserConnected)
                    Catch ex As Exception
                        logger.Error($"Erreur lors du chargement des paramètres utilisateur : {ex.Message}")
                        MessageBox.Show("Erreur lors du chargement des paramètres utilisateur.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error)
                        Shutdown()
                        Return False
                    End Try
                Else
                    ' Si PlanningEnabled est True, implémenter la logique pour déterminer quel utilisateur charger
                    ' Placeholder pour votre logique :
                    ' Dim userToLoad As String = DetermineUserToLoad()
                    ' UserSettingsModels.LoadUserSettingsFronJson(userToLoad)
                End If

                Return True
            Catch ex As Exception
                logger.Error($"Erreur lors du traitement de la configuration Planning : {ex.Message}")
                MessageBox.Show("Erreur dans la configuration Planning. L'application va se fermer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error)
                Shutdown()
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Affiche la fenêtre InputDefaulUserWindow pour obtenir le nom d'utilisateur par défaut.
        ''' </summary>
        ''' 
        Private Function ShowInputDefaultUserWindow(config As JObject, configFilePath As String) As Boolean
            Try
                Dim inputWindow As New InputDefaulUserWindow()
                Dim result As Boolean? = inputWindow.ShowDialog()

                If result.HasValue AndAlso result.Value Then
                    ' Récupérer le nom de l'utilisateur depuis la fenêtre
                    UserConnected = inputWindow.UserName
                    logger.Info($"Nom d'utilisateur saisi : {UserConnected}")

                    ' Enregistrer le nom dans la configuration
                    Try
                        config("Planning")("DefaultUser") = UserConnected
                        SaveConfig(configFilePath, config)
                        logger.Info($"DefaultUser mis à jour avec succès : {UserConnected}")
                    Catch ex As Exception
                        logger.Error($"Erreur lors de la sauvegarde de DefaultUser : {ex.Message}")
                        MessageBox.Show("Erreur lors de la sauvegarde de l'utilisateur par défaut.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error)
                        Return False
                    End Try
                Else
                    ' Si l'utilisateur annule, fermer l'application
                    logger.Warn("Aucun utilisateur spécifié. Fermeture de l'application.")
                    MessageBox.Show("Aucun utilisateur spécifié. L'application va se fermer.", "Information", MessageBoxButton.OK, MessageBoxImage.Information)
                    Shutdown()
                    Return False
                End If

                Return True
            Catch ex As Exception
                logger.Error($"Erreur lors de l'affichage de la fenêtre : {ex.Message}")
                MessageBox.Show($"Erreur lors de l'affichage de la fenêtre : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error)
                Shutdown()
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Configure les logs en fonction de la configuration.
        ''' </summary>
        Private Sub ConfigureLogging(config As JObject)
            Dim logLevel As String = config("AppSettings")?("LogLevel")?.ToString()
            Dim hierarchy = DirectCast(LogManager.GetRepository(), log4net.Repository.Hierarchy.Hierarchy)

            Select Case logLevel?.ToUpper()
                Case "DEBUG"
                    hierarchy.Root.Level = log4net.Core.Level.Debug
                Case "INFO"
                    hierarchy.Root.Level = log4net.Core.Level.Info
                Case "WARN"
                    hierarchy.Root.Level = log4net.Core.Level.Warn
                Case "ERROR"
                    hierarchy.Root.Level = log4net.Core.Level.Error
                Case "FATAL"
                    hierarchy.Root.Level = log4net.Core.Level.Fatal
                Case Else
                    hierarchy.Root.Level = log4net.Core.Level.Warn
                    logger.Warn("Niveau de log non spécifié ou invalide. Utilisation du niveau WARN par défaut.")
            End Select

            hierarchy.RaiseConfigurationChanged(EventArgs.Empty)
            logger.Info($"Niveau de log configuré à : {hierarchy.Root.Level}")
        End Sub

        ''' <summary>
        ''' Assure qu'une valeur de configuration est définie.
        ''' </summary>
        Private Sub EnsureConfigValue(config As JObject, key As String, defaultValue As Func(Of String))
            If String.IsNullOrEmpty(config("AppSettings")?(key)?.ToString()) Then
                Dim value = defaultValue()
                config("AppSettings")(key) = value
                logger.Warn($"{key} non rempli. Valeur par défaut utilisée : {value}")
            Else
                logger.Info($"{key} existant trouvé : {config("AppSettings")(key)}")
            End If
        End Sub

        ''' <summary>
        ''' Sauvegarde le fichier de configuration.
        ''' </summary>
        Private Sub SaveConfig(filePath As String, config As JObject)
            Dim updatedConfig As String = Newtonsoft.Json.JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented)
            File.WriteAllText(filePath, updatedConfig)
            logger.Info("Configuration mise à jour avec succès dans appsettings.json.")
        End Sub




    End Class
End Namespace
