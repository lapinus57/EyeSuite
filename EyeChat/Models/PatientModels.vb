Imports System.Collections.ObjectModel
Imports System.IO
Imports EyeChat.Utilities
Imports Newtonsoft.Json

Namespace Models
    Public Class PatientModels

        <JsonProperty("Id")>
        Public Property Id As String

        <JsonProperty("Colors")>
        Public Property Colors As String

        <JsonProperty("Title")>
        Public Property Title As String

        <JsonProperty("LastName")>
        Public Property LastName As String

        <JsonProperty("FirstName")>
        Public Property FirstName As String

        <JsonProperty("Exams")>
        Public Property Exams As String

        <JsonProperty("Annotation")>
        Public Property Annotation As String

        <JsonProperty("Position")>
        Public Property Position As String

        <JsonProperty("Hold_Time")>
        Public Property Hold_Time As DateTime

        <JsonProperty("Pick_up_Time")>
        Public Property Pick_up_Time As DateTime?

        <JsonProperty("Time_Order")>
        Public Property Time_Order As TimeSpan

        <JsonProperty("Examinator")>
        Public Property Examinator As String

        <JsonProperty("OperatorName")>
        Public Property OperatorName As String

        <JsonProperty("IsTaken")>
        Public Property IsTaken As Boolean


        Public Shared Sub LoadPatientsFromJson()
            Try
                Dim filePath = AppConfig.PatientFilePath
                If File.Exists(filePath) Then
                    Dim json As String = File.ReadAllText(filePath)
                    PatientsALLList = JsonConvert.DeserializeObject(Of ObservableCollection(Of PatientModels))(json)
                    PatientsRDCList = New ObservableCollection(Of PatientModels)(PatientsALLList.Where(Function(p) p.Position = "RDC"))
                    Patients1erList = New ObservableCollection(Of PatientModels)(PatientsALLList.Where(Function(p) p.Position = "1er"))
                Else
                    PatientsALLList = New ObservableCollection(Of PatientModels)()
                    PatientsRDCList = New ObservableCollection(Of PatientModels)()
                    Patients1erList = New ObservableCollection(Of PatientModels)()
                    SavePatientsToJson()
                End If
            Catch ex As Exception
                Console.WriteLine($"Erreur lors du chargement et de l'organisation des patients : {ex.Message}")
                PatientsALLList = New ObservableCollection(Of PatientModels)()
                PatientsRDCList = New ObservableCollection(Of PatientModels)()
                Patients1erList = New ObservableCollection(Of PatientModels)()
                SavePatientsToJson()
            End Try
        End Sub


        ' Enregistrement des patients dans le fichier JSON
        Public Shared Sub SavePatientsToJson()
            Try
                Dim dossier As String = Path.GetDirectoryName(AppConfig.PatientFilePath)
                If Not Directory.Exists(dossier) Then
                    Directory.CreateDirectory(dossier)
                End If
                Dim serializedPatients As String = JsonConvert.SerializeObject(PatientsALLList, Formatting.Indented)
                File.WriteAllText(AppConfig.PatientFilePath, serializedPatients)
            Catch ex As Exception
                ' Gérer les exceptions et journaliser l'erreur si nécessaire
                Console.WriteLine($"Erreur lors de l'enregistrement des patients : {ex.Message}")
            End Try
        End Sub
    End Class
End Namespace
