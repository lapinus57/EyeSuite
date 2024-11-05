Imports System.ComponentModel
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Namespace Utilities
    Public Class AppConfig
        Private Shared _config As JObject
        Private Shared configFilePath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Config", "appsettings.json")

        ' Chargement du fichier de configuration
        Public Shared Sub LoadConfig()
            If File.Exists(configFilePath) Then
                Dim json As String = File.ReadAllText(configFilePath)
                _config = JObject.Parse(json)
            Else
                Throw New FileNotFoundException("Le fichier de configuration appsettings.json est introuvable.")
            End If
        End Sub

        Public Shared Function LoadConfigFronFile() As JObject
            If File.Exists(configFilePath) Then
                Try
                    Dim json As String = File.ReadAllText(configFilePath)
                    Return JObject.Parse(json)
                Catch ex As Exception
                    ' logger.Error should also be shared or adapted accordingly.
                    ' Assuming logger is already shared.
                    logger.Error($"Erreur lors du chargement du fichier de configuration : {ex.Message}")
                End Try
            Else
                logger.Warn("Le fichier de configuration n'existe pas.")
            End If
            Return New JObject()
        End Function

        ' Méthode pour sauvegarder la configuration mise à jour
        Private Shared Sub SaveConfig()
            Try
                Dim updatedConfig As String = JsonConvert.SerializeObject(_config, Formatting.Indented)
                File.WriteAllText(configFilePath, updatedConfig)
            Catch ex As Exception
                ' Assurez-vous que `logger` est déjà défini et est `Shared` ou accessible ici
                logger.Error($"Erreur lors de la sauvegarde du fichier de configuration : {ex.Message}")
            End Try
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

        Public Shared ReadOnly Property PlanningFilePath As String
            Get
                Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config("Paths")("PlanningData").ToString())
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

        Public Shared ReadOnly Property UniqueId As String
            Get
                Return _config("AppSettings")("UniqueId").ToString()
            End Get
        End Property

        Public Shared Property ComputerName As String
            Get
                Return _config("AppSettings")("ComputerName").ToString()
            End Get
            Set(value As String)
                _config("AppSettings")("ComputerName") = value
                SaveConfig()
            End Set
        End Property

        Public Shared Property WindowsUser As String
            Get
                Return _config("AppSettings")("WindowsUser").ToString()
            End Get
            Set(value As String)
                _config("AppSettings")("WindowsUser") = value
                SaveConfig()
            End Set
        End Property

        Public Shared Property LogLevel As String
            Get
                Return _config("AppSettings")("LogLevel").ToString()
            End Get
            Set(value As String)
                _config("AppSettings")("LogLevel") = value
                SaveConfig()
            End Set
        End Property

        Public Shared Property PlanningMode As Boolean
            Get
                Return _config("Planning")("PlanningEnabled")
            End Get
            Set(value As Boolean)
                _config("Planning")("PlanningEnabled") = value
                SaveConfig()
            End Set
        End Property
    End Class
End Namespace
