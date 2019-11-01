Imports System.Data

Namespace Internal.Query

  ' TODO: SIP - add documentation to this class.
  Public Structure SqlParameter

    Public ReadOnly Property Name As String

    Public ReadOnly Property Value As Object

    Public ReadOnly Property DbType As DbType?

    Sub New(name As String, value As Object)
      Me.Name = name
      Me.Value = value
      Me.DbType = Nothing
    End Sub

    Sub New(name As String, value As Object, dbType As DbType)
      Me.Name = name
      Me.Value = value
      Me.DbType = dbType
    End Sub

  End Structure
End Namespace