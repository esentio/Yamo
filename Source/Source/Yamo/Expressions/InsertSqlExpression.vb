Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Expressions.Builders
Imports Yamo.Internal
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents SQL INSERT statement.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  Public Class InsertSqlExpression(Of T)
    Inherits InsertSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="InsertSqlExpression(Of T)"/>.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="tableNameOverride"></param>
    Friend Sub New(context As DbContext, Optional tableNameOverride As String = Nothing)
      MyBase.New(context, New InsertSqlExpressionBuilder(context, tableNameOverride), New QueryExecutor(context))
    End Sub

    ''' <summary>
    ''' Adds table hint(s).
    ''' </summary>
    ''' <param name="tableHints"></param>
    ''' <returns></returns>
    Public Function WithHints(<DisallowNull> tableHints As String) As WithHintsInsertSqlExpression(Of T)
      Me.Builder.SetTableHints(tableHints)
      Return New WithHintsInsertSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Executes INSERT statement and returns the number of affected rows.
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <param name="useDbIdentityAndDefaults"></param>
    ''' <param name="setAutoFields"></param>
    ''' <returns></returns>
    Public Function Execute(<DisallowNull> obj As T, Optional useDbIdentityAndDefaults As Boolean = True, Optional setAutoFields As Boolean = True) As Int32
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