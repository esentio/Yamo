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
    ''' Stores whether soft delete is used.
    ''' </summary>
    Private m_SoftDelete As Boolean

    ''' <summary>
    ''' Stores SQL model.
    ''' </summary>
    Private m_Model As SqlModel

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
    ''' <param name="softDelete"></param>
    Public Sub New(context As DbContext, softDelete As Boolean)
      MyBase.New(context)
      m_SoftDelete = softDelete
      m_Model = New SqlModel(Me.DbContext.Model)
      m_Visitor = New SqlExpressionVisitor(Me, m_Model)
      m_WhereExpressions = New List(Of String)
      m_Parameters = New List(Of SqlParameter)
      m_ParameterIndexShift = Nothing ' lazy assigned
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
    ''' Adds where.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="predicate"></param>
    Public Sub AddWhere(predicate As Expression)
      If Not m_ParameterIndexShift.HasValue Then
        If m_SoftDelete Then
          m_ParameterIndexShift = m_Model.GetFirstEntity().Entity.GetNonKeyProperties().Where(Function(x) x.Property.SetOnDelete).Count()
        Else
          m_ParameterIndexShift = 0
        End If
      End If

      Dim result = m_Visitor.Translate(predicate, ExpressionParametersType.Entities, {0}, m_Parameters.Count + m_ParameterIndexShift.Value, False, False)
      m_WhereExpressions.Add(result.Sql)
      m_Parameters.AddRange(result.Parameters)
    End Sub

    ''' <summary>
    ''' Adds where.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="predicate"></param>
    Public Sub AddWhere(predicate As String)
      m_WhereExpressions.Add(predicate)
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
      Dim sql As New StringBuilder

      sql.Append("DELETE FROM ")
      Dim entity = m_Model.GetFirstEntity().Entity
      Me.DialectProvider.Formatter.AppendIdentifier(sql, entity.TableName, entity.Schema)
      sql.AppendLine()

      If m_WhereExpressions.Any() Then
        sql.Append(" WHERE ")
        Helpers.Text.AppendJoin(sql, " AND ", m_WhereExpressions)
      End If

      Return New Query(sql.ToString(), m_Parameters.ToList())
    End Function

    ''' <summary>
    ''' Creates soft delete query.
    ''' </summary>
    ''' <returns></returns>
    Private Function CreateSoftDeleteQuery() As Query
      Dim entity = m_Model.GetFirstEntity()

      Dim getter = EntityAutoFieldsGetterCache.GetOnDeleteGetter(m_Model.Model, entity.Entity.EntityType)
      Dim values = getter(Me.DbContext)

      Dim provider = EntitySqlStringProviderCache.GetSoftDeleteWithoutConditionProvider(Me, entity.Entity.EntityType)
      Dim sqlString = provider(values)

      Dim sql As New StringBuilder
      Dim parameters As List(Of SqlParameter)

      sql.Append(sqlString.Sql)

      If m_WhereExpressions.Any() Then
        sql.Append(" WHERE ")
        Helpers.Text.AppendJoin(sql, " AND ", m_WhereExpressions)

        parameters = New List(Of SqlParameter)(sqlString.Parameters.Count + m_Parameters.Count)
        parameters.AddRange(sqlString.Parameters)
        parameters.AddRange(m_Parameters)
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
    Public Function CreateDeleteQuery(obj As Object) As Query
      Dim provider = EntitySqlStringProviderCache.GetDeleteProvider(Me, obj.GetType())
      Dim sqlString = provider(obj)

      Return New Query(sqlString)
    End Function

    ''' <summary>
    ''' Creates soft delete query.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Public Function CreateSoftDeleteQuery(obj As Object) As Query
      Dim provider = EntitySqlStringProviderCache.GetSoftDeleteProvider(Me, obj.GetType())
      Dim sqlString = provider(obj)

      Return New Query(sqlString)
    End Function

  End Class
End Namespace
