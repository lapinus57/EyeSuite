Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Runtime.Remoting.Messaging
Imports EyeChat.EyeChat
Imports EyeChat.Utilities
Imports log4net
Imports Newtonsoft.Json

Namespace Models
    Public Class MessageModels
        Inherits DependencyObject

        <JsonProperty("Name")>
        Public Property Name As String

        <JsonProperty("Sender")>
        Public Property Sender As String

        <JsonProperty("Room")>
        Public Property Room As String

        <JsonProperty("Content")>
        Public Property Content As String

        <JsonProperty("Timestamp")>
        Public Property Timestamp As DateTime = DateTime.Now

        <JsonProperty("Avatar")>
        Public Property Avatar As String

        <JsonProperty("IsAlignedRight")>
        Public Property IsAlignedRight As Boolean
            Get
                Return CBool(GetValue(IsAlignedRightProperty))
            End Get
            Set(ByVal value As Boolean)
                SetValue(IsAlignedRightProperty, value)
            End Set
        End Property

        Public Shared ReadOnly IsAlignedRightProperty As DependencyProperty = DependencyProperty.Register("IsAlignedRight", GetType(Boolean), GetType(MessageModels), New PropertyMetadata(False))

        ' Chargement des messages à partir du fichier JSON
        Public Shared Sub LoadMessagesFromJson()
            Try
                Dim filepath = AppConfig.messagesFilePath
                If File.Exists(filepath) Then
                    Dim json As String = File.ReadAllText(filepath)
                    MessagesListGlobal = JsonConvert.DeserializeObject(Of ObservableCollection(Of MessageModels))(json)
                    For Each message In MessagesListGlobal
                        message.IsAlignedRight = (message.Sender = GlobalConfig.UserSettingsList.UserName)
                    Next
                Else
                    MessagesListGlobal = New ObservableCollection(Of MessageModels)()
                    SaveMessagesToJson()
                End If
            Catch ex As Exception
                logger.Error($"Erreur lors du chargement des messages : {ex.Message}")
                MessagesListGlobal = New ObservableCollection(Of MessageModels)()
                SaveMessagesToJson()
            End Try
        End Sub

        ' Enregistrement des messages dans le fichier JSON
        Public Shared Sub SaveMessagesToJson()
            Try
                Dim dossier As String = Path.GetDirectoryName(AppConfig.messagesFilePath)
                If Not Directory.Exists(dossier) Then
                    Directory.CreateDirectory(dossier)
                End If
                Dim serializedMessages As String = JsonConvert.SerializeObject(MessagesListGlobal, Formatting.Indented)
                File.WriteAllText(AppConfig.messagesFilePath, serializedMessages)
            Catch ex As Exception
                logger.Error($"Erreur lors de la sauvegarde des messages : {ex.Message}")
            End Try
        End Sub

        Public Shared Sub AddMessage(ByVal name As String, ByVal sender As String, ByVal room As String, ByVal content As String, ByVal isAlignedRight As Boolean, ByVal avatar As String)
            If MessagesListGlobal IsNot Nothing Then
                MessagesListGlobal.Add(New MessageModels With {.Name = name, .Sender = sender, .Room = room, .Content = content, .IsAlignedRight = isAlignedRight, .Timestamp = DateTime.Now, .Avatar = avatar})
                SaveMessagesToJson()
            End If
        End Sub
    End Class
End Namespace

