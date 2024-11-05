Imports EyeChat.EyeChat
Imports EyeChat.Models
Imports System.ComponentModel
Imports System.Runtime.Remoting.Messaging

Namespace Controls
    Public Class MessageBubbleCtrl
        Inherits UserControl
        Implements INotifyPropertyChanged

        Public Shared ReadOnly ContentTextProperty As DependencyProperty = DependencyProperty.Register("ContentText", GetType(String), GetType(MessageBubbleCtrl))
        Public Shared ReadOnly IsAlignedRightProperty As DependencyProperty = DependencyProperty.Register("IsAlignedRight", GetType(Boolean), GetType(MessageBubbleCtrl))
        Public Shared ReadOnly SizeProperty As DependencyProperty = DependencyProperty.Register("SizeDisplayUsers", GetType(Double), GetType(MessageBubbleCtrl))

        Public Property ContentText As String
            Get
                Return CStr(GetValue(ContentTextProperty))
            End Get
            Set(value As String)
                SetValue(ContentTextProperty, value)
            End Set
        End Property



        Public Property Size As Double
            Get
                Return CDbl(GetValue(SizeProperty))
            End Get
            Set(value As Double)
                SetValue(SizeProperty, value)
            End Set
        End Property

        Public Property IsAlignedRight As Boolean
            Get
                Return CBool(GetValue(IsAlignedRightProperty))
            End Get
            Set(value As Boolean)
                SetValue(IsAlignedRightProperty, value)
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
            Size = UserSettingsList.AppSizeDisplay
        End Sub
        'Protected Sub OnPropertyChanged(propertyName As String)
        'RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        'End Sub
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private Sub CopyMenuItem_Click(sender As Object, e As RoutedEventArgs)
            Dim message As MessageModels = DirectCast(DataContext, MessageModels)
            If message IsNot Nothing Then
                Dim textToCopy As String = $"{message.Name} & {message.Sender}: {message.Content}, à {message.Timestamp:HH:mm}"
                'Dim textToCopy As String = $"{message.Name} & {message.Sender}: {message.Content}, à {message.Timestamp.ToString("HH:mm")}"
                Clipboard.SetText(textToCopy)
            End If
        End Sub

        Public Sub DeleteMenuItem_Click(sender As Object, e As RoutedEventArgs)
            Dim message As MessageModels = DirectCast(DataContext, MessageModels)
            If message IsNot Nothing Then

                Dim messageToRemove As MessageModels = MessagesListGlobal.FirstOrDefault(Function(m) m.Name = message.Name AndAlso m.Sender = message.Sender AndAlso m.Content = message.Content AndAlso m.Timestamp = message.Timestamp)

                If messageToRemove IsNot Nothing Then
                    MessagesListGlobal.Remove(messageToRemove)
                    MessageModels.SaveMessagesToJson()
                    ''SelectUser(selectedUserName)
                End If
            End If
        End Sub

        Private Sub AMenuItem_Click(sender As Object, e As RoutedEventArgs)

        End Sub
        Private Sub BMenuItem_Click(sender As Object, e As RoutedEventArgs)

        End Sub
        Private Sub CMenuItem_Click(sender As Object, e As RoutedEventArgs)

        End Sub
        Private Sub DMenuItem_Click(sender As Object, e As RoutedEventArgs)

        End Sub
    End Class
End Namespace