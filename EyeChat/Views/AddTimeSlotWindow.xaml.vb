Imports EyeChat.Models
Imports MahApps.Metro.Controls
Imports System.Collections.ObjectModel
Imports System.Windows

Namespace Views
    Public Class AddTimeSlotWindow
        Inherits MetroWindow

        Public Property SelectedDay As String
        Public Property StartTime As DateTime?
        Public Property EndTime As DateTime?
        Public Property SelectedUser As String

        ' Le planning actuel passé pour vérifier les conflits
        Private ReadOnly CurrentPlanning As ObservableCollection(Of PlanningModels)

        Public Sub New(currentPlanning As ObservableCollection(Of PlanningModels))
            InitializeComponent()
            DataContext = Me
            currentPlanning = currentPlanning
        End Sub

        Private Sub InitializeComponent()
            Throw New NotImplementedException()
        End Sub

        Private Sub AddButton_Click(sender As Object, e As RoutedEventArgs)
            ' Vérifier que tous les champs sont remplis
            If String.IsNullOrEmpty(SelectedDay) OrElse Not StartTime.HasValue OrElse Not EndTime.HasValue OrElse String.IsNullOrEmpty(SelectedUser) Then
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error)
                Return
            End If

            ' Vérifier que l'heure de début est avant l'heure de fin
            If StartTime.Value >= EndTime.Value Then
                MessageBox.Show("L'heure de début doit être avant l'heure de fin.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error)
                Return
            End If

            ' Vérifier les conflits avec les créneaux existants
            For Each planning In CurrentPlanning
                If planning.Day = SelectedDay Then
                    For Each slot In planning.DaySlots
                        Dim existingStartTime = DateTime.Parse(slot.StartTime)
                        Dim existingEndTime = DateTime.Parse(slot.EndTime)

                        If (StartTime.Value < existingEndTime) AndAlso (EndTime.Value > existingStartTime) Then
                            MessageBox.Show("Le créneau horaire est en conflit avec un créneau existant.", "Conflit de créneau", MessageBoxButton.OK, MessageBoxImage.Warning)
                            Return
                        End If
                    Next
                End If
            Next

            ' Ajouter le créneau au planning
            Dim newSlot As New PlanningModels.TimeSlot() With {
                .StartTime = StartTime.Value.ToString("HH:mm"),
                .EndTime = EndTime.Value.ToString("HH:mm"),
                .User = SelectedUser
            }

            ' Rechercher le jour et ajouter le créneau
            Dim planningForDay = CurrentPlanning.FirstOrDefault(Function(p) p.Day = SelectedDay)
            If planningForDay Is Nothing Then
                planningForDay = New PlanningModels() With {.Day = SelectedDay}
                CurrentPlanning.Add(planningForDay)
            End If
            planningForDay.DaySlots.Add(newSlot)

            ' Fermer la fenêtre après l'ajout
            Me.DialogResult = True
            Me.Close()
        End Sub
    End Class
End Namespace
