Imports Yamo.Internal
Imports Yamo.Internal.Query

Namespace Expressions.Builders

  Public Class UpdateSqlExpressionBuilder
    Inherits SqlExpressionBuilderBase

    Public Sub New(context As DbContext)
      MyBase.New(context)
    End Sub

    Public Function CreateQuery(obj As Object) As Query
      Dim provider = EntitySqlStringProviderCache.GetUpdateProvider(Me, obj.GetType())
      Dim sqlString = provider(obj)

      Return New Query(sqlString)
    End Function

  End Class
End Namespace