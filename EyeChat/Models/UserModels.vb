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
        Public Shared ReadOnly AuxiliaireTilteProperty As DependencyProperty = DependencyProperty.Register("AuxiliaireTilte", GetType(String), GetType(UserModels))
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
        Public Property AuxiliaireTilte As String
            Get
                Return GetValue(AuxiliaireTilteProperty)
            End Get
            Set(value As String)
                SetValue(AuxiliaireTilteProperty, value)
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
            AuxiliaireTilte = String.Empty
            ColorUser = System.Windows.Media.Colors.White
            Status = "Offline"
            Initials = String.Empty
            UUID = String.Empty
        End Sub

        Public Shared Function LoadUsersFromJson(Optional userNameToExclude As String = Nothing) As ObservableCollection(Of UserModels)
            Dim usersList As ObservableCollection(Of UserModels) = Nothing
            Try
                Dim filepath = AppConfig.UsersFilePath
                If File.Exists(filepath) Then
                    Dim json As String = File.ReadAllText(filepath)
                    usersList = JsonConvert.DeserializeObject(Of ObservableCollection(Of UserModels))(json)
                    If Not String.IsNullOrEmpty(userNameToExclude) Then
                        Dim userToRemove As UserModels = usersList.FirstOrDefault(Function(user) user.Name = userNameToExclude)
                        If userToRemove IsNot Nothing Then
                            usersList.Remove(userToRemove)
                        End If
                    End If
                Else
                    ' Initialise une nouvelle liste si le fichier n'existe pas
                    usersList = New ObservableCollection(Of UserModels) From {
                New UserModels With {.Name = "A Tous", .Avatar = AppConfig.AvatarAtous, .Status = "0"},
                New UserModels With {.Name = "Secrétariat", .Avatar = AppConfig.AvatarSecretariat, .Status = "0"}
            }
                    SaveUsersToJson(usersList)
                End If
            Catch ex As Exception
                logger.Error("Erreur lors du chargement des utilisateurs : " & ex.Message)
                usersList = New ObservableCollection(Of UserModels) From {
            New UserModels With {.Name = "A Tous", .Avatar = AppConfig.AvatarAtous, .Status = "0"},
            New UserModels With {.Name = "Secrétariat", .Avatar = AppConfig.AvatarSecretariat, .Status = "0"}
        }
                SaveUsersToJson(usersList)
            End Try
            Return usersList
        End Function

        Public Shared Sub SaveUsersToJson(ByVal usersList As ObservableCollection(Of UserModels))
            Try
                Dim dossier As String = Path.GetDirectoryName(AppConfig.UsersFilePath)
                If Not Directory.Exists(dossier) Then
                    Directory.CreateDirectory(dossier)
                End If

                ' Sérialiser uniquement les propriétés souhaitées
                Dim simplifiedUsers = usersList.Select(Function(m) New With {
            .Name = m.Name,
            .AuxiliaireTilte = m.AuxiliaireTilte,
            .Status = m.Status,
            .Avatar = m.Avatar,
            .Initials = m.Initials,
            .UUID = m.UUID
        })

                Dim serializedUsers As String = JsonConvert.SerializeObject(simplifiedUsers, Formatting.Indented)
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
