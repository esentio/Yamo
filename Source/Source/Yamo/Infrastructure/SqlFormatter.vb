Namespace Infrastructure

  Public Class SqlFormatter

    Public Overridable ReadOnly Property LikeWildcardsInParameter As Boolean
      Get
        Return False
      End Get
    End Property

    Public Overridable ReadOnly Property StringConcatenationOperator As String
      Get
        Return "+"
      End Get
    End Property

    Public Overridable Function GetConstantEmptyStringValue() As String
      Return "''"
    End Function

    Public Overridable Function GetConstantValue(value As Boolean) As String
      Return If(value, "1", "0")
    End Function

    Public Overridable Function GetConstantValue(value As Int16) As String
      Return value.ToString(Globalization.CultureInfo.InvariantCulture)
    End Function

    Public Overridable Function GetConstantValue(value As Int32) As String
      Return value.ToString(Globalization.CultureInfo.InvariantCulture)
    End Function

    Public Overridable Function GetConstantValue(value As Int64) As String
      Return value.ToString(Globalization.CultureInfo.InvariantCulture)
    End Function

    Public Overridable Function GetConstantValue(value As Decimal) As String
      Return value.ToString(Globalization.CultureInfo.InvariantCulture)
    End Function

    Public Overridable Function GetConstantValue(value As Single) As String
      Return value.ToString(Globalization.CultureInfo.InvariantCulture)
    End Function

    Public Overridable Function GetConstantValue(value As Double) As String
      Return value.ToString(Globalization.CultureInfo.InvariantCulture)
    End Function

    Public Overridable Function CreateParameter(name As String) As String
      Return $"@{name}"
    End Function

    Public Overridable Function CreateIdentifier(name As String) As String
      Return $"[{name}]"
    End Function

  End Class
End Namespace