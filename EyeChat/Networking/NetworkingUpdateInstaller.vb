Imports System.IO
Imports System.IO.Compression
Imports log4net

Public Class NetworkingUpdateInstaller
    Private Shared ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

    Public Sub InstallUpdate(updateFilePath As String)
        Try
            Dim extractPath As String = "update_temp"
            If File.Exists(updateFilePath) Then
                ZipFile.ExtractToDirectory(updateFilePath, extractPath)
                ' Logic to copy files to target location and restart app if needed
                logger.Info("Update installed successfully")
            End If
        Catch ex As Exception
            logger.Error($"Error installing update: {ex.Message}")
        End Try
    End Sub
End Class
