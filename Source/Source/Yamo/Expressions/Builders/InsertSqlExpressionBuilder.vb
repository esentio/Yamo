Imports Yamo.Internal
Imports Yamo.Internal.Query

Namespace Expressions.Builders

  ''' <summary>
  ''' Represents insert SQL expression builder.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class InsertSqlExpressionBuilder
    Inherits SqlExpressionBuilderBase

    ''' <summary>
    ''' Creates new instance of <see cref="InsertSqlExpressionBuilder"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="context"></param>
    Public Sub New(context As DbContext)
      MyBase.New(context)
    End Sub

    ''' <summary>
    ''' Creates query.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <param name="useDbIdentityAndDefaults"></param>
    ''' <returns></returns>
    Public Function CreateQuery(obj As Object, useDbIdentityAndDefaults As Boolean) As InsertQuery
      Dim provider = EntitySqlStringProviderCache.GetInsertProvider(Me, useDbIdentityAndDefaults, obj.GetType())
      Dim result = provider(obj, useDbIdentityAndDefaults)

      Return New InsertQuery(result.SqlString, result.ReadDbGeneratedValues, obj)
    End Function

  End Class
End Namespace
