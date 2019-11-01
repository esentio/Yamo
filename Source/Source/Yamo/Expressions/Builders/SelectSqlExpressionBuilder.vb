Imports System.Linq.Expressions
Imports System.Text
Imports Yamo.Internal
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Metadata

Namespace Expressions.Builders

  ' TODO: SIP - add documentation to this class.
  Public Class SelectSqlExpressionBuilder
    Inherits SqlExpressionBuilderBase

    Private m_Model As SqlModel

    Private m_Visitor As SqlExpressionVisitor

    Private m_JoinExpressions As List(Of String)

    Private m_WhereExpressions As List(Of String)

    Private m_GroupByExpressions As List(Of String) ' couldn't be just string?

    Private m_HavingExpressions As List(Of String)

    Private m_OrderByExpressions As List(Of String)

    Private m_SelectExpression As String

    Private m_UseDistinct As Boolean

    Private m_Parameters As List(Of SqlParameter)

    Public Sub New(context As DbContext)
      MyBase.New(context)
      m_Model = New SqlModel(Me.DbContext.Model)
      m_Visitor = New SqlExpressionVisitor(Me, m_Model)
      ' lists are created only when necessary
      m_JoinExpressions = Nothing
      m_WhereExpressions = Nothing
      m_GroupByExpressions = Nothing
      m_HavingExpressions = Nothing
      m_OrderByExpressions = Nothing
      m_SelectExpression = Nothing
      m_UseDistinct = False
      m_Parameters = New List(Of SqlParameter)
    End Sub

    Public Function CreateQuery() As SelectQuery
      Dim sql = New StringBuilder

      If m_SelectExpression Is Nothing Then
        BuildAndAppendSelectExpression(sql)
      Else
        sql.Append("SELECT ")

        If m_UseDistinct Then
          sql.Append("DISTINCT ")
        End If

        sql.Append(m_SelectExpression)
      End If

      sql.Append(" FROM ")
      Me.DialectProvider.Formatter.AppendIdentifier(sql, m_Model.GetFirstEntity().Entity.TableName)
      sql.Append(" ")
      Me.DialectProvider.Formatter.AppendIdentifier(sql, m_Model.GetFirstTableAlias())

      If m_JoinExpressions IsNot Nothing Then
        For Each joinExpression In m_JoinExpressions
          sql.Append(" ")
          sql.Append(joinExpression)
        Next
      End If

      If m_WhereExpressions IsNot Nothing Then
        sql.Append(" WHERE ")
        Helpers.Text.AppendJoin(sql, " AND ", m_WhereExpressions)
      End If

      If m_GroupByExpressions IsNot Nothing Then
        sql.Append(" GROUP BY ")
        Helpers.Text.AppendJoin(sql, ", ", m_GroupByExpressions)
      End If

      If m_HavingExpressions IsNot Nothing Then
        sql.Append(" HAVING ")
        Helpers.Text.AppendJoin(sql, " AND ", m_HavingExpressions)
      End If

      If m_OrderByExpressions IsNot Nothing Then
        sql.Append(" ORDER BY ")
        Helpers.Text.AppendJoin(sql, ", ", m_OrderByExpressions)
      End If

      Return New SelectQuery(sql.ToString(), m_Parameters.ToList(), m_Model)
    End Function

    Public Sub SetMainTable(Of T)()
      m_Model.SetMainTable(Of T)()
    End Sub

    Private Function TryGetEntityIndexHints(predicate As Expression) As Int32()
      If predicate.NodeType = ExpressionType.Lambda Then
        Dim lambda = DirectCast(predicate, LambdaExpression)

        If lambda.Parameters.Count = 1 AndAlso GetType(IJoin).IsAssignableFrom(lambda.Parameters(0).Type) Then
          Dim visitor = New SqlExpressionEntitiesVisitor()
          Dim indexes = visitor.GetIndexesOfReferencedEntities(predicate)

          ' if only 2 entities were used and latter one is currently joined entity, we can use this as a hint
          ' since at this moment joined entity hasn't been added to the model yet, value returned from GetEntityCount() call is the (future) index
          If indexes.Length = 2 AndAlso indexes(1) = m_Model.GetEntityCount() Then
            Return indexes
          End If
        End If
      End If

      Return Nothing
    End Function

    Private Function TryGetRelationship(Of TJoined)(predicate As Expression, entityIndexHints As Int32()) As SqlEntityRelationship
      Dim declaringEntityIndexHint = entityIndexHints?(0)

      If Not declaringEntityIndexHint.HasValue Then
        ' no relationship hint (exception is thrown later if needed)
        ' e.g. if we want just to use joined table in query and not return it at all
        Return Nothing

      ElseIf declaringEntityIndexHint.HasValue Then
        ' try to infer relationship from model
        Dim declaringSqlEntity = m_Model.GetEntity(declaringEntityIndexHint.Value)
        Dim relationshipNavigations = declaringSqlEntity.Entity.GetRelationshipNavigations(GetType(TJoined))

        If relationshipNavigations.Count = 1 Then
          Select Case relationshipNavigations(0).GetType()
            Case GetType(ReferenceNavigation)
              Return New SqlEntityRelationship(declaringSqlEntity, relationshipNavigations(0))
            Case GetType(CollectionNavigation)
              Return New SqlEntityRelationship(declaringSqlEntity, relationshipNavigations(0))
            Case Else
              Throw New NotSupportedException($"Relationship of type '{relationshipNavigations(0).GetType()}' is not supported.")
          End Select
        Else
          ' no unambiguous match found; relationship might be specified later
          Return Nothing
        End If
      End If

      Return Nothing
    End Function

    Public Sub AddJoin(Of TJoined)(joinType As JoinType, predicate As Expression, entityIndexHints As Int32())
      If m_JoinExpressions Is Nothing Then
        m_JoinExpressions = New List(Of String)
      End If

      If entityIndexHints Is Nothing Then
        entityIndexHints = TryGetEntityIndexHints(predicate)
      End If

      Dim relationship = TryGetRelationship(Of TJoined)(predicate, entityIndexHints)

      m_Model.AddJoinedTable(Of TJoined)(relationship)

      Dim sql As String
      Dim joinTypeString As String

      Select Case joinType
        Case JoinType.Inner
          joinTypeString = "INNER JOIN"
        Case JoinType.LeftOuter
          joinTypeString = "LEFT OUTER JOIN"
        Case JoinType.RightOuter
          joinTypeString = "RIGHT OUTER JOIN"
        Case JoinType.FullOuter
          joinTypeString = "FULL OUTER JOIN"
        Case JoinType.CrossJoin
          joinTypeString = "CROSS JOIN"
        Case Else
          Throw New NotSupportedException($"Unsupported join type '{joinType}'.")
      End Select

      Dim entity = m_Model.Model.GetEntity(GetType(TJoined))
      Dim tableAlias = m_Model.GetLastTableAlias()

      If predicate Is Nothing Then
        sql = joinTypeString & " " & Me.DialectProvider.Formatter.CreateIdentifier(entity.TableName) & " " & Me.DialectProvider.Formatter.CreateIdentifier(tableAlias)
        m_JoinExpressions.Add(sql)
      Else
        Dim parametersType = If(entityIndexHints Is Nothing, ExpressionParametersType.IJoin, ExpressionParametersType.Entities)
        Dim result = m_Visitor.Translate(predicate, parametersType, entityIndexHints, m_Parameters.Count, True, True)
        sql = joinTypeString & " " & Me.DialectProvider.Formatter.CreateIdentifier(entity.TableName) & " " & Me.DialectProvider.Formatter.CreateIdentifier(tableAlias) & " ON " & result.Sql
        m_JoinExpressions.Add(sql)
        m_Parameters.AddRange(result.Parameters)
      End If
    End Sub

    Public Sub SetLastJoinRelationship(relationship As Expression)
      ' we expect lambda expression with one parameter

      Dim lambda = DirectCast(relationship, LambdaExpression)
      Dim parameterType = lambda.Parameters(0).Type

      Dim index As Int32
      Dim propertyName As String = Nothing

      If GetType(IJoin).IsAssignableFrom(parameterType) Then
        If lambda.Body.NodeType = ExpressionType.MemberAccess Then
          Dim node = DirectCast(lambda.Body, MemberExpression)

          If node.Expression.NodeType = ExpressionType.MemberAccess Then
            index = Helpers.Common.GetEntityIndexFromJoinMemberName(DirectCast(node.Expression, MemberExpression).Member.Name)
            propertyName = node.Member.Name
          End If
        End If

      Else
        Dim entities = m_Model.GetEntities()
        Dim possibleDeclaringEntities = entities.Take(entities.Length - 1).Where(Function(x) x.Entity.EntityType Is parameterType).ToArray()

        If possibleDeclaringEntities.Length = 0 Then
          Throw New Exception($"Cannot infer relationship, because there are no joined entities of type '{parameterType}'.")
        ElseIf possibleDeclaringEntities.Length = 1 Then
          index = possibleDeclaringEntities(0).Index

          If lambda.Body.NodeType = ExpressionType.MemberAccess Then
            propertyName = DirectCast(lambda.Body, MemberExpression).Member.Name
          End If
        Else
          Throw New Exception($"Cannot infer relationship, because there are multiple joined entities of type '{parameterType}'. Use {NameOf(IJoin)} in relationship predicate to avoid unambiguous match.")
        End If
      End If

      If propertyName Is Nothing Then
        Throw New Exception("Cannot infer relationship. Use expression that contains relationship property only.")
      End If

      Dim declaringSqlEntity = m_Model.GetEntity(index)

      If GetType(IList).IsAssignableFrom(lambda.ReturnType) Then
        Dim genericTypes = lambda.ReturnType.GetGenericArguments()

        If Not genericTypes.Count = 1 Then
          Throw New Exception($"Unable to infer item type from '{lambda.ReturnType}'.")
        End If

        ' there is still small possibility that item type is not genericTypes(0) type, but in most cases like List(Of) we should be ok

        m_Model.GetLastEntity().SetRelationship(New SqlEntityRelationship(declaringSqlEntity, New CollectionNavigation(propertyName, genericTypes(0), lambda.ReturnType)))
      Else
        m_Model.GetLastEntity().SetRelationship(New SqlEntityRelationship(declaringSqlEntity, New ReferenceNavigation(propertyName, lambda.ReturnType)))
      End If
    End Sub

    Public Sub AddWhere(predicate As Expression, entityIndexHints As Int32())
      If m_WhereExpressions Is Nothing Then
        m_WhereExpressions = New List(Of String)
      End If

      Dim parametersType = If(entityIndexHints Is Nothing, ExpressionParametersType.IJoin, ExpressionParametersType.Entities)
      Dim result = m_Visitor.Translate(predicate, parametersType, entityIndexHints, m_Parameters.Count, True, True)
      m_WhereExpressions.Add(result.Sql)
      m_Parameters.AddRange(result.Parameters)
    End Sub

    Public Sub AddWhere(predicate As String)
      If m_WhereExpressions Is Nothing Then
        m_WhereExpressions = New List(Of String)
      End If

      m_WhereExpressions.Add(predicate)
    End Sub

    Public Sub AddGroupBy(keySelector As Expression, entityIndexHints As Int32())
      If m_GroupByExpressions Is Nothing Then
        m_GroupByExpressions = New List(Of String)
      End If

      Dim parametersType = If(entityIndexHints Is Nothing, ExpressionParametersType.IJoin, ExpressionParametersType.Entities)
      Dim result = m_Visitor.Translate(keySelector, parametersType, entityIndexHints, m_Parameters.Count, True, True)
      m_GroupByExpressions.Add(result.Sql)
      m_Parameters.AddRange(result.Parameters)
    End Sub

    Public Sub AddHaving(predicate As Expression, entityIndexHints As Int32())
      If m_HavingExpressions Is Nothing Then
        m_HavingExpressions = New List(Of String)
      End If

      Dim parametersType = If(entityIndexHints Is Nothing, ExpressionParametersType.IJoin, ExpressionParametersType.Entities)
      Dim result = m_Visitor.Translate(predicate, parametersType, entityIndexHints, m_Parameters.Count, True, True)
      m_HavingExpressions.Add(result.Sql)
      m_Parameters.AddRange(result.Parameters)
    End Sub

    Public Sub AddHaving(predicate As String)
      If m_HavingExpressions Is Nothing Then
        m_HavingExpressions = New List(Of String)
      End If

      m_HavingExpressions.Add(predicate)
    End Sub

    Public Sub AddOrderBy(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean)
      If m_OrderByExpressions Is Nothing Then
        m_OrderByExpressions = New List(Of String)
      End If

      Dim parametersType = If(entityIndexHints Is Nothing, ExpressionParametersType.IJoin, ExpressionParametersType.Entities)
      Dim result = m_Visitor.Translate(keySelector, parametersType, entityIndexHints, m_Parameters.Count, True, True)

      If ascending Then
        m_OrderByExpressions.Add(result.Sql)
      Else
        m_OrderByExpressions.Add(result.Sql & " DESC")
      End If

      m_Parameters.AddRange(result.Parameters)
    End Sub

    Public Sub AddSelectAll(ParamArray entityTypes As Type())
      ' right now this does nothing; refactor?
    End Sub

    Public Sub ExcludeSelected(propertyExpression As Expression)
      ' TODO: SIP - refactor and combine with SetLastJoinRelationship
      ' we expect lambda expression with one parameter

      Dim lambda = DirectCast(propertyExpression, LambdaExpression)
      Dim parameterType = lambda.Parameters(0).Type

      Dim index As Int32
      Dim propertyName As String = Nothing

      If GetType(IJoin).IsAssignableFrom(parameterType) Then
        If lambda.Body.NodeType = ExpressionType.MemberAccess Then
          Dim node = DirectCast(lambda.Body, MemberExpression)

          If node.Expression.NodeType = ExpressionType.MemberAccess Then
            index = Helpers.Common.GetEntityIndexFromJoinMemberName(DirectCast(node.Expression, MemberExpression).Member.Name)
            propertyName = node.Member.Name
          End If
        End If

      Else
        Dim possibleEntities = m_Model.GetEntities().Where(Function(x) x.Entity.EntityType Is parameterType).ToArray()

        If possibleEntities.Length = 0 Then
          Throw New Exception($"Cannot infer entity for column exclude, because there are no joined entities of type '{parameterType}'.")
        ElseIf possibleEntities.Length = 1 Then
          index = possibleEntities(0).Index

          If lambda.Body.NodeType = ExpressionType.MemberAccess Then
            propertyName = DirectCast(lambda.Body, MemberExpression).Member.Name
          End If
        Else
          Throw New Exception($"Cannot infer entity for column exclude, because there are multiple joined entities of type '{parameterType}'. Use {NameOf(IJoin)} in exclude expression to avoid unambiguous match.")
        End If
      End If

      If propertyName Is Nothing Then
        Throw New Exception("Cannot infer excluded column. Use expression that contains entity property only.")
      End If

      Dim entity = m_Model.GetEntity(index)
      Dim prop = entity.Entity.GetPropertyWithIndex(propertyName)

      If prop.Property.IsKey Then
        Throw New ArgumentException("Primary key columns cannot be excluded from the query.")
      End If

      entity.IncludedColumns(prop.Index) = False
    End Sub

    Public Sub ExcludeSelected(entityIndex As Int32)
      m_Model.GetEntity(entityIndex).Exclude()
    End Sub

    Public Sub AddSelectCount()
      m_SelectExpression = "COUNT(*)"
    End Sub

    Public Sub AddSelect(selector As Expression, entityIndexHints As Int32())
      Dim parametersType = If(entityIndexHints Is Nothing, ExpressionParametersType.IJoin, ExpressionParametersType.Entities)
      Dim result = m_Visitor.TranslateCustomSelect(selector, parametersType, entityIndexHints, m_Parameters.Count)
      m_SelectExpression = result.SqlString.Sql
      m_Parameters.AddRange(result.SqlString.Parameters)
      m_Model.SetCustomEntities(result.CustomEntities)
    End Sub

    Public Sub AddDistinct()
      m_UseDistinct = True
    End Sub

    Private Sub BuildAndAppendSelectExpression(sql As StringBuilder)
      sql.Append("SELECT ")

      If m_UseDistinct Then
        sql.Append("DISTINCT ")
      End If

      Dim first = True

      For i = 0 To m_Model.GetEntityCount() - 1
        Dim entity = m_Model.GetEntity(i)

        If Not entity.IsExcluded Then
          Dim formattedTableAlias = Me.DialectProvider.Formatter.CreateIdentifier(entity.TableAlias)
          Dim properties = entity.Entity.GetProperties()

          For j = 0 To properties.Count - 1
            If entity.IncludedColumns(j) Then
              If first Then
                first = False
              Else
                sql.Append(", ")
              End If

              sql.Append(formattedTableAlias)
              sql.Append(".")
              Me.DialectProvider.Formatter.AppendIdentifier(sql, properties(j).ColumnName)
            End If
          Next
        End If
      Next
    End Sub

  End Class
End Namespace
