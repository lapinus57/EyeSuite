Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data

Namespace Converters
    Public Class BoolToVisibilityConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim boolValue As Boolean = False
            If Boolean.TryParse(value.ToString(), boolValue) Then
                Dim isVisibleIfTrue As Boolean = parameter Is Nothing OrElse parameter.ToString().ToLower() <> "false"
                Return If((boolValue AndAlso isVisibleIfTrue) OrElse (Not boolValue AndAlso Not isVisibleIfTrue), Visibility.Visible, Visibility.Collapsed)
            End If
            Return Visibility.Collapsed
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
