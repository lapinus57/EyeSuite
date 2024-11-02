Imports System.IO
Imports EyeChat.EyeChat
Imports log4net

Namespace Services
    Public Class FileWatcherSV
        Private Shared ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
        Private watcher As FileSystemWatcher
        Private iniFilePath As String
        Private mainWindow As MainWindow

        Public Sub New(iniFilePath As String, mainWindow As MainWindow)
            Me.iniFilePath = iniFilePath
            Me.mainWindow = mainWindow

            Try
                If Not File.Exists(iniFilePath) Then
                    logger.Error("Le fichier INI n'existe pas : " & iniFilePath)
                    Return
                End If

                ' Initialisation du FileSystemWatcher
                watcher = New FileSystemWatcher With {
                .Path = Path.GetDirectoryName(iniFilePath),
                .Filter = Path.GetFileName(iniFilePath),
                .NotifyFilter = NotifyFilters.LastWrite,
                .EnableRaisingEvents = True
            }

                AddHandler watcher.Changed, AddressOf OnIniFileChanged

                ' Lire initialement la valeur NumPat
                ReadNumPatValue()
            Catch ex As Exception
                logger.Error("Erreur lors de l'initialisation du FileSystemWatcher : " & ex.Message)
            End Try
        End Sub

        ' Méthode déclenchée lorsque le fichier INI est modifié
        Private Sub OnIniFileChanged(source As Object, e As FileSystemEventArgs)
            Try
                ReadNumPatValue()
            Catch ex As Exception
                logger.Error("Erreur lors du traitement de la modification du fichier INI : " & ex.Message)
            End Try
        End Sub

        ' Méthode pour lire la valeur NumPat du fichier INI
        Private Sub ReadNumPatValue()
            Try
                If Not File.Exists(iniFilePath) Then
                    logger.Error("Le fichier INI n'existe pas : " & iniFilePath)
                    Return
                End If

                Using fs As New FileStream(iniFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                    Using sr As New StreamReader(fs)
                        Dim line As String = vbEmpty
                        While InlineAssignHelper(line, sr.ReadLine()) IsNot Nothing
                            If line.StartsWith("NumPat=") Then
                                Dim numPatValueStr As String = line.Split("="c)(1)
                                Dim numPatValueLine As Integer
                                If Integer.TryParse(numPatValueStr, numPatValueLine) Then
                                    GlobalConfig.numPatValue = numPatValueLine
                                    logger.Info("La valeur NumPat a été mise à jour : " & GlobalConfig.numPatValue)
                                Else
                                    logger.Error("La valeur NumPat dans le fichier INI n'est pas un nombre valide.")
                                End If
                                Exit While
                            End If
                        End While
                    End Using
                End Using
            Catch ex As FileNotFoundException
                logger.Error("Fichier INI non trouvé : " & ex.Message)
            Catch ex As IOException
                logger.Error("Erreur d'entrée/sortie lors de la lecture du fichier INI : " & ex.Message)
            Catch ex As Exception
                logger.Error("Erreur lors de la lecture du fichier INI : " & ex.Message)
            End Try
        End Sub

        ' Méthode pour assigner une valeur à une variable et retourner cette valeur
        Private Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class

End Namespace