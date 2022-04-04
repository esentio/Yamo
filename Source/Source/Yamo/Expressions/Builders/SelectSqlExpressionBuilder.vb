﻿Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
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
    ''' Gets subquery context.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SubqueryContext As <MaybeNull> SubqueryContext

    ''' <summary>
    ''' Stores SQL model.
    ''' </summary>
    Private m_Model As SelectSqlModel

    ''' <summary>
    ''' Stores SQL expression visitor.
    ''' </summary>
    Private m_Visitor As SqlExpressionVisitor

    ''' <summary>
    ''' Stores main table source expression.
    ''' </summary>
    Private m_MainTableSourceExpression As String

    ''' <summary>
    ''' Stores main table hints.
    ''' </summary>
    Private m_MainTableHints As String

    ''' <summary>
    ''' Stores current join info.
    ''' </summary>
    Private m_CurrentJoinInfo As JoinInfo?

    ''' <summary>
    ''' Stores current joined table hints.
    ''' </summary>
    Private m_CurrentJoinTableHints As String

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
    ''' Stores limit expression.
    ''' </summary>
    Private m_LimitExpression As String

    ''' <summary>
    ''' Stores whether top should be used for limit.
    ''' </summary>
    Private m_UseTopForLimit As Boolean

    ''' <summary>
    ''' Stores select expressions.
    ''' </summary>
    Private m_SelectExpression As String

    ''' <summary>
    ''' Stores included select expressions counter.
    ''' </summary>
    Private m_IncludedExpressionsCount As Int32

    ''' <summary>
    ''' Stores whether distincs is used.
    ''' </summary>
    Private m_UseDistinct As Boolean

    ''' <summary>
    ''' Stores parameters.
    ''' </summary>
    Private m_Parameters As List(Of SqlParameter)

    ''' <summary>
    ''' Stores parameter index offset.
    ''' </summary>
    Private m_ParameterIndexOffset As Int32

    ''' <summary>
    ''' Creates new instance of <see cref="SelectSqlExpressionBuilder"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="mainEntityType"></param>
    Public Sub New(<DisallowNull> context As DbContext, <DisallowNull> mainEntityType As Type)
      MyBase.New(context)
      Me.SubqueryContext = Nothing
      Initialize(mainEntityType)
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="SelectSqlExpressionBuilder"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="mainEntityType"></param>
    Public Sub New(<DisallowNull> context As SubqueryContext, <DisallowNull> mainEntityType As Type)
      MyBase.New(context.DbContext)
      Me.SubqueryContext = context
      Initialize(mainEntityType)
    End Sub

    ''' <summary>
    ''' Initializes values.
    ''' </summary>
    ''' <param name="mainEntityType"></param>
    Private Sub Initialize(<DisallowNull> mainEntityType As Type)
      m_Model = New SelectSqlModel(Me.DbContext.Model, GetMainEntity(mainEntityType))
      m_Visitor = New SqlExpressionVisitor(Me, m_Model)
      ' lists are created only when necessary
      m_MainTableSourceExpression = Nothing
      m_MainTableHints = Nothing
      m_CurrentJoinInfo = Nothing
      m_CurrentJoinTableHints = Nothing
      m_JoinExpressions = Nothing
      m_WhereExpressions = Nothing
      m_GroupByExpressions = Nothing
      m_HavingExpressions = Nothing
      m_OrderByExpressions = Nothing
      m_LimitExpression = Nothing
      m_UseTopForLimit = False
      m_SelectExpression = Nothing
      m_IncludedExpressionsCount = 0
      m_UseDistinct = False
      m_Parameters = New List(Of SqlParameter)
    End Sub

    ''' <summary>
    ''' Sets main table source.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="tableSource"></param>
    Public Sub SetMainTableSource(<DisallowNull> tableSource As FormattableString)
      Dim sql = ConvertToSqlString(tableSource, GetParameterIndex())
      m_MainTableSourceExpression = sql.Sql
      m_Parameters.AddRange(sql.Parameters)
    End Sub

    ''' <summary>
    ''' Sets main table source.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="tableSource"></param>
    ''' <param name="parameters"></param>
    Public Sub SetMainTableSource(<DisallowNull> tableSource As RawSqlString, <DisallowNull> ParamArray parameters() As Object)
      If parameters Is Nothing OrElse parameters.Length = 0 Then
        m_MainTableSourceExpression = tableSource.Value
      Else
        Dim sql = ConvertToSqlString(tableSource.Value, parameters, GetParameterIndex())
        m_MainTableSourceExpression = sql.Sql
        m_Parameters.AddRange(sql.Parameters)
      End If
    End Sub

    ''' <summary>
    ''' Sets main table hint(s).<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="tableHints"></param>
    Public Sub SetMainTableHints(<DisallowNull> tableHints As String)
      m_MainTableHints = tableHints
    End Sub

    ''' <summary>
    ''' Sets table hint(s) for last joined table.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="tableHints"></param>
    Public Sub SetLastJoinTableHints(<DisallowNull> tableHints As String)
      If Not m_CurrentJoinInfo.HasValue Then
        ' join has been conditionally ignored
        Exit Sub
      End If

      m_CurrentJoinTableHints = tableHints
    End Sub

    ''' <summary>
    ''' Adds join.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="joinType"></param>
    Public Sub AddJoin(Of TJoined)(<DisallowNull> joinType As JoinType)
      m_CurrentJoinInfo = New JoinInfo(joinType)
    End Sub

    ''' <summary>
    ''' Adds join.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="joinType"></param>
    ''' <param name="tableSourceFactory"></param>
    Public Sub AddJoin(Of TJoined)(joinType As JoinType, <DisallowNull> tableSourceFactory As Func(Of SubqueryContext, Subquery(Of TJoined)))
      ' TODO: SIP - implement subquery

      ' TODO: SIP - implement subquery - pass executor? change API?
      Dim context = New SubqueryContext(Me.DbContext, Nothing, GetParameterIndex())
      Dim subquery = tableSourceFactory.Invoke(context)
      Dim sql = subquery.Query

      m_CurrentJoinInfo = New JoinInfo(joinType, "(" & sql.Sql & ")", sql.Model.NonModelEntity)
      m_Parameters.AddRange(sql.Parameters)
    End Sub

    ''' <summary>
    ''' Adds join.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="joinType"></param>
    ''' <param name="tableSource"></param>
    Public Sub AddJoin(Of TJoined)(joinType As JoinType, <DisallowNull> tableSource As FormattableString)
      Dim sql = ConvertToSqlString(tableSource, GetParameterIndex())
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
    Public Sub AddJoin(Of TJoined)(joinType As JoinType, <DisallowNull> tableSource As RawSqlString, <DisallowNull> ParamArray parameters() As Object)
      If parameters Is Nothing OrElse parameters.Length = 0 Then
        m_CurrentJoinInfo = New JoinInfo(joinType, tableSource.Value)
      Else
        Dim sql = ConvertToSqlString(tableSource.Value, parameters, GetParameterIndex())
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
    Public Sub AddJoin(Of TJoined)(joinType As JoinType, <DisallowNull> predicate As Expression, entityIndexHints As Int32())
      AddJoin(Of TJoined)(New JoinInfo(joinType), Nothing, predicate, entityIndexHints)
    End Sub

    ''' <summary>
    ''' Adds join.<br/>
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="joinInfo"></param>
    ''' <param name="tableHints"></param>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    Private Sub AddJoin(Of TJoined)(joinInfo As JoinInfo, tableHints As String, predicate As Expression, entityIndexHints As Int32())
      If m_JoinExpressions Is Nothing Then
        m_JoinExpressions = New List(Of String)
      End If

      Dim relationship As SqlEntityRelationship

      If entityIndexHints Is Nothing Then
        relationship = TryGetRelationship(Of TJoined)(TryGetEntityIndexHints(predicate))
      Else
        relationship = TryGetRelationship(Of TJoined)(entityIndexHints)
      End If

      Dim nonModelEntity = joinInfo.NonModelEntity

      Dim sql As String
      Dim joinTypeString = GetJoinTypeString(joinInfo.JoinType)

      If nonModelEntity Is Nothing Then
        Dim entity = Me.DbContext.Model.GetEntity(GetType(TJoined))
        Dim sqlEntity = m_Model.AddJoin(entity, relationship)
        Dim tableAlias = sqlEntity.TableAlias

        If predicate Is Nothing Then
          If joinInfo.TableSource Is Nothing Then
            sql = joinTypeString & " " & Me.DialectProvider.Formatter.CreateIdentifier(entity.TableName, entity.Schema) & " " & Me.DialectProvider.Formatter.CreateIdentifier(tableAlias) & If(tableHints Is Nothing, "", " " & tableHints)
          Else
            sql = joinTypeString & " " & joinInfo.TableSource & " " & Me.DialectProvider.Formatter.CreateIdentifier(tableAlias) & If(tableHints Is Nothing, "", " " & tableHints)
          End If

          m_JoinExpressions.Add(sql)

        Else
          Dim result = m_Visitor.Translate(predicate, ExpressionTranslateMode.Condition, entityIndexHints, GetParameterIndex(), True, True)

          If joinInfo.TableSource Is Nothing Then
            sql = joinTypeString & " " & Me.DialectProvider.Formatter.CreateIdentifier(entity.TableName, entity.Schema) & " " & Me.DialectProvider.Formatter.CreateIdentifier(tableAlias) & If(tableHints Is Nothing, "", " " & tableHints) & " ON " & result.Sql
          Else
            sql = joinTypeString & " " & joinInfo.TableSource & " " & Me.DialectProvider.Formatter.CreateIdentifier(tableAlias) & If(tableHints Is Nothing, "", " " & tableHints) & " ON " & result.Sql
          End If

          m_JoinExpressions.Add(sql)
          m_Parameters.AddRange(result.Parameters)
        End If

      Else
        Dim sqlEntity = m_Model.AddJoin(nonModelEntity, relationship)
        Dim tableAlias = sqlEntity.TableAlias

        If predicate Is Nothing Then
          sql = joinTypeString & " " & joinInfo.TableSource & " " & Me.DialectProvider.Formatter.CreateIdentifier(tableAlias) & If(tableHints Is Nothing, "", " " & tableHints)

          m_JoinExpressions.Add(sql)

        Else
          Dim result = m_Visitor.Translate(predicate, ExpressionTranslateMode.Condition, entityIndexHints, GetParameterIndex(), True, True)

          sql = joinTypeString & " " & joinInfo.TableSource & " " & Me.DialectProvider.Formatter.CreateIdentifier(tableAlias) & If(tableHints Is Nothing, "", " " & tableHints) & " ON " & result.Sql

          m_JoinExpressions.Add(sql)
          m_Parameters.AddRange(result.Parameters)
        End If
      End If
    End Sub

    ''' <summary>
    ''' Gets SQL join type string.
    ''' </summary>
    ''' <param name="joinType"></param>
    ''' <returns></returns>
    Private Shared Function GetJoinTypeString(joinType As JoinType) As String
      Select Case joinType
        Case JoinType.Inner
          Return "INNER JOIN"
        Case JoinType.LeftOuter
          Return "LEFT OUTER JOIN"
        Case JoinType.RightOuter
          Return "RIGHT OUTER JOIN"
        Case JoinType.FullOuter
          Return "FULL OUTER JOIN"
        Case JoinType.CrossJoin
          Return "CROSS JOIN"
        Case Else
          Throw New NotSupportedException($"Unsupported join type '{joinType}'.")
      End Select
    End Function

    ''' <summary>
    ''' Adds ignored join.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entityType"></param>
    Public Sub AddIgnoredJoin(entityType As Type)
      ' TODO: SIP - implement subquery
      Dim entity = Me.DbContext.Model.GetEntity(entityType)
      m_Model.AddIgnoredJoin(entity)
    End Sub

    ''' <summary>
    ''' Adds on.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    Public Sub AddOn(Of TJoined)(<DisallowNull> predicate As Expression, entityIndexHints As Int32())
      If Not m_CurrentJoinInfo.HasValue Then
        ' join has been conditionally ignored
        Exit Sub
      End If

      AddJoin(Of TJoined)(m_CurrentJoinInfo.Value, m_CurrentJoinTableHints, predicate, entityIndexHints)

      m_CurrentJoinInfo = Nothing
      m_CurrentJoinTableHints = Nothing
    End Sub

    ''' <summary>
    ''' Tries to get entity index hints.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Private Function TryGetEntityIndexHints(predicate As Expression) As Int32()
      ' check is probably redundant, because right now this method is only called for IJoin parameter
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
      If entityIndexHints Is Nothing Then
        ' no relationship hint (exception is thrown later if needed)
        ' e.g. if we want just to use joined table in query and not return it at all
        Return Nothing
      End If

      ' try to infer relationship from model
      Dim sqlEntity = m_Model.GetEntity(entityIndexHints(0))

      If TypeOf sqlEntity IsNot EntityBasedSqlEntity Then
        ' relationships are supported only for model entities
        Return Nothing
      End If

      Dim declaringSqlEntity = DirectCast(sqlEntity, EntityBasedSqlEntity)
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
      End If

      ' no unambiguous match found; relationship might be specified later
      Return Nothing
    End Function

    ''' <summary>
    ''' Sets last join relationship.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="relationship">Lambda expression with one parameter is expected.</param>
    Public Sub SetLastJoinRelationship(<DisallowNull> relationship As Expression)
      Dim result = GetEntityAndProperty(relationship, True)

      If result.NotFound Then
        Throw New Exception($"Cannot infer relationship, because there are no joined entities of type '{result.EntityType}'.")
      End If

      If result.MultipleResults Then
        Throw New Exception($"Cannot infer relationship, because there are multiple joined entities of type '{result.EntityType}'. Use {NameOf(IJoin)} in relationship predicate to avoid unambiguous match.")
      End If

      If result.PropertyName Is Nothing Then
        Throw New Exception("Cannot infer relationship. Use expression that contains relationship property only.")
      End If

      If result.EntityNotEntityBased Then
        Throw New Exception($"Cannot set relationship. Relationship is supported only for model entities. Type '{result.EntityType}' is not defined in the model.")
      End If

      Dim lastEntity = m_Model.GetLastEntity()

      If TypeOf lastEntity IsNot EntityBasedSqlEntity Then
        Throw New Exception($"Cannot set relationship. Relationship is supported only for model entities. Type '{lastEntity.EntityType}' is not defined in the model.")
      End If

      Dim declaringSqlEntity = result.Entity
      Dim propertyType = result.PropertyType
      Dim propertyName = result.PropertyName

      If GetType(IList).IsAssignableFrom(propertyType) Then
        Dim genericTypes = propertyType.GetGenericArguments()

        If Not genericTypes.Count = 1 Then
          Throw New Exception($"Unable to infer item type from '{propertyType}'.")
        End If

        ' there is still small possibility that item type is not genericTypes(0) type, but in most cases like List(Of) we should be ok

        lastEntity.SetRelationship(New SqlEntityRelationship(declaringSqlEntity, New CollectionNavigation(propertyName, genericTypes(0), propertyType)))
      Else
        lastEntity.SetRelationship(New SqlEntityRelationship(declaringSqlEntity, New ReferenceNavigation(propertyName, propertyType)))
      End If
    End Sub

    ''' <summary>
    ''' Adds where.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    Public Sub AddWhere(<DisallowNull> predicate As Expression, entityIndexHints As Int32())
      If m_WhereExpressions Is Nothing Then
        m_WhereExpressions = New List(Of String)
      End If

      Dim result = m_Visitor.Translate(predicate, ExpressionTranslateMode.Condition, entityIndexHints, GetParameterIndex(), True, True)
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
      If m_WhereExpressions Is Nothing Then
        m_WhereExpressions = New List(Of String)
      End If

      If parameters Is Nothing OrElse parameters.Length = 0 Then
        m_WhereExpressions.Add(predicate)
      Else
        Dim sql = ConvertToSqlString(predicate, parameters, GetParameterIndex())
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
    Public Sub AddGroupBy(<DisallowNull> keySelector As Expression, entityIndexHints As Int32())
      If m_GroupByExpressions Is Nothing Then
        m_GroupByExpressions = New List(Of String)
      End If

      Dim result = m_Visitor.Translate(keySelector, ExpressionTranslateMode.GroupBy, entityIndexHints, GetParameterIndex(), True, True)
      m_GroupByExpressions.Add(result.Sql)
      m_Parameters.AddRange(result.Parameters)
    End Sub

    ''' <summary>
    ''' Adds having.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    Public Sub AddHaving(<DisallowNull> predicate As Expression, entityIndexHints As Int32())
      If m_HavingExpressions Is Nothing Then
        m_HavingExpressions = New List(Of String)
      End If

      Dim result = m_Visitor.Translate(predicate, ExpressionTranslateMode.Condition, entityIndexHints, GetParameterIndex(), True, True)
      m_HavingExpressions.Add(result.Sql)
      m_Parameters.AddRange(result.Parameters)
    End Sub

    ''' <summary>
    ''' Adds having.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    Public Sub AddHaving(<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object)
      If m_HavingExpressions Is Nothing Then
        m_HavingExpressions = New List(Of String)
      End If

      If parameters Is Nothing OrElse parameters.Length = 0 Then
        m_HavingExpressions.Add(predicate)
      Else
        Dim sql = ConvertToSqlString(predicate, parameters, GetParameterIndex())
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
    Public Sub AddOrderBy(<DisallowNull> keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean)
      If m_OrderByExpressions Is Nothing Then
        m_OrderByExpressions = New List(Of String)
      End If

      Dim result = m_Visitor.Translate(keySelector, ExpressionTranslateMode.OrderBy, entityIndexHints, GetParameterIndex(), True, True)

      If ascending Then
        m_OrderByExpressions.Add(result.Sql)
      Else
        m_OrderByExpressions.Add(result.Sql & " DESC")
      End If

      m_Parameters.AddRange(result.Parameters)
    End Sub

    ''' <summary>
    ''' Adds order by.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="ascending"></param>
    ''' <param name="parameters"></param>
    Public Sub AddOrderBy(<DisallowNull> predicate As String, ascending As Boolean, <DisallowNull> ParamArray parameters() As Object)
      If m_OrderByExpressions Is Nothing Then
        m_OrderByExpressions = New List(Of String)
      End If

      If parameters Is Nothing OrElse parameters.Length = 0 Then
        If ascending Then
          m_OrderByExpressions.Add(predicate)
        Else
          m_OrderByExpressions.Add(predicate & " DESC")
        End If
      Else
        Dim sql = ConvertToSqlString(predicate, parameters, GetParameterIndex())

        If ascending Then
          m_OrderByExpressions.Add(sql.Sql)
        Else
          m_OrderByExpressions.Add(sql.Sql & " DESC")
        End If

        m_Parameters.AddRange(sql.Parameters)
      End If
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
    ''' Adds automatic select of (all) columns.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="behavior"></param>
    Public Sub AddSelectAll(behavior As SelectColumnsBehavior)
      If behavior = SelectColumnsBehavior.ExcludeNonRequiredColumns Then
        ' main entity is always included
        For i = 1 To m_Model.GetEntityCount() - 1
          Dim sqlEntity = m_Model.GetEntity(i)

          If sqlEntity.Relationship Is Nothing Then
            sqlEntity.Exclude()
          End If
        Next
      End If

      m_Model.SqlResult = New EntitySqlResult(m_Model.MainEntity)
    End Sub

    ''' <summary>
    ''' Excludes selected property.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="propertyExpression">Lambda expression with one parameter is expected.</param>
    Public Sub ExcludeSelected(<DisallowNull> propertyExpression As Expression)
      Dim result = GetEntityAndProperty(propertyExpression)

      If result.NotFound Then
        Throw New Exception($"Cannot infer entity for column exclude, because there are no joined entities of type '{result.EntityType}'.")
      End If

      If result.MultipleResults Then
        Throw New Exception($"Cannot infer entity for column exclude, because there are multiple joined entities of type '{result.EntityType}'. Use {NameOf(IJoin)} in exclude expression to avoid unambiguous match.")
      End If

      If result.PropertyName Is Nothing Then
        Throw New Exception("Cannot infer excluded column. Use expression that contains entity property only.")
      End If

      If result.EntityNotEntityBased Then
        Throw New Exception($"Cannot exclude column. Exclusion is supported only for model entities. Type '{result.EntityType}' is not defined in the model.")
      End If

      Dim entity = result.Entity
      Dim prop = entity.Entity.GetProperty(result.PropertyName)

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
    ''' Includes selected property.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="entityIndexHints"></param>
    Public Sub IncludeToSelected(<DisallowNull> action As Expression, entityIndexHints As Int32())
      Dim result = m_Visitor.TranslateIncludeAction(action, entityIndexHints, GetParameterIndex(), m_IncludedExpressionsCount)
      m_Parameters.AddRange(result.SqlString.Parameters)
      m_IncludedExpressionsCount += 1

      Dim sqlEntity = m_Model.GetEntity(result.EntityIndex)
      sqlEntity.AddIncludedSqlResult(New SqlEntityIncludedResult(result.SqlString.Sql, result.PropertyName, result.Result))
    End Sub

    ''' <summary>
    ''' Includes selected property.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="keySelectorEntityIndexHints"></param>
    ''' <param name="valueSelectorEntityIndexHints"></param>
    Public Sub IncludeToSelected(<DisallowNull> keySelector As Expression, <DisallowNull> valueSelector As Expression, keySelectorEntityIndexHints As Int32(), valueSelectorEntityIndexHints As Int32())
      Dim keyResult = GetEntityAndProperty(keySelector)

      If keyResult.NotFound Then
        Throw New Exception($"Cannot infer entity for column include, because there are no joined entities of type '{keyResult.EntityType}'.")
      End If

      If keyResult.MultipleResults Then
        Throw New Exception($"Cannot infer entity for column include, because there are multiple joined entities of type '{keyResult.EntityType}'. Use {NameOf(IJoin)} in include expression to avoid unambiguous match.")
      End If

      If keyResult.PropertyName Is Nothing Then
        Throw New Exception("Cannot infer included column. Use expression that contains entity property only.")
      End If

      If keyResult.EntityNotEntityBased Then
        Throw New Exception($"Cannot include column. Inclusion is supported only for model entities. Type '{keyResult.EntityType}' is not defined in the model.")
      End If

      Dim valueResult = m_Visitor.TranslateIncludeValueSelector(valueSelector, valueSelectorEntityIndexHints, GetParameterIndex(), m_IncludedExpressionsCount)
      m_Parameters.AddRange(valueResult.SqlString.Parameters)
      m_IncludedExpressionsCount += 1

      Dim sqlEntity = keyResult.Entity
      sqlEntity.AddIncludedSqlResult(New SqlEntityIncludedResult(valueResult.SqlString.Sql, keyResult.PropertyName, valueResult.Result))
    End Sub

    ''' <summary>
    ''' Adds select count.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    Public Sub AddSelectCount()
      m_SelectExpression = "COUNT(*)"
    End Sub

    ''' <summary>
    ''' Adds custom select.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="selector"></param>
    ''' <param name="entityIndexHints"></param>
    Public Sub AddSelect(<DisallowNull> selector As Expression, entityIndexHints As Int32())
      Dim isSubquery = Me.SubqueryContext IsNot Nothing
      Dim result = m_Visitor.TranslateCustomSelect(selector, entityIndexHints, GetParameterIndex(), isSubquery)
      m_SelectExpression = result.SqlString.Sql
      m_Parameters.AddRange(result.SqlString.Parameters)
      m_Model.SqlResult = result.SqlResult
      m_Model.NonModelEntity = result.NonModelEntity
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
        Dim entityBase = m_Model.GetEntity(i)

        If Not entityBase.IsExcludedOrIgnored Then
          ' NOTE: all non model based entities should be excluded
          If TypeOf entityBase IsNot EntityBasedSqlEntity Then
            Throw New Exception("Model based entity is expected.")
          End If

          Dim entity = DirectCast(entityBase, EntityBasedSqlEntity)

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

          Dim includedSqlResults = entity.IncludedSqlResults

          If includedSqlResults IsNot Nothing Then
            For j = 0 To includedSqlResults.Count - 1
              sql.Append(", ") ' there should be at least one column already
              sql.Append(includedSqlResults(j).Sql)
            Next
          End If
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
        Dim entity = m_Model.MainEntity.Entity
        Me.DialectProvider.Formatter.AppendIdentifier(sql, entity.TableName, entity.Schema)
      Else
        sql.Append(m_MainTableSourceExpression)
      End If

      sql.Append(" ")
      Me.DialectProvider.Formatter.AppendIdentifier(sql, m_Model.MainEntity.TableAlias)

      If m_MainTableHints IsNot Nothing Then
        sql.Append(" ")
        sql.Append(m_MainTableHints)
      End If

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

    ''' <summary>
    ''' Creates SQL subquery.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <returns></returns>
    Public Function CreateSubquery(Of T)() As Subquery(Of T)
      Return New Subquery(Of T)(CreateQuery())
    End Function

    ''' <summary>
    ''' Gets parameter index.
    ''' </summary>
    ''' <returns></returns>
    Private Function GetParameterIndex() As Int32
      Return m_ParameterIndexOffset + m_Parameters.Count
    End Function

    ''' <summary>
    ''' Gets entity and property name from an expression.
    ''' </summary>
    ''' <param name="propertyExpression"></param>
    ''' <param name="excludeLastModelEntity"></param>
    ''' <returns></returns>
    Private Function GetEntityAndProperty(propertyExpression As Expression, Optional excludeLastModelEntity As Boolean = False) As (EntityType As Type, Entity As EntityBasedSqlEntity, PropertyType As Type, PropertyName As String, NotFound As Boolean, MultipleResults As Boolean, EntityNotEntityBased As Boolean)
      If TypeOf propertyExpression IsNot LambdaExpression Then
        Throw New ArgumentException("Expression must be of type LambdaExpression.")
      End If

      Dim lambda = DirectCast(propertyExpression, LambdaExpression)

      Dim parameterType = lambda.Parameters(0).Type
      Dim returnType = lambda.ReturnType
      Dim entity As EntityBasedSqlEntity = Nothing
      Dim propertyName As String = Nothing
      Dim notFound = False
      Dim multipleResults = False
      Dim entityNotEntityBased = False

      If GetType(IJoin).IsAssignableFrom(parameterType) Then
        If lambda.Body.NodeType = ExpressionType.MemberAccess Then
          Dim node = DirectCast(lambda.Body, MemberExpression)

          If node.Expression.NodeType = ExpressionType.MemberAccess Then
            Dim index = Helpers.Common.GetEntityIndexFromJoinMemberName(DirectCast(node.Expression, MemberExpression).Member.Name)

            If excludeLastModelEntity AndAlso index = m_Model.GetEntityCount() - 1 Then
              ' this should never happen, because we only use excludeLastModelEntity in SetLastJoinRelationship and there the IJoin doesn't contain the last entity
            Else
              Dim entityItem = m_Model.GetEntity(index)

              If TypeOf entityItem Is EntityBasedSqlEntity Then
                entity = DirectCast(m_Model.GetEntity(index), EntityBasedSqlEntity)
                propertyName = node.Member.Name
              Else
                entityNotEntityBased = True
              End If
            End If
          End If
        End If

      Else
        ' LINQ not used for performance and allocation reasons

        Dim entities = m_Model.GetEntities()
        Dim count = entities.Length - If(excludeLastModelEntity, 1, 0)

        For i = 0 To count - 1
          Dim item = entities(i)

          If item.EntityType Is parameterType Then
            If entity Is Nothing Then
              If TypeOf item Is EntityBasedSqlEntity Then
                entity = DirectCast(item, EntityBasedSqlEntity)

                If lambda.Body.NodeType = ExpressionType.MemberAccess Then
                  propertyName = DirectCast(lambda.Body, MemberExpression).Member.Name
                End If
              Else
                entityNotEntityBased = True
              End If
            Else
              entity = Nothing
              multipleResults = True
              Exit For
            End If
          End If
        Next

        If entity Is Nothing AndAlso Not multipleResults Then
          notFound = True
        End If
      End If

      Return (parameterType, entity, returnType, propertyName, notFound, multipleResults, entityNotEntityBased)
    End Function

  End Class
End Namespace
