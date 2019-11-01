Imports Yamo.Internal
Imports Yamo.Internal.Query

Namespace Expressions.Builders

  ' TODO: SIP - add documentation to this class.
  Public Class InsertSqlExpressionBuilder
    Inherits SqlExpressionBuilderBase

    Private m_UseDbIdentityAndDefaults As Boolean

    Public Sub New(context As DbContext, useDbIdentityAndDefaults As Boolean)
      MyBase.New(context)
      m_UseDbIdentityAndDefaults = useDbIdentityAndDefaults
    End Sub

    Public Function CreateQuery(obj As Object) As InsertQuery
      Dim provider = EntitySqlStringProviderCache.GetInsertProvider(Me, m_UseDbIdentityAndDefaults, obj.GetType())
      Dim result = provider(obj, m_UseDbIdentityAndDefaults)

      Return New InsertQuery(result.SqlString, result.ReadDbGeneratedValues, obj)
    End Function

  End Class
End Namespace
