Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents WHERE clause in SQL UPDATE statement.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  Public Class FilteredUpdateSqlExpression(Of T)
    Inherits UpdateSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="FilteredUpdateSqlExpression(Of T)"/>.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(context As DbContext, builder As UpdateSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(context, builder, executor)
    End Sub

    ''' <summary>
    ''' Executes UPDATE statement and returns the number of affected rows.
    ''' </summary>
    ''' <param name="setAutoFields"></param>
    ''' <returns></returns>
    Public Function Execute(Optional setAutoFields As Boolean = True) As Int32
      Dim query = Me.Builder.CreateQuery(setAutoFields)
      Return Me.Executor.Execute(query)
    End Function

  End Class
End Namespace