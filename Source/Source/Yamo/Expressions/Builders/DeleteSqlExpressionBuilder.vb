Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports System.Text
Imports Yamo.Internal
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata

Namespace Expressions.Builders

  ''' <summary>
  ''' Represents delete SQL expression builder.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class DeleteSqlExpressionBuilder
    Inherits SqlExpressionBuilderBase

    ''' <summary>
    ''' Stores SQL model.
    ''' </summary>
    Private m_Model As DeleteSqlModel

    ''' <summary>
    ''' Stores whether soft delete is used.
    ''' </summary>
    Private m_SoftDelete As Boolean

    ''' <summary>
    ''' Stores table name override.
    ''' </summary>
    Private m_TableNameOverride As String

    ''' <summary>
    ''' Stores table hints.
    ''' </summary>
    Private m_TableHints As String

    ''' <summary>
    ''' Stores SQL expression visitor.
    ''' </summary>
    Private m_Visitor As SqlExpressionVisitor

    ''' <summary>
    ''' Stores where expressions.
    ''' </summary>
    Private m_WhereExpressions As List(Of String)

    ''' <summary>
    ''' Stores parameters.
    ''' </summary>
    Private m_Parameters As List(Of SqlParameter)

    ''' <summary>
    ''' Stores paarmeter index shift.
    ''' </summary>
    Private m_ParameterIndexShift As Int32?

    ''' <summary>
    ''' Creates new instance of <see cref="DeleteSqlExpressionBuilder"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="mainEntityType"></param>
    ''' <param name="softDelete"></param>
    ''' <param name="tableNameOverride"></param>
    Public Sub New(<DisallowNull> context As DbContext, <DisallowNull> mainEntityType As Type, softDelete As Boolean, tableNameOverride As String)
      MyBase.New(context)
      m_Model = New DeleteSqlModel(Me.DbContext.Model, GetMainEntity(mainEntityType))
      m_SoftDelete = softDelete
      m_TableNameOverride = tableNameOverride
      m_TableHints = Nothing
      m_Visitor = New SqlExpressionVisitor(Me, m_Model)
      m_WhereExpressions = New List(Of String)
      m_Parameters = New List(Of SqlParameter)
      m_ParameterIndexShift = Nothing ' lazy assigned
    End Sub

    ''' <summary>
    ''' Sets table hint(s).<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="tableHints"></param>
    Public Sub SetTableHints(<DisallowNull> tableHints As String)
      m_TableHints = tableHints
    End Sub

    ''' <summary>
    ''' Adds where.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="predicate"></param>
    Public Sub AddWhere(<DisallowNull> predicate As Expression)
      If Not m_ParameterIndexShift.HasValue Then
        If m_SoftDelete Then
          m_ParameterIndexShift = m_Model.MainEntity.Entity.GetSetOnDeleteProperties().Count
        Else
          m_ParameterIndexShift = 0
        End If
      End If

      Dim result = m_Visitor.Translate(predicate, ExpressionTranslateMode.Condition, {0}, m_Parameters.Count + m_ParameterIndexShift.Value, False, False)
      m_WhereExpressions.Add(result.Sql)
      m_Parameters.AddRange(result.Parameters)
    End Sub

    ''' <summary>
    ''' Adds where.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    Public Sub AddWhere(<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object)
      If parameters Is Nothing OrElse parameters.Length = 0 Then
        m_WhereExpressions.Add(predicate)
      Else
        Dim sql = ConvertToSqlString(predicate, parameters, m_Parameters.Count)
        m_WhereExpressions.Add(sql.Sql)
        m_Parameters.AddRange(sql.Parameters)
      End If
    End Sub

    ''' <summary>
    ''' Creates query.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function CreateQuery() As Query
      If m_SoftDelete Then
        Return CreateSoftDeleteQuery()
      Else
        Return CreateDeleteQuery()
      End If
    End Function

    ''' <summary>
    ''' Creates delete query.
    ''' </summary>
    ''' <returns></returns>
    Private Function CreateDeleteQuery() As Query
      Dim sql = New StringBuilder

      sql.Append("DELETE FROM ")

      If m_TableNameOverride Is Nothing Then
        Dim entity = m_Model.MainEntity.Entity
        Me.DialectProvider.Formatter.AppendIdentifier(sql, entity.TableName, entity.Schema)
      Else
        sql.Append(m_TableNameOverride)
      End If

      If m_TableHints IsNot Nothing Then
        sql.Append(" ")
        sql.Append(m_TableHints)
      End If

      sql.AppendLine()

      If Not m_WhereExpressions.Count = 0 Then
        sql.Append(" WHERE ")
        Helpers.Text.AppendJoin(sql, " AND ", m_WhereExpressions)
      End If

      Return New Query(sql.ToString(), m_Parameters)
    End Function

    ''' <summary>
    ''' Creates soft delete query.
    ''' </summary>
    ''' <returns></returns>
    Private Function CreateSoftDeleteQuery() As Query
      Dim entity = m_Model.MainEntity.Entity

      Dim table = If(m_TableNameOverride, Me.DialectProvider.Formatter.CreateIdentifier(entity.TableName, entity.Schema))

      If m_TableHints IsNot Nothing Then
        table = table & " " & m_TableHints
      End If

      Dim getter = EntityAutoFieldsGetterCache.GetOnDeleteGetter(m_Model.Model, entity.EntityType)
      Dim values = getter(Me.DbContext)

      Dim provider = EntitySqlStringProviderCache.GetSoftDeleteWithoutConditionProvider(Me, entity.EntityType)
      Dim sqlString = provider(table, values)

      Dim sql = New StringBuilder
      Dim parameters As IReadOnlyList(Of SqlParameter)

      sql.Append(sqlString.Sql)

      If Not m_WhereExpressions.Count = 0 Then
        sql.Append(" WHERE ")
        Helpers.Text.AppendJoin(sql, " AND ", m_WhereExpressions)

        Dim params = New List(Of SqlParameter)(sqlString.Parameters.Count + m_Parameters.Count)
        params.AddRange(sqlString.Parameters)
        params.AddRange(m_Parameters)
        parameters = params
      Else
        parameters = sqlString.Parameters
      End If

      Return New Query(sql.ToString(), parameters)
    End Function

    ''' <summary>
    ''' Creates delete query.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Public Function CreateDeleteQuery(<DisallowNull> obj As Object) As Query
      Dim table As String

      If m_TableNameOverride Is Nothing Then
        Dim entity = m_Model.MainEntity.Entity
        table = Me.DialectProvider.Formatter.CreateIdentifier(entity.TableName, entity.Schema)
      Else
        table = m_TableNameOverride
      End If

      If m_TableHints IsNot Nothing Then
        table = table & " " & m_TableHints
      End If

      Dim provider = EntitySqlStringProviderCache.GetDeleteProvider(Me, obj.GetType())
      Dim sqlString = provider(obj, table)

      Return New Query(sqlString)
    End Function

    ''' <summary>
    ''' Creates soft delete query.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Public Function CreateSoftDeleteQuery(<DisallowNull> obj As Object) As Query
      Dim table As String

      If m_TableNameOverride Is Nothing Then
        Dim entity = m_Model.MainEntity.Entity
        table = Me.DialectProvider.Formatter.CreateIdentifier(entity.TableName, entity.Schema)
      Else
        table = m_TableNameOverride
      End If

      If m_TableHints IsNot Nothing Then
        table = table & " " & m_TableHints
      End If

      Dim provider = EntitySqlStringProviderCache.GetSoftDeleteProvider(Me, obj.GetType())
      Dim sqlString = provider(obj, table)

      Return New Query(sqlString)
    End Function

  End Class
End Namespace
