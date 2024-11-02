Imports log4net

Namespace Utilities
    Public Class UIDManager
        Private Shared ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

        Public Shared Function GenerateUniqueId() As String
            ' Génération d'un identifiant unique basé sur le nom d'utilisateur de Windows et le nom du PC
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

        Public Shared Function GetUniqueIdHashCode() As Integer
            ' Génération d'un entier unique basé sur UniqueId
            logger.Debug("Génération du HashCode de l'identifiant unique")
            Try
                Dim uniqueId As String = My.Settings.UniqueId
                If String.IsNullOrEmpty(uniqueId) Then
                    logger.Warn("UniqueId dans les paramètres est vide.")
                    Return 0
                End If

                Dim hashCode As Integer = uniqueId.GetHashCode()
                logger.Info($"HashCode de UniqueId : {hashCode}")
                Return hashCode
            Catch ex As Exception
                logger.Error($"Erreur lors de la génération du HashCode de l'identifiant unique : {ex.Message}")
                Return 0
            End Try
        End Function
    End Class
End Namespace

