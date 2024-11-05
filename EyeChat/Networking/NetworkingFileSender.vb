Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports EyeChat.Models
Imports EyeChat.Utilities
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
                    For Each computer In ComputersListGlobal
                        ' Assurez-vous de ne pas vous envoyer un message à vous-même
                        If computer.ComputerID <> AppConfig.UniqueId Then
                            Dim message As String = $"SYS20{computer.ComputerID}|{AppConfig.UniqueId}|{folder}|{fileToSend}"
                            SendManager.SendMessage(message)
                            SendFile(fileToSend, computer.ComputerIp, 12345)
                            logger.Info($"Fichier envoyé à l'ordinateur {computer.ComputerID}")
                        End If
                    Next
                Else
                    ' Un utilisateur a été spécifié, envoyez le message uniquement à cet utilisateur
                    Dim targetComputer As ComputerModels = ComputersListGlobal.FirstOrDefault(Function(comp) comp.ComputerUser = user)
                    If targetComputer IsNot Nothing Then
                        Dim message As String = $"SYS20{targetComputer.ComputerID}|{AppConfig.UniqueId}|{folder}|{fileToSend}"
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
            ' Vérification des paramètres d'entrée
            If String.IsNullOrEmpty(ipAddress) OrElse port <= 0 Then
                logger.Error("Adresse IP ou port invalide.")
                Return
            End If

            If Not File.Exists(filePath) Then
                logger.Error("Le fichier spécifié n'existe pas : " & filePath)
                Return
            End If

            Try
                logger.Info($"Tentative de connexion à {ipAddress}:{port} pour envoyer le fichier {filePath}")

                Using client As New TcpClient(ipAddress, port)
                    Dim data As Byte() = File.ReadAllBytes(filePath)
                    Using stream As NetworkStream = client.GetStream()
                        Dim totalBytesSent As Integer = 0
                        Dim bufferSize As Integer = 4096
                        Dim offset As Integer = 0

                        While offset < data.Length
                            Dim bytesToSend As Integer = Math.Min(bufferSize, data.Length - offset)
                            stream.Write(data, offset, bytesToSend)
                            totalBytesSent += bytesToSend
                            offset += bytesToSend
                        End While

                        logger.Info($"Fichier {filePath} envoyé avec succès ({totalBytesSent} octets).")
                    End Using
                End Using
            Catch ex As Exception
                logger.Error($"Erreur lors de l'envoi du fichier {filePath} à {ipAddress}:{port} : {ex.Message}")
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

