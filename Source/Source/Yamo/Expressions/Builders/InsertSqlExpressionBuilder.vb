Imports Yamo.Internal
Imports Yamo.Internal.Query

Namespace Expressions.Builders

  Public Class InsertSqlExpressionBuilder
    Inherits SqlExpressionBuilderBase

    Public Sub New(context As DbContext)
      MyBase.New(context)
    End Sub

    Public Function CreateQuery(obj As Object, Optional useDbIdentityAndDefaults As Boolean = True) As InsertQuery
      Dim provider = EntitySqlStringProviderCache.GetInsertProvider(Me, useDbIdentityAndDefaults, obj.GetType())
      Dim result = provider(obj, useDbIdentityAndDefaults)

      Return New InsertQuery(result.SqlString, result.ReadDbGeneratedValues, obj)
    End Function

  End Class
End Namespace
