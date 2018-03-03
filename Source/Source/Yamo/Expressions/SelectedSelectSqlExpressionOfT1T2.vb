Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  Public Class SelectedSelectSqlExpression(Of T1, T2)
    Inherits SelectSqlExpressionBase

    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    Public Function Exclude(Of TProperty)(propertyExpression As Expression(Of Func(Of T1, TProperty))) As SelectedSelectSqlExpression(Of T1, T2)
      Return InternalExclude(propertyExpression)
    End Function

    Public Function Exclude(Of TProperty)(propertyExpression As Expression(Of Func(Of T2, TProperty))) As SelectedSelectSqlExpression(Of T1, T2)
      Return InternalExclude(propertyExpression)
    End Function

    Public Function Exclude(Of TProperty)(propertyExpression As Expression(Of Func(Of Join(Of T1, T2), TProperty))) As SelectedSelectSqlExpression(Of T1, T2)
      Return InternalExclude(propertyExpression)
    End Function

    Public Function ExcludeT2() As SelectedSelectSqlExpression(Of T1, T2)
      Return InternalExclude(1)
    End Function

    Private Function InternalExclude(propertyExpression As Expression) As SelectedSelectSqlExpression(Of T1, T2)
      Me.Builder.ExcludeSelected(propertyExpression)
      Return Me
    End Function

    Private Function InternalExclude(entityIndex As Int32) As SelectedSelectSqlExpression(Of T1, T2)
      Me.Builder.ExcludeSelected(entityIndex)
      Return Me
    End Function

    Public Function ToList() As List(Of T1)
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadList(Of T1)(query)
    End Function

    Public Function FirstOrDefault() As T1
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadFirstOrDefault(Of T1)(query)
    End Function

  End Class
End Namespace