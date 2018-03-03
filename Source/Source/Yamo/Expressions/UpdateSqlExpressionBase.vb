﻿Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  Public Class UpdateSqlExpressionBase
    Inherits SqlExpressionBase

    Protected ReadOnly DbContext As DbContext

    Protected Property Builder As UpdateSqlExpressionBuilder

    Protected Property Executor As QueryExecutor

    Friend Sub New(context As DbContext, builder As UpdateSqlExpressionBuilder, executor As QueryExecutor)
      Me.DbContext = context
      Me.Builder = builder
      Me.Executor = executor
    End Sub

  End Class
End Namespace
