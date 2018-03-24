Imports System.Linq.Expressions
Imports System.Text
Imports Yamo.Internal
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata

Namespace Expressions.Builders

  Public Class UpdateSqlExpressionBuilder
    Inherits SqlExpressionBuilderBase

    Private m_Model As SqlModel

    Private m_Visitor As SqlExpressionVisitor

    Private m_SetExpressions As List(Of String)

    Private m_WhereExpressions As List(Of String)

    Private m_Parameters As List(Of SqlParameter)

    Private m_ParameterIndexShift As Int32?

    Public Sub New(context As DbContext)
      MyBase.New(context)
      m_Model = New SqlModel(Me.DbContext.Model)
      m_Visitor = New SqlExpressionVisitor(Me, m_Model)
      m_SetExpressions = New List(Of String)
      m_WhereExpressions = New List(Of String)
      m_Parameters = New List(Of SqlParameter)
      m_ParameterIndexShift = Nothing ' lazy assigned
    End Sub

    Public Sub SetMainTable(Of T)()
      m_Model.SetMainTable(Of T)()
    End Sub

    Public Sub AddSet(predicate As Expression)
      Dim result = m_Visitor.Translate(predicate, {0}, m_Parameters.Count, False)
      m_SetExpressions.Add(result.Sql)
      m_Parameters.AddRange(result.Parameters)
    End Sub

    Public Sub AddSet(predicate As String)
      m_SetExpressions.Add(predicate)
    End Sub

    Public Sub AddWhere(predicate As Expression)
      If Not m_ParameterIndexShift.HasValue Then
        m_ParameterIndexShift = m_Model.GetFirstEntity().Entity.GetNonKeyProperties().Where(Function(x) x.Property.SetOnUpdate).Count()
      End If

      Dim result = m_Visitor.Translate(predicate, {0}, m_Parameters.Count + m_ParameterIndexShift.Value, False)
      m_WhereExpressions.Add(result.Sql)
      m_Parameters.AddRange(result.Parameters)
    End Sub

    Public Sub AddWhere(predicate As String)
      m_WhereExpressions.Add(predicate)
    End Sub

    Public Function CreateQuery() As Query
      Dim sql As New StringBuilder

      sql.AppendLine($"UPDATE {Me.DialectProvider.Formatter.CreateIdentifier(m_Model.GetFirstEntity().Entity.TableName)}")

      sql.Append($" SET ")
      sql.Append(String.Join(", ", m_SetExpressions))

      ' TODO: SIP - update SetOnUpdate fields when needed

      If m_WhereExpressions.Any() Then
        sql.Append($" WHERE ")
        sql.Append(String.Join(" AND ", m_WhereExpressions))
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