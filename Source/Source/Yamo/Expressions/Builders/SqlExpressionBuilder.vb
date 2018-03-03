Imports Yamo.Internal.Query

Namespace Expressions.Builders

  Public Class SqlExpressionBuilder
    Inherits SqlExpressionBuilderBase

    Public Sub New(context As DbContext)
      MyBase.New(context)
    End Sub

    Public Function CreateQuery(sql As FormattableString) As Query
      Return New Query(ConvertToSqlString(sql, 0))
    End Function

    Public Function CreateQuery(sql As RawSqlString) As Query
      Return New Query(New SqlString(sql.Value))
    End Function

  End Class
End Namespace
