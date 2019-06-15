''' <summary>
''' Helper class that enables to pass raw SQL string to SQL expression builder methods.
''' </summary>
Public Structure RawSqlString

  ''' <summary>
  ''' Gets string value.
  ''' </summary>
  Public ReadOnly Value As String

  ''' <summary>
  ''' Creates new instance of <see cref="RawSqlString"/>.
  ''' </summary>
  ''' <param name="value"></param>
  Sub New(value As String)
    Me.Value = value
  End Sub

  ''' <summary>
  ''' Widening operator for <see cref="String"/> type.
  ''' </summary>
  ''' <param name="value"></param>
  ''' <returns></returns>
  Public Shared Widening Operator CType(value As String) As RawSqlString
    Return New RawSqlString(value)
  End Operator

  ''' <summary>
  ''' Widening operator for <see cref="FormattableString"/> type.
  ''' </summary>
  ''' <param name="s"></param>
  ''' <returns></returns>
  Public Shared Widening Operator CType(s As FormattableString) As RawSqlString
    Return Nothing
  End Operator

End Structure
