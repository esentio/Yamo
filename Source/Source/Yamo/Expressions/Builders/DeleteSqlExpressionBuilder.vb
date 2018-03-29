Imports System.Linq.Expressions
Imports System.Text
Imports Yamo.Internal
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata

Namespace Expressions.Builders

  Public Class DeleteSqlExpressionBuilder
    Inherits SqlExpressionBuilderBase

    Private m_SoftDelete As Boolean

    Private m_Model As SqlModel

    Private m_Visitor As SqlExpressionVisitor

    Private m_WhereExpressions As List(Of String)

    Private m_Parameters As List(Of SqlParameter)

    Private m_ParameterIndexShift As Int32?

    Public Sub New(context As DbContext, softDelete As Boolean)
      MyBase.New(context)
      m_SoftDelete = softDelete
      m_Model = New SqlModel(Me.DbContext.Model)
      m_Visitor = New SqlExpressionVisitor(Me, m_Model)
      m_WhereExpressions = New List(Of String)
      m_Parameters = New List(Of SqlParameter)
      m_ParameterIndexShift = Nothing ' lazy assigned
    End Sub

    Public Sub SetMainTable(Of T)()
      m_Model.SetMainTable(Of T)()
    End Sub

    Public Sub AddWhere(predicate As Expression)
      If Not m_ParameterIndexShift.HasValue Then
        If m_SoftDelete Then
          m_ParameterIndexShift = m_Model.GetFirstEntity().Entity.GetNonKeyProperties().Where(Function(x) x.Property.SetOnDelete).Count()
        Else
          m_ParameterIndexShift = 0
        End If
      End If

      Dim result = m_Visitor.Translate(predicate, {0}, m_Parameters.Count + m_ParameterIndexShift.Value, False, False)
      m_WhereExpressions.Add(result.Sql)
      m_Parameters.AddRange(result.Parameters)
    End Sub

    Public Sub AddWhere(predicate As String)
      m_WhereExpressions.Add(predicate)
    End Sub

    Public Function CreateQuery() As Query
      If m_SoftDelete Then
        Return CreateSoftDeleteQuery()
      Else
        Return CreateDeleteQuery()
      End If
    End Function

    Private Function CreateDeleteQuery() As Query
      Dim sql As New StringBuilder

      sql.AppendLine($"DELETE FROM {Me.DialectProvider.Formatter.CreateIdentifier(m_Model.GetFirstEntity().Entity.TableName)}")

      If m_WhereExpressions.Any() Then
        sql.Append($" WHERE ")
        sql.Append(String.Join(" AND ", m_WhereExpressions))
      End If

      Return New Query(sql.ToString(), m_Parameters.ToList())
    End Function

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
        sql.Append($"WHERE ")
        sql.Append(String.Join(" AND ", m_WhereExpressions))

        parameters = New List(Of SqlParameter)(sqlString.Parameters.Count + m_Parameters.Count)
        parameters.AddRange(sqlString.Parameters)
        parameters.AddRange(m_Parameters)
      Else
        parameters = sqlString.Parameters
      End If

      Return New Query(sql.ToString(), parameters)
    End Function

    Public Function CreateDeleteQuery(obj As Object) As Query
      Dim provider = EntitySqlStringProviderCache.GetDeleteProvider(Me, obj.GetType())
      Dim sqlString = provider(obj)

      Return New Query(sqlString)
    End Function

    Public Function CreateSoftDeleteQuery(obj As Object) As Query
      Dim provider = EntitySqlStringProviderCache.GetSoftDeleteProvider(Me, obj.GetType())
      Dim sqlString = provider(obj)

      Return New Query(sqlString)
    End Function

  End Class
End Namespace
