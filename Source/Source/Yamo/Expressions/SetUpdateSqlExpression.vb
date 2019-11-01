Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ' TODO: SIP - add documentation to this class.
  Public Class SetUpdateSqlExpression(Of T)
    Inherits UpdateSqlExpressionBase

    Friend Sub New(context As DbContext, builder As UpdateSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(context, builder, executor)
    End Sub

    Public Function [Set](action As Expression(Of Action(Of T))) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(action)
      Return Me
    End Function

    Public Function [Set](Of TProperty)(keySelector As Expression(Of Func(Of T, TProperty)), value As TProperty) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(keySelector, value)
      Return New SetUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    Public Function [Set](Of TProperty)(keySelector As Expression(Of Func(Of T, TProperty)), valueSelector As Expression(Of Func(Of T, TProperty))) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(keySelector, valueSelector)
      Return New SetUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    Public Function [Set](predicate As Expression(Of Func(Of T, FormattableString))) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(predicate)
      Return Me
    End Function

    Public Function [Set](predicate As String) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(predicate)
      Return Me
    End Function

    Public Function SetNull(Of TProperty)(keySelector As Expression(Of Func(Of T, TProperty))) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(keySelector, DirectCast(Nothing, Object))
      Return New SetUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    Public Function Where(predicate As Expression(Of Func(Of T, Boolean))) As FilteredUpdateSqlExpression(Of T)
      Me.Builder.AddWhere(predicate)
      Return New FilteredUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    Public Function Where(predicate As Expression(Of Func(Of T, FormattableString))) As FilteredUpdateSqlExpression(Of T)
      Me.Builder.AddWhere(predicate)
      Return New FilteredUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    Public Function Where(predicate As String) As FilteredUpdateSqlExpression(Of T)
      Me.Builder.AddWhere(predicate)
      Return New FilteredUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    Public Function Execute() As Int32
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.Execute(query)
    End Function

  End Class
End Namespace