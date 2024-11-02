Imports System.Net.Sockets
Imports System.Text
Imports log4net

Namespace Networking
    Public Class SendManager
        Private Shared ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)


        ' Initialisation du client d'envoi
        Public Shared Sub InitializeSender()
            Try
                sendingClient = New UdpClient With {
                .EnableBroadcast = False
            }
                sendingClient.Connect(MulticastAddress, Port)
                logger.Debug("Client d'envoi UDP initialisé avec succès")
            Catch ex As Exception
                logger.Error("Erreur lors de l'initialisation du client d'envoi UDP : " & ex.Message)
            End Try
        End Sub

        ' Nettoyage du client d'envoi pour libérer les ressources
        Public Shared Sub CloseSender()
            If sendingClient IsNot Nothing Then
                sendingClient.Close()
                sendingClient = Nothing
                logger.Debug("Client d'envoi UDP fermé avec succès")
            End If
        End Sub

        ' Fonction pour envoyer un message complet
        Public Shared Sub SendMessage(message As String)
            logger.Debug("Envoi d'un message ")
            Try
                If Not String.IsNullOrWhiteSpace(message) AndAlso sendingClient IsNot Nothing Then
                    Dim dataMessage As Byte() = Encoding.UTF8.GetBytes(message)
                    sendingClient.Send(dataMessage, dataMessage.Length)
                    logger.Debug("Message envoyé : " & message)
                End If
            Catch ex As Exception
                logger.Error($"Erreur lors de l'envoi du message : {ex.Message}")
            End Try
        End Sub

        ' Fonction pour envoyer un code et un contenu spécifié
        Public Shared Sub SendMessageWithCode(code As String, content As String)
            logger.Debug("Envoi d'un message avec code ")
            Try
                If code IsNot Nothing AndAlso content IsNot Nothing AndAlso sendingClient IsNot Nothing Then
                    Dim message As String = code + content
                    Dim messageBytes As Byte() = Encoding.UTF8.GetBytes(message)
                    sendingClient.Send(messageBytes, messageBytes.Length)
                    logger.Debug("Message envoyé avec code : " & code & " et contenu : " & content)
                End If
            Catch ex As Exception
                logger.Error($"Erreur lors de l'envoi du message avec code : {ex.Message}")
            End Try
        End Sub
    End Class
End Namespace
