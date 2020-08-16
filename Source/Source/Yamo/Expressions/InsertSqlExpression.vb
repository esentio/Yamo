Imports Yamo.Expressions.Builders
Imports Yamo.Internal
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents SQL INSERT statement.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  Public Class InsertSqlExpression(Of T)
    Inherits SqlExpressionBase

    ''' <summary>
    ''' Gets context.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    Protected ReadOnly DbContext As DbContext

    ''' <summary>
    ''' Gets builder.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Protected Property Builder As InsertSqlExpressionBuilder

    ''' <summary>
    ''' Gets query executor.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Protected Property Executor As QueryExecutor

    ''' <summary>
    ''' Creates new instance of <see cref="InsertSqlExpression(Of T)"/>.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="tableNameOverride"></param>
    Friend Sub New(context As DbContext, Optional tableNameOverride As String = Nothing)
      Me.DbContext = context
      Me.Builder = New InsertSqlExpressionBuilder(context, tableNameOverride)
      Me.Executor = New QueryExecutor(context)
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