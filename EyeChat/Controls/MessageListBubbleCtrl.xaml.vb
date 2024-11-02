Imports System.Collections.ObjectModel
Imports EyeChat.Models

Namespace Controls
    Public Class MessageListBubbleCtrl
        Public Shared ReadOnly MessagesProperty As DependencyProperty = DependencyProperty.Register("Messages", GetType(ObservableCollection(Of MessageModels)), GetType(MessageListBubbleCtrl))

        Public Property Messages As ObservableCollection(Of MessageModels)
            Get
                Return CType(GetValue(MessagesProperty), ObservableCollection(Of MessageModels))
            End Get
            Set(value As ObservableCollection(Of MessageModels))
                SetValue(MessagesProperty, value)
            End Set
        End Property

        Private Sub SetValue(messagesProperty As DependencyProperty, value As ObservableCollection(Of MessageModels))
            Throw New NotImplementedException()
        End Sub

        Private Function GetValue(messagesProperty As DependencyProperty) As Object
            Throw New NotImplementedException()
        End Function

        Public Sub New()
            ''InitializeComponent()
        End Sub
        Public Sub ScrollToEnd()
            'ScrollViewer.ScrollToEnd()
        End Sub
    End Class
End Namespace
