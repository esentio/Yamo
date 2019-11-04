Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ' TODO: SIP - add documentation to this class.
  Public Class SelectedSelectSqlExpression(Of T)
    Inherits SelectSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="SelectedSelectSqlExpression(Of T)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT statement.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(propertyExpression As Expression(Of Func(Of T, TProperty))) As SelectedSelectSqlExpression(Of T)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT statement.
    ''' </summary>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Private Function InternalExclude(propertyExpression As Expression) As SelectedSelectSqlExpression(Of T)
      Me.Builder.ExcludeSelected(propertyExpression)
      Return Me
    End Function

    ''' <summary>
    ''' Adds DISTINCT statement.
    ''' </summary>
    ''' <returns></returns>
    Public Function Distinct() As DistinctSqlExpression(Of T)
      Me.Builder.AddDistinct()
      Return New DistinctSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Executes SQL query and returns list of records.
    ''' </summary>
    ''' <returns></returns>
    Public Function ToList() As List(Of T)
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadList(Of T)(query)
    End Function

    ''' <summary>
    ''' Executes SQL query and returns first record or default.
    ''' </summary>
    ''' <returns></returns>
    Public Function FirstOrDefault() As T
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadFirstOrDefault(Of T)(query)
    End Function

  End Class
End Namespace