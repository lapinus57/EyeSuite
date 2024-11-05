Imports EyeChat.Models
Imports System.Collections.ObjectModel
Imports System.ComponentModel

Namespace ViewModel
    Public Class MainWindowsViewModels

        Implements INotifyPropertyChanged

        Public Property UsersMain As ObservableCollection(Of UserModels)
            Get
                Return UsersListGlobal
            End Get
            Set(value As ObservableCollection(Of UserModels))
                UsersListGlobal = value
                UserModels.SaveUsersToJson(UsersListGlobal)
                OnPropertyChanged(NameOf(UsersMain))

            End Set
        End Property

        Public Property AppSizeDisplay As String
            Get
                Return UserSettingsList.AppSizeDisplay
            End Get
            Set(value As String)
                UserSettingsList.AppSizeDisplay = value
                'UserSettingsModels.SaveUserSettingsToJson(UserSettingsList.UserName)
                OnPropertyChanged(NameOf(AppSizeDisplay))
            End Set
        End Property

        Public Sub New()
            ' Charger les utilisateurs depuis JSON si la liste est vide
            If UsersListGlobal Is Nothing Then
                UsersListGlobal = UserModels.LoadUsersFromJson()
            End If
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class
End Namespace

