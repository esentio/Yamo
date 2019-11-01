Namespace Internal.Query

  ' TODO: SIP - add documentation to this class.
  Public Structure CreateInsertSqlStringResult

    Public ReadOnly Property SqlString As SqlString

    Public ReadOnly Property ReadDbGeneratedValues As Boolean

    Sub New(sqlString As SqlString, readDbGeneratedValues As Boolean)
      Me.SqlString = sqlString
      Me.ReadDbGeneratedValues = readDbGeneratedValues
    End Sub

  End Structure
End Namespace