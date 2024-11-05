Imports System.IO
Imports System.Windows
Imports EyeChat.EyeChat
Imports EyeChat.Models
Imports EyeChat.Networking
Imports EyeChat.Utilities
Imports log4net
Imports log4net.Config
Imports Newtonsoft.Json.Linq

Namespace EyeChat
    Partial Class Application
        Inherits System.Windows.Application

        Protected Overrides Sub OnStartup(e As StartupEventArgs)
            MyBase.OnStartup(e)

            ' Créer le dossier Logs si non existant
            If Not Directory.Exists("Logs") Then
                Directory.CreateDirectory("Logs")
            End If
            XmlConfigurator.Configure(New FileInfo("log4net.config"))

            ' Charger les configurations initiales
            Try
                AppConfig.LoadConfig()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Erreur")
            End Try


            Dim configFilePath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Config", "appsettings.json")
            Dim config As JObject = AppConfig.LoadConfigFronFile ' Charger la configuration depuis le fichier appsettings.json

            ' Charger et configurer le niveau de log
            Dim logLevel As String = config("AppSettings")?("LogLevel")?.ToString()
            If Not String.IsNullOrEmpty(logLevel) Then
                Dim hierarchy = DirectCast(LogManager.GetRepository(), log4net.Repository.Hierarchy.Hierarchy)
                hierarchy.Root.Level = log4net.Core.Level.All ' Niveau par défaut si non défini
                Select Case logLevel.ToUpper()
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
                End Select
                hierarchy.RaiseConfigurationChanged(EventArgs.Empty)
                logger.Info($"Niveau de log configuré à : {logLevel}")
            Else
                logger.Warn("Niveau de log non spécifié dans appsettings.json. Utilisation du niveau par défaut.")
            End If

            ' Vérifier et initialiser UniqueId
            Dim uniqueId As String = config("AppSettings")?("UniqueId")?.ToString()
            If String.IsNullOrEmpty(uniqueId) Then
                logger.Warn("UniqueId n'est pas rempli. Génération d'un nouvel UniqueId.")
                uniqueId = UIDManager.GenerateUniqueId() ' Générer un UniqueId et le mettre dans la variable
                config("AppSettings")("UniqueId") = uniqueId ' Mettre à jour l'objet JSON avec le nouvel UniqueId
            Else
                logger.Info($"UniqueId existant trouvé : {uniqueId}")
            End If

            ' Vérifier et initialiser ComputerName
            Dim ComputerName As String = config("AppSettings")?("ComputerName")?.ToString()
            If String.IsNullOrEmpty(ComputerName) Then
                logger.Warn("ComputerName n'est pas rempli. Génération d'un nouvel ComputerName.")
                ComputerName = Environment.MachineName
                config("AppSettings")("ComputerName") = ComputerName ' Mettre à jour l'objet JSON avec le nouveau ComputerName
            Else
                logger.Info($"ComputerName existant trouvé : {ComputerName}")
            End If

            ' Vérifier et initialiser WindowsUser
            Dim WindowsUser As String = config("AppSettings")?("WindowsUser")?.ToString()
            If String.IsNullOrEmpty(WindowsUser) Then
                logger.Warn("WindowsUser n'est pas rempli. Génération d'un nouvel WindowsUser.")
                WindowsUser = Environment.UserName
                config("AppSettings")("WindowsUser") = WindowsUser ' Mettre à jour l'objet JSON avec le nouveau WindowsUser
            Else
                logger.Info($"WindowsUser existant trouvé : {WindowsUser}")
            End If

            ' Sauvegarder les valeurs mises à jour dans le fichier de configuration
            Try
                Dim updatedConfig As String = Newtonsoft.Json.JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented)
                File.WriteAllText(configFilePath, updatedConfig)
                logger.Info("Configuration mise à jour avec succès dans appsettings.json.")
            Catch ex As Exception
                logger.Error($"Erreur lors de la sauvegarde de la configuration : {ex.Message}")
            End Try

            ' Initialiser les collections
            MessageModels.LoadMessagesFromJson()
            UsersListGlobal = UserModels.LoadUsersFromJson()
            PlanningListGlobal = PlanningModels.LoadPlanningFromJson()
            ExamOptionModels.LoadExamOptionFromJson()
            PatientModels.LoadPatientsFromJson()
            ComputerModels.LoadComputersFromJson()
            SpeedMessageModels.LoadSpeedMessages()
            EggPhrasesModels.LoadEggPhrasesFromJson()

            ' Initialisation de SendManager
            SendManager.InitializeSender()

            ' Charger les paramètres utilisateur
            UserSettingsModels.LoadUserSettingsFronJson("Benoti")

            ' Afficher la fenêtre principale
            Dim mainWin As New MainWindow()
            mainWin.Show()
        End Sub
    End Class
End Namespace
