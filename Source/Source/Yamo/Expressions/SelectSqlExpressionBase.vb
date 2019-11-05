Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Base class for SQL SELECT statements.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public MustInherit Class SelectSqlExpressionBase
    Inherits SqlExpressionBase

    ''' <summary>
    ''' Gets builder.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Protected Property Builder As SelectSqlExpressionBuilder

    ''' <summary>
    ''' Gets query executor.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Protected Property Executor As QueryExecutor

    ''' <summary>
    ''' Creates new instance of <see cref="SelectSqlExpressionBase"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      Me.Builder = builder
      Me.Executor = executor
    End Sub

  End Class
End Namespace