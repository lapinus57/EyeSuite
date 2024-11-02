Imports System.Text.RegularExpressions
Imports log4net

Namespace Utilities
    Public Class PatientStringHelper

        Private Shared ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

        Public Shared Sub ExtractInfoFromInput(inputText As String, ByRef patientTitre As String, ByRef patientNom As String, ByRef patientPrenom As String)
            Try
                ' Modèle pour extraire le titre, le nom et le prénom
                Dim patternTitleName As String = "^(?i)(Mr|Mme|Mlle|Enfant)?\s*(?<name>[^\d\s\-]+)\s+(?<firstName>[^\s]+)"

                Dim matchTitleName As Match = Regex.Match(inputText, patternTitleName)

                If matchTitleName.Success Then
                    Dim titre As String = matchTitleName.Groups(1).Value
                    Dim nom As String = matchTitleName.Groups("name").Value
                    Dim prenom As String = matchTitleName.Groups("firstName").Value

                    ' Mettre en majuscules le nom du patient
                    patientNom = nom.ToUpper()

                    ' Si titre est vide, définir "Iel" par défaut
                    If String.IsNullOrWhiteSpace(titre) Then
                        patientTitre = ""
                    Else
                        ' Utiliser le titre tel quel (en minuscules)
                        patientTitre = titre
                    End If

                    ' Mettre en majuscules seulement la première lettre du prénom
                    If Not String.IsNullOrEmpty(prenom) Then
                        patientPrenom = Char.ToUpper(prenom(0)) + prenom.Substring(1)
                    Else
                        patientPrenom = ""
                    End If

                    ' Enregistre des informations de débogage
                    logger.Debug("Extraction des informations du texte d'entrée réussie.")
                    logger.Debug("Titre : " & patientTitre)
                    logger.Debug("Nom : " & patientNom)
                    logger.Debug("Prénom : " & patientPrenom)
                Else
                    ' Aucune correspondance trouvée
                    patientTitre = ""
                    patientNom = ""
                    patientPrenom = ""
                End If
            Catch ex As Exception
                logger.Error("Erreur lors de l'extraction des informations du texte d'entrée : " & ex.Message)
                patientTitre = ""
                patientNom = ""
                patientPrenom = ""
            End Try
            End Sub
        End Class
    End Namespace

