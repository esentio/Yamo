Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ' TODO: SIP - add documentation to this class.
  Public MustInherit Class DeleteSqlExpressionBase
    Inherits SqlExpressionBase

    Protected ReadOnly DbContext As DbContext

    Protected Property Builder As DeleteSqlExpressionBuilder

    Protected Property Executor As QueryExecutor

    Friend Sub New(context As DbContext, builder As DeleteSqlExpressionBuilder, executor As QueryExecutor)
      Me.DbContext = context
      Me.Builder = builder
      Me.Executor = executor
    End Sub

  End Class
End Namespace