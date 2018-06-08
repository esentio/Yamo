Imports System.Linq.Expressions
Imports System.Text
Imports Yamo.Internal
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata

Namespace Expressions.Builders

  Public Class UpdateSqlExpressionBuilder
    Inherits SqlExpressionBuilderBase

    Private m_SetAutoFields As Boolean

    Private m_Model As SqlModel

    Private m_Visitor As SqlExpressionVisitor

    Private m_SetExpressions As List(Of String)

    Private m_WhereExpressions As List(Of String)

    Private m_Parameters As List(Of SqlParameter)

    Private m_AutoFieldsParametersInfo As (Index As Int32, Columns As String())?

    Public Sub New(context As DbContext, setAutoFields As Boolean)
      MyBase.New(context)
      m_SetAutoFields = setAutoFields
      m_Model = New SqlModel(Me.DbContext.Model)
      m_Visitor = New SqlExpressionVisitor(Me, m_Model)
      m_SetExpressions = New List(Of String)
      m_WhereExpressions = New List(Of String)
      m_Parameters = New List(Of SqlParameter)
      m_AutoFieldsParametersInfo = Nothing ' lazy assigned
    End Sub

    Public Sub SetMainTable(Of T)()
      m_Model.SetMainTable(Of T)()
    End Sub

    Public Sub AddSet(predicate As Expression)
      Dim result = m_Visitor.Translate(predicate, ExpressionParametersType.Entities, {0}, m_Parameters.Count, False, False)
      m_SetExpressions.Add(result.Sql)
      m_Parameters.AddRange(result.Parameters)
    End Sub

    Public Sub AddSet(predicate As Expression, value As Object)
      Dim result = m_Visitor.Translate(predicate, ExpressionParametersType.Entities, {0}, m_Parameters.Count, False, False)
      m_Parameters.AddRange(result.Parameters)
      Dim parameterName = CreateParameter(m_Parameters.Count)
      m_SetExpressions.Add($"{result.Sql} = {parameterName}")
      m_Parameters.Add(New SqlParameter(parameterName, value))
    End Sub

    Public Sub AddSet(predicate As Expression, valueSelector As Expression)
      Dim result1 = m_Visitor.Translate(predicate, ExpressionParametersType.Entities, {0}, m_Parameters.Count, False, False)
      m_Parameters.AddRange(result1.Parameters)
      Dim result2 = m_Visitor.Translate(valueSelector, ExpressionParametersType.Entities, {0}, m_Parameters.Count, False, False)
      m_Parameters.AddRange(result2.Parameters)
      m_SetExpressions.Add($"{result1.Sql} = {result2.Sql}")
    End Sub

    Public Sub AddSet(predicate As String)
      m_SetExpressions.Add(predicate)
    End Sub

    Public Sub AddWhere(predicate As Expression)
      If Not m_AutoFieldsParametersInfo.HasValue Then
        AddAutoFieldSetters()
      End If

      Dim result = m_Visitor.Translate(predicate, ExpressionParametersType.Entities, {0}, m_Parameters.Count, False, False)
      m_WhereExpressions.Add(result.Sql)
      m_Parameters.AddRange(result.Parameters)
    End Sub

    Public Sub AddWhere(predicate As String)
      m_WhereExpressions.Add(predicate)
    End Sub

    Private Sub AddAutoFieldSetters()
      If m_SetAutoFields Then
        Dim index = m_Parameters.Count
        Dim columns = m_Model.GetFirstEntity().Entity.GetNonKeyProperties().Where(Function(x) x.Property.SetOnUpdate).Select(Function(x) x.Property.ColumnName).ToArray()

        m_AutoFieldsParametersInfo = (index, columns)

        For i = 0 To columns.Length - 1
          m_SetExpressions.Add(Me.DialectProvider.Formatter.CreateIdentifier(columns(i)) & " = " & CreateParameter(index + i))
          m_Parameters.Add(Nothing) ' will ve set later
        Next
      Else
        m_AutoFieldsParametersInfo = (-1, {})
      End If
    End Sub

    Public Function CreateQuery() As Query
      Dim entity = m_Model.GetFirstEntity().Entity

      Dim sql As New StringBuilder

      sql.Append("UPDATE ")
      Me.DialectProvider.Formatter.AppendIdentifier(sql, entity.TableName)
      sql.AppendLine()

      If Not m_AutoFieldsParametersInfo.HasValue Then
        AddAutoFieldSetters()
      End If

      If m_SetAutoFields AndAlso 0 < m_AutoFieldsParametersInfo.Value.Columns.Length Then
        Dim getter = EntityAutoFieldsGetterCache.GetOnUpdateGetter(m_Model.Model, entity.EntityType)
        Dim values = getter(Me.DbContext)

        Dim index = m_AutoFieldsParametersInfo.Value.Index
        Dim columns = m_AutoFieldsParametersInfo.Value.Columns

        For i = 0 To m_AutoFieldsParametersInfo.Value.Columns.Length - 1
          m_Parameters(index + i) = New SqlParameter(CreateParameter(index + i), values(i))
        Next
      End If

      sql.Append("SET ")
      Helpers.Text.AppendJoin(sql, ", ", m_SetExpressions)

      If m_WhereExpressions.Any() Then
        sql.AppendLine()
        sql.Append("WHERE ")
        Helpers.Text.AppendJoin(sql, " AND ", m_WhereExpressions)
      End If

      Return New Query(sql.ToString(), m_Parameters.ToList())
    End Function

    Public Function CreateQuery(obj As Object) As Query
      Dim provider = EntitySqlStringProviderCache.GetUpdateProvider(Me, obj.GetType())
      Dim sqlString = provider(obj)

      Return New Query(sqlString)
    End Function

  End Class
End Namespace