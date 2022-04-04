Imports System.Linq.Expressions
Imports System.Text
Imports System.Collections.ObjectModel
Imports System.Runtime.CompilerServices
Imports System.Reflection
Imports Yamo.Internal.Query
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Sql
Imports System.Diagnostics.CodeAnalysis

Namespace Internal

  ''' <summary>
  ''' SQL expression visitor.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SqlExpressionVisitor
    Inherits ExpressionVisitor

    ''' <summary>
    ''' New expression type
    ''' </summary>
    Private Enum NewExpressionType
      ValueTuple
      Anonymous
      AdHoc
    End Enum

    ''' <summary>
    ''' Stores SQL expression builder.
    ''' </summary>
    Private m_Builder As SqlExpressionBuilderBase

    ''' <summary>
    ''' Stores SQL model.
    ''' </summary>
    Private m_Model As SqlModelBase

    ''' <summary>
    ''' Stores expression translate mode.
    ''' </summary>
    Private m_Mode As ExpressionTranslateMode

    ''' <summary>
    ''' Stores expression parameters.
    ''' </summary>
    Private m_ExpressionParameters As ReadOnlyCollection(Of ParameterExpression)

    ''' <summary>
    ''' Stores expression parameters type.
    ''' </summary>
    Private m_ExpressionParametersType As ExpressionParametersType

    ''' <summary>
    ''' Stores entity index hints.
    ''' </summary>
    Private m_EntityIndexHints As Int32()

    ''' <summary>
    ''' Stores output SQL string.
    ''' </summary>
    Private m_Sql As StringBuilder

    ''' <summary>
    ''' Stores output SQL parameters.
    ''' </summary>
    Private m_Parameters As List(Of SqlParameter)

    ''' <summary>
    ''' Stores parameter index.
    ''' </summary>
    Private m_ParameterIndex As Int32

    ''' <summary>
    ''' Stores include index.
    ''' </summary>
    Private m_IncludeIndex As Int32

    ''' <summary>
    ''' Stores whether aliases should be used.
    ''' </summary>
    Private m_UseAliases As Boolean

    ''' <summary>
    ''' Shores whether table names or aliases should be used.
    ''' </summary>
    Private m_UseTableNamesOrAliases As Boolean

    ''' <summary>
    ''' Stores current like parameter format.
    ''' </summary>
    Private m_CurrentLikeParameterFormat As String

    ''' <summary>
    ''' Stores custom SQL result.
    ''' </summary>
    Private m_CustomSqlResult As SqlResultBase

    ''' <summary>
    ''' Stores index of custom SQL result item.
    ''' </summary>
    Private m_CustomSqlResultItemIndex As Int32

    ''' <summary>
    ''' Stores non model entity.
    ''' </summary>
    Private m_NonModelEntity As NonModelEntity

    ''' <summary>
    ''' Stores stack of nodes.
    ''' </summary>
    Private m_Stack As Stack(Of NodeInfo)

    ''' <summary>
    ''' Creates new instance of <see cref="SqlExpressionVisitor"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="model"></param>
    Public Sub New(<DisallowNull> builder As SqlExpressionBuilderBase, <DisallowNull> model As SqlModelBase)
      m_Builder = builder
      m_Model = model
      m_Stack = New Stack(Of NodeInfo)
    End Sub

    ''' <summary>
    ''' Initializes values before translate call.
    ''' </summary>
    ''' <param name="lambda"></param>
    ''' <param name="mode"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <param name="parameterIndex"></param>
    ''' <param name="includeIndex"></param>
    ''' <param name="useAliases"></param>
    ''' <param name="useTableNamesOrAliases"></param>
    Private Sub Initialize(lambda As LambdaExpression, mode As ExpressionTranslateMode, entityIndexHints As Int32(), parameterIndex As Int32, includeIndex As Int32, useAliases As Boolean, useTableNamesOrAliases As Boolean)
      m_Mode = mode
      m_ExpressionParameters = lambda.Parameters
      m_ExpressionParametersType = If(entityIndexHints Is Nothing, ExpressionParametersType.IJoin, ExpressionParametersType.Entities)
      m_EntityIndexHints = entityIndexHints
      m_Sql = New StringBuilder()
      m_Parameters = New List(Of SqlParameter)
      m_ParameterIndex = parameterIndex
      m_IncludeIndex = includeIndex
      m_UseAliases = useAliases
      m_UseTableNamesOrAliases = useTableNamesOrAliases
      m_CurrentLikeParameterFormat = Nothing
      m_CustomSqlResult = Nothing
      m_CustomSqlResultItemIndex = 0
      m_NonModelEntity = Nothing
      m_Stack.Clear()
    End Sub

    ''' <summary>
    ''' Translates expression to SQL string.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="expression"></param>
    ''' <param name="mode"></param>
    ''' <param name="entityIndexHints"><see langword="Nothing"/> for <see cref="ExpressionParametersType.IJoin"/> and not <see langword="Nothing"/> for <see cref="ExpressionParametersType.Entities"/>.</param>
    ''' <param name="parameterIndex"></param>
    ''' <param name="useAliases"></param>
    ''' <param name="useTableNamesOrAliases"></param>
    ''' <returns></returns>
    Public Function Translate(<DisallowNull> expression As Expression, mode As ExpressionTranslateMode, entityIndexHints As Int32(), parameterIndex As Int32, useAliases As Boolean, useTableNamesOrAliases As Boolean) As SqlString
      If TypeOf expression IsNot LambdaExpression Then
        Throw New ArgumentException("Expression must be of type LambdaExpression.")
      End If

      Dim lambda = DirectCast(expression, LambdaExpression)

      Initialize(lambda, mode, entityIndexHints, parameterIndex, -1, useAliases, useTableNamesOrAliases)

      Visit(lambda.Body)

      m_ExpressionParameters = Nothing

      Return New SqlString(m_Sql.ToString(), m_Parameters)
    End Function

    ''' <summary>
    ''' Translates custom select expression.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="expression"></param>
    ''' <param name="entityIndexHints"><see langword="Nothing"/> for <see cref="ExpressionParametersType.IJoin"/> and not <see langword="Nothing"/> for <see cref="ExpressionParametersType.Entities"/>.</param>
    ''' <param name="parameterIndex"></param>
    ''' <param name="createNonModelEntity"></param>
    ''' <returns></returns>
    Public Function TranslateCustomSelect(<DisallowNull> expression As Expression, entityIndexHints As Int32(), parameterIndex As Int32, createNonModelEntity As Boolean) As (SqlString As SqlString, SqlResult As SqlResultBase, NonModelEntity As NonModelEntity)
      If TypeOf expression IsNot LambdaExpression Then
        Throw New ArgumentException("Expression must be of type LambdaExpression.")
      End If

      Dim lambda = DirectCast(expression, LambdaExpression)

      Initialize(lambda, ExpressionTranslateMode.CustomSelect, entityIndexHints, parameterIndex, -1, True, True)

      If createNonModelEntity Then
        m_NonModelEntity = New NonModelEntity(lambda.ReturnType)
      End If

      VisitInCustomSelectOrIncludeMode(lambda.Body)

      m_ExpressionParameters = Nothing

      If createNonModelEntity Then
        m_NonModelEntity.SetSqlResult(m_CustomSqlResult)
      End If

      Return (New SqlString(m_Sql.ToString(), m_Parameters), m_CustomSqlResult, m_NonModelEntity)
    End Function

    ''' <summary>
    ''' Translates custom column(s) include action expression.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="expression"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <param name="parameterIndex"></param>
    ''' <param name="includeIndex"></param>
    ''' <returns></returns>
    Public Function TranslateIncludeAction(<DisallowNull> expression As Expression, entityIndexHints As Int32(), parameterIndex As Int32, includeIndex As Int32) As (SqlString As SqlString, EntityIndex As Int32, PropertyName As String, Result As SqlResultBase)
      If TypeOf expression IsNot LambdaExpression Then
        Throw New ArgumentException("Expression must be of type LambdaExpression.")
      End If

      Dim lambda = DirectCast(expression, LambdaExpression)

      Initialize(lambda, ExpressionTranslateMode.Include, entityIndexHints, parameterIndex, includeIndex, True, True)

      If Not lambda.Body.NodeType = ExpressionType.Call Then
        Throw New Exception($"Cannot process the expression. Body NodeType {lambda.Body.NodeType} is not allowed.")
      End If

      Dim node = DirectCast(lambda.Body, MethodCallExpression)
      Dim isEntity = False
      Dim isJoinedEntity = False

      If node.Method.IsSpecialName AndAlso node.Object IsNot Nothing AndAlso node.Method.Name.StartsWith("set_") Then
        isEntity = Me.IsEntity(node.Object)
        isJoinedEntity = Me.IsJoinedEntity(node.Object)
      End If

      Dim entityIndex As Int32

      If isEntity Then
        entityIndex = GetEntityIndex(DirectCast(node.Object, ParameterExpression))
      ElseIf isJoinedEntity Then
        entityIndex = Helpers.Common.GetEntityIndexFromJoinMemberName(DirectCast(node.Object, MemberExpression).Member.Name)
      Else
        Throw New Exception("Cannot process the expression.")
      End If

      Dim propertyName = node.Method.Name.Substring(4) ' trim "set_"

      Dim valueNode = node.Arguments(0)

      If valueNode.Type Is GetType(Object) AndAlso (valueNode.NodeType = ExpressionType.Convert OrElse valueNode.NodeType = ExpressionType.ConvertChecked) Then
        ' get correct type in cases of assigning to property of type Object (ignore implicit cast)
        valueNode = DirectCast(valueNode, UnaryExpression).Operand
      End If

      VisitInCustomSelectOrIncludeMode(valueNode)

      m_ExpressionParameters = Nothing

      Return (New SqlString(m_Sql.ToString(), m_Parameters), entityIndex, propertyName, m_CustomSqlResult)
    End Function

    ''' <summary>
    ''' Translates custom column(s) include value selector expression.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="expression"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <param name="parameterIndex"></param>
    ''' <param name="includeIndex"></param>
    ''' <returns></returns>
    Public Function TranslateIncludeValueSelector(<DisallowNull> expression As Expression, entityIndexHints As Int32(), parameterIndex As Int32, includeIndex As Int32) As (SqlString As SqlString, Result As SqlResultBase)
      If TypeOf expression IsNot LambdaExpression Then
        Throw New ArgumentException("Expression must be of type LambdaExpression.")
      End If

      Dim lambda = DirectCast(expression, LambdaExpression)

      Initialize(lambda, ExpressionTranslateMode.Include, entityIndexHints, parameterIndex, includeIndex, True, True)

      Dim node = lambda.Body

      If node.Type Is GetType(Object) AndAlso (node.NodeType = ExpressionType.Convert OrElse node.NodeType = ExpressionType.ConvertChecked) Then
        ' get correct type in cases of assigning to property of type Object (ignore implicit cast)
        node = DirectCast(node, UnaryExpression).Operand
      End If

      VisitInCustomSelectOrIncludeMode(node)

      m_ExpressionParameters = Nothing

      Return (New SqlString(m_Sql.ToString(), m_Parameters), m_CustomSqlResult)
    End Function

    ''' <summary>
    ''' Visits expression.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Public Overrides Function Visit(node As Expression) As Expression
      m_Stack.Push(New NodeInfo(node))
      Dim result = MyBase.Visit(node)
      m_Stack.Pop()
      Return result
    End Function

    ''' <summary>
    ''' Visits method call.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Protected Overrides Function VisitMethodCall(node As MethodCallExpression) As Expression
      If node.Method.DeclaringType Is GetType(String) Then
        Select Case node.Method.Name
          Case NameOf(String.StartsWith)
            Return VisitStringStartsWithMethodCall(node)
          Case NameOf(String.EndsWith)
            Return VisitStringEndsWithMethodCall(node)
          Case NameOf(String.Contains)
            Return VisitStringContainsMethodCall(node)
          Case NameOf(String.Concat)
            Return VisitStringConcatMethodCall(node)
        End Select
      End If

      If node.Method.DeclaringType Is GetType(FormattableStringFactory) Then
        If node.Method.Name = NameOf(FormattableStringFactory.Create) Then
          Return VisitFormattableStringFactoryCreateMethodCall(node)
        End If
      End If

      If node.Method.IsStatic Then
        If GetType(SqlHelper).IsAssignableFrom(node.Method.DeclaringType) Then
          Return VisitSqlHelperMethodCall(node)
        End If

        If node.Method.DeclaringType Is GetType(RawSqlString) Then
          Return VisitRawSqlStringCreateMethodCall(node)
        End If
      End If

      If node.Method.DeclaringType Is GetType(Enumerable) Then
        ' arrays
        If node.Method.Name = NameOf(Enumerable.Contains) Then
          Return VisitEnumerableContainsMethodCall(node)
        End If
      End If

      If GetType(IEnumerable).IsAssignableFrom(node.Method.DeclaringType) Then
        ' lists, etc. (IEnumerable without Contains are filtered out)
        If node.Method.Name = NameOf(Enumerable.Contains) Then
          Return VisitIEnumerableContainsMethodCall(node)
        End If
      End If

      ' node.Object is null when static indexer is called
      If node.Method.IsSpecialName AndAlso node.Object IsNot Nothing AndAlso node.Object.NodeType = ExpressionType.Parameter AndAlso node.Method.Name.StartsWith("set_") Then
        ' this is needed when we pass Action (in UPDATE SET... expressions)
        ' TODO: SIP - investigate how indexers behave and whether they can cause a problem here
        Return VisitEntityPropertySetter(node)
      End If

      Dim result = VisitAndEvaluate(node)

      ExpandToBooleanComparisonIfNeeded(node)

      Return result
    End Function

    ''' <summary>
    ''' Visits <see cref="[String].StartsWith"/> method call.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitStringStartsWithMethodCall(node As MethodCallExpression) As Expression
      Visit(node.Object)

      m_Sql.Append(" LIKE ")

      If m_Builder.DialectProvider.Formatter.LikeWildcardsInParameter Then
        m_CurrentLikeParameterFormat = "{0}%"
        Visit(node.Arguments(0))

        If m_CurrentLikeParameterFormat IsNot Nothing Then
          Throw New Exception("Only constant string or evaluable expression is allowed as StartsWith parameter.")
        End If
      Else
        Visit(node.Arguments(0))
        m_Sql.Append(" + '%'")
      End If

      Return node
    End Function

    ''' <summary>
    ''' Visits <see cref="[String].EndsWith"/> method call.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitStringEndsWithMethodCall(node As MethodCallExpression) As Expression
      Visit(node.Object)

      If m_Builder.DialectProvider.Formatter.LikeWildcardsInParameter Then
        m_Sql.Append(" LIKE ")

        m_CurrentLikeParameterFormat = "%{0}"
        Visit(node.Arguments(0))

        If m_CurrentLikeParameterFormat IsNot Nothing Then
          Throw New Exception("Only constant string or evaluable expression is allowed as EndsWith parameter.")
        End If
      Else
        m_Sql.Append(" LIKE '%' + ")
        Visit(node.Arguments(0))
      End If

      Return node
    End Function

    ''' <summary>
    ''' Visits <see cref="[String].Contains"/> method call.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitStringContainsMethodCall(node As MethodCallExpression) As Expression
      Visit(node.Object)

      If m_Builder.DialectProvider.Formatter.LikeWildcardsInParameter Then
        m_Sql.Append(" LIKE ")

        m_CurrentLikeParameterFormat = "%{0}%"
        Visit(node.Arguments(0))

        If m_CurrentLikeParameterFormat IsNot Nothing Then
          Throw New Exception("Only constant string or evaluable expression is allowed as Contains parameter.")
        End If
      Else
        m_Sql.Append(" LIKE '%' + ")
        Visit(node.Arguments(0))
        m_Sql.Append(" + '%'")
      End If

      Return node
    End Function

    ''' <summary>
    ''' Visits <see cref="[String].Concat"/> method call.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitStringConcatMethodCall(node As MethodCallExpression) As Expression
      Dim expressions = node.Arguments
      Dim count = node.Arguments.Count

      If count = 1 AndAlso node.Arguments(0).NodeType = ExpressionType.NewArrayInit Then
        ' for too many concat parameters, overload that accepts String() or Object() is used
        ' there are also overloads that accepts IEnumerable(Of String) and IEnumerable(Of T), but I guess, they'll never be used in this context
        Dim arr = DirectCast(node.Arguments(0), NewArrayExpression)
        expressions = arr.Expressions
        count = expressions.Count
      End If

      For i = 0 To count - 1
        Visit(expressions(i))

        If Not i = count - 1 Then
          m_Sql.Append(" ")
          m_Sql.Append(m_Builder.DialectProvider.Formatter.StringConcatenationOperator)
          m_Sql.Append(" ")
        End If
      Next

      Return node
    End Function

    ''' <summary>
    ''' Visits <see cref="FormattableStringFactory.Create"/> method call.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitFormattableStringFactoryCreateMethodCall(node As MethodCallExpression) As Expression
      Dim format = DirectCast(DirectCast(node.Arguments(0), ConstantExpression).Value, String)
      Dim argsExpression = DirectCast(node.Arguments(1), NewArrayExpression)

      Dim formatArgs = New StringBuilder(argsExpression.Expressions.Count - 1) {}
      Dim sqlBuilder = m_Sql

      For i = 0 To argsExpression.Expressions.Count - 1
        m_Sql = New StringBuilder()
        Visit(argsExpression.Expressions(i))
        formatArgs(i) = m_Sql
      Next

      m_Sql = sqlBuilder

      m_Sql.AppendFormat(format, formatArgs)

      Return node
    End Function

    ''' <summary>
    ''' Visits SQL helper method call.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitSqlHelperMethodCall(node As MethodCallExpression) As Expression
      Dim dialectProvider = m_Builder.DialectProvider

      Dim sqlHelperType = node.Method.DeclaringType
      Dim dialectSpecifictSqlHelperType = dialectProvider.GetDialectSpecificSqlHelper(sqlHelperType)

      If dialectSpecifictSqlHelperType IsNot Nothing Then
        sqlHelperType = dialectSpecifictSqlHelperType
      End If

      Dim getSqlFormatMethod = sqlHelperType.GetMethod(NameOf(SqlHelper.GetSqlFormat), BindingFlags.Public Or BindingFlags.Static)
      Dim sqlFormat = DirectCast(getSqlFormatMethod.Invoke(Nothing, {node, dialectProvider}), SqlFormat)

      Dim args = New StringBuilder(sqlFormat.Arguments.Count - 1) {}
      Dim sqlBuilder = m_Sql

      For i = 0 To sqlFormat.Arguments.Count - 1
        m_Sql = New StringBuilder()
        Visit(sqlFormat.Arguments(i))
        args(i) = m_Sql
      Next

      m_Sql = sqlBuilder

      m_Sql.AppendFormat(sqlFormat.Format, args)

      Return node
    End Function

    ''' <summary>
    ''' Visits <see cref="RawSqlString.Create"/> method call.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitRawSqlStringCreateMethodCall(node As MethodCallExpression) As Expression
      m_Sql.Append(Evaluate(node.Arguments(0)))
      Return node
    End Function

    ''' <summary>
    ''' Visits <see cref="Enumerable.Contains"/> method call.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitEnumerableContainsMethodCall(node As MethodCallExpression) As Expression
      Dim valueExpression = node.Arguments(1)

      Dim enumerableExpression = node.Arguments(0)

      If enumerableExpression.NodeType = ExpressionType.Convert OrElse enumerableExpression.NodeType = ExpressionType.ConvertChecked Then
        enumerableExpression = DirectCast(enumerableExpression, UnaryExpression).Operand
      End If

      If enumerableExpression.NodeType = ExpressionType.NewArrayBounds Then
        m_Sql.Append("(0 = 1)")

      ElseIf enumerableExpression.NodeType = ExpressionType.NewArrayInit Then
        Dim newArrayExpression = DirectCast(enumerableExpression, NewArrayExpression)
        Dim count = newArrayExpression.Expressions.Count

        If count = 0 Then
          m_Sql.Append("(0 = 1)")

        Else
          m_Sql.Append("(")
          Visit(valueExpression)
          m_Sql.Append(" IN (")

          For i = 0 To count - 2
            Visit(newArrayExpression.Expressions(i))
            m_Sql.Append(", ")
          Next
          Visit(newArrayExpression.Expressions(count - 1))

          m_Sql.Append("))")
        End If

      Else
        Dim enumerableValues = DirectCast(Evaluate(enumerableExpression), IEnumerable).Cast(Of Object).ToArray()
        Dim count = enumerableValues.Length

        If count = 0 Then
          m_Sql.Append("(0 = 1)")

        Else
          m_Sql.Append("(")
          Visit(valueExpression)
          m_Sql.Append(" IN (")

          For i = 0 To count - 2
            AppendNewParameter(enumerableValues(i))
            m_Sql.Append(", ")
          Next
          AppendNewParameter(enumerableValues(count - 1))

          m_Sql.Append("))")
        End If
      End If

      Return node
    End Function

    ''' <summary>
    ''' Visits <see cref="Enumerable.Contains"/> method call on <see cref="IEnumerable"/>.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitIEnumerableContainsMethodCall(node As MethodCallExpression) As Expression
      Dim valueExpression = node.Arguments(0)

      Dim enumerableValues = DirectCast(Evaluate(node.Object), IEnumerable).Cast(Of Object).ToArray()
      Dim count = enumerableValues.Length

      If count = 0 Then
        m_Sql.Append("(0 = 1)")

      Else
        m_Sql.Append("(")
        Visit(valueExpression)
        m_Sql.Append(" IN (")

        For i = 0 To count - 2
          AppendNewParameter(enumerableValues(i))
          m_Sql.Append(", ")
        Next
        AppendNewParameter(enumerableValues(count - 1))

        m_Sql.Append("))")
      End If

      Return node
    End Function

    ''' <summary>
    ''' Visits entity property setter.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitEntityPropertySetter(node As MethodCallExpression) As Expression
      Dim propertyName = node.Method.Name.Substring(4) ' trim "set_"
      Dim entityIndex = 0 ' should be always 0 in so far allowed scenarios (UPDATE SET) and m_EntityIndexHints should always be set

      AppendEntityMemberAccess(entityIndex, propertyName)

      m_Sql.Append(" = ")

      Visit(node.Arguments(0))

      Return node
    End Function

    ''' <summary>
    ''' Visits unary.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Protected Overrides Function VisitUnary(node As UnaryExpression) As Expression
      Select Case node.NodeType
        Case ExpressionType.[Not]
          m_Stack.Peek().IsNegation = True

          If ShouldIgnoreNegation(node) Then
            m_Stack.Peek().IsIgnoredNegation = True
          Else
            m_Sql.Append("NOT ")
          End If

        Case ExpressionType.Convert, ExpressionType.ConvertChecked
          m_Stack.Peek().IsConvert = True

        Case Else
          Throw New NotSupportedException($"The unary operator '{node.NodeType}' is not supported.")
      End Select

      Visit(node.Operand)

      Return node
    End Function

    ''' <summary>
    ''' Visits binary.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Protected Overrides Function VisitBinary(node As BinaryExpression) As Expression
      Dim left = node.Left
      Dim right = node.Right
      Dim nodeType = node.NodeType

      ' this is special handling for VB string comparison, which has form of {(CompareString(foo, "bar", False) == 0)}
      If TypeOf node.Left Is MethodCallExpression Then
        Dim methodCallExpr = DirectCast(node.Left, MethodCallExpression)

        ' we should check type, but we would have to reference Microsoft.VisualBasic.dll for that
        If methodCallExpr.Method.Name = "CompareString" AndAlso methodCallExpr.Method.DeclaringType.FullName = "Microsoft.VisualBasic.CompilerServices.Operators" Then
          left = methodCallExpr.Arguments(0)
          right = methodCallExpr.Arguments(1)
        End If
      End If

      Dim useBrackets = False
      Dim expOperator As String

      Select Case nodeType
        Case ExpressionType.[And], ExpressionType.[AndAlso]
          useBrackets = True
          expOperator = " AND "

        Case ExpressionType.[Or], ExpressionType.[OrElse]
          useBrackets = True
          expOperator = " OR "

        Case ExpressionType.Equal
          m_Stack.Peek().IsCompare = True

          If IsNullConstant(right) Then
            expOperator = " IS "
          Else
            expOperator = " = "
          End If

        Case ExpressionType.NotEqual
          m_Stack.Peek().IsCompare = True

          If IsNullConstant(right) Then
            expOperator = " IS NOT "
          Else
            expOperator = " <> "
          End If

        Case ExpressionType.LessThan
          expOperator = " < "

        Case ExpressionType.LessThanOrEqual
          expOperator = " <= "

        Case ExpressionType.GreaterThan
          expOperator = " > "

        Case ExpressionType.GreaterThanOrEqual
          expOperator = " >= "

        Case ExpressionType.Add, ExpressionType.AddChecked
          useBrackets = True
          expOperator = " + "

        Case ExpressionType.Subtract, ExpressionType.SubtractChecked
          useBrackets = True
          expOperator = " - "

        Case ExpressionType.Multiply, ExpressionType.MultiplyChecked
          useBrackets = True
          expOperator = " * "

        Case ExpressionType.Divide
          useBrackets = True
          expOperator = " / "

        Case ExpressionType.Modulo
          useBrackets = True
          expOperator = " % "

        Case Else
          Throw New NotSupportedException($"The binary operator '{nodeType}' is not supported.")
      End Select

      If useBrackets Then
        m_Sql.Append("(")
      End If

      Visit(left)
      m_Sql.Append(expOperator)
      Visit(right)

      If useBrackets Then
        m_Sql.Append(")")
      End If

      Return node
    End Function

    ''' <summary>
    ''' Visits constant.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Protected Overrides Function VisitConstant(node As ConstantExpression) As Expression
      If node.Value Is Nothing Then
        m_Sql.Append("NULL")
      Else
        Select Case node.Value.GetType()
          Case GetType(String)
            Dim value = DirectCast(node.Value, String)

            If m_CurrentLikeParameterFormat IsNot Nothing Then
              value = String.Format(m_CurrentLikeParameterFormat, value)
              m_CurrentLikeParameterFormat = Nothing
            End If

            If value = "" Then
              ' safe to use constant string (nicer SQL for debug purposes)
              m_Sql.Append(m_Builder.DialectProvider.Formatter.GetConstantEmptyStringValue())
            Else
              ' too dangerous to use raw string
              AppendNewParameter(value)
            End If

          Case GetType(Boolean)
            m_Sql.Append(m_Builder.DialectProvider.Formatter.GetConstantValue(DirectCast(node.Value, Boolean)))

          Case GetType(Int16)
            m_Sql.Append(m_Builder.DialectProvider.Formatter.GetConstantValue(DirectCast(node.Value, Int16)))

          Case GetType(Int32)
            m_Sql.Append(m_Builder.DialectProvider.Formatter.GetConstantValue(DirectCast(node.Value, Int32)))

          Case GetType(Int64)
            m_Sql.Append(m_Builder.DialectProvider.Formatter.GetConstantValue(DirectCast(node.Value, Int64)))

          Case GetType(Decimal)
            m_Sql.Append(m_Builder.DialectProvider.Formatter.GetConstantValue(DirectCast(node.Value, Decimal)))

          Case GetType(Single)
            m_Sql.Append(m_Builder.DialectProvider.Formatter.GetConstantValue(DirectCast(node.Value, Single)))

          Case GetType(Double)
            m_Sql.Append(m_Builder.DialectProvider.Formatter.GetConstantValue(DirectCast(node.Value, Double)))

          Case Else
            AppendNewParameter(node.Value)
        End Select
      End If

      Return node
    End Function

    ''' <summary>
    ''' Visits parameter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Protected Overrides Function VisitParameter(node As ParameterExpression) As Expression
      AppendEntityMembersAccess(GetEntityIndex(node))
      Return node
    End Function

    ''' <summary>
    ''' Visits member.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Protected Overrides Function VisitMember(node As MemberExpression) As Expression
      ' special handling for:
      ' - entityParameter.Property
      ' - joinParameter.T1.Property
      ' - x.Value
      ' - x.HasValue
      ' - ...
      ' other expressions are evaluated

      Dim nodeIsEntityMemberAccess = False
      Dim entityIndex As Int32 = -1
      Dim memberName As String = Nothing

      Dim expression = node.Expression

      If expression IsNot Nothing Then
        memberName = node.Member.Name

        If Helpers.Types.IsNullable(expression.Type) Then
          If Helpers.Types.IsNullableValueAccess(node.Member) Then
            m_Stack.Peek().IsNullableValueAccess = True
            Visit(expression)
            Return node

          ElseIf Helpers.Types.IsNullableHasValueAccess(node.Member) Then
            m_Stack.Peek().IsNullableHasValueAccess = True
            Visit(expression)

            If ParentNodeIsIgnoredNegation() Then
              m_Sql.Append(" IS NULL")
            Else
              m_Sql.Append(" IS NOT NULL")
            End If

            Return node
          End If
        End If

        If IsEntity(expression) Then
          nodeIsEntityMemberAccess = True
          entityIndex = GetEntityIndex(DirectCast(expression, ParameterExpression))

        ElseIf IsJoinedEntity(expression) Then
          nodeIsEntityMemberAccess = True
          entityIndex = Helpers.Common.GetEntityIndexFromJoinMemberName(DirectCast(expression, MemberExpression).Member.Name)

        ElseIf IsJoin(expression) Then
          entityIndex = Helpers.Common.GetEntityIndexFromJoinMemberName(memberName)
          AppendEntityMembersAccess(entityIndex)
          Return node
        End If
      End If

      If nodeIsEntityMemberAccess Then
        AppendEntityMemberAccess(entityIndex, memberName)
      Else
        If IsInNullableValueAccess() Then
          AppendNewParameter(Evaluate(m_Stack(1).Node))
        Else
          AppendNewParameter(Evaluate(node))
        End If
      End If

      ExpandToBooleanComparisonIfNeeded(node)

      Return node
    End Function

    ''' <summary>
    ''' Visit member init.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Protected Overrides Function VisitMemberInit(node As MemberInitExpression) As Expression
      m_Stack.Peek().IsMemberInit = True

      Visit(node.NewExpression)

      If m_CustomSqlResult Is Nothing OrElse TypeOf m_CustomSqlResult IsNot AdHocTypeSqlResult Then
        Throw New Exception("Member init is only allowed in custom select or include mode for ad hoc types.")
      End If

      Dim adHocTypeSqlResult = DirectCast(m_CustomSqlResult, AdHocTypeSqlResult)

      Dim isInCustomSelectMode = m_Mode = ExpressionTranslateMode.CustomSelect

      Dim bindings = node.Bindings
      Dim count = bindings.Count

      Dim members = New MemberInfo(count - 1) {}
      Dim items = New Expression(count - 1) {}

      For i = 0 To count - 1
        Dim binding = bindings(i)

        If Not binding.BindingType = MemberBindingType.Assignment Then
          Throw New Exception($"Unsupported binding type {binding.BindingType}. Only {MemberBindingType.Assignment} is allowed.")
        End If

        members(i) = binding.Member
        items(i) = DirectCast(binding, MemberAssignment).Expression
      Next

      If Not adHocTypeSqlResult.CtorArguments.Length = 0 Then
        m_Sql.Append(", ")
      End If

      Dim resultItems = ProcessItemsInCustomSelectOrIncludeMode(items, isInCustomSelectMode, NewExpressionType.AdHoc, True, members)

      adHocTypeSqlResult.SetMembers(members, resultItems)

      Return node
    End Function

    ''' <summary>
    ''' Visits new.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Protected Overrides Function VisitNew(node As NewExpression) As Expression
      Dim type = node.Type

      If type Is GetType(RawSqlString) Then
        m_Sql.Append(Evaluate(node.Arguments(0)))
        Return node
      ElseIf Helpers.Types.IsValueTuple(type) Then
        Return VisitNewValueTupleOrAnonymousOrAdHocType(node, NewExpressionType.ValueTuple, True)
      ElseIf Helpers.Types.IsAnonymousType(type) Then
        Return VisitNewValueTupleOrAnonymousOrAdHocType(node, NewExpressionType.Anonymous, True)
      Else
        Return VisitNewValueTupleOrAnonymousOrAdHocType(node, NewExpressionType.AdHoc, False)
      End If
    End Function

    ''' <summary>
    ''' Visits new expression of ValueTuple or anonymous or ad hoc type.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <param name="newType"></param>
    ''' <param name="canUseMembersForNonModelEntity"></param>
    ''' <returns></returns>
    Private Function VisitNewValueTupleOrAnonymousOrAdHocType(node As NewExpression, newType As NewExpressionType, canUseMembersForNonModelEntity As Boolean) As Expression
      If Helpers.Types.IsNullable(node.Type) Then
        m_Stack.Peek().IsNullableConstructor = True
        Return Visit(node.Arguments(0))
      End If

      Dim args As IReadOnlyList(Of Expression)

      Dim isValueTuple = newType = NewExpressionType.ValueTuple
      Dim isAnonymous = newType = NewExpressionType.Anonymous

      If isValueTuple Then
        args = FlattenValueTupleArguments(node)
      Else
        args = node.Arguments
      End If

      Dim isInCustomSelectMode = m_Mode = ExpressionTranslateMode.CustomSelect
      Dim isInIncludeMode = m_Mode = ExpressionTranslateMode.Include

      If isInCustomSelectMode OrElse isInIncludeMode Then
        Dim items = ProcessItemsInCustomSelectOrIncludeMode(args, isInCustomSelectMode, newType, canUseMembersForNonModelEntity, node.Members)

        If isValueTuple Then
          Dim isNullable = IsNestedConstructorOfNullableValueTupleTypeSqlResult()
          Dim type = If(isNullable, GetType(Nullable(Of )).MakeGenericType(node.Type), node.Type)
          m_CustomSqlResult = New ValueTupleSqlResult(type, items)
        ElseIf isAnonymous Then
          m_CustomSqlResult = New AnonymousTypeSqlResult(node.Type, items)
        Else
          Dim isNullable = IsNestedConstructorOfNullableAdHocTypeSqlResult()
          Dim type = If(isNullable, GetType(Nullable(Of )).MakeGenericType(node.Type), node.Type)
          m_CustomSqlResult = New AdHocTypeSqlResult(type, node.Constructor, items)
        End If
      Else
        ProcessItems(args)
      End If

      Return node
    End Function

    ''' <summary>
    ''' Visits new array.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Protected Overrides Function VisitNewArray(node As NewArrayExpression) As Expression
      Return VisitAndEvaluate(node)
    End Function

    ''' <summary>
    ''' Visits and evaluate.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitAndEvaluate(node As Expression) As Expression
      AppendNewParameter(Evaluate(node))
      Return node
    End Function

    ''' <summary>
    ''' Process items in custom select or include mode.
    ''' </summary>
    ''' <param name="items"></param>
    ''' <param name="isInCustomSelectMode"></param>
    ''' <param name="newType"></param>
    ''' <param name="canUseMembersForNonModelEntity"></param>
    ''' <param name="members"></param>
    ''' <returns></returns>
    Private Function ProcessItemsInCustomSelectOrIncludeMode(items As IReadOnlyList(Of Expression), isInCustomSelectMode As Boolean, newType As NewExpressionType, canUseMembersForNonModelEntity As Boolean, members As IReadOnlyList(Of MemberInfo)) As SqlResultBase()
      Dim count = items.Count

      Dim resultItems = New SqlResultBase(count - 1) {}

      Dim entities = m_Model.GetEntities()

      For i = 0 To count - 1
        Dim arg = items(i)
        Dim type = arg.Type
        Dim entity = entities.OfType(Of EntityBasedSqlEntity).FirstOrDefault(Function(x) x.EntityType = type)
        Dim isEntity = entity IsNot Nothing
        Dim columnIndex = m_CustomSqlResultItemIndex

        m_CustomSqlResultItemIndex += 1

        Visit(arg)

        If isEntity Then
          resultItems(i) = New EntitySqlResult(entity)
        Else
          Dim columnAlias = If(isInCustomSelectMode, CreateColumnAlias(columnIndex), CreateIncludeColumnAlias(columnIndex))

          If m_NonModelEntity Is Nothing OrElse Not canUseMembersForNonModelEntity Then
            AppendColumnAliasWithSpace(columnAlias)
          Else
            AppendColumnAliasWithSpace(columnAlias, newType, members, i)
          End If

          resultItems(i) = New ScalarValueSqlResult(type)
        End If

        If Not i = count - 1 Then
          m_Sql.Append(", ")
        End If
      Next

      Return resultItems
    End Function

    ''' <summary>
    ''' Process items.
    ''' </summary>
    ''' <param name="items"></param>
    Private Sub ProcessItems(items As IReadOnlyList(Of Expression))
      Dim count = items.Count

      For i = 0 To count - 1
        Dim arg = items(i)

        Visit(arg)

        If Not i = count - 1 Then
          m_Sql.Append(", ")
        End If
      Next
    End Sub

    ''' <summary>
    ''' Gets flattened ValueTuple constructor arguments.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function FlattenValueTupleArguments(node As NewExpression) As List(Of Expression)
      Dim args = New List(Of Expression)
      AddValueTupleArguments(node, args)
      Return args
    End Function

    ''' <summary>
    ''' Recursively adds ValueTuple arguments to the list.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <param name="allArgs"></param>
    Private Sub AddValueTupleArguments(node As NewExpression, allArgs As List(Of Expression))
      Dim args = node.Arguments
      'Dim args = node.Type.GetGenericArguments()
      Dim count = args.Count

      If 0 < count Then
        For i = 0 To count - 2
          allArgs.Add(args(i))
        Next

        Dim lastArg = args(count - 1)

        If lastArg.NodeType = ExpressionType.New AndAlso Helpers.Types.IsValueTuple(lastArg.Type) Then
          AddValueTupleArguments(DirectCast(lastArg, NewExpression), allArgs)
        Else
          allArgs.Add(lastArg)
        End If
      End If
    End Sub

    ''' <summary>
    ''' Visits in custom select or include mode.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitInCustomSelectOrIncludeMode(node As Expression) As Expression
      ' NOTE: the handling of various special cases below (and in other methods) is quite ugly.
      ' It also offers limited capabilities of nesting. This should be refactored in the future.
      ' Also, if we support ValueTuples, we should probably support ValueTuple.Create() factory
      ' methods as well.

      If node.NodeType = ExpressionType.New OrElse node.NodeType = ExpressionType.MemberInit Then
        ' anonymous type, value tuple, ad hoc type
        Return Visit(node)
      ElseIf node.NodeType = ExpressionType.Convert OrElse node.NodeType = ExpressionType.ConvertChecked Then
        ' ignore (probably implicit) cast of anonymous type, value tuple, ad hoc type
        Dim operand = DirectCast(node, UnaryExpression).Operand

        If operand.NodeType = ExpressionType.New OrElse operand.NodeType = ExpressionType.MemberInit Then
          Return Visit(node)
        ElseIf node.NodeType = ExpressionType.Convert OrElse node.NodeType = ExpressionType.ConvertChecked Then
          ' check for special case - look at point 3.) mentioned in IsNestedConstructorOfNullableValueTupleTypeSqlResult method
          If Helpers.Types.IsValueTuple(operand.Type) Then
            Return Visit(node)
          End If
        End If
      End If

      Visit(node)

      Dim type = node.Type
      Dim entity = m_Model.GetEntities().FirstOrDefault(Function(x) x.EntityType = type)

      Dim isInCustomSelectMode = m_Mode = ExpressionTranslateMode.CustomSelect

      If entity Is Nothing Then
        ' simple scalar value
        Dim columnAlias = If(isInCustomSelectMode, CreateColumnAlias(0), CreateIncludeColumnAlias(0))
        AppendColumnAliasWithSpace(columnAlias)
        m_CustomSqlResult = New ScalarValueSqlResult(type)
      Else
        ' whole entity
        If TypeOf entity IsNot EntityBasedSqlEntity Then
          Throw New NotSupportedException($"Unsupported type '{type}' in custom select.")
        End If

        m_CustomSqlResult = New EntitySqlResult(DirectCast(entity, EntityBasedSqlEntity))
      End If

      Return node
    End Function

    ''' <summary>
    ''' Evaluates an expression.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function Evaluate(node As Expression) As Object
      ' We try to use optimized evaluation for these expressions:
      ' - field access (captured closure variables are fields too)
      ' - static field access
      ' - property access
      ' - static property access
      ' - parameterless method call
      ' - parameterless static method call
      ' For mentioned scenarios, we dynamically compile caller function in first use (slow). All repeated calls should be fast then.
      ' Other option would be to use reflection and call FieldInfo.GetValue, PropertyInfo.GetValue and MethodInfo.Invoke methods.
      ' Note that this is still faster than lambda compilation (of whole expression)!
      '
      ' Following expressions are currently not optimized:
      ' - method call with parameters
      ' - static method call with parameters
      ' - indexers
      ' For these scenarios, expression is wrapped in lambda and compiled, which is slow.
      ' We can apply same technique as mentioned above to optimize also these cases. We just need to evaluate parameters in the same
      ' way and be able to compile caller functions that accept various parameters. But it would complicate the solution and the effect
      ' is currently questionable (is there "enough" ConstantExpression subexpressions?; how many parameters is enough to hardcode; ...).
      ' Maybe for hot paths it still makes sense to do it (performance tests needed).
      '
      ' If the expression represents "chain" like "value.Property1.Property2.Method()", we recursively evaluate subexpressions.
      ' As with parameters and functions, also here we might end up with slower execution than with lambda compilation (multiple
      ' compilations vs one). But expressions like "localCapturedValue.PropertyValue" will be quite often, so it makes sense imho.
      ' And unless most subexpression are not unoptimized evaluations, it'll help on hot paths.

      ' Links:
      ' https://stackoverflow.com/questions/2616638/access-the-value-of-a-member-expression
      ' https://stackoverflow.com/questions/1613239/getting-the-object-out-of-a-memberexpression
      ' https://blogs.msdn.microsoft.com/csharpfaq/2010/03/11/how-can-i-get-objects-and-property-values-from-expression-trees/
      ' https://github.com/Miaplaza/expression-utils
      ' https://stackoverflow.com/questions/38345606/how-can-i-access-the-value-of-a-local-variable-from-within-an-expression-tree

      Dim value As Object = Nothing
      Dim valueSet = False

      If node.NodeType = ExpressionType.MemberAccess Then
        ' field/property
        Dim memberExpression = DirectCast(node, MemberExpression)
        Dim memberInfo = memberExpression.Member
        Dim exp = memberExpression.Expression

        If exp Is Nothing Then
          ' static field/property
          If memberInfo.MemberType = MemberTypes.Field Then
            value = MemberCallerCache.GetStaticFieldCaller(memberInfo.ReflectedType, DirectCast(memberInfo, FieldInfo))()
            valueSet = True
          ElseIf memberInfo.MemberType = MemberTypes.Property Then
            value = MemberCallerCache.GetStaticPropertyCaller(memberInfo.ReflectedType, DirectCast(memberInfo, PropertyInfo))()
            valueSet = True
          End If
        Else
          ' instance field/property
          Dim obj = Nothing

          If exp.NodeType = ExpressionType.Constant Then
            obj = DirectCast(exp, ConstantExpression).Value
          Else
            obj = Evaluate(exp) ' if the chain is too long, isn't it faster just to compile whole expression?
          End If

          If memberInfo.MemberType = MemberTypes.Field Then
            value = MemberCallerCache.GetFieldCaller(memberInfo.ReflectedType, DirectCast(memberInfo, FieldInfo))(obj)
            valueSet = True
          ElseIf memberInfo.MemberType = MemberTypes.Property Then
            value = MemberCallerCache.GetPropertyCaller(memberInfo.ReflectedType, DirectCast(memberInfo, PropertyInfo))(obj)
            valueSet = True
          End If
        End If

      ElseIf node.NodeType = ExpressionType.Call Then
        ' method/indexer
        Dim methodCallExpression = DirectCast(node, MethodCallExpression)

        ' for simplicity, we optimize only calls to parameterless methods
        If methodCallExpression.Arguments.Count = 0 Then
          Dim methodInfo = methodCallExpression.Method
          Dim exp = methodCallExpression.Object

          If exp Is Nothing Then
            ' static method
            value = MemberCallerCache.GetStaticMethodCaller(methodInfo.ReflectedType, methodInfo)()
            valueSet = True
          Else
            ' instance method
            Dim obj = Nothing

            If exp.NodeType = ExpressionType.Constant Then
              obj = DirectCast(exp, ConstantExpression).Value
            Else
              obj = Evaluate(exp) ' if the chain is too long, isn't it faster just to compile whole expression?
            End If

            value = MemberCallerCache.GetMethodCaller(methodInfo.ReflectedType, methodInfo)(obj)
            valueSet = True
          End If
        End If
      End If

      If Not valueSet Then
        Dim expr = Expression.Convert(node, GetType(Object))
        Dim lambda = Expression.Lambda(Of Func(Of Object))(expr)
        Dim getter = lambda.Compile()
        value = getter.Invoke()
      End If

      If node.Type Is GetType(String) Then
        If m_CurrentLikeParameterFormat IsNot Nothing Then
          value = String.Format(m_CurrentLikeParameterFormat, value)
          m_CurrentLikeParameterFormat = Nothing
        End If
      End If

      Return value
    End Function

    ''' <summary>
    ''' Creates column alias.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Private Function CreateColumnAlias(index As Int32) As String
      Return "C" & index.ToString(Globalization.CultureInfo.InvariantCulture)
    End Function

    ''' <summary>
    ''' Creates column alias for included column.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Private Function CreateIncludeColumnAlias(index As Int32) As String
      Return "CI_" & m_IncludeIndex.ToString(Globalization.CultureInfo.InvariantCulture) & "_" & index.ToString(Globalization.CultureInfo.InvariantCulture)
    End Function

    ''' <summary>
    ''' Creates column alias.
    ''' </summary>
    ''' <param name="index1"></param>
    ''' <param name="index2"></param>
    ''' <returns></returns>
    Private Function CreateColumnAlias(index1 As Int32, index2 As Int32) As String
      Return "C" & index1.ToString(Globalization.CultureInfo.InvariantCulture) & "_" & index2.ToString(Globalization.CultureInfo.InvariantCulture)
    End Function

    ''' <summary>
    ''' Creates column alias for included column.
    ''' </summary>
    ''' <param name="index1"></param>
    ''' <param name="index2"></param>
    ''' <returns></returns>
    Private Function CreateIncludeColumnAlias(index1 As Int32, index2 As Int32) As String
      Return "CI_" & m_IncludeIndex.ToString(Globalization.CultureInfo.InvariantCulture) & "_" & index1.ToString(Globalization.CultureInfo.InvariantCulture) & "_" & index2.ToString(Globalization.CultureInfo.InvariantCulture)
    End Function

    ''' <summary>
    ''' Appends new parameter.
    ''' </summary>
    ''' <param name="value"></param>
    Private Sub AppendNewParameter(value As Object)
      Dim parameterName = m_Builder.CreateParameter(m_ParameterIndex + m_Parameters.Count)

      m_Sql.Append(parameterName)
      m_Parameters.Add(New SqlParameter(parameterName, value))
    End Sub

    ''' <summary>
    ''' Appends column alias.
    ''' </summary>
    ''' <param name="columnAlias"></param>
    Private Sub AppendColumnAliasWithSpace(columnAlias As String)
      m_Sql.Append(" ")
      m_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, columnAlias)

      If m_NonModelEntity IsNot Nothing Then
        m_NonModelEntity.AddColumn(columnAlias)
      End If
    End Sub

    ''' <summary>
    ''' Appends column alias.
    ''' </summary>
    ''' <param name="columnAlias"></param>
    ''' <param name="newType"></param>
    ''' <param name="members"></param>
    ''' <param name="memberIndex"></param>
    Private Sub AppendColumnAliasWithSpace(columnAlias As String, newType As NewExpressionType, members As IReadOnlyList(Of MemberInfo), memberIndex As Int32)
      m_Sql.Append(" ")
      m_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, columnAlias)

      If m_NonModelEntity IsNot Nothing Then
        Dim propertyName As String

        If newType = NewExpressionType.ValueTuple Then
          ' NOTE: for large value tuples, this won't work properly, because of nesting.
          ' We could throw an exception here. But that would be limiting. Returning such large value tuple
          ' will work and so will accessing Item1 - Item7 members later in the query. Problem only occurs
          ' when nested member (wrongly translated to Item8+) will be accessed in the expression later.
          ' Then, there will be an exception.
          propertyName = "Item" & (memberIndex + 1).ToString(Globalization.CultureInfo.InvariantCulture)
        Else
          propertyName = members(memberIndex).Name
        End If

        m_NonModelEntity.AddColumn(propertyName, columnAlias)
      End If
    End Sub

    ''' <summary>
    ''' Appends entity member access.
    ''' </summary>
    ''' <param name="entityIndex"></param>
    ''' <param name="propertyName"></param>
    Private Sub AppendEntityMemberAccess(entityIndex As Int32, propertyName As String)
      Dim entity = m_Model.GetEntity(entityIndex)

      If entity.IsIgnored Then
        ' NOTE: currently, we append NULL if table is ignored. This could be solved better for SELECT clauses.
        ' We could omit column in SQL and instead of using reader, set value directly to Nothing (default).
        m_Sql.Append("NULL")
      ElseIf Not m_UseTableNamesOrAliases Then
        m_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, entity.GetColumnName(propertyName))
      ElseIf m_UseAliases Then
        m_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, entity.TableAlias)
        m_Sql.Append(".")
        m_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, entity.GetColumnName(propertyName))
      Else
        ' TODO: SIP - implement subquery
        Throw New NotSupportedException()
        ' NOTE: this is not used right now
        'm_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, entity.Entity.TableName, entity.Entity.Schema)
        'm_Sql.Append(".")
        'm_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, entity.GetColumnName(propertyName))
      End If
    End Sub

    ''' <summary>
    ''' Appends entity member access.
    ''' </summary>
    ''' <param name="entityIndex"></param>
    Private Sub AppendEntityMembersAccess(entityIndex As Int32)
      ' this might be called from group by or custom select

      Dim entity = m_Model.GetEntity(entityIndex)

      ' NOTE: excluding columns is not (yet) supported in this scenario, but column enumeration belows already supports it.
      ' In case exclusion is added, test this! Also, if whole table is excluded, entity.GetColumnCount() returns 0 (and we'll
      ' most likely get exception later). In this case we propably shouldn't support excluding whole table (it doesn't make sense anyway)!
      ' Also, think about supporting included results here.

      Dim isInCustomSelectMode = m_Mode = ExpressionTranslateMode.CustomSelect
      Dim isInIncludeMode = m_Mode = ExpressionTranslateMode.Include

      Dim isIgnored = entity.IsIgnored
      Dim columnNames = entity.GetColumnNames()
      Dim columnCount = entity.GetColumnCount(isInIncludeMode)
      Dim columnIndex = 0

      For i = 0 To columnNames.Count - 1
        If entity.IncludedColumns(i) Then
          If isIgnored Then
            ' NOTE: currently, we append NULL if table is ignored. This could be solved better for SELECT clauses.
            ' We could omit columns in SQL and instead of using entity reader, set value directly to Nothing (default).
            m_Sql.Append("NULL")
          Else
            m_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, entity.TableAlias)
            m_Sql.Append(".")
            m_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, columnNames(i))
          End If

          If isInCustomSelectMode Then
            Dim columnAlias = CreateColumnAlias(m_CustomSqlResultItemIndex, columnIndex)
            AppendColumnAliasWithSpace(columnAlias)
          ElseIf isInIncludeMode Then
            Dim columnAlias = CreateIncludeColumnAlias(m_CustomSqlResultItemIndex, columnIndex)
            AppendColumnAliasWithSpace(columnAlias)
          End If

          columnIndex += 1
        End If

        If columnIndex = columnCount Then
          Exit For
        Else
          m_Sql.Append(", ")
        End If
      Next
    End Sub

    ''' <summary>
    ''' Gets entity index.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function GetEntityIndex(node As ParameterExpression) As Int32
      Dim index = m_ExpressionParameters.IndexOf(node)

      If m_EntityIndexHints Is Nothing Then
        ' this should not happen, because IJoin should be used when index hints are not available
        Throw New Exception("Unable to match expression parameter with entity.")
      End If

      If index < 0 OrElse m_EntityIndexHints.Length <= index Then
        Throw New Exception($"None or ambiguous match of entity of type '{node.Type}'. Use IJoin instead?")
      End If

      Return m_EntityIndexHints(index)
    End Function

    ''' <summary>
    ''' Checks whether negation should be ignored in its current location and processed differently (elsewhere).
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function ShouldIgnoreNegation(node As UnaryExpression) As Boolean
      ' Look ahead optimization for negations of HasValue calls and certain boolean expressions (direct entity member access).
      ' Examples:
      ' - Function(x) Not x.Foo.HasValue => [T0].[Foo] IS NULL
      ' - Function(x) Not foo.Boo().HasValue => @p0 IS NULL
      ' - Function(x) Not x.Foo => [T0].[Foo] = 0
      ' - Function(x) Not x.Foo.Value => [T0].[Foo] = 0
      ' - Function(j) Not j.T1.Foo => [T0].[Foo] = 0
      ' - Function(j) Not j.T1.Foo.Value => [T0].[Foo] = 0
      ' Following is not optimized:
      ' - Function(x) Not foo.Boo => NOT @p0 = 1
      ' - Function(x) Not foo.Boo() => NOT @p0 = 1
      ' - ...
      ' It's like this for simplicity. We could "optimize" (when BinaryExpression is not Operand) everything, maybe except SQL helpers.
      ' But the additional code complexity is probably not worth it.

      If node.Operand.NodeType = ExpressionType.MemberAccess Then
        Dim memberExpression = DirectCast(node.Operand, MemberExpression)
        Dim expression = memberExpression.Expression

        If expression IsNot Nothing Then
          Dim isNullable = Helpers.Types.IsNullable(expression.Type)

          If isNullable AndAlso Helpers.Types.IsNullableHasValueAccess(memberExpression.Member) Then
            ' Nullable.HasValue access
            Return True
          End If

          If isNullable AndAlso Helpers.Types.IsNullableValueAccess(memberExpression.Member) Then
            If expression.NodeType = ExpressionType.MemberAccess Then
              memberExpression = DirectCast(expression, MemberExpression)
              expression = memberExpression.Expression

              If expression IsNot Nothing Then
                Return IsEntityOrJoinedEntityMemberAccess(memberExpression)
              End If
            End If
          Else
            Return IsEntityOrJoinedEntityMemberAccess(memberExpression)
          End If
        End If
      End If

      Return False
    End Function

    ''' <summary>
    ''' Expands "simple" boolean node to boolean comparison if needed.
    ''' </summary>
    ''' <param name="node"></param>
    Private Sub ExpandToBooleanComparisonIfNeeded(node As Expression)
      If Not m_Mode = ExpressionTranslateMode.Condition Then
        Exit Sub
      End If

      ' checking Boolean? is probably faster that calling IsInNullableValueAccess()
      If Not (node.Type Is GetType(Boolean) OrElse node.Type Is GetType(Boolean?)) Then
        Exit Sub
      End If

      Dim depth = m_Stack.Count
      Dim value = True
      Dim parentIndex = 1

      If parentIndex < depth AndAlso m_Stack(parentIndex).IsNullableValueAccess Then
        parentIndex += 1
      End If

      If parentIndex < depth AndAlso m_Stack(parentIndex).IsNegation Then
        value = Not m_Stack(parentIndex).IsIgnoredNegation
        parentIndex += 1
      End If

      If depth <= parentIndex OrElse (Not m_Stack(parentIndex).IsCompare AndAlso Not m_Stack(parentIndex).IsNullableHasValueAccess) Then
        m_Sql.Append(" = ")
        m_Sql.Append(m_Builder.DialectProvider.Formatter.GetConstantValue(value))
      End If
    End Sub

    ''' <summary>
    ''' Checks whether node represents a null constant.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function IsNullConstant(node As Expression) As Boolean
      Return node.NodeType = ExpressionType.Constant AndAlso DirectCast(node, ConstantExpression).Value Is Nothing
    End Function

    ''' <summary>
    ''' Checks whether node represents <see cref="IJoin"/>.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function IsJoin(node As Expression) As Boolean
      If m_ExpressionParametersType = ExpressionParametersType.IJoin Then
        Return node.NodeType = ExpressionType.Parameter
      End If

      Return False
    End Function

    ''' <summary>
    ''' Checks whether node represents an entity.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function IsEntity(node As Expression) As Boolean
      If m_ExpressionParametersType = ExpressionParametersType.Entities Then
        Return node.NodeType = ExpressionType.Parameter
      End If

      Return False
    End Function

    ''' <summary>
    ''' Checks whether node represents an entity from <see cref="IJoin"/>.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function IsJoinedEntity(node As Expression) As Boolean
      If m_ExpressionParametersType = ExpressionParametersType.IJoin Then
        If node.NodeType = ExpressionType.MemberAccess Then
          Dim memberExpression = DirectCast(node, MemberExpression).Expression

          If memberExpression IsNot Nothing Then
            Return memberExpression.NodeType = ExpressionType.Parameter
          End If
        End If
      End If

      Return False
    End Function

    ''' <summary>
    ''' Checks whether node represents a member access of an entity or of an entity from <see cref="IJoin"/>.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function IsEntityOrJoinedEntityMemberAccess(node As MemberExpression) As Boolean
      Dim expression = node.Expression

      If expression IsNot Nothing Then
        If m_ExpressionParametersType = ExpressionParametersType.IJoin Then
          If expression.NodeType = ExpressionType.MemberAccess Then
            expression = DirectCast(expression, MemberExpression).Expression

            If expression IsNot Nothing Then
              Return expression.NodeType = ExpressionType.Parameter
            End If
          End If
        Else
          Return expression.NodeType = ExpressionType.Parameter
        End If
      End If

      Return False
    End Function

    ''' <summary>
    ''' Checks whether parent node is ignored negation.
    ''' </summary>
    ''' <returns></returns>
    Private Function ParentNodeIsIgnoredNegation() As Boolean
      Return 2 <= m_Stack.Count AndAlso m_Stack(1).IsIgnoredNegation
    End Function

    ''' <summary>
    ''' Checks whether parent node is <see cref="Nullable(Of T).Value"/> access.
    ''' </summary>
    ''' <returns></returns>
    Private Function IsInNullableValueAccess() As Boolean
      Return 2 <= m_Stack.Count AndAlso m_Stack(1).IsNullableValueAccess
    End Function

    ''' <summary>
    ''' Checks whether node represents nested constructor of nullable ValueTuple SQL result.
    ''' </summary>
    ''' <returns></returns>
    Private Function IsNestedConstructorOfNullableValueTupleTypeSqlResult() As Boolean
      ' NOTE: there could be following situations:
      '
      ' 1.) nullable constructor -> constructor
      '     e.g.: Select(Function(x) New(String, String)?((x.Foo, x.Bar))).
      '
      ' 2.) convert to nullable -> constructor
      '     This actually doesn't seems to happen. Instead, it's always 3.) with unnecessary cast. Not sure why.
      '
      ' 3.) convert to nullable -> convert to the same value tuple type -> constructor
      '     e.g.: Select(Function(x) CType((x.Foo, x.Bar), (String, String)?)).
      '           Select(Of (String, String)?)(Function(x) (x.Foo, x.Bar)).

      Dim count = m_Stack.Count

      If 2 <= count Then
        Dim currentNode = m_Stack(0).Node
        Dim parent = m_Stack(1)

        If parent.IsNullableConstructor Then
          Return True
        End If

        If parent.IsConvert AndAlso Nullable.GetUnderlyingType(parent.Node.Type) Is currentNode.Type Then
          Return True
        End If

        If parent.IsConvert AndAlso parent.Node.Type Is currentNode.Type AndAlso 3 <= count Then
          Dim parent2 = m_Stack(2)

          If parent2.IsNullableConstructor Then
            Return True
          End If

          If parent2.IsConvert AndAlso Nullable.GetUnderlyingType(parent2.Node.Type) Is currentNode.Type Then
            Return True
          End If
        End If
      End If

      Return False
    End Function

    ''' <summary>
    ''' Checks whether node represents nested constructor of nullable ad hoc SQL result.
    ''' </summary>
    ''' <returns></returns>
    Private Function IsNestedConstructorOfNullableAdHocTypeSqlResult() As Boolean
      ' NOTE: there could be following situations:
      '
      ' 1.) nullable constructor -> constructor
      '     e.g.: Select(Function(x) New FooStruct?(New FooStruct(...))).
      '
      ' 2.) convert to nullable -> constructor
      '     e.g.: Select(Function(x) CType(New FooStruct(...), FooStruct?)).
      '           Select(Of FooStruct?)(Function(x) New FooStruct(...)).
      '
      ' 3.) nullable constructor -> member init -> constructor
      '      e.g.: Select(Function(x) New FooStruct?(New FooStruct With {...}))
      '
      ' 4.) convert to nullable -> member init -> constructor
      '      e.g.: Select(Function(x) CType(New FooStruct With {...}, FooStruct?)).
      '            Select(Of FooStruct?)(Function(x) New FooStruct With {...}).

      Dim count = m_Stack.Count

      If 2 <= count Then
        Dim currentNode = m_Stack(0).Node
        Dim parent = m_Stack(1)

        If parent.IsNullableConstructor Then
          Return True
        End If

        If parent.IsConvert AndAlso Nullable.GetUnderlyingType(parent.Node.Type) Is currentNode.Type Then
          Return True
        End If

        If parent.IsMemberInit AndAlso 3 <= count Then
          Dim parent2 = m_Stack(2)

          If parent2.IsNullableConstructor Then
            Return True
          End If

          If parent2.IsConvert AndAlso Nullable.GetUnderlyingType(parent2.Node.Type) Is currentNode.Type Then
            Return True
          End If
        End If
      End If

      Return False
    End Function

  End Class
End Namespace