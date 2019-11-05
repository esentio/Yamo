Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents WHERE clause in SQL DELETE statement.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  Public Class FilteredDeleteSqlExpression(Of T)
    Inherits DeleteSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="FilteredDeleteSqlExpression(Of T)"/>.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(context As DbContext, builder As DeleteSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(context, builder, executor)
    End Sub

    ''' <summary>
    ''' Executes DELETE statement or UPDATE statement that marks record(s) as (soft) deleted if expression was created with <see cref="DbContext.SoftDelete(Of T)"/> call. Returns the number of affected rows.
    ''' </summary>
    ''' <returns></returns>
    Public Function Execute() As Int32
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.Execute(query)
    End Function

  End Class
End Namespace