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
    ''' Stores whether auto fields should be set.
    ''' </summary>
    Private m_SetAutoFields As Boolean

    ''' <summary>
    ''' Creates new instance of <see cref="InsertSqlExpression(Of T)"/>.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="useDbIdentityAndDefaults"></param>
    ''' <param name="setAutoFields"></param>
    Friend Sub New(context As DbContext, useDbIdentityAndDefaults As Boolean, setAutoFields As Boolean)
      Me.DbContext = context
      Me.Builder = New InsertSqlExpressionBuilder(context, useDbIdentityAndDefaults)
      Me.Executor = New QueryExecutor(context)
      m_SetAutoFields = setAutoFields
    End Sub

    ''' <summary>
    ''' Executes INSERT statement and returns the number of affected rows.
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Public Function Insert(obj As T) As Int32
      If m_SetAutoFields Then
        Dim setter = EntityAutoFieldsSetterCache.GetOnInsertSetter(Me.DbContext.Model, GetEntityType(obj))
        setter(obj, Me.DbContext)
      End If

      Dim query = Me.Builder.CreateQuery(obj)
      Dim affectedRows = Me.Executor.ExecuteInsert(query)
      ResetDbPropertyModifiedTracking(obj)
      Return affectedRows
    End Function

  End Class
End Namespace