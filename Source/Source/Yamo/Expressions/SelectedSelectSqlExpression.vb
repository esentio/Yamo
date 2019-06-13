Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  Public Class SelectedSelectSqlExpression(Of T)
    Inherits SelectSqlExpressionBase

    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    Public Function Exclude(Of TProperty)(propertyExpression As Expression(Of Func(Of T, TProperty))) As SelectedSelectSqlExpression(Of T)
      Return InternalExclude(propertyExpression)
    End Function

    Private Function InternalExclude(propertyExpression As Expression) As SelectedSelectSqlExpression(Of T)
      Me.Builder.ExcludeSelected(propertyExpression)
      Return Me
    End Function

    Public Function Distinct() As DistinctSqlExpression(Of T)
      Me.Builder.AddDistinct()
      Return New DistinctSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    Public Function ToList() As List(Of T)
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadList(Of T)(query)
    End Function

    Public Function FirstOrDefault() As T
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadFirstOrDefault(Of T)(query)
    End Function

  End Class
End Namespace