﻿Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  Public Class JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)
    Inherits SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)

    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    Public Function [As](Of TProperty)(relationship As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10), TProperty))) As SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)
      Return InternalAs(relationship)
    End Function

    Private Function InternalAs(relationship As Expression) As SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)
      Me.Builder.SetLastJoinRelationship(relationship)
      Return New SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)(Me.Builder, Me.Executor)
    End Function

  End Class
End Namespace