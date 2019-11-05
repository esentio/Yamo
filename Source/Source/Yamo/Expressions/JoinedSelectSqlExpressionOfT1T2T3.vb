Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents SQL SELECT statement from 3 tables (entities).
  ''' </summary>
  ''' <typeparam name="T1"></typeparam>
  ''' <typeparam name="T2"></typeparam>
  ''' <typeparam name="T3"></typeparam>
  Public Class JoinedSelectSqlExpression(Of T1, T2, T3)
    Inherits SelectSqlExpression(Of T1, T2, T3)

    ''' <summary>
    ''' Creates new instance of <see cref="JoinedSelectSqlExpression(Of T1, T2, T3)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    ''' <summary>
    ''' Specifies last joined entity.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="relationship"></param>
    ''' <returns></returns>
    Public Function [As](Of TProperty)(relationship As Expression(Of Func(Of T1, TProperty))) As SelectSqlExpression(Of T1, T2, T3)
      Return InternalAs(relationship)
    End Function

    ''' <summary>
    ''' Specifies last joined entity.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="relationship"></param>
    ''' <returns></returns>
    Public Function [As](Of TProperty)(relationship As Expression(Of Func(Of T2, TProperty))) As SelectSqlExpression(Of T1, T2, T3)
      Return InternalAs(relationship)
    End Function

    ''' <summary>
    ''' Specifies last joined entity.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="relationship"></param>
    ''' <returns></returns>
    Public Function [As](Of TProperty)(relationship As Expression(Of Func(Of Join(Of T1, T2), TProperty))) As SelectSqlExpression(Of T1, T2, T3)
      Return InternalAs(relationship)
    End Function

    ''' <summary>
    ''' Specifies last joined entity.
    ''' </summary>
    ''' <param name="relationship"></param>
    ''' <returns></returns>
    Private Function InternalAs(relationship As Expression) As SelectSqlExpression(Of T1, T2, T3)
      Me.Builder.SetLastJoinRelationship(relationship)
      Return New SelectSqlExpression(Of T1, T2, T3)(Me.Builder, Me.Executor)
    End Function

  End Class
End Namespace