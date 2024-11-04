
Imports System.Net.Http
Imports System.Reflection
Imports System.Text
Imports EyeChat.Controls
Imports EyeChat.EyeChat
Imports log4net
Imports MahApps.Metro.Controls.Dialogs
Imports Newtonsoft.Json.Linq

Namespace ViewModel
    Public Class GitHubViewModel

        Private ReadOnly dialogCoordinator As IDialogCoordinator
        Public Property Title As String
        Public Property AssemblyVersion As String
        Public Property FileVersion As String
        Public Property Description As String
        Public Property Company As String
        Public Property Copyright As String
        Public Property Trademark As String
        Public Property DescriptionText As String
        Public Property SelectedCategory As String

        Private Shared ReadOnly logger As ILog = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType)

        Public Sub New(dialogCoordinator As IDialogCoordinator)
            Me.dialogCoordinator = dialogCoordinator
            GetAssemblyInfos()
        End Sub

        Public Async Function DisplayMessageAsync(ByVal title As String, ByVal message As String) As Task
            Await dialogCoordinator.ShowMessageAsync(Me, title, message)
        End Function

        Public ReadOnly Property AppSizeDisplay As Integer
            Get
                Try
                    Return UserSettingsList.AppSizeDisplay
                Catch ex As Exception
                    logger.Error($"Erreur lors de la lecture de la propriété AppSizeDisplay : {ex.Message}")
                    Return 14 ' Retourne un entier cohérent avec le type de la propriété
                End Try
            End Get
        End Property

        Public ReadOnly Property ArrowSize As Double
            Get
                Return AppSizeDisplay * 0.8 ' Ajustez le coefficient selon vos préférences
            End Get
        End Property

        Public Sub GetAssemblyInfos()
            Try
                Dim assembly As Assembly = Assembly.GetExecutingAssembly()
                Dim assemblyName As AssemblyName = assembly.GetName()

                ' Récupérer les informations de version
                Dim assemblyVersionAttribut As Version = assembly.GetName().Version
                AssemblyVersion = assemblyVersionAttribut.ToString()

                ' Obtenir la version du fichier
                FileVersion = FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion

                ' Récupérer le titre de l'application
                Title = assemblyName.Name

                Dim copyrightAttribute As AssemblyCopyrightAttribute = DirectCast(assembly.GetCustomAttribute(GetType(AssemblyCopyrightAttribute)), AssemblyCopyrightAttribute)
                Copyright = copyrightAttribute.Copyright

                ' Récupérer la description de l'application
                Dim descriptionAttribute As AssemblyDescriptionAttribute = DirectCast(assembly.GetCustomAttribute(GetType(AssemblyDescriptionAttribute)), AssemblyDescriptionAttribute)
                Description = descriptionAttribute.Description

                ' Récupérer les informations du développeur
                Dim companyAttribute As AssemblyCompanyAttribute = DirectCast(assembly.GetCustomAttribute(GetType(AssemblyCompanyAttribute)), AssemblyCompanyAttribute)
                Company = companyAttribute.Company

                Trademark = DirectCast(assembly.GetCustomAttribute(GetType(AssemblyTrademarkAttribute)), AssemblyTrademarkAttribute).Trademark
            Catch ex As Exception
                logger.Error($"Erreur lors de la lecture des informations d'assembly : {ex.Message}")
            End Try
        End Sub

        Public Async Function CreateGitHubIssueAsync(ByVal title As String, ByVal body As String) As Task
            Dim apiUrl As String = "https://api.github.com/repos/{owner}/{repo}/issues"
            Dim owner As String = "lapinus57"
            Dim repo As String = "EyeChat"
            Dim personalAccessToken As String = Environment.GetEnvironmentVariable("GITHUB_TOKEN")

            If String.IsNullOrWhiteSpace(personalAccessToken) Then
                logger.Error("Le token d'accès personnel est manquant.")
                Throw New InvalidOperationException("Le token d'accès personnel est requis pour accéder à l'API GitHub.")
            End If

            Dim url As String = apiUrl.Replace("{owner}", owner).Replace("{repo}", repo)

            Try
                Using client As New HttpClient()
                    client.DefaultRequestHeaders.Add("User-Agent", "VotreApplication")
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {personalAccessToken}")

                    Dim requestBody As New JObject(
                        New JProperty("title", title),
                        New JProperty("body", body)
                    )

                    Dim content As HttpContent = New StringContent(requestBody.ToString(), Encoding.UTF8, "application/json")
                    Dim response As HttpResponseMessage = Await client.PostAsync(url, content)

                    If response.IsSuccessStatusCode Then
                        logger.Info("Issue created successfully!")
                        Await DisplayMessageAsync("EyeChat - GitHub", "Issue created successfully!")
                    Else
                        Dim errorDetails As String = Await response.Content.ReadAsStringAsync()
                        logger.Error($"Failed to create issue. Status Code: {response.StatusCode}, Details: {errorDetails}")
                        Await DisplayMessageAsync("EyeChat - GitHub", $"Failed to create issue. Status Code: {response.StatusCode}")
                    End If
                End Using
            Catch ex As Exception
                logger.Error($"Erreur lors de la création de l'issue GitHub : {ex.Message}")
                Throw
            End Try
        End Function

        Public Async Sub SendReport_Click(sender As Object, e As RoutedEventArgs)
            Try
                Dim view As GithubWindows = TryCast(Application.Current.MainWindow.FindName("GitHubView"), GithubWindows)
                If view IsNot Nothing Then
                    Dim descriptionText As String = view.DescriptionTextBox.Text.Trim()
                    Dim selectedCategoryItem As ComboBoxItem = TryCast(view.categoryComboBox.SelectedItem, ComboBoxItem)
                    Dim selectedCategory As String = If(selectedCategoryItem IsNot Nothing, selectedCategoryItem.Tag.ToString(), String.Empty)

                    If Not String.IsNullOrWhiteSpace(descriptionText) Then
                        Select Case selectedCategory
                            Case "problem"
                                Await CreateGitHubIssueAsync($"{UserSettingsList.UserName} a rencontré un problème", descriptionText)
                            Case "idea"
                                Await CreateGitHubIssueAsync($"{UserSettingsList.UserName} a une idée", descriptionText)
                            Case Else
                                logger.Warn("Catégorie non reconnue lors de l'envoi du rapport.")
                        End Select
                    Else
                        Await DisplayMessageAsync("Erreur", "Le champ de description est vide.")
                    End If
                End If
            Catch ex As Exception
                logger.Error($"Erreur lors de l'envoi du rapport : {ex.Message}")
            End Try
        End Sub

        Public Sub WikiButton_Click(sender As Object, e As RoutedEventArgs)
            Try
                Process.Start(New ProcessStartInfo("https://github.com/lapinus57/EyeChat/wiki") With {.UseShellExecute = True})
            Catch ex As Exception
                logger.Error($"Erreur lors de l'ouverture du lien Wiki : {ex.Message}")
                'Dim task = task.Run(Function() DisplayMessageAsync("Erreur", "Une erreur est survenue lors de l'envoi du rapport."))
            End Try
        End Sub

        Public Sub HomePageButton_Click(sender As Object, e As RoutedEventArgs)
            Try
                Process.Start(New ProcessStartInfo("https://github.com/lapinus57/EyeChat") With {.UseShellExecute = True})
            Catch ex As Exception
                logger.Error($"Erreur lors de l'ouverture de la page d'accueil : {ex.Message}")
                'Dim task = task.Run(Function() DisplayMessageAsync("Erreur", "Une erreur est survenue lors de l'envoi du rapport."))
            End Try
        End Sub


    End Class
End Namespace



