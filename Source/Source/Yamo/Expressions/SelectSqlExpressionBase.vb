Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ' TODO: SIP - add documentation to this class.
  Public MustInherit Class SelectSqlExpressionBase
    Inherits SqlExpressionBase

    Protected Property Builder As SelectSqlExpressionBuilder

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