﻿Imports System.Linq.Expressions
Imports System.Text
Imports Yamo.Internal
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata

Namespace Expressions.Builders

  ''' <summary>
  ''' Represents update SQL expression builder.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class UpdateSqlExpressionBuilder
    Inherits SqlExpressionBuilderBase

    ''' <summary>
    ''' Stores table name override.
    ''' </summary>
    Private m_TableNameOverride As String

    ''' <summary>
    ''' Stores SQL model.
    ''' </summary>
    Private m_Model As SqlModel

    ''' <summary>
    ''' Stores SQL expression visitor.
    ''' </summary>
    Private m_Visitor As SqlExpressionVisitor

    ''' <summary>
    ''' Stores set expressions.
    ''' </summary>
    Private m_SetExpressions As List(Of String)

    ''' <summary>
    ''' Stores where expressions.
    ''' </summary>
    Private m_WhereExpressions As List(Of String)

    ''' <summary>
    ''' Stores parameters.
    ''' </summary>
    Private m_Parameters As List(Of SqlParameter)

    ''' <summary>
    ''' Creates new instance of <see cref="UpdateSqlExpressionBuilder"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="context"></param>
    Public Sub New(context As DbContext, tableNameOverride As String)
      MyBase.New(context)
      m_TableNameOverride = tableNameOverride
      m_Model = New SqlModel(Me.DbContext.Model)
      m_Visitor = New SqlExpressionVisitor(Me, m_Model)
      m_SetExpressions = New List(Of String)
      m_WhereExpressions = New List(Of String)
      m_Parameters = New List(Of SqlParameter)
    End Sub

    ''' <summary>
    ''' Sets main table.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    Public Sub SetMainTable(Of T)()
      m_Model.SetMainTable(Of T)()
    End Sub

    ''' <summary>
    ''' Adds set.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="predicate"></param>
    Public Sub AddSet(predicate As Expression)
      Dim result = m_Visitor.Translate(predicate, ExpressionParametersType.Entities, {0}, m_Parameters.Count, False, False)
      m_SetExpressions.Add(result.Sql)
      m_Parameters.AddRange(result.Parameters)
    End Sub

    ''' <summary>
    ''' Adds set.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="value"></param>
    Public Sub AddSet(predicate As Expression, value As Object)
      Dim result = m_Visitor.Translate(predicate, ExpressionParametersType.Entities, {0}, m_Parameters.Count, False, False)
      m_Parameters.AddRange(result.Parameters)
      Dim parameterName = CreateParameter(m_Parameters.Count)
      m_SetExpressions.Add($"{result.Sql} = {parameterName}")
      m_Parameters.Add(New SqlParameter(parameterName, value))
    End Sub

    ''' <summary>
    ''' Adds set.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="valueSelector"></param>
    Public Sub AddSet(predicate As Expression, valueSelector As Expression)
      Dim result1 = m_Visitor.Translate(predicate, ExpressionParametersType.Entities, {0}, m_Parameters.Count, False, False)
      m_Parameters.AddRange(result1.Parameters)
      Dim result2 = m_Visitor.Translate(valueSelector, ExpressionParametersType.Entities, {0}, m_Parameters.Count, False, False)
      m_Parameters.AddRange(result2.Parameters)
      m_SetExpressions.Add($"{result1.Sql} = {result2.Sql}")
    End Sub

    ''' <summary>
    ''' Adds set.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    Public Sub AddSet(predicate As String, ParamArray parameters() As Object)
      If parameters Is Nothing OrElse parameters.Length = 0 Then
        m_SetExpressions.Add(predicate)
      Else
        Dim sql = ConvertToSqlString(predicate, parameters, m_Parameters.Count)
        m_SetExpressions.Add(sql.Sql)
        m_Parameters.AddRange(sql.Parameters)
      End If
    End Sub

    ''' <summary>
    ''' Adds where.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="predicate"></param>
    Public Sub AddWhere(predicate As Expression)
      Dim result = m_Visitor.Translate(predicate, ExpressionParametersType.Entities, {0}, m_Parameters.Count, False, False)
      m_WhereExpressions.Add(result.Sql)
      m_Parameters.AddRange(result.Parameters)
    End Sub

    ''' <summary>
    ''' Adds where.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    Public Sub AddWhere(predicate As String, ParamArray parameters() As Object)
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
    ''' <param name="setAutoFields"></param>
    ''' <returns></returns>
    Public Function CreateQuery(setAutoFields As Boolean) As Query
      Dim entity = m_Model.GetFirstEntity().Entity

      Dim sql = New StringBuilder

      sql.Append("UPDATE ")

      If m_TableNameOverride Is Nothing Then
        Me.DialectProvider.Formatter.AppendIdentifier(sql, entity.TableName, entity.Schema)
      Else
        sql.Append(m_TableNameOverride)
      End If

      sql.AppendLine()

      If setAutoFields Then
        Dim index = m_Parameters.Count
        Dim columns = entity.GetNonKeyProperties().Where(Function(x) x.Property.SetOnUpdate).Select(Function(x) x.Property.ColumnName).ToArray()

        If 0 < columns.Length Then
          Dim getter = EntityAutoFieldsGetterCache.GetOnUpdateGetter(m_Model.Model, entity.EntityType)
          Dim values = getter(Me.DbContext)

          For i = 0 To columns.Length - 1
            m_SetExpressions.Add(Me.DialectProvider.Formatter.CreateIdentifier(columns(i)) & " = " & CreateParameter(index + i))
            m_Parameters.Add(New SqlParameter(CreateParameter(index + i), values(i)))
          Next
        End If
      End If

      sql.Append("SET ")
      Helpers.Text.AppendJoin(sql, ", ", m_SetExpressions)

      If m_WhereExpressions.Any() Then
        sql.AppendLine()
        sql.Append("WHERE ")
        Helpers.Text.AppendJoin(sql, " AND ", m_WhereExpressions)
      End If

      Return New Query(sql.ToString(), m_Parameters)
    End Function

    ''' <summary>
    ''' Creates query.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <param name="forceUpdateAllFields"></param>
    ''' <returns></returns>
    Public Function CreateQuery(obj As Object, forceUpdateAllFields As Boolean) As Query
      Dim table As String

      If m_TableNameOverride Is Nothing Then
        Dim entity = m_Model.GetFirstEntity().Entity
        table = Me.DialectProvider.Formatter.CreateIdentifier(entity.TableName, entity.Schema)
      Else
        table = m_TableNameOverride
      End If

      Dim provider = EntitySqlStringProviderCache.GetUpdateProvider(Me, obj.GetType())
      Dim sqlString = provider(obj, table, forceUpdateAllFields)

      Return New Query(sqlString)
    End Function

  End Class
End Namespace