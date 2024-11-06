Imports MahApps.Metro.Controls

Namespace Views
    Public Class InputDefaulUserWindow
        Inherits MetroWindow

        Public Property UserName As String
        Private Sub OKButton_Click(sender As Object, e As RoutedEventArgs)
            If String.IsNullOrWhiteSpace(UserNameTextBox.Text) Then
                MessageBox.Show("Veuillez entrer un nom valide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error)
                Return
            End If
            UserName = UserNameTextBox.Text.Trim()
            DialogResult = True
            'Close()
        End Sub

        Private Sub CancelButton_Click(sender As Object, e As RoutedEventArgs)
            DialogResult = False
            ''Close()
        End Sub
    End Class
End Namespace
