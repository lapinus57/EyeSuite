Imports EyeChat.Utilities
Imports Newtonsoft.Json
Imports System.ComponentModel
Imports System.IO


Namespace Models
    Public Class UserSettingsModels
        Implements INotifyPropertyChanged

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

        Private _debugLevel As String = "ERROR"
        <JsonProperty("DebugLevel")>
        Public Property DebugLevel As String
            Get
                Return _debugLevel
            End Get
            Set(value As String)
                If _debugLevel <> value Then
                    _debugLevel = value
                    NotifyPropertyChanged(NameOf(DebugLevel))
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

        Private _f5Enabled As Boolean = False
        <JsonProperty("F5Enabled")>
        Public Property F5Enabled As Boolean
            Get
                Return _f5Enabled
            End Get
            Set(value As Boolean)
                If _f5Enabled <> value Then
                    _f5Enabled = value
                    NotifyPropertyChanged(NameOf(F5Enabled))
                End If
            End Set
        End Property

        Private _f5Page1 As String = ""
        <JsonProperty("F5Page1")>
        Public Property F5Page1 As String
            Get
                Return _f5Page1
            End Get
            Set(value As String)
                If _f5Page1 <> value Then
                    _f5Page1 = value
                    NotifyPropertyChanged(NameOf(F5Page1))
                End If
            End Set
        End Property

        Private _f5Text1 As String = ""
        <JsonProperty("F5Text1")>
        Public Property F5Text1 As String
            Get
                Return _f5Text1
            End Get
            Set(value As String)
                If _f5Text1 <> value Then
                    _f5Text1 = value
                    NotifyPropertyChanged(NameOf(F5Text1))
                End If
            End Set
        End Property

        Private _f5Page2 As String = ""
        <JsonProperty("F5Page2")>
        Public Property F5Page2 As String
            Get
                Return _f5Page2
            End Get
            Set(value As String)
                If _f5Page2 <> value Then
                    _f5Page2 = value
                    NotifyPropertyChanged(NameOf(F5Page2))
                End If
            End Set
        End Property

        Private _f5Text2 As String = ""
        <JsonProperty("F5Text2")>
        Public Property F5Text2 As String
            Get
                Return _f5Text2
            End Get
            Set(value As String)
                If _f5Text2 <> value Then
                    _f5Text2 = value
                    NotifyPropertyChanged(NameOf(F5Text2))
                End If
            End Set
        End Property

        Private _f5Page3 As String = ""
        <JsonProperty("F5Page3")>
        Public Property F5Page3 As String
            Get
                Return _f5Page3
            End Get
            Set(value As String)
                If _f5Page3 <> value Then
                    _f5Page3 = value
                    NotifyPropertyChanged(NameOf(F5Page3))
                End If
            End Set
        End Property

        Private _f5Text3 As String = ""
        <JsonProperty("F5Text3")>
        Public Property F5Text3 As String
            Get
                Return _f5Text3
            End Get
            Set(value As String)
                If _f5Text3 <> value Then
                    _f5Text3 = value
                    NotifyPropertyChanged(NameOf(F5Text3))
                End If
            End Set
        End Property

        Private _f5Page4 As String = ""
        <JsonProperty("F5Page4")>
        Public Property F5Page4 As String
            Get
                Return _f5Page4
            End Get
            Set(value As String)
                If _f5Page4 <> value Then
                    _f5Page4 = value
                    NotifyPropertyChanged(NameOf(F5Page4))
                End If
            End Set
        End Property

        Private _f5Text4 As String = ""
        <JsonProperty("F5Text4")>
        Public Property F5Text4 As String
            Get
                Return _f5Text4
            End Get
            Set(value As String)
                If _f5Text4 <> value Then
                    _f5Text4 = value
                    NotifyPropertyChanged(NameOf(F5Text4))
                End If
            End Set
        End Property

        Private _f5Page5 As String = ""
        <JsonProperty("F5Page5")>
        Public Property F5Page5 As String
            Get
                Return _f5Page5
            End Get
            Set(value As String)
                If _f5Page5 <> value Then
                    _f5Page5 = value
                    NotifyPropertyChanged(NameOf(F5Page5))
                End If
            End Set
        End Property

        Private _f5Text5 As String = ""
        <JsonProperty("F5Text5")>
        Public Property F5Text5 As String
            Get
                Return _f5Text5
            End Get
            Set(value As String)
                If _f5Text5 <> value Then
                    _f5Text5 = value
                    NotifyPropertyChanged(NameOf(F5Text5))
                End If
            End Set
        End Property

        Private _f6Enabled As Boolean = False
        <JsonProperty("F6Enabled")>
        Public Property F6Enabled As Boolean
            Get
                Return _f6Enabled
            End Get
            Set(value As Boolean)
                If _f6Enabled <> value Then
                    _f6Enabled = value
                    NotifyPropertyChanged(NameOf(F6Enabled))
                End If
            End Set
        End Property

        Private _f6Page1 As String = ""
        <JsonProperty("F6Page1")>
        Public Property F6Page1 As String
            Get
                Return _f6Page1
            End Get
            Set(value As String)
                If _f6Page1 <> value Then
                    _f6Page1 = value
                    NotifyPropertyChanged(NameOf(F6Page1))
                End If
            End Set
        End Property

        Private _f6Text1 As String = ""
        <JsonProperty("F6Text1")>
        Public Property F6Text1 As String
            Get
                Return _f6Text1
            End Get
            Set(value As String)
                If _f6Text1 <> value Then
                    _f6Text1 = value
                    NotifyPropertyChanged(NameOf(F6Text1))
                End If
            End Set
        End Property

        Private _f6Page2 As String = ""
        <JsonProperty("F6Page2")>
        Public Property F6Page2 As String
            Get
                Return _f6Page2
            End Get
            Set(value As String)
                If _f6Page2 <> value Then
                    _f6Page2 = value
                    NotifyPropertyChanged(NameOf(F6Page2))
                End If
            End Set
        End Property

        Private _f6Text2 As String = ""
        <JsonProperty("F6Text2")>
        Public Property F6Text2 As String
            Get
                Return _f6Text2
            End Get
            Set(value As String)
                If _f6Text2 <> value Then
                    _f6Text2 = value
                    NotifyPropertyChanged(NameOf(F6Text2))
                End If
            End Set
        End Property

        Private _f6Page3 As String = ""
        <JsonProperty("F6Page3")>
        Public Property F6Page3 As String
            Get
                Return _f6Page3
            End Get
            Set(value As String)
                If _f6Page3 <> value Then
                    _f6Page3 = value
                    NotifyPropertyChanged(NameOf(F6Page3))
                End If
            End Set
        End Property

        Private _f6Text3 As String = ""
        <JsonProperty("F6Text3")>
        Public Property F6Text3 As String
            Get
                Return _f6Text3
            End Get
            Set(value As String)
                If _f6Text3 <> value Then
                    _f6Text3 = value
                    NotifyPropertyChanged(NameOf(F6Text3))
                End If
            End Set
        End Property

        Private _f6Page4 As String = ""
        <JsonProperty("F6Page4")>
        Public Property F6Page4 As String
            Get
                Return _f6Page4
            End Get
            Set(value As String)
                If _f6Page4 <> value Then
                    _f6Page4 = value
                    NotifyPropertyChanged(NameOf(F6Page4))
                End If
            End Set
        End Property

        Private _f6Text4 As String = ""
        <JsonProperty("F6Text4")>
        Public Property F6Text4 As String
            Get
                Return _f6Text4
            End Get
            Set(value As String)
                If _f6Text4 <> value Then
                    _f6Text4 = value
                    NotifyPropertyChanged(NameOf(F6Text4))
                End If
            End Set
        End Property

        Private _f6Page5 As String = ""
        <JsonProperty("F6Page5")>
        Public Property F6Page5 As String
            Get
                Return _f6Page5
            End Get
            Set(value As String)
                If _f6Page5 <> value Then
                    _f6Page5 = value
                    NotifyPropertyChanged(NameOf(F6Page5))
                End If
            End Set
        End Property

        Private _f6Text5 As String = ""
        <JsonProperty("F6Text5")>
        Public Property F6Text5 As String
            Get
                Return _f6Text5
            End Get
            Set(value As String)
                If _f6Text5 <> value Then
                    _f6Text5 = value
                    NotifyPropertyChanged(NameOf(F6Text5))
                End If
            End Set
        End Property

        Private _f7Enabled As Boolean = False
        <JsonProperty("F7Enabled")>
        Public Property F7Enabled As Boolean
            Get
                Return _f7Enabled
            End Get
            Set(value As Boolean)
                If _f7Enabled <> value Then
                    _f7Enabled = value
                    NotifyPropertyChanged(NameOf(F7Enabled))
                End If
            End Set
        End Property

        Private _f7Page1 As String = ""
        <JsonProperty("F7Page1")>
        Public Property F7Page1 As String
            Get
                Return _f7Page1
            End Get
            Set(value As String)
                If _f7Page1 <> value Then
                    _f7Page1 = value
                    NotifyPropertyChanged(NameOf(F7Page1))
                End If
            End Set
        End Property

        Private _f7Text1 As String = ""
        <JsonProperty("F7Text1")>
        Public Property F7Text1 As String
            Get
                Return _f7Text1
            End Get
            Set(value As String)
                If _f7Text1 <> value Then
                    _f7Text1 = value
                    NotifyPropertyChanged(NameOf(F7Text1))
                End If
            End Set
        End Property

        Private _f7Page2 As String = ""
        <JsonProperty("F7Page2")>
        Public Property F7Page2 As String
            Get
                Return _f7Page2
            End Get
            Set(value As String)
                If _f7Page2 <> value Then
                    _f7Page2 = value
                    NotifyPropertyChanged(NameOf(F7Page2))
                End If
            End Set
        End Property

        Private _f7Text2 As String = ""
        <JsonProperty("F7Text2")>
        Public Property F7Text2 As String
            Get
                Return _f7Text2
            End Get
            Set(value As String)
                If _f7Text2 <> value Then
                    _f7Text2 = value
                    NotifyPropertyChanged(NameOf(F7Text2))
                End If
            End Set
        End Property

        Private _f7Page3 As String = ""
        <JsonProperty("F7Page3")>
        Public Property F7Page3 As String
            Get
                Return _f7Page3
            End Get
            Set(value As String)
                If _f7Page3 <> value Then
                    _f7Page3 = value
                    NotifyPropertyChanged(NameOf(F7Page3))
                End If
            End Set
        End Property

        Private _f7Text3 As String = ""
        <JsonProperty("F7Text3")>
        Public Property F7Text3 As String
            Get
                Return _f7Text3
            End Get
            Set(value As String)
                If _f7Text3 <> value Then
                    _f7Text3 = value
                    NotifyPropertyChanged(NameOf(F7Text3))
                End If
            End Set
        End Property

        Private _f7Page4 As String = ""
        <JsonProperty("F7Page4")>
        Public Property F7Page4 As String
            Get
                Return _f7Page4
            End Get
            Set(value As String)
                If _f7Page4 <> value Then
                    _f7Page4 = value
                    NotifyPropertyChanged(NameOf(F7Page4))
                End If
            End Set
        End Property

        Private _f7Text4 As String = ""
        <JsonProperty("F7Text4")>
        Public Property F7Text4 As String
            Get
                Return _f7Text4
            End Get
            Set(value As String)
                If _f7Text4 <> value Then
                    _f7Text4 = value
                    NotifyPropertyChanged(NameOf(F7Text4))
                End If
            End Set
        End Property

        Private _f7Page5 As String = ""
        <JsonProperty("F7Page5")>
        Public Property F7Page5 As String
            Get
                Return _f7Page5
            End Get
            Set(value As String)
                If _f7Page5 <> value Then
                    _f7Page5 = value
                    NotifyPropertyChanged(NameOf(F7Page5))
                End If
            End Set
        End Property

        Private _f7Text5 As String = ""
        <JsonProperty("F7Text5")>
        Public Property F7Text5 As String
            Get
                Return _f7Text5
            End Get
            Set(value As String)
                If _f7Text5 <> value Then
                    _f7Text5 = value
                    NotifyPropertyChanged(NameOf(F7Text5))
                End If
            End Set
        End Property

        Private _f8Enabled As Boolean = False
        <JsonProperty("F8Enabled")>
        Public Property F8Enabled As Boolean
            Get
                Return _f8Enabled
            End Get
            Set(value As Boolean)
                If _f8Enabled <> value Then
                    _f8Enabled = value
                    NotifyPropertyChanged(NameOf(F8Enabled))
                End If
            End Set
        End Property

        Private _f8Page1 As String = ""
        <JsonProperty("F8Page1")>
        Public Property F8Page1 As String
            Get
                Return _f8Page1
            End Get
            Set(value As String)
                If _f8Page1 <> value Then
                    _f8Page1 = value
                    NotifyPropertyChanged(NameOf(F8Page1))
                End If
            End Set
        End Property

        Private _f8Text1 As String = ""
        <JsonProperty("F8Text1")>
        Public Property F8Text1 As String
            Get
                Return _f8Text1
            End Get
            Set(value As String)
                If _f8Text1 <> value Then
                    _f8Text1 = value
                    NotifyPropertyChanged(NameOf(F8Text1))
                End If
            End Set
        End Property

        Private _f8Page2 As String = ""
        <JsonProperty("F8Page2")>
        Public Property F8Page2 As String
            Get
                Return _f8Page2
            End Get
            Set(value As String)
                If _f8Page2 <> value Then
                    _f8Page2 = value
                    NotifyPropertyChanged(NameOf(F8Page2))
                End If
            End Set
        End Property

        Private _f8Text2 As String = ""
        <JsonProperty("F8Text2")>
        Public Property F8Text2 As String
            Get
                Return _f8Text2
            End Get
            Set(value As String)
                If _f8Text2 <> value Then
                    _f8Text2 = value
                    NotifyPropertyChanged(NameOf(F8Text2))
                End If
            End Set
        End Property

        Private _f8Page3 As String = ""
        <JsonProperty("F8Page3")>
        Public Property F8Page3 As String
            Get
                Return _f8Page3
            End Get
            Set(value As String)
                If _f8Page3 <> value Then
                    _f8Page3 = value
                    NotifyPropertyChanged(NameOf(F8Page3))
                End If
            End Set
        End Property

        Private _f8Text3 As String = ""
        <JsonProperty("F8Text3")>
        Public Property F8Text3 As String
            Get
                Return _f8Text3
            End Get
            Set(value As String)
                If _f8Text3 <> value Then
                    _f8Text3 = value
                    NotifyPropertyChanged(NameOf(F8Text3))
                End If
            End Set
        End Property

        Private _f8Page4 As String = ""
        <JsonProperty("F8Page4")>
        Public Property F8Page4 As String
            Get
                Return _f8Page4
            End Get
            Set(value As String)
                If _f8Page4 <> value Then
                    _f8Page4 = value
                    NotifyPropertyChanged(NameOf(F8Page4))
                End If
            End Set
        End Property

        Private _f8Text4 As String = ""
        <JsonProperty("F8Text4")>
        Public Property F8Text4 As String
            Get
                Return _f8Text4
            End Get
            Set(value As String)
                If _f8Text4 <> value Then
                    _f8Text4 = value
                    NotifyPropertyChanged(NameOf(F8Text4))
                End If
            End Set
        End Property

        Private _f8Page5 As String = ""
        <JsonProperty("F8Page5")>
        Public Property F8Page5 As String
            Get
                Return _f8Page5
            End Get
            Set(value As String)
                If _f8Page5 <> value Then
                    _f8Page5 = value
                    NotifyPropertyChanged(NameOf(F8Page5))
                End If
            End Set
        End Property

        Private _f8Text5 As String = ""
        <JsonProperty("F8Text5")>
        Public Property F8Text5 As String
            Get
                Return _f8Text5
            End Get
            Set(value As String)
                If _f8Text5 <> value Then
                    _f8Text5 = value
                    NotifyPropertyChanged(NameOf(F8Text5))
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
                    _roomDisplay = value
                    NotifyPropertyChanged(NameOf(RoomDisplay))
                End If
            End Set
        End Property

        Private _roomDisplayStr As String = "True"
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
        End Sub

        ' Charger les paramètres pour un utilisateur spécifique
        Public Shared Function LoadUserSettingsFronJson(userName As String) As UserSettingsModels
            Dim filePath As String = Path.Combine(AppConfig.UserSettingsBasePath, $"{userName}_UserSettings.json")

            If Not File.Exists(filePath) Then
                ' Crée une instance par défaut si le fichier n'existe pas
                Dim settings As New UserSettingsModels() With {.UserName = userName}
                settings.SaveUserSettingsToJson(userName)
                Return settings
            Else
                Dim json = File.ReadAllText(filePath)
                Return JsonConvert.DeserializeObject(Of UserSettingsModels)(json)
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
    End Class
End Namespace
