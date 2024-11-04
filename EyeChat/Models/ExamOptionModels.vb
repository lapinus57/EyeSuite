Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.IO
Imports EyeChat.Utilities
Imports log4net
Imports Newtonsoft.Json

Namespace Models
    Public Class ExamOptionModels
        Implements INotifyPropertyChanged
        Private Shared ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

        ' Propriétés de l'option d'examen
        Private _index As Integer
        Public Property index As Integer
            Get
                Return _index
            End Get
            Set(value As Integer)
                If _index <> value Then
                    _index = value
                    NotifyPropertyChanged("Index")
                End If
            End Set
        End Property

        Private _name As String
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(value As String)
                If _name <> value Then
                    _name = value
                    NotifyPropertyChanged("Name")
                End If
            End Set
        End Property

        Private _color As String
        Public Property Color As String
            Get
                Return _color
            End Get
            Set(value As String)
                If _color <> value Then
                    _color = value
                    NotifyPropertyChanged("Color")
                End If
            End Set
        End Property

        Private _CodeMSG As String
        Public Property CodeMSG As String
            Get
                Return _CodeMSG
            End Get
            Set(value As String)
                If _CodeMSG <> value Then
                    _CodeMSG = value
                    NotifyPropertyChanged("CodeMSG")
                End If
            End Set
        End Property

        Private _Annotation As String
        Public Property Annotation As String
            Get
                Return _Annotation
            End Get
            Set(value As String)
                If _Annotation <> value Then
                    _Annotation = value
                    NotifyPropertyChanged("Annotation")
                End If
            End Set
        End Property

        Private _Floor As String
        Public Property Floor As String
            Get
                Return _Floor
            End Get
            Set(value As String)
                If _Floor <> value Then
                    _Floor = value
                    NotifyPropertyChanged("Floor")
                End If
            End Set
        End Property


        ' Méthode pour initialiser la collection d'options d'examen
        Public Shared Sub initializeExamOptionsList()
            Try
                LoadExamOption()
            Catch ex As Exception
                logger.Error("Erreur lors de l'initialisation des options d'examen : ", ex)
            End Try
        End Sub

        ' Méthode pour charger les options d'examen depuis un fichier JSON
        Public Shared Sub LoadExamOptionFromJson()
            Try
                Dim filePath = AppConfig.ExamOptionsPath
                If File.Exists(filePath) Then
                    Dim json As String = File.ReadAllText(filePath)
                    ExamOptionsList = JsonConvert.DeserializeObject(Of ObservableCollection(Of ExamOptionModels))(json)
                Else
                    ' Crée une collection vide si le fichier n'existe pas
                    ExamOptionsList = New ObservableCollection(Of ExamOptionModels)()
                    SaveExamOptionsToJson()
                End If
            Catch ex As Exception
                logger.Error("Erreur lors du chargement des options d'examen : ", ex)
                ExamOptionsList = New ObservableCollection(Of ExamOptionModels)()
                SaveExamOptionsToJson()
            End Try
        End Sub

        ' Méthode pour sauvegarder les options d'examen dans un fichier JSON
        Public Shared Sub SaveExamOptionsToJson()
            Try
                Dim dossier As String = Path.GetDirectoryName(AppConfig.ExamOptionsPath)
                If Not Directory.Exists(dossier) Then
                    Directory.CreateDirectory(dossier)
                End If
                Dim optionsJson As String = JsonConvert.SerializeObject(ExamOptionsList, Formatting.Indented)
                File.WriteAllText(AppConfig.ExamOptionsPath, optionsJson)
            Catch ex As Exception
                logger.Error("Erreur lors de la sauvegarde des ExamOptions : ", ex)
            End Try
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub NotifyPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

    End Class
End Namespace

