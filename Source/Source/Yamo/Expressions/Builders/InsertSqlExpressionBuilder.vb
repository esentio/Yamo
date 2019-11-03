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
    ''' Stores wheter database identity and defaults are used.
    ''' </summary>
    Private m_UseDbIdentityAndDefaults As Boolean

    ''' <summary>
    ''' Creates new instance of <see cref="InsertSqlExpressionBuilder"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="useDbIdentityAndDefaults"></param>
    Public Sub New(context As DbContext, useDbIdentityAndDefaults As Boolean)
      MyBase.New(context)
      m_UseDbIdentityAndDefaults = useDbIdentityAndDefaults
    End Sub

    ''' <summary>
    ''' Creates query.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Public Function CreateQuery(obj As Object) As InsertQuery
      Dim provider = EntitySqlStringProviderCache.GetInsertProvider(Me, m_UseDbIdentityAndDefaults, obj.GetType())
      Dim result = provider(obj, m_UseDbIdentityAndDefaults)

      Return New InsertQuery(result.SqlString, result.ReadDbGeneratedValues, obj)
    End Function

  End Class
End Namespace
