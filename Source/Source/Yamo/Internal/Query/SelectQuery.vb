Imports Yamo.Internal.Query.Metadata

Namespace Internal.Query

  ' TODO: SIP - add documentation to this class.
  Public Class SelectQuery
    Inherits Query

    Public ReadOnly Property Model As SqlModel

    Sub New(sql As SqlString, model As SqlModel)
      MyBase.New(sql)
      Me.Model = model
    End Sub

    Sub New(sql As String, parameters As List(Of SqlParameter), model As SqlModel)
      MyBase.New(sql, parameters)
      Me.Model = model
    End Sub

  End Class
End Namespace