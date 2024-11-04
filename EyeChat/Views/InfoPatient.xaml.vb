Imports EyeChat.Models

Namespace Views
    ''' <summary>
    ''' Logique d'interaction pour InfoPatient.xaml
    ''' </summary>
    Public Class InfoPatient
        Public Sub New()
            InitializeComponent()
        End Sub

        ' Exemple de méthode pour modifier le contenu des TextBlock
        Public Sub UpdatePatientDetails(patient As PatientModels)
            PatientName.Text = $"{patient.Title} {patient.LastName} {patient.FirstName}"
            Examinator.Text = patient.Examinator
            Hold_Time.Text = patient.Hold_Time.ToString("HH:mm")
            Annotation.Text = patient.Annotation
            Exams.Text = patient.Exams
            OperatorName.Text = patient.OperatorName
            Pick_up_Time.Text = patient.Pick_up_Time.ToString("HH:mm")
            Time_Order.Text = patient.Time_Order.ToString("hh\:mm")
        End Sub

        Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
            Me.Close()
        End Sub
    End Class
End Namespace
