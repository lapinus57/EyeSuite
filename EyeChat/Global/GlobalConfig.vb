Imports System.Collections.ObjectModel
Imports EyeChat.Models

Module GlobalConfig


    ' Informations sur le dépôt GitHub
    Public Const RepoOwner As String = "lapinus57"
    Public Const RepoName As String = "EyeChat"

    ' Déclare la collection de phrases d'oeuf en tant que propriété statique partagée
    Public EggPhrasesList As EggPhrasesModels

    ' Collection d'ordinateurs partagée
    Public Property ComputersList As ObservableCollection(Of ComputerModel)


    ' Déclare la collection d'options d'examen en tant que propriété statique partagée
    Public Property ExamOptionsList As New ObservableCollection(Of ExamOptionModels)()




End Module
