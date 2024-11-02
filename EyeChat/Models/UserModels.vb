Imports System.Collections.ObjectModel
Imports System.IO
Imports Newtonsoft.Json
Imports System.Windows
Imports System.Windows.Media
Imports log4net.Repository.Hierarchy
Imports EyeChat.Utilities

Namespace Models
    Public Class UserModels
        Inherits DependencyObject

        ' Définition des propriétés de dépendance pour le binding
        Public Shared ReadOnly NameProperty As DependencyProperty = DependencyProperty.Register("Name", GetType(String), GetType(UserModels))
        Public Shared ReadOnly StatusProperty As DependencyProperty = DependencyProperty.Register("Status", GetType(String), GetType(UserModels))
        Public Shared ReadOnly AvatarProperty As DependencyProperty = DependencyProperty.Register("Avatar", GetType(String), GetType(UserModels))
        Public Shared ReadOnly ColorUserProperty As DependencyProperty = DependencyProperty.Register("ColorUser", GetType(Color), GetType(UserModels))
        Public Shared ReadOnly AuxiliaireTitleProperty As DependencyProperty = DependencyProperty.Register("AuxiliaireTitle", GetType(String), GetType(UserModels))
        Public Shared ReadOnly InitialsProperty As DependencyProperty = DependencyProperty.Register("Initials", GetType(String), GetType(UserModels))
        Public Shared ReadOnly UUIDProperty As DependencyProperty = DependencyProperty.Register("UUID", GetType(String), GetType(UserModels))

        ' Propriétés exposées pour le binding
        <JsonProperty("Name")>
        Public Property Name As String
            Get
                Return GetValue(NameProperty)
            End Get
            Set(value As String)
                SetValue(NameProperty, value)
            End Set
        End Property

        <JsonProperty("Status")>
        Public Property Status As String
            Get
                Return GetValue(StatusProperty)
            End Get
            Set(value As String)
                SetValue(StatusProperty, value)
            End Set
        End Property

        <JsonProperty("Avatar")>
        Public Property Avatar As String
            Get
                Return GetValue(AvatarProperty)
            End Get
            Set(value As String)
                SetValue(AvatarProperty, value)
            End Set
        End Property

        <JsonProperty("ColorUser")>
        Public Property ColorUser As System.Windows.Media.Color
            Get
                Return GetValue(ColorUserProperty)
            End Get
            Set(value As System.Windows.Media.Color)
                SetValue(ColorUserProperty, value)
            End Set
        End Property

        <JsonProperty("AuxiliaireTilte")>
        Public Property AuxiliaireTitle As String
            Get
                Return GetValue(AuxiliaireTitleProperty)
            End Get
            Set(value As String)
                SetValue(AuxiliaireTitleProperty, value)
            End Set
        End Property

        <JsonProperty("initials")>
        Public Property Initials As String
            Get
                Return GetValue(InitialsProperty)
            End Get
            Set(value As String)
                SetValue(InitialsProperty, value)
            End Set
        End Property

        <JsonProperty("UUID")>
        Public Property UUID As String
            Get
                Return GetValue(UUIDProperty)
            End Get
            Set(value As String)
                SetValue(UUIDProperty, value)
            End Set
        End Property

        Public Sub New()
            Avatar = "/Avatar/avataaars.png"
            AuxiliaireTitle = String.Empty
            ColorUser = System.Windows.Media.Colors.White
            Status = "Offline"
            Initials = String.Empty
            UUID = String.Empty
        End Sub

        Public Shared Sub LoadUsersFromJson(Optional userNameToExclude As String = Nothing)
            Try
                Dim filepath = AppConfig.UsersFilePath
                If File.Exists(filepath) Then
                    Dim json As String = File.ReadAllText(filepath)
                    UsersList = JsonConvert.DeserializeObject(Of ObservableCollection(Of UserModels))(json)
                    If Not String.IsNullOrEmpty(userNameToExclude) Then
                        Dim userToRemove As UserModels = UsersList.FirstOrDefault(Function(user) user.Name = userNameToExclude)
                        If userToRemove IsNot Nothing Then
                            UsersList.Remove(userToRemove)
                        End If
                    End If
                Else
                    UsersList = New ObservableCollection(Of UserModels)()
                    If UsersList.Count = 0 Then
                        UsersList.Add(New UserModels With {.Name = "A Tous", .Avatar = AppConfig.AvatarAtous, .Status = "0"})
                        UsersList.Add(New UserModels With {.Name = "Secrétariat", .Avatar = AppConfig.AvatarSecretariat, .Status = "0"})
                        UserModels.SaveUsersToJson()
                    End If
                End If
            Catch ex As Exception
                logger.Error("Erreur lors du chargement des utilisateurs : " & ex.Message)
                UsersList = New ObservableCollection(Of UserModels)()
                If UsersList.Count = 0 Then
                    UsersList.Add(New UserModels With {.Name = "A Tous", .Avatar = AppConfig.AvatarAtous, .Status = "0"})
                    UsersList.Add(New UserModels With {.Name = "Secrétariat", .Avatar = AppConfig.AvatarSecretariat, .Status = "0"})
                    UserModels.SaveUsersToJson()
                End If
            End Try

        End Sub

        Public Shared Sub SaveUsersToJson()
            Try
                Dim dossier As String = Path.GetDirectoryName(AppConfig.UsersFilePath)
                If Not Directory.Exists(dossier) Then
                    Directory.CreateDirectory(dossier)
                End If
                Dim serializedUsers As String = JsonConvert.SerializeObject(UsersList, Formatting.Indented)
                File.WriteAllText(AppConfig.UsersFilePath, serializedUsers)
            Catch ex As Exception
                logger.Error("Erreur lors de la sauvegarde des utilisateurs : " & ex.Message)
            End Try
        End Sub

        Shared Function IsNameInList(users As ObservableCollection(Of UserModels), nameToSearch As String) As Boolean
            ' Fonction qui teste la présence d'un user et retourne une boolean
            logger.Info($"Test de la présence de {nameToSearch} dans la liste des users.")
            Try
                Return users.Any(Function(u) u.Name = nameToSearch)
                logger.Debug($"{nameToSearch} est dans la liste des users ")
            Catch ex As Exception
                Return False
                logger.Error($"Erreur lors du test de la présence de {nameToSearch} dans la liste des users.")
            End Try

        End Function
    End Class
End Namespace
