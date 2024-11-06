Imports System.ComponentModel

Public Class WaitingRoomModels
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

    Private _Description As String
    Public Property Description As String
        Get
            Return _Description
        End Get
        Set(value As String)
            If _Description <> value Then
                _Description = value
                NotifyPropertyChanged(NameOf(Description))
            End If
        End Set
    End Property

    Private _Color As String
    Public Property Color As String
        Get
            Return _Color
        End Get
        Set(value As String)
            If _Color <> value Then
                _Color = value
                NotifyPropertyChanged(NameOf(Color))
            End If
        End Set
    End Property


    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub NotifyPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
End Class
