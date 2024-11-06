Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Drawing
Imports ControlzEx.Theming
Imports EyeChat.EyeChat
Imports EyeChat.Models
Imports EyeChat.Utilities
Imports Octokit

Namespace ViewModel
    Public Class SettingsViewModel
        Implements INotifyPropertyChanged


        Private _mainWindow As MainWindow

        Public Property ColorItems As New ObservableCollection(Of ColorItemViewModel)()
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Public Property DebugLevels As New ObservableCollection(Of String)() From {"DEBUG", "INFO", "WARN", "ERROR", "FATAL"}
        Public Shared ReadOnly Property AvailableDays As List(Of String) = New List(Of String) From {
            "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi"
        }
        Protected Sub NotifyPropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub


#Region "Raccourcis clavier consultation"
        Public Property F5Enabled As Boolean
            Get
                Try
                    logger.Debug("Lecture de la propriété F5Enabled")
                    Return UserSettingsList.ShortcutKeys("F5").Enabled
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F5Enabled : {ex.Message}")
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Try
                    If UserSettingsList.ShortcutKeys("F5").Enabled <> value Then
                        UserSettingsList.ShortcutKeys("F5").Enabled = value

                        NotifyPropertyChanged("F5Enabled")
                        logger.Info($"La valeur de F5Enabled a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F5Enabled : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F5Page1 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F5Page1")
                    Return UserSettingsList.ShortcutKeys("F5").Pages(0)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F5Page1 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F5").Pages(0) <> value Then
                        UserSettingsList.ShortcutKeys("F5").Pages(0) = value

                        NotifyPropertyChanged("F5Page1")
                        logger.Info($"La valeur de F5Page1 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F5Page1 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F5Page2 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F5Page2")
                    Return UserSettingsList.ShortcutKeys("F5").Pages(1)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F5Page2 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F5").Pages(1) <> value Then
                        UserSettingsList.ShortcutKeys("F5").Pages(1) = value

                        NotifyPropertyChanged("F5Page2")
                        logger.Info($"La valeur de F5Page2 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F5Page2 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F5Page3 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F5Page3")
                    Return UserSettingsList.ShortcutKeys("F5").Pages(2)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F5Page3 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F5").Pages(2) <> value Then
                        UserSettingsList.ShortcutKeys("F5").Pages(2) = value

                        NotifyPropertyChanged("F5Page3")
                        logger.Info($"La valeur de F5Page3 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F5Page3 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F5Page4 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F5Page4")
                    Return UserSettingsList.ShortcutKeys("F5").Pages(3)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F5Page4 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F5").Pages(3) <> value Then
                        UserSettingsList.ShortcutKeys("F5").Pages(3) = value

                        NotifyPropertyChanged("F5Page4")
                        logger.Info($"La valeur de F5Page4 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F5Page4 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F5Page5 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F5Page5")
                    Return UserSettingsList.ShortcutKeys("F5").Pages(4)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F5Page5 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F5").Pages(4) <> value Then
                        UserSettingsList.ShortcutKeys("F5").Pages(4) = value

                        NotifyPropertyChanged("F5Page5")
                        logger.Info($"La valeur de F5Page5 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F5Page5 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F5Text1 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F5Text1")
                    Return UserSettingsList.ShortcutKeys("F5").Texts(0)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F5Text1 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F5").Texts(0) <> value Then
                        UserSettingsList.ShortcutKeys("F5").Texts(0) = value

                        NotifyPropertyChanged("F5Text1")
                        logger.Info($"La valeur de F5Text1 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F5Text1 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F5Text2 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F5Text2")
                    Return UserSettingsList.ShortcutKeys("F5").Texts(1)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F5Text2 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F5").Texts(1) <> value Then
                        UserSettingsList.ShortcutKeys("F5").Texts(1) = value

                        NotifyPropertyChanged("F5Text2")
                        logger.Info($"La valeur de F5Text2 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F5Text2 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F5Text3 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F5Text3")
                    Return UserSettingsList.ShortcutKeys("F5").Texts(2)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F5Text3 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F5").Texts(2) <> value Then
                        UserSettingsList.ShortcutKeys("F5").Texts(2) = value

                        NotifyPropertyChanged("F5Text3")
                        logger.Info($"La valeur de F5Text3 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F5Text3 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F5Text4 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F5Text4")
                    Return UserSettingsList.ShortcutKeys("F5").Texts(3)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F5Text4 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F5").Texts(3) <> value Then
                        UserSettingsList.ShortcutKeys("F5").Texts(3) = value

                        NotifyPropertyChanged("F5Text4")
                        logger.Info($"La valeur de F5Text4 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F5Text4 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F5Text5 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F5Text5")
                    Return UserSettingsList.ShortcutKeys("F5").Texts(4)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F5Text5 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F5").Texts(4) <> value Then
                        UserSettingsList.ShortcutKeys("F5").Texts(4) = value

                        NotifyPropertyChanged("F5Text5")
                        logger.Info($"La valeur de F5Text5 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F5Text5 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F6Enabled As Boolean
            Get
                Try
                    logger.Debug("Lecture de la propriété F6Enabled")
                    Return UserSettingsList.ShortcutKeys("F6").Enabled
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F6Enabled : {ex.Message}")
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Try
                    If UserSettingsList.ShortcutKeys("F6").Enabled <> value Then
                        UserSettingsList.ShortcutKeys("F6").Enabled = value

                        NotifyPropertyChanged("F6Enabled")
                        logger.Info($"La valeur de F6Enabled a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F6Enabled : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F6Page1 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F6Page1")
                    Return UserSettingsList.ShortcutKeys("F6").Pages(0)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F6Page1 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F6").Pages(0) <> value Then
                        UserSettingsList.ShortcutKeys("F6").Pages(0) = value

                        NotifyPropertyChanged("F6Page1")
                        logger.Info($"La valeur de F6Page1 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F6Page1 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F6Page2 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F6Page2")
                    Return UserSettingsList.ShortcutKeys("F6").Pages(1)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F6Page2 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F6").Pages(1) <> value Then
                        UserSettingsList.ShortcutKeys("F6").Pages(1) = value

                        NotifyPropertyChanged("F6Page2")
                        logger.Info($"La valeur de F6Page2 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F6Page2 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F6Text1 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F6Text1")
                    Return UserSettingsList.ShortcutKeys("F6").Texts(0)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F6Text1 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F6").Texts(0) <> value Then
                        UserSettingsList.ShortcutKeys("F6").Texts(0) = value

                        NotifyPropertyChanged("F6Text1")
                        logger.Info($"La valeur de F6Text1 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F6Text1 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F6Text2 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F6Text2")
                    Return UserSettingsList.ShortcutKeys("F6").Texts(1)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F6Text2 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F6").Texts(1) <> value Then
                        UserSettingsList.ShortcutKeys("F6").Texts(1) = value

                        NotifyPropertyChanged("F6Text2")
                        logger.Info($"La valeur de F6Text2 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F6Text2 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F6Text3 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F6Text3")
                    Return UserSettingsList.ShortcutKeys("F6").Texts(2)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F6Text3 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F6").Texts(2) <> value Then
                        UserSettingsList.ShortcutKeys("F6").Texts(2) = value

                        NotifyPropertyChanged("F6Text3")
                        logger.Info($"La valeur de F6Text3 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F6Text3 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F6Text4 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F6Text4")
                    Return UserSettingsList.ShortcutKeys("F6").Texts(3)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F6Text4 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F6").Texts(3) <> value Then
                        UserSettingsList.ShortcutKeys("F6").Texts(3) = value

                        NotifyPropertyChanged("F6Text4")
                        logger.Info($"La valeur de F6Text4 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F6Text4 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F6Text5 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F6Text5")
                    Return UserSettingsList.ShortcutKeys("F6").Texts(4)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F6Text5 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F6").Texts(4) <> value Then
                        UserSettingsList.ShortcutKeys("F6").Texts(4) = value

                        NotifyPropertyChanged("F6Text5")
                        logger.Info($"La valeur de F6Text5 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F6Text5 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F7Enabled As Boolean
            Get
                Try
                    logger.Debug("Lecture de la propriété F7Enabled")
                    Return UserSettingsList.ShortcutKeys("F7").Enabled
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F7Enabled : {ex.Message}")
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Try
                    If UserSettingsList.ShortcutKeys("F7").Enabled <> value Then
                        UserSettingsList.ShortcutKeys("F7").Enabled = value

                        NotifyPropertyChanged("F7Enabled")
                        logger.Info($"La valeur de F7Enabled a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F7Enabled : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F7Page1 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F7Page1")
                    Return UserSettingsList.ShortcutKeys("F7").Pages(0)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F7Page1 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F7").Pages(0) <> value Then
                        UserSettingsList.ShortcutKeys("F7").Pages(0) = value

                        NotifyPropertyChanged("F7Page1")
                        logger.Info($"La valeur de F7Page1 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F7Page1 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F7Page2 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F7Page2")
                    Return UserSettingsList.ShortcutKeys("F7").Pages(1)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F7Page2 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F7").Pages(1) <> value Then
                        UserSettingsList.ShortcutKeys("F7").Pages(1) = value

                        NotifyPropertyChanged("F7Page2")
                        logger.Info($"La valeur de F7Page2 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F7Page2 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F7Page3 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F7Page3")
                    Return UserSettingsList.ShortcutKeys("F7").Pages(2)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F7Page3 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F7").Pages(2) <> value Then
                        UserSettingsList.ShortcutKeys("F7").Pages(2) = value

                        NotifyPropertyChanged("F7Page3")
                        logger.Info($"La valeur de F7Page3 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F7Page3 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F7Page4 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F7Page4")
                    Return UserSettingsList.ShortcutKeys("F7").Pages(3)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F7Page4 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F7").Pages(3) <> value Then
                        UserSettingsList.ShortcutKeys("F7").Pages(3) = value

                        NotifyPropertyChanged("F7Page4")
                        logger.Info($"La valeur de F7Page4 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F7Page4 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F7Page5 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F7Page5")
                    Return UserSettingsList.ShortcutKeys("F7").Pages(4)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F7Page5 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F7").Pages(4) <> value Then
                        UserSettingsList.ShortcutKeys("F7").Pages(4) = value

                        NotifyPropertyChanged("F7Page5")
                        logger.Info($"La valeur de F7Page5 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F7Page5 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F7Text1 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F7Text1")
                    Return UserSettingsList.ShortcutKeys("F7").Texts(0)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F7Text1 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F7").Texts(0) <> value Then
                        UserSettingsList.ShortcutKeys("F7").Texts(0) = value

                        NotifyPropertyChanged("F7Text1")
                        logger.Info($"La valeur de F7Text1 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F7Text1 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F7Text2 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F7Text2")
                    Return UserSettingsList.ShortcutKeys("F7").Texts(1)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F7Text2 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F7").Texts(1) <> value Then
                        UserSettingsList.ShortcutKeys("F7").Texts(1) = value

                        NotifyPropertyChanged("F7Text2")
                        logger.Info($"La valeur de F7Text2 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F7Text2 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F7Text3 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F7Text3")
                    Return UserSettingsList.ShortcutKeys("F7").Texts(2)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F7Text3 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F7").Texts(2) <> value Then
                        UserSettingsList.ShortcutKeys("F7").Texts(2) = value

                        NotifyPropertyChanged("F7Text3")
                        logger.Info($"La valeur de F7Text3 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F7Text3 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F7Text4 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F7Text4")
                    Return UserSettingsList.ShortcutKeys("F7").Texts(3)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F7Text4 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F7").Texts(3) <> value Then
                        UserSettingsList.ShortcutKeys("F7").Texts(3) = value

                        NotifyPropertyChanged("F7Text4")
                        logger.Info($"La valeur de F7Text4 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F7Text4 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F7Text5 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F7Text5")
                    Return UserSettingsList.ShortcutKeys("F7").Texts(4)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F7Text5 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F7").Texts(4) <> value Then
                        UserSettingsList.ShortcutKeys("F7").Texts(4) = value

                        NotifyPropertyChanged("F7Text5")
                        logger.Info($"La valeur de F7Text5 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F7Text5 : {ex.Message}")
                End Try
            End Set
        End Property



        Public Property F8Enabled As Boolean
            Get
                Try
                    logger.Debug("Lecture de la propriété F8Enabled")
                    Return UserSettingsList.ShortcutKeys("F8").Enabled
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F8Enabled : {ex.Message}")
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Try
                    If UserSettingsList.ShortcutKeys("F8").Enabled <> value Then
                        UserSettingsList.ShortcutKeys("F8").Enabled = value

                        NotifyPropertyChanged("F8Enabled")
                        logger.Info($"La valeur de F8Enabled a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F8Enabled : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F8Page1 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F8Page1")
                    Return UserSettingsList.ShortcutKeys("F8").Pages(0)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F8Page1 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F8").Pages(0) <> value Then
                        UserSettingsList.ShortcutKeys("F8").Pages(0) = value

                        NotifyPropertyChanged("F8Page1")
                        logger.Info($"La valeur de F8Page1 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F8Page1 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F8Page2 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F8Page2")
                    Return UserSettingsList.ShortcutKeys("F8").Pages(1)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F8Page2 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F8").Pages(1) <> value Then
                        UserSettingsList.ShortcutKeys("F8").Pages(1) = value

                        NotifyPropertyChanged("F8Page2")
                        logger.Info($"La valeur de F8Page2 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F8Page2 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F8Page3 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F8Page3")
                    Return UserSettingsList.ShortcutKeys("F8").Pages(2)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F8Page3 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F8").Pages(2) <> value Then
                        UserSettingsList.ShortcutKeys("F8").Pages(2) = value

                        NotifyPropertyChanged("F8Page3")
                        logger.Info($"La valeur de F8Page3 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F8Page3 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F8Page4 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F8Page4")
                    Return UserSettingsList.ShortcutKeys("F8").Pages(3)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F8Page4 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F8").Pages(3) <> value Then
                        UserSettingsList.ShortcutKeys("F8").Pages(3) = value

                        NotifyPropertyChanged("F8Page4")
                        logger.Info($"La valeur de F8Page4 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F8Page4 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F8Page5 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F8Page5")
                    Return UserSettingsList.ShortcutKeys("F8").Pages(4)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F8Page5 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F8").Pages(4) <> value Then
                        UserSettingsList.ShortcutKeys("F8").Pages(4) = value

                        NotifyPropertyChanged("F8Page5")
                        logger.Info($"La valeur de F8Page5 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F8Page5 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F8Text1 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F8Text1")
                    Return UserSettingsList.ShortcutKeys("F8").Texts(0)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F8Text1 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F8").Texts(0) <> value Then
                        UserSettingsList.ShortcutKeys("F8").Texts(0) = value

                        NotifyPropertyChanged("F8Text1")
                        logger.Info($"La valeur de F8Text1 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F8Text1 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F8Text2 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F8Text2")
                    Return UserSettingsList.ShortcutKeys("F8").Texts(1)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F8Text2 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F8").Texts(1) <> value Then
                        UserSettingsList.ShortcutKeys("F8").Texts(1) = value

                        NotifyPropertyChanged("F8Text2")
                        logger.Info($"La valeur de F8Text2 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F8Text2 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F8Text3 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F8Text3")
                    Return UserSettingsList.ShortcutKeys("F8").Texts(2)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F8Text3 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F8").Texts(2) <> value Then
                        UserSettingsList.ShortcutKeys("F8").Texts(2) = value

                        NotifyPropertyChanged("F8Text3")
                        logger.Info($"La valeur de F8Text3 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F8Text3 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F8Text4 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F8Text4")
                    Return UserSettingsList.ShortcutKeys("F8").Texts(3)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F8Text4 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F8").Texts(3) <> value Then
                        UserSettingsList.ShortcutKeys("F8").Texts(3) = value

                        NotifyPropertyChanged("F8Text4")
                        logger.Info($"La valeur de F8Text4 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F8Text4 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property F8Text5 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété F8Text5")
                    Return UserSettingsList.ShortcutKeys("F8").Texts(4)
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété F8Text5 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    If UserSettingsList.ShortcutKeys("F8").Texts(4) <> value Then
                        UserSettingsList.ShortcutKeys("F8").Texts(4) = value

                        NotifyPropertyChanged("F8Text5")
                        logger.Info($"La valeur de F8Text5 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété F8Text5 : {ex.Message}")
                End Try
            End Set
        End Property

#End Region

#Region "Non utilisé ici"
        Public Property RoomDisplay As Boolean
            Get
                logger.Debug("Lecture de la propriété RoomDisplay")
                Return UserSettingsList.RoomDisplay
            End Get
            Set(value As Boolean)
                Try
                    If UserSettingsList.RoomDisplay <> value Then
                        UserSettingsList.RoomDisplay = value
                        If value = True Then
                            UserSettingsList.RoomDisplayStr = "Visible"
                        Else
                            UserSettingsList.RoomDisplayStr = "Collapsed"
                        End If
                        NotifyPropertyChanged("RoomDisplay")
                        NotifyPropertyChanged("RoomDisplayStr")
                        logger.Info($"La valeur de RoomDisplay a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété RoomDisplay : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property RoomDisplayStr As String
            Get
                logger.Debug("Lecture de la propriété RoomDisplayStr")
                Return UserSettingsList.RoomDisplayStr
            End Get
            Set(value As String)
                Try
                    If UserSettingsList.RoomDisplayStr <> value Then
                        UserSettingsList.RoomDisplayStr = value
                        NotifyPropertyChanged("RoomDisplay")
                        NotifyPropertyChanged("RoomDisplayStr")
                        logger.Info($"La valeur de RoomDisplayStr a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété RoomDisplayStr : {ex.Message}")
                End Try
            End Set
        End Property
#End Region

#Region "Fonctionalité"
        Public Property SecretaryMode As Boolean
            Get
                Try
                    logger.Debug("Lecture de la propriété SecretaryMode")
                    Return UserSettingsList.SecretaryMode
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété SecretaryMode: {ex.Message}")
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Try
                    ' Mise à jour des paramètres uniquement si la valeur change
                    If UserSettingsList.SecretaryMode <> value Then
                        UserSettingsList.SecretaryMode = value

                        NotifyPropertyChanged("SecretaryMode")
                        logger.Info($"La valeur de SecretaryMode a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété SecretaryMode : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property DoctorMode As Boolean
            Get
                Try
                    logger.Debug("Lecture de la propriété DoctorMode")
                    Return UserSettingsList.DoctorMode
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété DoctorMode: {ex.Message}")
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Try
                    ' Mise à jour des paramètres uniquement si la valeur change
                    If UserSettingsList.DoctorMode <> value Then
                        UserSettingsList.DoctorMode = value

                        NotifyPropertyChanged("DoctorMode")
                        logger.Info($"La valeur de DoctorMode a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété DoctorMode : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property OrthoMode As Boolean
            Get
                Try
                    logger.Debug("Lecture de la propriété OrthoMode")
                    Return UserSettingsList.OrthoMode
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété OrthoMode: {ex.Message}")
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Try
                    ' Mise à jour des paramètres uniquement si la valeur change
                    If UserSettingsList.OrthoMode <> value Then
                        UserSettingsList.OrthoMode = value

                        NotifyPropertyChanged("OrthoMode")
                        logger.Info($"La valeur de OrthoMode a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété OrthoMode : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property AdvanvedMode As Boolean
            Get
                Try
                    logger.Debug("Lecture de la propriété AdvanvedMode")
                    Return UserSettingsList.AdvanvedMode
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété AdvanvedMode: {ex.Message}")
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Try
                    ' Mise à jour des paramètres uniquement si la valeur change
                    If UserSettingsList.AdvanvedMode <> value Then
                        UserSettingsList.AdvanvedMode = value

                        NotifyPropertyChanged("AdvanvedMode")
                        logger.Info($"La valeur de AdvanvedMode a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété AdvanvedMode : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property AdminMode As Boolean
            Get
                Try
                    logger.Debug("Lecture de la propriété AdminMode")
                    Return UserSettingsList.AdminMode
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété AdminMode: {ex.Message}")
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Try
                    ' Mise à jour des paramètres uniquement si la valeur change
                    If UserSettingsList.AdminMode <> value Then
                        UserSettingsList.AdminMode = value

                        NotifyPropertyChanged("AdminMode")
                        logger.Info($"La valeur de AdminMode a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété AdminMode : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property NFCMode As Boolean
            Get
                Try
                    logger.Debug("Lecture de la propriété NFCMode")
                    Return UserSettingsList.NFCMode
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété NFCMode: {ex.Message}")
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Try
                    ' Mise à jour des paramètres uniquement si la valeur change
                    If UserSettingsList.NFCMode <> value Then
                        UserSettingsList.NFCMode = value

                        NotifyPropertyChanged("NFCMode")
                        logger.Info($"La valeur de NFCMode a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété NFCMode : {ex.Message}")
                End Try
            End Set
        End Property
#End Region

#Region "Raccourcis clavier examens"
        Public Property CtrlF9 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété CtrlF9")
                    Return UserSettingsList.CtrlF9
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété CtrlF9 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    ' Mise à jour des paramètres si la valeur change
                    If UserSettingsList.CtrlF9 <> value Then
                        UserSettingsList.CtrlF9 = value

                        NotifyPropertyChanged("CtrlF9")
                        logger.Info($"La valeur de CtrlF9 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété CtrlF9 : {ex.Message}")
                End Try
            End Set
        End Property


        Public Property CtrlF9Enabled As Boolean
            Get
                Try
                    logger.Debug("Lecture de la propriété CtrlF9Enabled")
                    Return UserSettingsList.CtrlF9Enabled
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété CtrlF9Enabled : {ex.Message}")
                    Return False
                End Try
            End Get
            Set(value As Boolean)
                Try
                    If UserSettingsList.CtrlF9Enabled <> value Then
                        UserSettingsList.CtrlF9Enabled = value

                        NotifyPropertyChanged("CtrlF9Enabled")
                        logger.Info($"La valeur de CtrlF9Enabled a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété CtrlF9Enabled1 : {ex.Message}")
                End Try


            End Set
        End Property


        Public Property CtrlF10 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété CtrlF10")
                    Return UserSettingsList.CtrlF10
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété CtrlF10 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    ' Mise à jour des paramètres uniquement si la valeur change
                    If UserSettingsList.CtrlF10 <> value Then
                        UserSettingsList.CtrlF10 = value

                        NotifyPropertyChanged("CtrlF10")
                        logger.Info($"La valeur de CtrlF10 a été modifiée : {value}")
                    End If

                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété CtrlF10 : {ex.Message}")
                End Try
            End Set
        End Property



        Public Property CtrlF10Enabled As Boolean
            Get
                Try
                    logger.Debug("Lecture de la propriété CtrlF10Enabled")
                    Return UserSettingsList.CtrlF10Enabled
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété CtrlF10Enabled : {ex.Message}")
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Try
                    ' Mise à jour des paramètres uniquement si la valeur change
                    If UserSettingsList.CtrlF10Enabled <> value Then
                        UserSettingsList.CtrlF10Enabled = value

                        NotifyPropertyChanged("CtrlF10Enabled")
                        logger.Info($"La valeur de CtrlF10Enabled a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété CtrlF10Enabled : {ex.Message}")
                End Try
            End Set
        End Property
        Public Property CtrlF11 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété CtrlF11")
                    Return UserSettingsList.CtrlF11
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété CtrlF11 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    ' Mise à jour des paramètres uniquement si la valeur change
                    If UserSettingsList.CtrlF11 <> value Then
                        UserSettingsList.CtrlF11 = value

                        NotifyPropertyChanged("CtrlF11")
                        logger.Info($"La valeur de CtrlF11 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété CtrlF11 : {ex.Message}")
                End Try
            End Set
        End Property
        Public Property CtrlF11Enabled As Boolean
            Get
                Try
                    logger.Debug("Lecture de la propriété CtrlF11Enabled")
                    Return UserSettingsList.CtrlF11Enabled
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété CtrlF11Enabled : {ex.Message}")
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Try
                    ' Mise à jour des paramètres uniquement si la valeur change
                    If UserSettingsList.CtrlF11Enabled <> value Then
                        UserSettingsList.CtrlF11Enabled = value

                        NotifyPropertyChanged("CtrlF11Enabled")
                        logger.Info($"La valeur de CtrlF11Enabled a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété CtrlF11Enabled : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property ShiftF9 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété ShiftF9")
                    Return UserSettingsList.ShiftF9
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété ShiftF9 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    ' Mise à jour des paramètres uniquement si la valeur change
                    If UserSettingsList.ShiftF9 <> value Then
                        UserSettingsList.ShiftF9 = value

                        NotifyPropertyChanged("ShiftF9")
                        logger.Info($"La valeur de ShiftF9 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété ShifttF9 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property ShiftF9Enabled As Boolean
            Get
                Try
                    logger.Debug("Lecture de la propriété ShiftF9Enabled")
                    Return UserSettingsList.ShiftF9Enabled
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété ShiftF9Enabled : {ex.Message}")
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Try
                    ' Mise à jour des paramètres uniquement si la valeur change
                    If UserSettingsList.ShiftF9Enabled <> value Then
                        UserSettingsList.ShiftF9Enabled = value

                        NotifyPropertyChanged("ShiftF9Enabled")
                        logger.Info($"La valeur de ShiftF9Enabled a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété ShiftF9Enabled : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property ShiftF10 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété ShiftF10")
                    Return UserSettingsList.ShiftF10
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété ShiftF10 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    ' Mise à jour des paramètres uniquement si la valeur change
                    If UserSettingsList.ShiftF10 <> value Then
                        UserSettingsList.ShiftF10 = value

                        NotifyPropertyChanged("ShiftF10")
                        logger.Info($"La valeur de ShiftF10 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété ShiftF10 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property ShiftF10Enabled As Boolean
            Get
                Try
                    logger.Debug("Lecture de la propriété ShiftF10Enabled")
                    Return UserSettingsList.ShiftF10Enabled
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété ShiftF10Enabled : {ex.Message}")
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Try
                    ' Mise à jour des paramètres uniquement si la valeur change
                    If UserSettingsList.ShiftF10Enabled <> value Then
                        UserSettingsList.ShiftF10Enabled = value

                        NotifyPropertyChanged("ShiftF10Enabled")
                        logger.Info($"La valeur de ShiftF10Enabled a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété ShiftF10Enabled : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property ShiftF11 As String
            Get
                Try
                    logger.Debug("Lecture de la propriété ShiftF11")
                    Return UserSettingsList.ShiftF11
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété ShiftF11 : {ex.Message}")
                    Return String.Empty
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    ' Mise à jour des paramètres uniquement si la valeur change
                    If UserSettingsList.ShiftF11 <> value Then
                        UserSettingsList.ShiftF11 = value

                        NotifyPropertyChanged("ShiftF11")
                        logger.Info($"La valeur de ShiftF11 a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété ShiftF11 : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property ShiftF11Enabled As Boolean
            Get
                Try
                    logger.Debug("Lecture de la propriété ShiftF11Enabled")
                    Return UserSettingsList.ShiftF11Enabled
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété ShiftF11Enabled : {ex.Message}")
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Try
                    ' Mise à jour des paramètres uniquement si la valeur change
                    If UserSettingsList.ShiftF11Enabled <> value Then
                        UserSettingsList.ShiftF11Enabled = value

                        NotifyPropertyChanged("ShiftF11Enabled")
                        logger.Info($"La valeur de ShiftF11Enabled a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété ShiftF11Enabled : {ex.Message}")
                End Try
            End Set
        End Property
#End Region

#Region "themecouleur"
        Public Property AppTheme As String
            Get
                Try
                    Return UserSettingsList.AppTheme
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété AppTheme : {ex.Message}")
                    Return "White"
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    ' Éviter les doublons avant de modifier AppTheme
                    If UserSettingsList.AppTheme = value Then
                        logger.Info("AppTheme n'a pas changé, aucun besoin de mise à jour.")
                        Return
                    End If

                    ' Mise à jour des paramètres
                    UserSettingsList.AppTheme = value
                    NotifyPropertyChanged("AppTheme")
                    logger.Error($"Le thème a été modifié : {value}")
                Catch ex As Exception
                    ' Gérer l'exception
                    logger.Error($"Erreur lors de la modification de la propriété AppTheme : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property AppColorString As String
            Get
                Try
                    Dim storedValue As String = UserSettingsList.AppColorString
                    ' Supprimer les guillemets simples si présents
                    Return If(Not String.IsNullOrEmpty(storedValue), storedValue.Trim("'"c), "Blue")
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété AppColorString : {ex.Message}")
                    Return "Blue"
                End Try
            End Get
            Set(ByVal value As String)
                Try
                    ' Éviter les doublons avant de modifier AppColorString
                    If UserSettingsList.AppColorString = value Then
                        logger.Info("AppColorString n'a pas changé, aucun besoin de mise à jour.")
                        Return
                    End If
                    value = If(Not String.IsNullOrEmpty(value), value.Trim("'"c), value)
                    Dim converter As New System.Windows.Media.ColorConverter()
                    Dim Color As System.Windows.Media.Color = CType(System.Windows.Media.ColorConverter.ConvertFromString(value), System.Windows.Media.Color)
                    ' Mise à jour des paramètres
                    UserSettingsList.AppColorString = value
                    UserSettingsList.AppColor = System.Drawing.Color.FromArgb(Color.A, Color.R, Color.G, Color.B)
                    NotifyPropertyChanged("AppColorString")
                    ''SelectUser(SelectedUser)
                Catch ex As Exception
                    ' Gérer l'exception
                    logger.Error($"Erreur lors de la modification de la propriété AppColorString : {ex.Message}")
                End Try
            End Set
        End Property


        Public Property AppColor As Color
            Get
                Try
                    logger.Debug("Lecture de la propriété AppColor")
                    Dim storedValue As System.Drawing.Color = UserSettingsList.AppColor

                    ' Vérifiez si storedValue est null ou vide
                    If storedValue = Nothing Then
                        Return Color.Blue ' Valeur par défaut
                    End If

                    Return storedValue

                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété AppColor : {ex.Message}")
                    Return Color.Blue
                End Try

            End Get
            Set(ByVal value As Color)
                Try
                    ' Éviter les doublons avant de modifier AppColor
                    If UserSettingsList.AppColor = value Then
                        logger.Info("AppColor n'a pas changé, aucun besoin de mise à jour.")
                        Return
                    End If
                    UserSettingsList.AppColor = value
                    NotifyPropertyChanged("AppColor")
                    ''SelectUser(SelectedUser)
                Catch ex As Exception
                    ' Gérer l'exception ici (par exemple, enregistrer l'erreur dans les journaux)
                    logger.Error($"Erreur lors de la modification de la propriété AppColor : {ex.Message}")
                End Try
            End Set
        End Property
#End Region

#Region "Planning"

        Public Property PlanningMode As Boolean
            Get
                Try
                    logger.Debug("Lecture de la propriété PlanningMode")
                    Return AppConfig.PlanningMode
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété PlanningMode : {ex.Message}")
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Try
                    ' Mise à jour des paramètres uniquement si la valeur change
                    If AppConfig.PlanningMode <> value Then
                        AppConfig.PlanningMode = value

                        NotifyPropertyChanged("PlanningMode")
                        logger.Info($"La valeur de PlanningMode a été modifiée : {value}")
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété PlanningMode : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property PlanningList As ObservableCollection(Of PlanningModels)
            Get
                Return New ObservableCollection(Of PlanningModels)(PlanningListGlobal)
            End Get
            Set(value As ObservableCollection(Of PlanningModels))
                Try
                    PlanningListGlobal = value
                    NotifyPropertyChanged("PlanningList")
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété PlanningList : {ex.Message}")
                End Try
            End Set
        End Property
#End Region



#Region "GénéralSettings"
        Public Property SelectedDebugLevel As String
            Get
                Try
                    logger.Debug("Lecture de la propriété SelectedDebugLevel")
                    Return AppConfig.LogLevel
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété SelectedDebugLevel : {ex.Message}")
                    Return "DEBUG"
                End Try

            End Get
            Set(ByVal value As String)
                ' Mise à jour des paramètres uniquement si la valeur change
                If AppConfig.LogLevel <> value Then
                    AppConfig.LogLevel = value
                    NotifyPropertyChanged("SelectedDebugLevel")
                    logger.Info($"Le niveau de débogage a été modifié : {value}")
                    ' Vérifier le niveau de débogage sélectionné
                    Select Case value
                        Case "DEBUG"
                            logger.Logger.Repository.Threshold = log4net.Core.Level.Debug
                        Case "INFO"
                            logger.Logger.Repository.Threshold = log4net.Core.Level.Info
                        Case "WARN"
                            logger.Logger.Repository.Threshold = log4net.Core.Level.Warn
                        Case "ERROR"
                            logger.Logger.Repository.Threshold = log4net.Core.Level.Error
                        Case "FATAL"
                            logger.Logger.Repository.Threshold = log4net.Core.Level.Fatal
                    End Select
                End If
                NotifyPropertyChanged("SelectedDebugLevel")
            End Set
        End Property
        Public Property ComputerName As String
            Get
                Try
                    Return AppConfig.ComputerName

                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété ComputerName : {ex.Message}")
                    Return "NoName"
                End Try

            End Get
            Set(ByVal value As String)
                Try
                    AppConfig.ComputerName = value
                    NotifyPropertyChanged("ComputerName")
                Catch ex As Exception
                    ' Gérer l'exception ici (par exemple, enregistrer l'erreur dans les journaux)
                    logger.Error($"Erreur lors de la modification de la propriété ComputerName : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property WindowsName As String
            Get
                Try
                    Return AppConfig.WindowsUser

                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété WindowsName : {ex.Message}")
                    Return "Bender"
                End Try

            End Get
            Set(ByVal value As String)
                Try
                    AppConfig.WindowsUser = value
                    NotifyPropertyChanged("WindowsName")
                Catch ex As Exception
                    ' Gérer l'exception ici (par exemple, enregistrer l'erreur dans les journaux)
                    logger.Error($"Erreur lors de la modification de la propriété WindowsName : {ex.Message}")
                End Try
            End Set
        End Property
#End Region


        Public Property SpeedMessage As ObservableCollection(Of SpeedMessageModels)
            Get
                Return New ObservableCollection(Of SpeedMessageModels)(SpeedMessagesListGlobal)
            End Get
            Set(value As ObservableCollection(Of SpeedMessageModels))
                Try
                    ' Vous pouvez reconstituer la liste globale à partir de la ObservableCollection si nécessaire
                    SpeedMessagesListGlobal = value.ToList()
                    NotifyPropertyChanged("SpeedMessage")
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété SpeedMessage : {ex.Message}")
                End Try
            End Set
        End Property

        Public Property ExamOptions As ObservableCollection(Of ExamOptionModels)
            Get
                Return ExamOptionsListGlobal
            End Get
            Set(value As ObservableCollection(Of ExamOptionModels))
                Try
                    ExamOptionsListGlobal = value
                    ''NotifyPropertyChanged("ExamOptions")
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété ExamOptions : {ex.Message}")
                End Try
            End Set
        End Property


        Public Property AppSizeDisplay As Double
            Get
                Try
                    logger.Info("Accès à la propriété AppSizeDisplay")
                    Return UserSettingsList.AppSizeDisplay
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété AppSizeDisplay : {ex.Message}")
                    Return 1.0 ' Valeur par défaut en cas d'erreur
                End Try
            End Get
            Set(ByVal value As Double)
                Try
                    If UserSettingsList.AppSizeDisplay <> value Then
                        UserSettingsList.AppSizeDisplay = value
                        NotifyPropertyChanged("AppSizeDisplay")
                        Dim mainWindow = CType(Application.Current.MainWindow, MainWindow)
                        ''mainWindow.SetHeaderFontSize(CInt(value))
                        mainWindow.UpdateLayout()
                        'My.Application.MainWindow.SetHeaderFontSize(CInt(value))
                        'PatientTabCtrl.FontSize = value
                        ''NotifyPropertyChanged("ArrowSize")
                        ''SelectUser("A Tous")
                        UsersListGlobal.Clear()
                        Dim loadedUsers = UserModels.LoadUsersFromJson()
                        For Each user In loadedUsers
                            UsersListGlobal.Add(user)
                        Next
                    End If
                Catch ex As Exception
                    logger.Error($"Erreur lors de la modification de la propriété AppSizeDisplay : {ex.Message}")
                End Try
            End Set
        End Property




#Region "GeneralSettings"

        Public Property UserName As String
            Get
                Try
                    Return UserSettingsList.UserName

                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété UserName : {ex.Message}")
                    Return "EyeChat"
                End Try

            End Get
            Set(ByVal value As String)
                Try
                    UserSettingsList.UserName = value
                    NotifyPropertyChanged("UserName")
                Catch ex As Exception
                    ' Gérer l'exception ici (par exemple, enregistrer l'erreur dans les journaux)
                    logger.Error($"Erreur lors de la modification de la propriété UserName : {ex.Message}")
                End Try
            End Set
        End Property
#End Region





        Public Sub New()
            ' Charger les configurations initiales
            AppConfig.LoadConfig()
            ' Récupérer les paramètres utilisateur
            UserSettingsList = UserSettingsModels.LoadUserSettingsFronJson(UserConnected)


        End Sub



    End Class
End Namespace
