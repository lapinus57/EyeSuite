Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports EyeChat.Models
Imports log4net
Imports log4net.Repository.Hierarchy

Namespace Networking
    Public Class NetworkingFileSender
        Private Shared ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

        ' Méthode pour envoyer un fichier sur le réseau
        Public Shared Sub SendFileOverNetwork(folder As String, fileToSend As String, Optional user As String = "")
            logger.Info("Envoi du fichier sur le réseau")
            Try
                ' Vérifie si un utilisateur a été spécifié
                If String.IsNullOrEmpty(user) Then
                    ' Aucun utilisateur spécifié, envoyez le message à tous les ordinateurs répertoriés
                    For Each computer In ComputersList
                        ' Assurez-vous de ne pas vous envoyer un message à vous-même
                        If computer.ComputerID <> My.Settings.UniqueId Then
                            Dim message As String = $"SYS20{computer.ComputerID}|{My.Settings.UniqueId}|{folder}|{fileToSend}"
                            SendManager.SendMessage(message)
                            SendFile(fileToSend, computer.ComputerIp, 12345)
                            logger.Info($"Fichier envoyé à l'ordinateur {computer.ComputerID}")
                        End If
                    Next
                Else
                    ' Un utilisateur a été spécifié, envoyez le message uniquement à cet utilisateur
                    Dim targetComputer As ComputerModels = ComputersList.FirstOrDefault(Function(comp) comp.ComputerUser = user)
                    If targetComputer IsNot Nothing Then
                        Dim message As String = $"SYS20{targetComputer.ComputerID}|{My.Settings.UniqueId}|{folder}|{fileToSend}"
                        SendManager.SendMessage(message)
                        SendFile(fileToSend, targetComputer.ComputerIp, 12345)
                        logger.Info($"Fichier envoyé à l'ordinateur {targetComputer.ComputerID}")
                    Else
                        logger.Error("L'utilisateur spécifié n'existe pas.")
                    End If
                End If
            Catch ex As Exception
                logger.Error("Erreur lors de l'envoi du fichier : " & ex.Message)
            End Try
        End Sub

        ' Méthode pour envoyer un fichier à un ordinateur spécifique
        Public Shared Sub SendFile(filePath As String, ipAddress As String, port As Integer)
            Try
                Dim client As New TcpClient(ipAddress, port)
                Dim data As Byte() = File.ReadAllBytes(filePath)

                Using stream As NetworkStream = client.GetStream()
                    stream.Write(data, 0, data.Length)
                    stream.Close()
                End Using

                client.Close()
            Catch ex As Exception
                logger.Error("Erreur lors de l'envoi du fichier : " & ex.Message)
            End Try
        End Sub

        ' Méthode pour créer un fichier texte
        Public Shared Sub CreateTextFile(ByVal folder As String, ByVal fileName As String, ByVal content As String)
            Dim filePath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder, fileName)
            File.WriteAllText(filePath, content)
            logger.Info($"Fichier texte créé : {filePath}")
        End Sub

        ' Méthode pour obtenir l'adresse IP locale
        Public Shared Function GetLocalIPAddress() As IPAddress
            logger.Info("Obtention de l'adresse IP locale")
            Try
                Dim networkInterfaces As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()
                For Each networkInterface As NetworkInterface In networkInterfaces
                    If networkInterface.OperationalStatus = OperationalStatus.Up Then
                        Dim ipProperties As IPInterfaceProperties = networkInterface.GetIPProperties()
                        Dim unicastIPAddresses As UnicastIPAddressInformationCollection = ipProperties.UnicastAddresses
                        For Each ipAddressInfo As UnicastIPAddressInformation In unicastIPAddresses
                            If ipAddressInfo.Address.AddressFamily = AddressFamily.InterNetwork Then
                                logger.Debug("L'adresse IP de l'utilisateur est : " & ipAddressInfo.Address.ToString())
                                Return ipAddressInfo.Address
                            End If
                        Next
                    End If
                Next
                Return Nothing
            Catch ex As Exception
                logger.Error("Erreur sur GetLocalIPAddress : " & ex.Message)
                Return Nothing
            End Try
        End Function
    End Class
End Namespace

