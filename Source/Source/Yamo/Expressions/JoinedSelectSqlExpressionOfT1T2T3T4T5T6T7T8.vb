Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ' TODO: SIP - add documentation to this class.
  Public Class JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8)
    Inherits SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8)

    ''' <summary>
    ''' Creates new instance of <see cref="JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    Public Function [As](Of TProperty)(relationship As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7), TProperty))) As SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8)
      Return InternalAs(relationship)
    End Function

    Private Function InternalAs(relationship As Expression) As SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8)
      Me.Builder.SetLastJoinRelationship(relationship)
      Return New SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8)(Me.Builder, Me.Executor)
    End Function

  End Class
End Namespace