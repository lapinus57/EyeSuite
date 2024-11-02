Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Namespace Utilities
    Public Class AppConfig
        Private Shared _config As JObject

        ' Chargement du fichier de configuration
        Public Shared Sub LoadConfig()
            Dim configFilePath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Config", "appsettings.json")
            If File.Exists(configFilePath) Then
                Dim json As String = File.ReadAllText(configFilePath)
                _config = JObject.Parse(json)
            Else
                Throw New FileNotFoundException("Le fichier de configuration appsettings.json est introuvable.")
            End If
        End Sub

        ' Propriétés pour accéder aux chemins
        Public Shared ReadOnly Property ComputerListPath As String
            Get
                Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config("Paths")("ComputerListData").ToString())
            End Get
        End Property

        Public Shared ReadOnly Property ExamOptionsPath As String
            Get
                Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config("Paths")("ExamOptionsData").ToString())
            End Get
        End Property

        Public Shared ReadOnly Property EggPhrasesPath As String
            Get
                Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config("Paths")("EggPhrasesData").ToString())
            End Get
        End Property
    End Class
End Namespace

