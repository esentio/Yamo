Imports Yamo.Expressions.Builders
Imports Yamo.Internal
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents SQL INSERT statement with defined table hints.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  Public Class WithHintsInsertSqlExpression(Of T)
    Inherits InsertSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="WithHintsInsertSqlExpression(Of T)"/>.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(context As DbContext, builder As InsertSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(context, builder, executor)
    End Sub

    ''' <summary>
    ''' Executes INSERT statement and returns the number of affected rows.
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <param name="useDbIdentityAndDefaults"></param>
    ''' <param name="setAutoFields"></param>
    ''' <returns></returns>
    Public Function Execute(obj As T, Optional useDbIdentityAndDefaults As Boolean = True, Optional setAutoFields As Boolean = True) As Int32
      If setAutoFields Then
        Dim setter = EntityAutoFieldsSetterCache.GetOnInsertSetter(Me.DbContext.Model, GetEntityType(obj))
        setter(obj, Me.DbContext)
      End If

      Dim query = Me.Builder.CreateQuery(obj, useDbIdentityAndDefaults)
      Dim affectedRows = Me.Executor.ExecuteInsert(query)
      ResetDbPropertyModifiedTracking(obj)
      Return affectedRows
    End Function

  End Class
End Namespace