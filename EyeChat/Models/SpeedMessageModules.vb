Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.IO
Imports EyeChat.Utilities
Imports log4net
Imports Newtonsoft.Json

Namespace Models
    Public Class SpeedMessageModels
        Implements INotifyPropertyChanged

        Private _Index As Integer
        Public Property Index As Integer
            Get
                Return _Index
            End Get
            Set(value As Integer)
                If _Index <> value Then
                    _Index = value
                    NotifyPropertyChanged(NameOf(Index))
                End If
            End Set
        End Property

        Private _Title As String
        Public Property Title As String
            Get
                Return _Title
            End Get
            Set(value As String)
                If _Title <> value Then
                    _Title = value
                    NotifyPropertyChanged(NameOf(Title))
                End If
            End Set
        End Property

        Private _destinataire As String
        Public Property Destinataire As String
            Get
                Return _destinataire
            End Get
            Set(value As String)
                If _destinataire <> value Then
                    _destinataire = value
                    NotifyPropertyChanged(NameOf(Destinataire))
                End If
            End Set
        End Property

        Private _message As String
        Public Property Message As String
            Get
                Return _message
            End Get
            Set(value As String)
                If _message <> value Then
                    _message = value
                    NotifyPropertyChanged(NameOf(Message))
                End If
            End Set
        End Property

        Private _options As String
        Public Property Options As String
            Get
                Return _options
            End Get
            Set(value As String)
                If _options <> value Then
                    _options = value
                    NotifyPropertyChanged(NameOf(Options))
                End If
            End Set
        End Property

        Private _Load As Boolean
        Public Property Load As Boolean
            Get
                Return _Load
            End Get
            Set(value As Boolean)
                If _Load <> value Then
                    _Load = value
                    NotifyPropertyChanged(NameOf(Load))
                End If
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub NotifyPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        Public Shared Sub LoadSpeedMessages()
            Try
                Dim filePath = AppConfig.SpeedMessageFilePath
                If File.Exists(filePath) Then
                    Dim json As String = File.ReadAllText(filePath)
                    SpeedMessagesListGlobal = JsonConvert.DeserializeObject(Of List(Of SpeedMessageModels))(json)
                Else
                    SpeedMessagesListGlobal = New List(Of SpeedMessageModels)()
                    SaveSpeedMessageToJson()
                End If
            Catch ex As Exception
                logger.Error("Erreur lors du chargement des messages de vitesse : " & ex.Message)
                SpeedMessagesListGlobal = New List(Of SpeedMessageModels)()
                SaveSpeedMessageToJson()
            End Try
        End Sub

        Public Shared Sub SaveSpeedMessageToJson()
            Try
                Dim dossier As String = Path.GetDirectoryName(AppConfig.SpeedMessageFilePath)
                If Not Directory.Exists(dossier) Then
                    Directory.CreateDirectory(dossier)
                End If
                Dim optionsJson As String = JsonConvert.SerializeObject(SpeedMessagesListGlobal, Formatting.Indented)
                File.WriteAllText(AppConfig.SpeedMessageFilePath, optionsJson)
            Catch ex As Exception
                logger.Error($"Erreur lors de la sauvegarde des SpeedMessage : {ex.Message}")
            End Try
        End Sub

    End Class
End Namespace
