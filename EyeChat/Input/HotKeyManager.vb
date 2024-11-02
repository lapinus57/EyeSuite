Imports System.Runtime.InteropServices
Imports System.Windows.Interop
Imports EyeChat.EyeChat
Imports log4net

Namespace Input
    Public Class HotKeyManager
        Private Shared ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

        Private _source As HwndSource
        Private Const HOTKEY_ID As Integer = 9000
        Private Const HOTKEY_ID10 As Integer = 9010

        Const VK_E As UInteger = &H45
        Const MOD_CTRL As UInteger = &H2
        Const MOD_SHIFT As UInteger = &H4
        Const VK_F5 As UInteger = &H74
        Const VK_F6 As UInteger = &H75
        Const VK_F7 As UInteger = &H76
        Const VK_F8 As UInteger = &H77
        Const VK_F9 As UInteger = &H78
        Const VK_F10 As UInteger = &H79
        Const VK_F11 As UInteger = &H7A
        Const VK_F12 As UInteger = &H7B

        Const KEYEVENTF_KEYUP As UInteger = &H2
        Const VK_CONTROL As Integer = &H11
        Const VK_V As Integer = &H56

        <DllImport("User32.dll")>
        Private Shared Function RegisterHotKey(<[In]> hWnd As IntPtr, <[In]> id As Integer, <[In]> fsModifiers As UInteger, <[In]> vk As UInteger) As Boolean
        End Function

        <DllImport("User32.dll")>
        Private Shared Function UnregisterHotKey(<[In]> hWnd As IntPtr, <[In]> id As Integer) As Boolean
        End Function

        <DllImport("user32.dll", SetLastError:=True)>
        Private Shared Sub keybd_event(bVk As Byte, bScan As Byte, dwFlags As UInteger, dwExtraInfo As UInteger)
        End Sub

        Public Sub New(source As HwndSource)
            _source = source
            _source.AddHook(AddressOf HwndHook)
        End Sub

        Public Sub RegisterHotKeys(helper As WindowInteropHelper)
            logger.Info("Enregistrement des raccourcis clavier")
            UnregisterHotKeys(helper)

            RegisterHotKey(helper.Handle, HOTKEY_ID, MOD_CTRL, VK_E)

            ' Raccourcis clavier pour les actions fenêtre patient
            If GlobalConfig.UserSettingsList.CtrlF9Enabled Then RegisterHotKey(helper.Handle, HOTKEY_ID + 1, MOD_CTRL, VK_F9)
            If GlobalConfig.UserSettingsList.CtrlF10Enabled Then RegisterHotKey(helper.Handle, HOTKEY_ID + 2, MOD_CTRL, VK_F10)
            If GlobalConfig.UserSettingsList.CtrlF11Enabled Then RegisterHotKey(helper.Handle, HOTKEY_ID + 3, MOD_CTRL, VK_F11)
            If GlobalConfig.UserSettingsList.ShiftF9Enabled Then RegisterHotKey(helper.Handle, HOTKEY_ID + 4, MOD_SHIFT, VK_F9)
            If GlobalConfig.UserSettingsList.ShiftF10Enabled Then RegisterHotKey(helper.Handle, HOTKEY_ID + 5, MOD_SHIFT, VK_F10)
            If GlobalConfig.UserSettingsList.ShiftF11Enabled Then RegisterHotKey(helper.Handle, HOTKEY_ID + 6, MOD_SHIFT, VK_F11)

            ' Raccourcis clavier pour l'actions post-it
            If GlobalConfig.UserSettingsList.CtrlF12Enabled Then RegisterHotKey(helper.Handle, HOTKEY_ID + 7, MOD_CTRL, VK_F12)

            ' Raccourci clavier pour coller le texte d'en-tête
            If GlobalConfig.UserSettingsList.F5Enabled Then RegisterHotKey(helper.Handle, HOTKEY_ID + 8, 0, VK_F5)
            If GlobalConfig.UserSettingsList.F6Enabled Then RegisterHotKey(helper.Handle, HOTKEY_ID + 9, 0, VK_F6)
            If GlobalConfig.UserSettingsList.F7Enabled Then RegisterHotKey(helper.Handle, HOTKEY_ID10, 0, VK_F7)
            If GlobalConfig.UserSettingsList.F8Enabled Then RegisterHotKey(helper.Handle, HOTKEY_ID10 + 1, 0, VK_F8)
        End Sub

        Public Sub UnregisterHotKeys(helper As WindowInteropHelper)
            logger.Info("Désenregistrement des raccourcis clavier")
            For i As Integer = HOTKEY_ID To HOTKEY_ID10 + 1
                UnregisterHotKey(helper.Handle, i)
            Next
        End Sub

        Private Function HwndHook(hwnd As IntPtr, msg As Integer, wParam As IntPtr, lParam As IntPtr, ByRef handled As Boolean) As IntPtr
            Const WM_HOTKEY As Integer = &H312
            If msg = WM_HOTKEY Then
                Select Case wParam.ToInt32()
                    Case HOTKEY_ID : OnHotKeyPressed("Ctrl + E", Nothing)
                    Case HOTKEY_ID + 1 : OnHotKeyPressed("Ctrl + F9", GlobalConfig.UserSettingsList.CtrlF9)
                    Case HOTKEY_ID + 2 : OnHotKeyPressed("Ctrl + F10", GlobalConfig.UserSettingsList.CtrlF10)
                    Case HOTKEY_ID + 3 : OnHotKeyPressed("Ctrl + F11", GlobalConfig.UserSettingsList.CtrlF11)
                    Case HOTKEY_ID + 4 : OnHotKeyPressed("Shift + F9", GlobalConfig.UserSettingsList.ShiftF9)
                    Case HOTKEY_ID + 5 : OnHotKeyPressed("Shift + F10", GlobalConfig.UserSettingsList.ShiftF10)
                    Case HOTKEY_ID + 6 : OnHotKeyPressed("Shift + F11", GlobalConfig.UserSettingsList.ShiftF11)
                    Case HOTKEY_ID + 7 : OnHotKeyPressed("Ctrl + F12", Nothing)
                    Case HOTKEY_ID + 8 : OnHotKeyPressed("F5", Nothing)
                    Case HOTKEY_ID + 9 : OnHotKeyPressed("F6", Nothing)
                    Case HOTKEY_ID10 : OnHotKeyPressed("F7", Nothing)
                    Case HOTKEY_ID10 + 1 : OnHotKeyPressed("F8", Nothing)
                End Select
            End If
            Return IntPtr.Zero
        End Function

        Private Sub OnHotKeyPressed(actionName As String, actionParam As String)
            Try
                Dim mainWindow As MainWindow = CType(Application.Current.MainWindow, MainWindow)

                logger.Debug($"Exécution du raccourci {actionName}")

                mainWindow.WindowState = WindowState.Normal
                mainWindow.Topmost = True
                mainWindow.Topmost = False
                mainWindow.Focus()

                If Not String.IsNullOrEmpty(actionParam) Then
                    ''mainWindow.OpenPatientDialogue("ODG", actionParam, "RDC")
                    ''mainWindow.EnumWindows(AddressOf mainWindow.EnumWindowCallBack, IntPtr.Zero)
                End If
            Catch ex As Exception
                logger.Error($"Erreur lors de l'exécution du raccourci {actionName} : {ex.Message}")
            End Try
        End Sub

        Public Shared Sub PasteText()
            keybd_event(VK_CONTROL, 0, 0, 0)
            keybd_event(VK_V, 0, 0, 0)
            keybd_event(VK_V, 0, KEYEVENTF_KEYUP, 0)
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0)
            keybd_event(VK_F12, 0, 0, 0)
            keybd_event(VK_F12, 0, KEYEVENTF_KEYUP, 0)
        End Sub
    End Class
End Namespace

