Imports System.IO
Imports Newtonsoft.Json.Linq
Imports log4net

Namespace Utilities
    Public Class UIDManager
        Private Shared ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
        Public Shared Function GenerateUniqueId()
            logger.Debug("Génération de l'identifiant unique")
            Try
                Dim windowsUser As String = Environment.UserName
                Dim computerName As String = Environment.MachineName

                If String.IsNullOrEmpty(windowsUser) OrElse String.IsNullOrEmpty(computerName) Then
                    logger.Warn("Le nom d'utilisateur ou le nom du PC est vide.")
                    Return String.Empty
                End If

                Dim uniqueId As String = $"{windowsUser}_{computerName}"
                logger.Info($"UniqueId généré : {uniqueId}")

                Return uniqueId

            Catch ex As Exception
                logger.Error($"Erreur lors de la génération de l'identifiant unique : {ex.Message}")
                Return String.Empty
            End Try
        End Function
    End Class
End Namespace
