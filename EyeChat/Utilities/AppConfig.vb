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

        Public Shared ReadOnly Property SpeedMessageFilePath As String
            Get
                Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config("Paths")("SpeedMessageData").ToString())
            End Get
        End Property

        Public Shared ReadOnly Property UsersFilePath As String
            Get
                Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config("Paths")("UsersData").ToString())
            End Get
        End Property
        Public Shared ReadOnly Property AvatarPathMarvin As String
            Get
                Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config("Paths")("AvatarMarvin").ToString())
            End Get
        End Property

        Public Shared ReadOnly Property AvatarAtous As String
            Get
                Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config("Paths")("AvatarAtous").ToString())
            End Get
        End Property

        Public Shared ReadOnly Property AvatarSecretariat As String
            Get
                Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config("Paths")("AvatarSecretariat").ToString())
            End Get
        End Property
        Public Shared ReadOnly Property AvatarPathJoy As String
            Get
                Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config("Paths")("AvatarJoy").ToString())
            End Get
        End Property

        Public Shared ReadOnly Property UserSettingsBasePath As String
            Get
                Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config("Paths")("UserSettingsBasePath").ToString())
            End Get
        End Property

        Public Shared ReadOnly Property PatientFilePath As String
            Get
                Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config("Paths")("PatientFilePath").ToString(), DateTime.Now.ToString("ddMMyyyy") & ".json")
            End Get
        End Property

        Public Shared ReadOnly Property messagesFilePath As String
            Get
                Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config("Paths")("MessageFilePah").ToString(), DateTime.Now.ToString("ddMMyyyy") & ".json")
            End Get
        End Property
    End Class
End Namespace

