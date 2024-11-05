Imports System.Net
Imports System.Net.Sockets

Namespace Networking
    Public Class NetworkingFileReceiver
        Public Sub ReceiveFile(savePath As String, port As Integer)
            Dim listener As New TcpListener(IPAddress.Any, port)
            listener.Start()
            Try
                Using client As TcpClient = listener.AcceptTcpClient()
                    Using stream As NetworkStream = client.GetStream()
                        Using fileStream As New System.IO.FileStream(savePath, System.IO.FileMode.Create)
                            Dim buffer(4096) As Byte
                            Dim bytesRead As Integer
                            Do
                                bytesRead = stream.Read(buffer, 0, buffer.Length)
                                If bytesRead > 0 Then
                                    fileStream.Write(buffer, 0, bytesRead)
                                End If
                            Loop While bytesRead > 0
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                ' Ajoutez un journal ou affichez un message d'erreur
                logger.Error("Erreur lors de la réception du fichier : " & ex.Message)
            Finally
                listener.Stop()
            End Try
        End Sub
    End Class
End Namespace

