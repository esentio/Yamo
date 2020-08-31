﻿Imports System.Linq.Expressions
Imports System.Text
Imports Yamo.Infrastructure
Imports Yamo.Internal
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Metadata

Namespace Expressions.Builders

  ''' <summary>
  ''' Represents select SQL expression builder.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SelectSqlExpressionBuilder
    Inherits SqlExpressionBuilderBase

    ''' <summary>
    ''' Stores SQL model.
    ''' </summary>
    Private m_Model As SqlModel

    ''' <summary>
    ''' Stores SQL expression visitor.
    ''' </summary>
    Private m_Visitor As SqlExpressionVisitor

    ''' <summary>
    ''' Stores main table source expression
    ''' </summary>
    Private m_MainTableSourceExpression As String

    ''' <summary>
    ''' Stores current join info
    ''' </summary>
    Private m_CurrentJoinInfo As JoinInfo?

    ''' <summary>
    ''' Stores join expressions.
    ''' </summary>
    Private m_JoinExpressions As List(Of String)

    ''' <summary>
    ''' Stores where expressions.
    ''' </summary>
    Private m_WhereExpressions As List(Of String)

    ''' <summary>
    ''' Stores group by expressions.
    ''' </summary>
    Private m_GroupByExpressions As List(Of String) ' couldn't be just string?

    ''' <summary>
    ''' Stores having expressions.
    ''' </summary>
    Private m_HavingExpressions As List(Of String)

    ''' <summary>
    ''' Stores order by expressions.
    ''' </summary>
    Private m_OrderByExpressions As List(Of String)

    ''' <summary>
    ''' Stores limit expression
    ''' </summary>
    Private m_LimitExpression As String

    ''' <summary>
    ''' Stores whether top should be used for limit
    ''' </summary>
    Private m_UseTopForLimit As Boolean

    ''' <summary>
    ''' Stores select expressions.
    ''' </summary>
    Private m_SelectExpression As String

    ''' <summary>
    ''' Stores whether distincs is used.
    ''' </summary>
    Private m_UseDistinct As Boolean

    ''' <summary>
    ''' Stores parameters.
    ''' </summary>
    Private m_Parameters As List(Of SqlParameter)

    ''' <summary>
    ''' Creates new instance of <see cref="SelectSqlExpressionBuilder"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="context"></param>
    Public Sub New(context As DbContext)
      MyBase.New(context)
      m_Model = New SqlModel(Me.DbContext.Model)
      m_Visitor = New SqlExpressionVisitor(Me, m_Model)
      ' lists are created only when necessary
      m_MainTableSourceExpression = Nothing
      m_CurrentJoinInfo = Nothing
      m_JoinExpressions = Nothing
      m_WhereExpressions = Nothing
      m_GroupByExpressions = Nothing
      m_HavingExpressions = Nothing
      m_OrderByExpressions = Nothing
      m_LimitExpression = Nothing
      m_UseTopForLimit = False
      m_SelectExpression = Nothing
      m_UseDistinct = False
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
    ''' Sets main table source.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="tableSource"></param>
    Public Sub SetMainTableSource(tableSource As FormattableString)
      Dim sql = ConvertToSqlString(tableSource, m_Parameters.Count)
      m_MainTableSourceExpression = sql.Sql
      m_Parameters.AddRange(sql.Parameters)
    End Sub

    ''' <summary>
    ''' Sets main table source.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="tableSource"></param>
    ''' <param name="parameters"></param>
    Public Sub SetMainTableSource(tableSource As RawSqlString, ParamArray parameters() As Object)
      If parameters Is Nothing OrElse parameters.Length = 0 Then
        m_MainTableSourceExpression = tableSource.Value
      Else
        Dim sql = ConvertToSqlString(tableSource.Value, parameters, m_Parameters.Count)
        m_MainTableSourceExpression = sql.Sql
        m_Parameters.AddRange(sql.Parameters)
      End If
    End Sub

    ''' <summary>
    ''' Adds join.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="joinType"></param>
    Public Sub AddJoin(Of TJoined)(joinType As JoinType)
      m_CurrentJoinInfo = New JoinInfo(joinType)
    End Sub

    ''' <summary>
    ''' Adds join.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="joinType"></param>
    ''' <param name="tableSource"></param>
    Public Sub AddJoin(Of TJoined)(joinType As JoinType, tableSource As FormattableString)
      Dim sql = ConvertToSqlString(tableSource, m_Parameters.Count)
      m_CurrentJoinInfo = New JoinInfo(joinType, sql.Sql)
      m_Parameters.AddRange(sql.Parameters)
    End Sub

    ''' <summary>
    ''' Adds join.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="joinType"></param>
    ''' <param name="tableSource"></param>
    ''' <param name="parameters"></param>
    Public Sub AddJoin(Of TJoined)(joinType As JoinType, tableSource As RawSqlString, ParamArray parameters() As Object)
      If parameters Is Nothing OrElse parameters.Length = 0 Then
        m_CurrentJoinInfo = New JoinInfo(joinType, tableSource.Value)
      Else
        Dim sql = ConvertToSqlString(tableSource.Value, parameters, m_Parameters.Count)
        m_CurrentJoinInfo = New JoinInfo(joinType, sql.Sql)
        m_Parameters.AddRange(sql.Parameters)
      End If
    End Sub

    ''' <summary>
    ''' Adds join.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="joinType"></param>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    Public Sub AddJoin(Of TJoined)(joinType As JoinType, predicate As Expression, entityIndexHints As Int32())
      AddJoin(Of TJoined)(New JoinInfo(joinType), predicate, entityIndexHints)
    End Sub

    ''' <summary>
    ''' Adds join.<br/>
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="joinInfo"></param>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    Private Sub AddJoin(Of TJoined)(joinInfo As JoinInfo, predicate As Expression, entityIndexHints As Int32())
      If m_JoinExpressions Is Nothing Then
        m_JoinExpressions = New List(Of String)
      End If

      If entityIndexHints Is Nothing Then
        entityIndexHints = TryGetEntityIndexHints(predicate)
      End If

      Dim relationship = TryGetRelationship(Of TJoined)(entityIndexHints)

      m_Model.AddJoinedTable(Of TJoined)(relationship)

      Dim sql As String
      Dim joinTypeString As String

      Select Case joinInfo.JoinType
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
          Throw New NotSupportedException($"Unsupported join type '{joinInfo.JoinType}'.")
      End Select

      Dim entity = m_Model.Model.GetEntity(GetType(TJoined))
      Dim tableAlias = m_Model.GetLastTableAlias()

      If predicate Is Nothing Then
        If joinInfo.TableSource Is Nothing Then
          sql = joinTypeString & " " & Me.DialectProvider.Formatter.CreateIdentifier(entity.TableName, entity.Schema) & " " & Me.DialectProvider.Formatter.CreateIdentifier(tableAlias)
        Else
          sql = joinTypeString & " " & joinInfo.TableSource & " " & Me.DialectProvider.Formatter.CreateIdentifier(tableAlias)
        End If

        m_JoinExpressions.Add(sql)
      Else
        Dim parametersType = If(entityIndexHints Is Nothing, ExpressionParametersType.IJoin, ExpressionParametersType.Entities)
        Dim result = m_Visitor.Translate(predicate, parametersType, entityIndexHints, m_Parameters.Count, True, True)

        If joinInfo.TableSource Is Nothing Then
          sql = joinTypeString & " " & Me.DialectProvider.Formatter.CreateIdentifier(entity.TableName, entity.Schema) & " " & Me.DialectProvider.Formatter.CreateIdentifier(tableAlias) & " ON " & result.Sql
        Else
          sql = joinTypeString & " " & joinInfo.TableSource & " " & Me.DialectProvider.Formatter.CreateIdentifier(tableAlias) & " ON " & result.Sql
        End If

        m_JoinExpressions.Add(sql)
        m_Parameters.AddRange(result.Parameters)
      End If
    End Sub

    ''' <summary>
    ''' Adds ignored join.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entityType"></param>
    Public Sub AddIgnoredJoin(entityType As Type)
      m_Model.AddIgnoredJoinedTable(entityType)
    End Sub

    ''' <summary>
    ''' Adds on.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    Public Sub AddOn(Of TJoined)(predicate As Expression, entityIndexHints As Int32())
      If Not m_CurrentJoinInfo.HasValue Then
        ' join has been conditionally ignored
        Exit Sub
      End If

      AddJoin(Of TJoined)(m_CurrentJoinInfo.Value, predicate, entityIndexHints)

      m_CurrentJoinInfo = Nothing
    End Sub

    ''' <summary>
    ''' Tries to get entity index hints.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Tries to get relationship.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function TryGetRelationship(Of TJoined)(entityIndexHints As Int32()) As SqlEntityRelationship
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
          Dim relationshipNavigation = relationshipNavigations(0)

          Select Case relationshipNavigation.GetType()
            Case GetType(ReferenceNavigation)
              Return New SqlEntityRelationship(declaringSqlEntity, relationshipNavigation)
            Case GetType(CollectionNavigation)
              Return New SqlEntityRelationship(declaringSqlEntity, relationshipNavigation)
            Case Else
              Throw New NotSupportedException($"Relationship of type '{relationshipNavigation.GetType()}' is not supported.")
          End Select
        Else
          ' no unambiguous match found; relationship might be specified later
          Return Nothing
        End If
      End If

      Return Nothing
    End Function

    ''' <summary>
    ''' Sets last join relationship.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="relationship">Lambda expression with one parameter is expected.</param>
    Public Sub SetLastJoinRelationship(relationship As Expression)
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

    ''' <summary>
    ''' Adds where.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    Public Sub AddWhere(predicate As Expression, entityIndexHints As Int32())
      If m_WhereExpressions Is Nothing Then
        m_WhereExpressions = New List(Of String)
      End If

      Dim parametersType = If(entityIndexHints Is Nothing, ExpressionParametersType.IJoin, ExpressionParametersType.Entities)
      Dim result = m_Visitor.Translate(predicate, parametersType, entityIndexHints, m_Parameters.Count, True, True)
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
      If m_WhereExpressions Is Nothing Then
        m_WhereExpressions = New List(Of String)
      End If

      If parameters Is Nothing OrElse parameters.Length = 0 Then
        m_WhereExpressions.Add(predicate)
      Else
        Dim sql = ConvertToSqlString(predicate, parameters, m_Parameters.Count)
        m_WhereExpressions.Add(sql.Sql)
        m_Parameters.AddRange(sql.Parameters)
      End If
    End Sub

    ''' <summary>
    ''' Adds group by.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <param name="entityIndexHints"></param>
    Public Sub AddGroupBy(keySelector As Expression, entityIndexHints As Int32())
      If m_GroupByExpressions Is Nothing Then
        m_GroupByExpressions = New List(Of String)
      End If

      Dim parametersType = If(entityIndexHints Is Nothing, ExpressionParametersType.IJoin, ExpressionParametersType.Entities)
      Dim result = m_Visitor.Translate(keySelector, parametersType, entityIndexHints, m_Parameters.Count, True, True)
      m_GroupByExpressions.Add(result.Sql)
      m_Parameters.AddRange(result.Parameters)
    End Sub

    ''' <summary>
    ''' Adds having.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    Public Sub AddHaving(predicate As Expression, entityIndexHints As Int32())
      If m_HavingExpressions Is Nothing Then
        m_HavingExpressions = New List(Of String)
      End If

      Dim parametersType = If(entityIndexHints Is Nothing, ExpressionParametersType.IJoin, ExpressionParametersType.Entities)
      Dim result = m_Visitor.Translate(predicate, parametersType, entityIndexHints, m_Parameters.Count, True, True)
      m_HavingExpressions.Add(result.Sql)
      m_Parameters.AddRange(result.Parameters)
    End Sub

    ''' <summary>
    ''' Adds having.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    Public Sub AddHaving(predicate As String, ParamArray parameters() As Object)
      If m_HavingExpressions Is Nothing Then
        m_HavingExpressions = New List(Of String)
      End If

      If parameters Is Nothing OrElse parameters.Length = 0 Then
        m_HavingExpressions.Add(predicate)
      Else
        Dim sql = ConvertToSqlString(predicate, parameters, m_Parameters.Count)
        m_HavingExpressions.Add(sql.Sql)
        m_Parameters.AddRange(sql.Parameters)
      End If
    End Sub

    ''' <summary>
    ''' Adds order by.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <param name="ascending"></param>
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

    ''' <summary>
    ''' Adds clause to limit rows returned by the query.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="offset"></param>
    ''' <param name="count"></param>
    Public Sub AddLimit(offset As Int32?, count As Int32)
      Dim limitType = Me.DialectProvider.SupportedLimitType

      m_UseTopForLimit = False

      If offset.HasValue Then
        If limitType.HasFlag(LimitType.Limit) Then
          m_LimitExpression = " LIMIT " & offset.Value.ToString(Globalization.CultureInfo.InvariantCulture) & ", " & count.ToString(Globalization.CultureInfo.InvariantCulture)
        ElseIf limitType.HasFlag(LimitType.OffsetFetch) Then
          m_LimitExpression = " OFFSET " & offset.Value.ToString(Globalization.CultureInfo.InvariantCulture) & " ROWS FETCH FIRST " & count.ToString(Globalization.CultureInfo.InvariantCulture) & " ROWS ONLY"
        Else
          Throw New NotSupportedException("Limit is not supported with current SQL dialect provider.")
        End If
      Else
        ' NOTE: prefer TOP over OFFSET FETCH, because it can be used in more scenarios (TOP doesn't require ORDER BY clause)
        If limitType.HasFlag(LimitType.Limit) Then
          m_LimitExpression = " LIMIT " & count.ToString(Globalization.CultureInfo.InvariantCulture)
        ElseIf limitType.HasFlag(LimitType.Top) Then
          m_LimitExpression = "TOP(" & count.ToString(Globalization.CultureInfo.InvariantCulture) & ") "
          m_UseTopForLimit = True
        ElseIf limitType.HasFlag(LimitType.OffsetFetch) Then
          m_LimitExpression = " OFFSET 0 ROWS FETCH FIRST " & count.ToString(Globalization.CultureInfo.InvariantCulture) & " ROWS ONLY"
        Else
          Throw New NotSupportedException("Limit is not supported with current SQL dialect provider.")
        End If
      End If
    End Sub

    ''' <summary>
    ''' Adds select all columns.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entityTypes"></param>
    Public Sub AddSelectAll(ParamArray entityTypes As Type())
      ' right now this does nothing; refactor?
    End Sub

    ''' <summary>
    ''' Excludes selected property.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="propertyExpression">Lambda expression with one parameter is expected.</param>
    Public Sub ExcludeSelected(propertyExpression As Expression)
      ' TODO: SIP - refactor and combine with SetLastJoinRelationship

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
      Dim prop = entity.Entity.GetProperty(propertyName)

      If prop.IsKey Then
        Throw New ArgumentException("Primary key columns cannot be excluded from the query.")
      End If

      entity.IncludedColumns(prop.Index) = False
    End Sub

    ''' <summary>
    ''' Excludes selected entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entityIndex"></param>
    Public Sub ExcludeSelected(entityIndex As Int32)
      m_Model.GetEntity(entityIndex).Exclude()
    End Sub

    ''' <summary>
    ''' Adds select count.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    Public Sub AddSelectCount()
      m_SelectExpression = "COUNT(*)"
    End Sub

    ''' <summary>
    ''' Adds select.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="selector"></param>
    ''' <param name="entityIndexHints"></param>
    Public Sub AddSelect(selector As Expression, entityIndexHints As Int32())
      Dim parametersType = If(entityIndexHints Is Nothing, ExpressionParametersType.IJoin, ExpressionParametersType.Entities)
      Dim result = m_Visitor.TranslateCustomSelect(selector, parametersType, entityIndexHints, m_Parameters.Count)
      m_SelectExpression = result.SqlString.Sql
      m_Parameters.AddRange(result.SqlString.Parameters)
      m_Model.SetCustomEntities(result.CustomEntities)
    End Sub

    ''' <summary>
    ''' Adds distinct.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    Public Sub AddDistinct()
      m_UseDistinct = True
    End Sub

    ''' <summary>
    ''' Build and append select expression to <see cref="StringBuilder"/>.
    ''' </summary>
    ''' <param name="sql"></param>
    Private Sub BuildAndAppendSelectExpression(sql As StringBuilder)
      Dim first = True

      For i = 0 To m_Model.GetEntityCount() - 1
        Dim entity = m_Model.GetEntity(i)

        If Not entity.IsExcludedOrIgnored Then
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

    ''' <summary>
    ''' Creates query.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function CreateQuery() As SelectQuery
      Dim sql = New StringBuilder

      sql.Append("SELECT ")

      If m_UseDistinct Then
        sql.Append("DISTINCT ")
      End If

      If m_LimitExpression IsNot Nothing AndAlso m_UseTopForLimit Then
        sql.Append(m_LimitExpression)
      End If

      If m_SelectExpression Is Nothing Then
        BuildAndAppendSelectExpression(sql)
      Else
        sql.Append(m_SelectExpression)
      End If

      sql.Append(" FROM ")

      If m_MainTableSourceExpression Is Nothing Then
        Dim entity = m_Model.GetFirstEntity().Entity
        Me.DialectProvider.Formatter.AppendIdentifier(sql, entity.TableName, entity.Schema)
      Else
        sql.Append(m_MainTableSourceExpression)
      End If

      sql.Append(" ")
      Me.DialectProvider.Formatter.AppendIdentifier(sql, m_Model.GetFirstTableAlias())

      If m_JoinExpressions IsNot Nothing Then
        For i = 0 To m_JoinExpressions.Count - 1
          sql.Append(" ")
          sql.Append(m_JoinExpressions(i))
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

      If m_LimitExpression IsNot Nothing AndAlso Not m_UseTopForLimit Then
        sql.Append(m_LimitExpression)
      End If

      Return New SelectQuery(sql.ToString(), m_Parameters, m_Model)
    End Function

  End Class
End Namespace
