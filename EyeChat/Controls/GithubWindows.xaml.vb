Imports EyeChat.ViewModel
Imports MahApps.Metro.Controls.Dialogs

Namespace Controls
    Public Class GithubWindows
        Inherits UserControl

        Private ReadOnly _viewModel As GitHubViewModel

        Public Sub New()
            InitializeComponent()

            ' Instancier le ViewModel et le lier à la vue
            _viewModel = New GitHubViewModel(DialogCoordinator.Instance)
            Me.DataContext = _viewModel
        End Sub

        ' Gestionnaire d'événements pour le bouton de la page d'accueil
        Private Sub HomePageButton_Click(sender As Object, e As RoutedEventArgs)
            _viewModel.HomePageButton_Click(sender, e)
        End Sub

        ' Gestionnaire d'événements pour le bouton Wiki
        Private Sub WikiButton_Click(sender As Object, e As RoutedEventArgs)
            _viewModel.WikiButton_Click(sender, e)
        End Sub

        ' Gestionnaire d'événements pour le bouton d'envoi du rapport
        Private Sub SendReport_Click(sender As Object, e As RoutedEventArgs)
            _viewModel.SendReport_Click(sender, e)
        End Sub
    End Class
End Namespace

