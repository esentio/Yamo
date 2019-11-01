Namespace Internal.Query

  ' TODO: SIP - add documentation to this class.
  Public Structure SqlString

    Public ReadOnly Property Sql As String

    Public ReadOnly Property Parameters As List(Of SqlParameter)

    Sub New(sql As String)
      Me.Sql = sql
      Me.Parameters = New List(Of SqlParameter)
    End Sub

    Sub New(sql As String, parameters As List(Of SqlParameter))
      Me.Sql = sql
      Me.Parameters = parameters
    End Sub

  End Structure
End Namespace