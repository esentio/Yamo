Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ' TODO: SIP - add documentation to this class.
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

    Public Function Execute() As Int32
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.Execute(query)
    End Function

  End Class
End Namespace