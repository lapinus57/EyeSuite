Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.IO
Imports EyeChat.Utilities
Imports log4net
Imports Newtonsoft.Json

Namespace Models
    Public Class PlanningModels
        Implements INotifyPropertyChanged

        ' Liste des jours disponibles (non partagée)
        Public ReadOnly Property AvailableDays As List(Of String)
            Get
                Return New List(Of String) From {
                "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi", "Dimanche"
            }
            End Get
        End Property


        ' Propriétés
        Private _Index As Integer
        Public Property Index As Integer
            Get
                Return _Index
            End Get
            Set(value As Integer)
                If _Index <> value Then
                    _Index = value
                    NotifyPropertyChanged("Index")
                End If
            End Set
        End Property

        Private _Day As String
        Public Property Day As String
            Get
                Return _Day
            End Get
            Set(value As String)
                If _Day <> value Then
                    _Day = value
                    NotifyPropertyChanged("Day")
                End If
            End Set
        End Property

        Private _DaySlots As ObservableCollection(Of TimeSlot)
        Public Property DaySlots As ObservableCollection(Of TimeSlot)
            Get
                Return _DaySlots
            End Get
            Set(value As ObservableCollection(Of TimeSlot))
                If _DaySlots IsNot value Then
                    _DaySlots = value
                    NotifyPropertyChanged("DaySlots")
                End If
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private Sub NotifyPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        ' Constructeur par défaut
        Public Sub New()
            _DaySlots = New ObservableCollection(Of TimeSlot)()
        End Sub

        ' Classe interne pour gérer les créneaux horaires
        Public Class TimeSlot
            Public Property StartTime As String ' Format HH:mm
            Public Property EndTime As String ' Format HH:mm
            Public Property User As String
        End Class

        ' Charger le Planning à partir d'un fichier JSON
        Public Shared Function LoadPlanningFromJson() As ObservableCollection(Of PlanningModels)
            Dim PlanningsList As ObservableCollection(Of PlanningModels) = Nothing
            Try
                Dim filePath = AppConfig.PlanningFilePath
                If File.Exists(filePath) Then
                    Dim json As String = File.ReadAllText(filePath)
                    PlanningsList = JsonConvert.DeserializeObject(Of ObservableCollection(Of PlanningModels))(json)
                Else
                    PlanningsList = New ObservableCollection(Of PlanningModels)()
                    SavePlanningToJson(PlanningsList)
                End If
            Catch ex As Exception
                ' Gérer les erreurs de chargement (par exemple, journaliser l'erreur)
                logger.Error($"Erreur lors du chargement du Planning : {ex.Message}")
                PlanningsList = New ObservableCollection(Of PlanningModels)()
                SavePlanningToJson(PlanningsList)
                Return PlanningsList
            End Try
            Return PlanningsList
        End Function

        ' Sauvegarder le Planning dans un fichier JSON
        Public Shared Sub SavePlanningToJson(ByVal plannings As ObservableCollection(Of PlanningModels))
            Try
                Dim dossier As String = Path.GetDirectoryName(AppConfig.PlanningFilePath)
                If Not Directory.Exists(dossier) Then
                    Directory.CreateDirectory(dossier)
                End If

                ' Convertir les options de planning en format JSON
                Dim planningJson As String = JsonConvert.SerializeObject(plannings.ToList(), Formatting.Indented)

                ' Écrire le JSON dans le fichier
                File.WriteAllText(AppConfig.PlanningFilePath, planningJson)
            Catch ex As Exception
                ' Gérer les erreurs de sauvegarde (par exemple, journaliser l'erreur)
                logger.Error($"Erreur lors de la sauvegarde du Planning : {ex.Message}")
            End Try
        End Sub

        ' Méthode pour trouver l'utilisateur en fonction de l'heure actuelle
        Public Function GetUserForCurrentTime() As String
            Dim currentTime As String = DateTime.Now.ToString("HH:mm")
            For Each slot In DaySlots
                If IsTimeInRange(currentTime, slot.StartTime, slot.EndTime) Then
                    Return slot.User
                End If
            Next
            Return "Aucun utilisateur défini pour cette heure"
        End Function

        ' Vérifie si une heure est comprise dans une plage
        Private Function IsTimeInRange(currentTime As String, startTime As String, endTime As String) As Boolean
            Dim current = DateTime.Parse(currentTime)
            Dim start = DateTime.Parse(startTime)
            Dim [end] = DateTime.Parse(endTime)
            Return current >= start AndAlso current < [end]
        End Function

        ' Méthode pour ajouter un créneau
        Public Sub AddTimeSlot(startTime As String, endTime As String, user As String)
            DaySlots.Add(New TimeSlot() With {
                .StartTime = startTime,
                .EndTime = endTime,
                .User = user
            })
            NotifyPropertyChanged("DaySlots")
        End Sub
    End Class
End Namespace
