Imports Yamo.Internal.Query.Metadata

Namespace Internal.Query

  Public Class CustomSelectQuery
    Inherits SelectQuery

    Public ReadOnly Property Entities As CustomSelectSqlEntity()

    Sub New(sql As String, parameters As List(Of SqlParameter), model As SqlModel, customEntities As CustomSelectSqlEntity())
      MyBase.New(sql, parameters, model)
      Me.Entities = customEntities
    End Sub

  End Class
End Namespace