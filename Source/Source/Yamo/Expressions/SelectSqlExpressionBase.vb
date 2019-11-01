Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ' TODO: SIP - add documentation to this class.
  Public MustInherit Class SelectSqlExpressionBase
    Inherits SqlExpressionBase

    Protected Property Builder As SelectSqlExpressionBuilder

    Protected Property Executor As QueryExecutor

    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      Me.Builder = builder
      Me.Executor = executor
    End Sub

  End Class
End Namespace