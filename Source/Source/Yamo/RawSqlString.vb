Public Structure RawSqlString

  Public ReadOnly Value As String

  Sub New(value As String)
    Me.Value = value
  End Sub

  Public Shared Widening Operator CType(value As String) As RawSqlString
    Return New RawSqlString(value)
  End Operator

  Public Shared Widening Operator CType(s As FormattableString) As RawSqlString
    Return Nothing
  End Operator

End Structure
