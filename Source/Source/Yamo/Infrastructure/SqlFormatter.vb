Imports System.Text

Namespace Infrastructure

  ''' <summary>
  ''' SQL formatter.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SqlFormatter

    ''' <summary>
    ''' Gets whether LIKE wildcards are in a parameter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Overridable ReadOnly Property LikeWildcardsInParameter As Boolean
      Get
        Return False
      End Get
    End Property

    ''' <summary>
    ''' Gets string concatenation operator.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Overridable ReadOnly Property StringConcatenationOperator As String
      Get
        Return "+"
      End Get
    End Property

    ''' <summary>
    ''' Gets constant empty string value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Function GetConstantEmptyStringValue() As String
      Return "''"
    End Function

    ''' <summary>
    ''' Gets constant <see cref="Boolean"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Overridable Function GetConstantValue(value As Boolean) As String
      Return If(value, "1", "0")
    End Function

    ''' <summary>
    ''' Gets constant <see cref="Int16"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Overridable Function GetConstantValue(value As Int16) As String
      Return value.ToString(Globalization.CultureInfo.InvariantCulture)
    End Function

    ''' <summary>
    ''' Gets constant <see cref="Int32"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Overridable Function GetConstantValue(value As Int32) As String
      Return value.ToString(Globalization.CultureInfo.InvariantCulture)
    End Function

    ''' <summary>
    ''' Gets constant <see cref="Int64"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Overridable Function GetConstantValue(value As Int64) As String
      Return value.ToString(Globalization.CultureInfo.InvariantCulture)
    End Function

    ''' <summary>
    ''' Gets constant <see cref="Decimal"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Overridable Function GetConstantValue(value As Decimal) As String
      Return value.ToString(Globalization.CultureInfo.InvariantCulture)
    End Function

    ''' <summary>
    ''' Gets constant <see cref="Single"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Overridable Function GetConstantValue(value As Single) As String
      Return value.ToString(Globalization.CultureInfo.InvariantCulture)
    End Function

    ''' <summary>
    ''' Gets constant <see cref="Double"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Overridable Function GetConstantValue(value As Double) As String
      Return value.ToString(Globalization.CultureInfo.InvariantCulture)
    End Function

    ''' <summary>
    ''' Creates a parameter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    Public Overridable Function CreateParameter(name As String) As String
      Return "@" & name
    End Function

    ''' <summary>
    ''' Creates an identifier.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    Public Overridable Function CreateIdentifier(name As String) As String
      Return "[" & name & "]"
    End Function

    ''' <summary>
    ''' Appends an identifier.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="name"></param>
    Public Overridable Sub AppendIdentifier(sql As StringBuilder, name As String)
      sql.Append("[")
      sql.Append(name)
      sql.Append("]")
    End Sub

  End Class
End Namespace