Imports EyeChat.Utilities
Imports Newtonsoft.Json
Imports NuGet
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.IO


Namespace Models
    Public Class UserSettingsModels
        Implements INotifyPropertyChanged

        Public Property ShortcutKeys As Dictionary(Of String, ShortcutKeySVModel) = New Dictionary(Of String, ShortcutKeySVModel)()

        Private _userName As String
        <JsonProperty("UserName")>
        Public Property UserName As String
            Get
                Return _userName
            End Get
            Set(value As String)
                If _userName <> value Then
                    _userName = value
                    NotifyPropertyChanged(NameOf(UserName))
                End If
            End Set
        End Property

        Private _userAvatar As String
        <JsonProperty("UserAvatar")>
        Public Property UserAvatar As String
            Get
                Return _userAvatar
            End Get
            Set(value As String)
                If _userAvatar <> value Then
                    _userAvatar = value
                    NotifyPropertyChanged(NameOf(UserAvatar))
                End If
            End Set
        End Property

        Private _appTheme As String = "Sombre"
        <JsonProperty("AppTheme")>
        Public Property AppTheme As String
            Get
                Return _appTheme
            End Get
            Set(value As String)
                If _appTheme <> value Then
                    _appTheme = value
                    NotifyPropertyChanged(NameOf(AppTheme))
                End If
            End Set
        End Property

        Private _appColor As System.Drawing.Color = System.Drawing.Color.Blue
        <JsonProperty("AppColor")>
        Public Property AppColor As System.Drawing.Color
            Get
                Return _appColor
            End Get
            Set(value As System.Drawing.Color)
                If _appColor <> value Then
                    _appColor = value
                    NotifyPropertyChanged(NameOf(AppColor))
                End If
            End Set
        End Property

        Private _appColorHex As String
        <JsonProperty("AppColorHex")>
        Public Property AppColorHex As String
            Get
                Return _appColorHex
            End Get
            Set(value As String)
                If _appColorHex <> value Then
                    _appColorHex = value
                    NotifyPropertyChanged(NameOf(AppColorHex))
                End If
            End Set
        End Property

        Private _appColorString As String = "#FF0000FF"
        <JsonProperty("AppColorString")>
        Public Property AppColorString As String
            Get
                Return _appColorString
            End Get
            Set(value As String)
                If _appColorString <> value Then
                    _appColorString = value
                    NotifyPropertyChanged(NameOf(AppColorString))
                End If
            End Set
        End Property

        Private _appSizeDisplay As Double = 16.0
        <JsonProperty("AppSizeDisplay")>
        Public Property AppSizeDisplay As Double
            Get
                Return _appSizeDisplay
            End Get
            Set(value As Double)
                If _appSizeDisplay <> value Then
                    _appSizeDisplay = value
                    NotifyPropertyChanged(NameOf(AppSizeDisplay))
                End If
            End Set
        End Property

        Private _ctrlF9 As String = ""
        <JsonProperty("CtrlF9")>
        Public Property CtrlF9 As String
            Get
                Return _ctrlF9
            End Get
            Set(value As String)
                If _ctrlF9 <> value Then
                    _ctrlF9 = value
                    NotifyPropertyChanged(NameOf(CtrlF9))
                End If
            End Set
        End Property

        Private _ctrlF9Enabled As Boolean = False
        <JsonProperty("CtrlF9Enabled")>
        Public Property CtrlF9Enabled As Boolean
            Get
                Return _ctrlF9Enabled
            End Get
            Set(value As Boolean)
                If _ctrlF9Enabled <> value Then
                    _ctrlF9Enabled = value
                    NotifyPropertyChanged(NameOf(CtrlF9Enabled))
                End If
            End Set
        End Property

        Private _ctrlF10 As String = ""
        <JsonProperty("CtrlF10")>
        Public Property CtrlF10 As String
            Get
                Return _ctrlF10
            End Get
            Set(value As String)
                If _ctrlF10 <> value Then
                    _ctrlF10 = value
                    NotifyPropertyChanged(NameOf(CtrlF10))
                End If
            End Set
        End Property

        Private _ctrlF10Enabled As Boolean = False
        <JsonProperty("CtrlF10Enabled")>
        Public Property CtrlF10Enabled As Boolean
            Get
                Return _ctrlF10Enabled
            End Get
            Set(value As Boolean)
                If _ctrlF10Enabled <> value Then
                    _ctrlF10Enabled = value
                    NotifyPropertyChanged(NameOf(CtrlF10Enabled))
                End If
            End Set
        End Property

        Private _ctrlF11 As String = ""
        <JsonProperty("CtrlF11")>
        Public Property CtrlF11 As String
            Get
                Return _ctrlF11
            End Get
            Set(value As String)
                If _ctrlF11 <> value Then
                    _ctrlF11 = value
                    NotifyPropertyChanged(NameOf(CtrlF11))
                End If
            End Set
        End Property

        Private _ctrlF11Enabled As Boolean = False
        <JsonProperty("CtrlF11Enabled")>
        Public Property CtrlF11Enabled As Boolean
            Get
                Return _ctrlF11Enabled
            End Get
            Set(value As Boolean)
                If _ctrlF11Enabled <> value Then
                    _ctrlF11Enabled = value
                    NotifyPropertyChanged(NameOf(CtrlF11Enabled))
                End If
            End Set
        End Property

        Private _ctrlF12 As Boolean = False
        <JsonProperty("CtrlF12Enabled")>
        Public Property CtrlF12Enabled As Boolean
            Get
                Return _ctrlF12
            End Get
            Set(value As Boolean)
                If _ctrlF12 <> value Then
                    _ctrlF12 = value
                    NotifyPropertyChanged(NameOf(CtrlF12Enabled))
                End If
            End Set
        End Property

        Private _shiftF9 As String = ""
        <JsonProperty("ShiftF9")>
        Public Property ShiftF9 As String
            Get
                Return _shiftF9
            End Get
            Set(value As String)
                If _shiftF9 <> value Then
                    _shiftF9 = value
                    NotifyPropertyChanged(NameOf(ShiftF9))
                End If
            End Set
        End Property

        Private _shiftF9Enabled As Boolean = False
        <JsonProperty("ShiftF9Enabled")>
        Public Property ShiftF9Enabled As Boolean
            Get
                Return _shiftF9Enabled
            End Get
            Set(value As Boolean)
                If _shiftF9Enabled <> value Then
                    _shiftF9Enabled = value
                    NotifyPropertyChanged(NameOf(ShiftF9Enabled))
                End If
            End Set
        End Property

        Private _shiftF10 As String = ""
        <JsonProperty("ShiftF10")>
        Public Property ShiftF10 As String
            Get
                Return _shiftF10
            End Get
            Set(value As String)
                If _shiftF10 <> value Then
                    _shiftF10 = value
                    NotifyPropertyChanged(NameOf(ShiftF10))
                End If
            End Set
        End Property

        Private _shiftF10Enabled As Boolean = False
        <JsonProperty("ShiftF10Enabled")>
        Public Property ShiftF10Enabled As Boolean
            Get
                Return _shiftF10Enabled
            End Get
            Set(value As Boolean)
                If _shiftF10Enabled <> value Then
                    _shiftF10Enabled = value
                    NotifyPropertyChanged(NameOf(ShiftF10Enabled))
                End If
            End Set
        End Property

        Private _shiftF11 As String = ""
        <JsonProperty("ShiftF11")>
        Public Property ShiftF11 As String
            Get
                Return _shiftF11
            End Get
            Set(value As String)
                If _shiftF11 <> value Then
                    _shiftF11 = value
                    NotifyPropertyChanged(NameOf(ShiftF11))
                End If
            End Set
        End Property

        Private _shiftF11Enabled As Boolean = False
        <JsonProperty("ShiftF11Enabled")>
        Public Property ShiftF11Enabled As Boolean
            Get
                Return _shiftF11Enabled
            End Get
            Set(value As Boolean)
                If _shiftF11Enabled <> value Then
                    _shiftF11Enabled = value
                    NotifyPropertyChanged(NameOf(ShiftF11Enabled))
                End If
            End Set
        End Property

        Private _secretaryMode As Boolean = False
        <JsonProperty("SecretaryMode")>
        Public Property SecretaryMode As Boolean
            Get
                Return _secretaryMode
            End Get
            Set(value As Boolean)
                If _secretaryMode <> value Then
                    _secretaryMode = value
                    NotifyPropertyChanged(NameOf(SecretaryMode))
                End If
            End Set
        End Property

        Private _doctorMode As Boolean = False
        <JsonProperty("DoctorMode")>
        Public Property DoctorMode As Boolean
            Get
                Return _doctorMode
            End Get
            Set(value As Boolean)
                If _doctorMode <> value Then
                    _doctorMode = value
                    NotifyPropertyChanged(NameOf(DoctorMode))
                End If
            End Set
        End Property

        Private _orthoMode As Boolean = False
        <JsonProperty("OrthoMode")>
        Public Property OrthoMode As Boolean
            Get
                Return _orthoMode
            End Get
            Set(value As Boolean)
                If _orthoMode <> value Then
                    _orthoMode = value
                    NotifyPropertyChanged(NameOf(OrthoMode))
                End If
            End Set
        End Property

        Private _advanvedMode As Boolean = False
        <JsonProperty("AdvanvedMode")>
        Public Property AdvanvedMode As Boolean
            Get
                Return _advanvedMode
            End Get
            Set(value As Boolean)
                If _advanvedMode <> value Then
                    _advanvedMode = value
                    NotifyPropertyChanged(NameOf(AdvanvedMode))
                End If
            End Set
        End Property

        Private _adminMode As Boolean = False
        <JsonProperty("AdminMode")>
        Public Property AdminMode As Boolean
            Get
                Return _adminMode
            End Get
            Set(value As Boolean)
                If _adminMode <> value Then
                    _adminMode = value
                    NotifyPropertyChanged(NameOf(AdminMode))
                End If
            End Set
        End Property
        Private _nfcMode As Boolean = False
        <JsonProperty("NFCMode")>
        Public Property NFCMode As Boolean
            Get
                Return _nfcMode
            End Get
            Set(value As Boolean)
                If _nfcMode <> value Then
                    _nfcMode = value
                    NotifyPropertyChanged(NameOf(NFCMode))
                End If
            End Set
        End Property

        Private _roomDisplay As Boolean = True
        <JsonProperty("RoomDisplay")>
        Public Property RoomDisplay As Boolean
            Get
                Return _roomDisplay
            End Get
            Set(value As Boolean)
                If _roomDisplay <> value Then
                    If value = True Then
                        RoomDisplayStr = "Visible"
                    Else
                        RoomDisplayStr = "Collapsed"
                    End If
                    _roomDisplay = value
                    NotifyPropertyChanged(NameOf(RoomDisplay))
                End If
            End Set
        End Property
        Private _roomDisplayStr As String = "Visible"
        <JsonProperty("RoomDisplayStr")>
        Public Property RoomDisplayStr As String
            Get
                Return _roomDisplayStr
            End Get
            Set(value As String)
                If _roomDisplayStr <> value Then
                    _roomDisplayStr = value
                    NotifyPropertyChanged(NameOf(RoomDisplayStr))
                End If
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub NotifyPropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
            SaveUserSettingsToJson(UserName)
        End Sub

        Public Shared Function LoadUserSettingsFronJson(userName As String) As UserSettingsModels
            Dim filePath As String = Path.Combine(AppConfig.UserSettingsBasePath, $"{userName}_UserSettings.json")

            If Not File.Exists(filePath) Then
                ' Crée une instance par défaut si le fichier n'existe pas
                Dim settings As New UserSettingsModels() With {.UserName = userName}
                settings.InitializeDefaultShortcuts()
                settings.SaveUserSettingsToJson(userName)
                Return settings
            Else
                Dim json = File.ReadAllText(filePath)
                Dim settings = JsonConvert.DeserializeObject(Of UserSettingsModels)(json)

                ' Réinitialise les raccourcis avec la référence parent
                Dim keys = settings.ShortcutKeys.Keys.ToList()

                For Each key In keys
                    settings.ShortcutKeys(key) = New ShortcutKeySVModel(settings) With {
        .Enabled = settings.ShortcutKeys(key).Enabled,
        .Pages = New ObservableCollection(Of String)(settings.ShortcutKeys(key).Pages),
        .Texts = New ObservableCollection(Of String)(settings.ShortcutKeys(key).Texts)
    }
                Next

                settings.InitializeDefaultShortcutsIfMissing()
                Return settings
            End If
        End Function


        ' Sauvegarder les paramètres dans un fichier spécifique à l'utilisateur
        Public Sub SaveUserSettingsToJson(userName As String)
            Dim filePath As String = Path.Combine(AppConfig.UserSettingsBasePath, $"{userName}_UserSettings.json")

            If Not Directory.Exists(AppConfig.UserSettingsBasePath) Then
                Directory.CreateDirectory(AppConfig.UserSettingsBasePath)
            End If

            Dim json = JsonConvert.SerializeObject(Me, Formatting.Indented)
            File.WriteAllText(filePath, json)
        End Sub
        Private Function CreateDefaultShortcut() As ShortcutKeySVModel
            Return New ShortcutKeySVModel(Me) With {
        .Enabled = False,
        .Pages = New ObservableCollection(Of String)(Enumerable.Repeat("", 5)),
        .Texts = New ObservableCollection(Of String)(Enumerable.Repeat("", 5))
    }
        End Function

        Public Sub InitializeDefaultShortcuts()
            ShortcutKeys = New Dictionary(Of String, ShortcutKeySVModel) From {
        {"F5", CreateDefaultShortcut()},
        {"F6", CreateDefaultShortcut()},
        {"F7", CreateDefaultShortcut()},
        {"F8", CreateDefaultShortcut()}
    }
        End Sub



        Private Sub InitializeDefaultShortcutsIfMissing()
            Dim keys As String() = {"F5", "F6", "F7", "F8"}
            For Each key In keys
                If Not ShortcutKeys.ContainsKey(key) Then
                    ShortcutKeys(key) = New ShortcutKeySVModel(Me) With {
                        .Enabled = False,
                        .Pages = New ObservableCollection(Of String)(Enumerable.Repeat("", 5)),
                        .Texts = New ObservableCollection(Of String)(Enumerable.Repeat("", 5))
                    }
                End If
            Next
        End Sub

    End Class
End Namespace
