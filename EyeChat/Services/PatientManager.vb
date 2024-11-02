Imports System.Globalization
Imports EyeChat.Models
Imports log4net

Namespace Services
    Public Class PatientManager
        Private Shared ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

        ' Ajout d'un constructeur public vide
        Public Sub New()
        End Sub

        Public Shared Sub PatientAdd(ByVal title As String, ByVal lastName As String, ByVal firstName As String, ByVal exams As String, ByVal annotation As String, ByVal position As String, ByVal examinator As String, ByVal holdTime As String)
            Try
                Dim examOption As ExamOptionModels = ExamOptionsList.FirstOrDefault(Function(opt) opt.Name = exams)
                If examOption IsNot Nothing Then
                    Dim patientColor As String = examOption.Color
                    Dim newPatient As New PatientModels With {
                        .Title = title,
                        .LastName = lastName,
                        .FirstName = firstName,
                        .Exams = exams,
                        .Annotation = annotation,
                        .Position = position,
                        .Hold_Time = holdTime,
                        .IsTaken = False,
                        .Colors = patientColor,
                        .Examinator = examinator
                    }
                    AddPatientToList(newPatient)
                    logger.Info($"Patient ajouté : {lastName}")
                End If
                ''PatientBubbleCtrl.UpdateList()
                PatientModels.SavePatientsToJson()
            Catch ex As Exception
                logger.Error($"Erreur lors de l'ajout du patient {lastName} : {ex.Message}")
            End Try
        End Sub

        Public Shared Sub PatientRemove(ByVal title As String, ByVal lastName As String, ByVal firstName As String, ByVal exams As String, ByVal annotation As String, ByVal floor As String, ByVal examinator As String, ByVal holdTime As String)
            Dim patientToRemove = FindPatient(title, lastName, firstName, exams, annotation, floor, examinator, holdTime)
            If patientToRemove IsNot Nothing Then
                RemovePatientFromList(patientToRemove)
                logger.Info($"Patient retiré : {lastName}")
            End If
            ''PatientBubbleCtrl.UpdateList()
            PatientModels.SavePatientsToJson()
        End Sub

        Public Shared Sub PatientUpdate(ByVal title As String, ByVal lastName As String, ByVal firstName As String, ByVal exams As String, ByVal annotation As String, ByVal floor As String, ByVal examinator As String, ByVal oldHoldTime As String, ByVal newHoldTime As String)
            Dim patientToUpdate = FindPatient(title, lastName, firstName, exams, annotation, floor, examinator, oldHoldTime)
            If patientToUpdate IsNot Nothing Then
                RemovePatientFromList(patientToUpdate)
                patientToUpdate.Hold_Time = newHoldTime
                AddPatientToList(patientToUpdate)
                logger.Info($"Patient mis à jour : {lastName}")
            End If
            ''PatientBubbleCtrl.UpdateList()
            PatientModels.SavePatientsToJson()
        End Sub

        Public Shared Sub PatientCheckPass(ByVal title As String, ByVal lastName As String, ByVal firstName As String, ByVal exams As String, ByVal annotation As String, ByVal floor As String, ByVal examinator As String, ByVal holdTime As String, ByVal operatorName As String)
            Dim patientToCheck = FindPatient(title, lastName, firstName, exams, annotation, floor, examinator, holdTime)
            If patientToCheck IsNot Nothing Then
                RemovePatientFromList(patientToCheck)
                With patientToCheck
                    .IsTaken = True
                    .OperatorName = operatorName
                    .Colors = "gray"
                    .Pick_up_Time = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff")
                    .Time_Order = DateTime.Parse(.Pick_up_Time) - DateTime.Parse(.Hold_Time)
                End With
                AddPatientToList(patientToCheck)
                logger.Info($"Patient passé : {lastName}")
            End If
            ''PatientBubbleCtrl.UpdateList()
            PatientModels.SavePatientsToJson()
        End Sub

        Public Shared Sub PatientUndoPass(ByVal title As String, ByVal lastName As String, ByVal firstName As String, ByVal exams As String, ByVal annotation As String, ByVal floor As String, ByVal examinator As String, ByVal holdTime As String, ByVal operatorName As String)
            Try
                Dim patientToUpdate As PatientModels = FindPatient(title, lastName, firstName, exams, annotation, floor, examinator, holdTime)

                If patientToUpdate IsNot Nothing Then
                    ' Mise à jour des propriétés du patient
                    With patientToUpdate
                        .IsTaken = False
                        .OperatorName = Nothing
                        .Pick_up_Time = Nothing
                        .Time_Order = Nothing
                        .Colors = ExamOptionsList.FirstOrDefault(Function(opt) opt.Name = .Exams)?.Color
                    End With

                    ' Mise à jour des listes
                    RemovePatientFromList(patientToUpdate)
                    AddPatientToList(patientToUpdate)

                    logger.Info($"Annulation du passage du patient : {lastName}")
                End If

                PatientModels.SavePatientsToJson()
            Catch ex As Exception
                logger.Error($"Erreur lors de l'annulation du passage du patient {lastName} : {ex.Message}")
            End Try
        End Sub

        ' Méthodes d'aide pour ajouter et retirer des patients des listes appropriées
        Private Shared Sub AddPatientToList(ByVal patient As PatientModels)
            PatientsALLList.Add(patient)
            If patient.Position = "RDC" Then
                PatientsRDCList.Add(patient)
            ElseIf patient.Position = "1er" Then
                Patients1erList.Add(patient)
            End If
        End Sub

        Private Shared Sub RemovePatientFromList(ByVal patient As PatientModels)
            PatientsALLList.Remove(patient)
            If patient.Position = "RDC" Then
                PatientsRDCList.Remove(patient)
            ElseIf patient.Position = "1er" Then
                Patients1erList.Remove(patient)
            End If
        End Sub

        Private Shared Function FindPatient(ByVal title As String, ByVal lastName As String, ByVal firstName As String, ByVal exams As String, ByVal annotation As String, ByVal floor As String, ByVal examinator As String, ByVal holdTime As String) As PatientModels
            Dim holdTimeDateTime As DateTime
            If DateTime.TryParseExact(holdTime, "yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, holdTimeDateTime) Then
                Return PatientsALLList.FirstOrDefault(Function(patient) patient.Title = title AndAlso
                    patient.LastName = lastName AndAlso patient.FirstName = firstName AndAlso
                    patient.Exams = exams AndAlso patient.Annotation = annotation AndAlso
                    patient.Position = floor AndAlso patient.Examinator = examinator AndAlso
                    patient.Hold_Time = holdTimeDateTime)
            End If
            Return Nothing
        End Function
    End Class
End Namespace

