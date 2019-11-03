Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ' TODO: SIP - add documentation to this class.
  Public Class SelectedSelectSqlExpression(Of T1, T2, T3, T4)
    Inherits SelectSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="SelectedSelectSqlExpression(Of T1, T2, T3, T4)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    Public Function Exclude(Of TProperty)(propertyExpression As Expression(Of Func(Of T1, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalExclude(propertyExpression)
    End Function

    Public Function Exclude(Of TProperty)(propertyExpression As Expression(Of Func(Of T2, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalExclude(propertyExpression)
    End Function

    Public Function Exclude(Of TProperty)(propertyExpression As Expression(Of Func(Of T3, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalExclude(propertyExpression)
    End Function

    Public Function Exclude(Of TProperty)(propertyExpression As Expression(Of Func(Of T4, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalExclude(propertyExpression)
    End Function

    Public Function Exclude(Of TProperty)(propertyExpression As Expression(Of Func(Of Join(Of T1, T2, T3, T4), TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalExclude(propertyExpression)
    End Function

    Public Function ExcludeT2() As SelectedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalExclude(1)
    End Function

    Public Function ExcludeT3() As SelectedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalExclude(2)
    End Function

    Public Function ExcludeT4() As SelectedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalExclude(3)
    End Function

    Private Function InternalExclude(propertyExpression As Expression) As SelectedSelectSqlExpression(Of T1, T2, T3, T4)
      Me.Builder.ExcludeSelected(propertyExpression)
      Return Me
    End Function

    Private Function InternalExclude(entityIndex As Int32) As SelectedSelectSqlExpression(Of T1, T2, T3, T4)
      Me.Builder.ExcludeSelected(entityIndex)
      Return Me
    End Function

    ''' <summary>
    ''' Adds DISTINCT statement.
    ''' </summary>
    ''' <returns></returns>
    Public Function Distinct() As DistinctSqlExpression(Of T1)
      Me.Builder.AddDistinct()
      Return New DistinctSqlExpression(Of T1)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Executes SQL query and returns list of records.
    ''' </summary>
    ''' <returns></returns>
    Public Function ToList() As List(Of T1)
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadList(Of T1)(query)
    End Function

    ''' <summary>
    ''' Executes SQL query and returns first record or default.
    ''' </summary>
    ''' <returns></returns>
    Public Function FirstOrDefault() As T1
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadFirstOrDefault(Of T1)(query)
    End Function

  End Class
End Namespace