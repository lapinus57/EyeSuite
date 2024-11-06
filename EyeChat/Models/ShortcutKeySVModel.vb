Imports EyeChat.Models
Imports System.Collections.ObjectModel
Imports System.ComponentModel

Public Class ShortcutKeySVModel
    Implements INotifyPropertyChanged

    Private ReadOnly _parentSettings As UserSettingsModels

    ' Constructeur avec référence au parent
    Public Sub New(parentSettings As UserSettingsModels)
        _parentSettings = parentSettings

        ' Détecter les changements dans les collections Pages et Texts
        AddHandler Pages.CollectionChanged, Sub()
                                                NotifyPropertyChanged(NameOf(Pages))
                                            End Sub
        AddHandler Texts.CollectionChanged, Sub()
                                                NotifyPropertyChanged(NameOf(Texts))
                                            End Sub
    End Sub

    Private _enabled As Boolean
    Public Property Enabled As Boolean
        Get
            Return _enabled
        End Get
        Set(value As Boolean)
            If _enabled <> value Then
                _enabled = value
                NotifyPropertyChanged(NameOf(Enabled))
            End If
        End Set
    End Property

    Private _pages As ObservableCollection(Of String) = New ObservableCollection(Of String)()
    Public Property Pages As ObservableCollection(Of String)
        Get
            Return _pages
        End Get
        Set(value As ObservableCollection(Of String))
            If Not _pages.Equals(value) Then
                ' Désabonnez-vous de l'ancienne collection
                RemoveHandler _pages.CollectionChanged, AddressOf OnCollectionChanged

                ' Mettez à jour la propriété
                _pages = value

                ' Abonnez-vous à la nouvelle collection
                AddHandler _pages.CollectionChanged, AddressOf OnCollectionChanged

                NotifyPropertyChanged(NameOf(Pages))
            End If
        End Set
    End Property

    Private _texts As ObservableCollection(Of String) = New ObservableCollection(Of String)()
    Public Property Texts As ObservableCollection(Of String)
        Get
            Return _texts
        End Get
        Set(value As ObservableCollection(Of String))
            If Not _texts.Equals(value) Then
                ' Désabonnez-vous de l'ancienne collection
                RemoveHandler _texts.CollectionChanged, AddressOf OnCollectionChanged

                ' Mettez à jour la propriété
                _texts = value

                ' Abonnez-vous à la nouvelle collection
                AddHandler _texts.CollectionChanged, AddressOf OnCollectionChanged

                NotifyPropertyChanged(NameOf(Texts))
            End If
        End Set
    End Property


    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Private Sub OnCollectionChanged(sender As Object, e As Specialized.NotifyCollectionChangedEventArgs)
        NotifyPropertyChanged(CType(sender, ObservableCollection(Of String)).ToString())
        ' Sauvegarde automatique si nécessaire
        _parentSettings?.SaveUserSettingsToJson(_parentSettings.UserName)
    End Sub

    Protected Sub NotifyPropertyChanged(ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        ' Sauvegarde automatique
        _parentSettings?.SaveUserSettingsToJson(_parentSettings.UserName)
    End Sub
End Class
