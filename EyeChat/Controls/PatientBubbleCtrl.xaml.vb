Imports EyeChat.EyeChat
Imports EyeChat.Models
Imports EyeChat.Networking
Imports EyeChat.Views
Imports log4net
Imports MahApps.Metro.SimpleChildWindow
Imports System.Collections.ObjectModel
Imports System.ComponentModel

Namespace Controls
    Public Class PatientBubbleCtrl
        Inherits UserControl
        Implements INotifyPropertyChanged

        Public Sub New()
            InitializeComponent()
        End Sub

        Private _mainWindowInstance As MainWindow

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Property MainWindowInstance As MainWindow
            Get
                Return _mainWindowInstance
            End Get
            Set(value As MainWindow)
                _mainWindowInstance = value
            End Set
        End Property

        ' Méthode utilitaire pour obtenir un patient à partir d'un MenuItem
        Private Function GetPatientFromMenuItem(sender As Object) As PatientModels
            Try
                If TypeOf sender Is MenuItem Then
                    Return TryCast(DirectCast(sender, MenuItem).DataContext, PatientModels)
                End If
            Catch ex As Exception
                logger.Error("Erreur lors de l'obtention du patient à partir du MenuItem : " & ex.Message)
            End Try
            Return Nothing
        End Function

        ' Méthode utilitaire pour envoyer un message
        Private Sub SendPatientMessage(command As String, patient As PatientModels, Optional newHoldTime As DateTime? = Nothing)
            Try
                Dim formattedHoldTime As String = patient.Hold_Time.ToString("yyyy-MM-ddTHH:mm:ss.fff")
                Dim formattedNewHoldTime As String = If(newHoldTime.HasValue, newHoldTime.Value.ToString("yyyy-MM-ddTHH:mm:ss.fff"), formattedHoldTime)
                Dim text As String = $"{command}{patient.Title}|{patient.LastName}|{patient.FirstName}|{patient.Exams}|{patient.Annotation}|{patient.Position}|{patient.Examinator}|{formattedHoldTime}|{UserSettingsList.UserName}|{formattedNewHoldTime}"
                SendManager.SendMessage(text)
                logger.Info($"Message envoyé : {text}")
            Catch ex As Exception
                logger.Error("Erreur lors de l'envoi du message du patient : " & ex.Message)
            End Try
        End Sub

        Private Sub MenuItem_PassageClick(sender As Object, e As RoutedEventArgs)
            Try
                Dim patient = GetPatientFromMenuItem(sender)
                If patient Is Nothing Then Exit Sub

                Dim command As String = If(patient.IsTaken, "PTN05", "PTN02")
                SendPatientMessage(command, patient)
            Catch ex As Exception
                logger.Error("Erreur lors du clic sur le menu 'Passage' : " & ex.Message)
            End Try
        End Sub

        Private Sub MenuItem_AttenteClick(sender As Object, e As RoutedEventArgs)
            Try
                Dim patient = GetPatientFromMenuItem(sender)
                If patient Is Nothing Then Exit Sub

                If Not patient.IsTaken Then
                    SendPatientMessage("PTN02", patient)
                    SendPatientMessage("PTN01", patient)
                Else
                    SendPatientMessage("PTN05", patient)
                End If
            Catch ex As Exception
                logger.Error("Erreur lors du clic sur le menu 'Attente' : " & ex.Message)
            End Try
        End Sub

        Private Async Sub MenuItem_InfoPatient(sender As Object, e As RoutedEventArgs)
            Try
                Dim patient = GetPatientFromMenuItem(sender)
                If patient Is Nothing Then Exit Sub

                Dim childWindow As New InfoPatient() With {
                    .Title = "Information Patient"
                }
                childWindow.UpdatePatientDetails(patient)

                Await CType(Application.Current.MainWindow, MahApps.Metro.Controls.MetroWindow).ShowChildWindowAsync(childWindow)
                logger.Info("Fenêtre d'informations du patient affichée.")
            Catch ex As Exception
                logger.Error("Erreur lors de l'affichage des informations du patient : " & ex.Message)
            End Try
        End Sub

        Private Sub MenuItem_DelteClick(sender As Object, e As RoutedEventArgs)
            Try
                Dim patient = GetPatientFromMenuItem(sender)
                If patient Is Nothing Then Exit Sub

                SendPatientMessage("PTN03", patient)
                logger.Info($"Patient supprimé : {patient.LastName} {patient.FirstName}")
            Catch ex As Exception
                logger.Error("Erreur lors de la suppression du patient : " & ex.Message)
            End Try
        End Sub

        Private Sub MenuItem_upClick(sender As Object, e As RoutedEventArgs)
            Try
                PatientsALLList = New ObservableCollection(Of PatientModels)(PatientsALLList.OrderBy(Function(p) p.Hold_Time))
                Dim patient = GetPatientFromMenuItem(sender)
                If patient Is Nothing Then Exit Sub

                Dim index As Integer = PatientsALLList.IndexOf(patient)
                Dim patientN1 As PatientModels = FindAdjacentPatient(index - 1, patient.Position, -1)
                Dim patientN2 As PatientModels = If(patientN1 IsNot Nothing, FindAdjacentPatient(PatientsALLList.IndexOf(patientN1) - 1, patient.Position, -1), Nothing)

                If patientN1 IsNot Nothing AndAlso patientN2 IsNot Nothing Then
                    Dim timeDiffN1N2 As TimeSpan = patientN1.Hold_Time.Subtract(patientN2.Hold_Time)
                    Dim adjustment As TimeSpan = TimeSpan.FromTicks(timeDiffN1N2.Ticks / 2)
                    UpdatePatientHoldTime(patient, patientN1.Hold_Time.Subtract(adjustment))
                ElseIf patientN1 IsNot Nothing Then
                    UpdatePatientHoldTime(patient, patientN1.Hold_Time.Subtract(TimeSpan.FromMinutes(2)))
                End If
            Catch ex As Exception
                logger.Error("Erreur lors du déplacement du patient vers le haut : " & ex.Message)
            End Try
        End Sub

        Private Sub MenuItem_downClick(sender As Object, e As RoutedEventArgs)
            Try
                PatientsALLList = New ObservableCollection(Of PatientModels)(PatientsALLList.OrderBy(Function(p) p.Hold_Time))
                Dim patient = GetPatientFromMenuItem(sender)
                If patient Is Nothing Then Exit Sub

                Dim index As Integer = PatientsALLList.IndexOf(patient)
                Dim patientN1 As PatientModels = FindAdjacentPatient(index + 1, patient.Position, 1)
                Dim patientN2 As PatientModels = If(patientN1 IsNot Nothing, FindAdjacentPatient(PatientsALLList.IndexOf(patientN1) + 1, patient.Position, 1), Nothing)

                If patientN1 IsNot Nothing AndAlso patientN2 IsNot Nothing Then
                    Dim timeDiffN1N2 As TimeSpan = patientN2.Hold_Time.Subtract(patientN1.Hold_Time)
                    Dim adjustment As TimeSpan = TimeSpan.FromTicks(timeDiffN1N2.Ticks / 2)
                    UpdatePatientHoldTime(patient, patientN1.Hold_Time.Add(adjustment))
                ElseIf patientN1 IsNot Nothing Then
                    UpdatePatientHoldTime(patient, patientN1.Hold_Time.Add(TimeSpan.FromMinutes(2)))
                End If
            Catch ex As Exception
                logger.Error("Erreur lors du déplacement du patient vers le bas : " & ex.Message)
            End Try
        End Sub

        Private Sub UpdatePatientHoldTime(patient As PatientModels, newHoldTime As DateTime)
            Try
                SendPatientMessage("PTN04", patient, newHoldTime)
                logger.Info($"Horaire du patient mis à jour : {patient.LastName} {patient.FirstName}, Nouveau Hold_Time : {newHoldTime}")
            Catch ex As Exception
                logger.Error("Erreur lors de la mise à jour de l'horaire du patient : " & ex.Message)
            End Try
        End Sub

        Private Function FindAdjacentPatient(startIndex As Integer, position As String, stepValue As Integer) As PatientModels
            Try
                For i As Integer = startIndex To If(stepValue > 0, PatientsALLList.Count - 1, 0) Step stepValue
                    If PatientsALLList(i).Position = position Then
                        Return PatientsALLList(i)
                    End If
                Next
            Catch ex As Exception
                logger.Error("Erreur lors de la recherche du patient adjacent : " & ex.Message)
            End Try
            Return Nothing
        End Function

        Private Sub UserControl_ContextMenuOpening(sender As Object, e As ContextMenuEventArgs)
            Dim contextMenu As ContextMenu = Me.ContextMenu
            If contextMenu IsNot Nothing Then
                ' Ajustez la visibilité des MenuItem en fonction de OrthoMode et AdvanvedMode
                DirectCast(contextMenu.Items(1), MenuItem).Visibility = If(UserSettingsList.OrthoMode, Visibility.Visible, Visibility.Collapsed)
                DirectCast(contextMenu.Items(3), MenuItem).Visibility = If(UserSettingsList.AdvanvedMode, Visibility.Visible, Visibility.Collapsed)
            End If
        End Sub

        Private Sub MenuItem_TopClick(sender As Object, e As RoutedEventArgs)
            PatientsALLList = New ObservableCollection(Of PatientModels)(PatientsALLList.OrderBy(Function(p) p.Hold_Time))
            If TypeOf sender Is MenuItem Then
                Dim menuItem As MenuItem = DirectCast(sender, MenuItem)
                Dim patient As PatientModels = DirectCast(menuItem.DataContext, PatientModels)

                ' Obtenir le premier patient non pris au même étage (position)
                Dim firstAvailablePatient As PatientModels = PatientsALLList.FirstOrDefault(Function(p) p.Position = patient.Position AndAlso Not p.IsTaken)
                ' Obtenir le dernier patient pris au même étage (position)
                Dim lastAvailablePatient As PatientModels = PatientsALLList.LastOrDefault(Function(p) p.Position = patient.Position AndAlso p.IsTaken)

                If firstAvailablePatient IsNot Nothing AndAlso lastAvailablePatient Is Nothing Then
                    Dim random As New Random()
                    Dim randomSeconds As Integer = random.Next(60, 361)
                    Dim randomTimeSpan As TimeSpan = TimeSpan.FromSeconds(randomSeconds)
                    Dim newHoldTime As DateTime = firstAvailablePatient.Hold_Time.Subtract(randomTimeSpan)
                    UpdatePatientHoldTime(patient, newHoldTime)
                ElseIf firstAvailablePatient IsNot Nothing AndAlso lastAvailablePatient IsNot Nothing Then
                    Dim averageTimeSpan As TimeSpan = TimeSpan.FromTicks((lastAvailablePatient.Hold_Time - firstAvailablePatient.Hold_Time).Ticks \ 2)
                    Dim random As New Random()
                    Dim randomOffsetTicks As Long = random.Next(-averageTimeSpan.Ticks, averageTimeSpan.Ticks + 1)
                    Dim randomTimeSpan As TimeSpan = TimeSpan.FromTicks(averageTimeSpan.Ticks + randomOffsetTicks)
                    Dim newHoldTime As DateTime = lastAvailablePatient.Hold_Time.Subtract(randomTimeSpan)
                    UpdatePatientHoldTime(patient, newHoldTime)
                End If
            End If
        End Sub


    End Class
End Namespace
