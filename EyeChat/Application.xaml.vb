
Imports System.Windows
Imports EyeChat.EyeChat
Imports EyeChat.Models
Imports EyeChat.Networking
Imports EyeChat.Utilities


Namespace EyeChat
    Partial Class Application
        Inherits System.Windows.Application

        Protected Overrides Sub OnStartup(e As StartupEventArgs)
            MyBase.OnStartup(e)

            ' Charger les configurations initiales
            AppConfig.LoadConfig()

            ' Charger les paramètres utilisateur (exemple avec un nom de test)
            UserSettingsModels.LoadUserSettingsFronJson("Benoti")

            ' Initialiser les collections
            MessageModels.LoadMessagesFromJson()
            UserModels.LoadUsersFromJson()
            ExamOptionModels.LoadExamOptionFromJson()
            PatientModels.LoadPatientsFromJson()
            ComputerModels.LoadComputersFromJson()
            SpeedMessageModels.LoadSpeedMessages()
            EggPhrasesModels.LoadEggPhrasesFromJson()

            ' Initialisation de SendManager
            SendManager.InitializeSender()

            ' Vous pouvez ajouter d'autres initialisations ici avant l'affichage de la fenêtre principale.

            ' Afficher la fenêtre principale
            Dim mainWin As New MainWindow()
            mainWin.Show()
        End Sub

    End Class
End Namespace


