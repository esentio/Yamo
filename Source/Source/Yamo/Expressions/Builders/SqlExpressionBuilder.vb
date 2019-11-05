Imports Yamo.Internal.Query

Namespace Expressions.Builders

  ''' <summary>
  ''' Represents SQL expression builder.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SqlExpressionBuilder
    Inherits SqlExpressionBuilderBase

    ''' <summary>
    ''' Creates new instance of <see cref="SqlExpressionBuilder"/>.<br/>
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
    ''' <param name="sql"></param>
    ''' <returns></returns>
    Public Function CreateQuery(sql As FormattableString) As Query
      Return New Query(ConvertToSqlString(sql, 0))
    End Function

    ''' <summary>
    ''' Creates query.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    Public Function CreateQuery(sql As RawSqlString) As Query
      Return New Query(New SqlString(sql.Value))
    End Function

  End Class
End Namespace
