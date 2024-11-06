Imports EyeChat.EyeChat
Imports EyeChat.Networking

Namespace Controls
    Public Class UsersBubbleCtrl
        Public Shared ReadOnly SizeProperty As DependencyProperty = DependencyProperty.Register("SizeDisplayUsers", GetType(Double), GetType(UsersBubbleCtrl))
        Public Sub New()

            ' Cet appel est requis par le concepteur.
            InitializeComponent()

            ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
            Size = UserSettingsList.AppSizeDisplay
        End Sub

        Public Property Size As Double
            Get
                Return CDbl(GetValue(SizeProperty))
            End Get
            Set(value As Double)
                SetValue(SizeProperty, value)
            End Set
        End Property
        Private Sub Border_MouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)


        End Sub

        Private Sub MenuItemMoveUp_Click(sender As Object, e As RoutedEventArgs)

        End Sub

        Private Sub MenuItemMoveDown_Click(sender As Object, e As RoutedEventArgs)

        End Sub

        Private Sub MenuItemSendNotification_Click(sender As Object, e As RoutedEventArgs)

        End Sub
    End Class
End Namespace
