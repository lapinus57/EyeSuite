Imports System.Collections.ObjectModel
Imports System.Net.Sockets
Imports EyeChat.Models
Imports EyeChat.Services
Imports System.IO
Imports log4net
Imports EyeChat.Views


Module GlobalConfig


    ' Informations sur le dépôt GitHub
    Public Const RepoOwner As String = "lapinus57"
    Public Const RepoName As String = "EyeSuite"



    ' variable pour le chemin du fichier INI et le fichier watcher
    Public fileWatcher As FileWatcherSV
    Public Const iniFilePath As String = "c:\studiov2000\STUDIOV.ini"
    Public numPatValue As Integer

    ' Déclare le client UDP en tant que propriété statique partagée
    Public sendingClient As UdpClient
    Public receivingClient As UdpClient
    Public Const BroadcastAddress As String = "255.255.255.255"
    Public Const MulticastAddress As String = "224.0.0.1"
    Public Const Port As Integer = 50545 ' Utilise la constante de port partagée


    Public Property PatientsRDCList As ObservableCollection(Of PatientModels)
    Public Property Patients1erList As ObservableCollection(Of PatientModels)
    Public Property PatientsALLList As ObservableCollection(Of PatientModels)
    Public Property MessagesList As ObservableCollection(Of MessageModels)
    Public Property speedMessagesList As New List(Of SpeedMessageModels)()
    Public Property ExamOptionsList As New ObservableCollection(Of ExamOptionModels)()
    Public Property ComputersList As ObservableCollection(Of ComputerModels)
    Public Property UsersList As ObservableCollection(Of UserModels)

    Public EggPhrasesList As EggPhrasesModels


    ' Créer un ContextMenu pour la TextBox
    Dim contextMenu As New ContextMenu()

    Public ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)


    Public UserSettingsList As UserSettingsModels

    Public isPostItOpened As Boolean = False ' Suivre l'état d'ouverture du Post-it
    Public postItWindowId As PostItWindow = Nothing ' Référence à la fenêtre Post-it


End Module
