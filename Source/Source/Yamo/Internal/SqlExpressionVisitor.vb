Imports System.Linq.Expressions
Imports System.Text
Imports System.Collections.ObjectModel
Imports System.Runtime.CompilerServices
Imports System.Reflection
Imports Yamo.Internal.Query
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Sql

Namespace Internal

  ''' <summary>
  ''' SQL expression visitor.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SqlExpressionVisitor
    Inherits ExpressionVisitor

    ''' <summary>
    ''' Stores SQL expression builder.
    ''' </summary>
    Private m_Builder As SqlExpressionBuilderBase

    ''' <summary>
    ''' Stores SQL model.
    ''' </summary>
    Private m_Model As SqlModel

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
    ''' Stores wheter aliases should be used.
    ''' </summary>
    Private m_UseAliases As Boolean

    ''' <summary>
    ''' Shores whether table names or aliases should be used.
    ''' </summary>
    Private m_UseTableNamesOrAliases As Boolean

    ''' <summary>
    ''' Stores whether compensation for ignored negation should be performed.
    ''' </summary>
    Private m_CompensateForIgnoredNegation As Boolean

    ''' <summary>
    ''' Stores current like parameter format.
    ''' </summary>
    Private m_CurrentLikeParameterFormat As String

    ''' <summary>
    ''' Stores whether visitor is in custom select mode.
    ''' </summary>
    Private m_InCustomSelectMode As Boolean

    ''' <summary>
    ''' Stores custom entities.
    ''' </summary>
    Private m_CustomEntities As CustomSqlEntity()

    ''' <summary>
    ''' Store custom entity index.
    ''' </summary>
    Private m_CustomEntityIndex As Int32

    ''' <summary>
    ''' Creates new instance of <see cref="SqlExpressionVisitor"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="model"></param>
    Public Sub New(builder As SqlExpressionBuilderBase, model As SqlModel)
      m_Builder = builder
      m_Model = model
    End Sub

    ''' <summary>
    ''' Translates expression to SQL string.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="expression"></param>
    ''' <param name="expressionParametersType"></param>
    ''' <param name="entityIndexHints">entityIndexHints is null when expressionParametersType is not ExpressionParametersType.Entities</param>
    ''' <param name="parameterIndex"></param>
    ''' <param name="useAliases"></param>
    ''' <param name="useTableNamesOrAliases"></param>
    ''' <returns></returns>
    Public Function Translate(expression As Expression, expressionParametersType As ExpressionParametersType, entityIndexHints As Int32(), parameterIndex As Int32, useAliases As Boolean, useTableNamesOrAliases As Boolean) As SqlString
      If TypeOf expression IsNot LambdaExpression Then
        Throw New ArgumentException("Expression must be of type LambdaExpression.")
      End If

      Dim lambda = DirectCast(expression, LambdaExpression)

      m_ExpressionParameters = lambda.Parameters
      m_ExpressionParametersType = expressionParametersType
      m_EntityIndexHints = entityIndexHints
      m_Sql = New StringBuilder()
      m_Parameters = New List(Of SqlParameter)
      m_ParameterIndex = parameterIndex
      m_UseAliases = useAliases
      m_UseTableNamesOrAliases = useTableNamesOrAliases
      m_CompensateForIgnoredNegation = False
      m_CurrentLikeParameterFormat = Nothing
      m_InCustomSelectMode = False
      m_CustomEntities = Nothing
      m_CustomEntityIndex = 0

      Visit(lambda.Body)

      m_ExpressionParameters = Nothing

      Return New SqlString(m_Sql.ToString(), m_Parameters)
    End Function

    ''' <summary>
    ''' Translates custom select.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="expression"></param>
    ''' <param name="expressionParametersType"></param>
    ''' <param name="entityIndexHints">entityIndexHints is null when expressionParametersType is not ExpressionParametersType.Entities</param>
    ''' <param name="parameterIndex"></param>
    ''' <returns></returns>
    Public Function TranslateCustomSelect(expression As Expression, expressionParametersType As ExpressionParametersType, entityIndexHints As Int32(), parameterIndex As Int32) As (SqlString As SqlString, CustomEntities As CustomSqlEntity())
      If TypeOf expression IsNot LambdaExpression Then
        Throw New ArgumentException("Expression must be of type LambdaExpression.")
      End If

      Dim lambda = DirectCast(expression, LambdaExpression)

      m_ExpressionParameters = lambda.Parameters
      m_ExpressionParametersType = expressionParametersType
      m_EntityIndexHints = entityIndexHints
      m_Sql = New StringBuilder()
      m_Parameters = New List(Of SqlParameter)
      m_ParameterIndex = parameterIndex
      m_UseAliases = True
      m_UseTableNamesOrAliases = True
      m_CompensateForIgnoredNegation = False
      m_CurrentLikeParameterFormat = Nothing
      m_InCustomSelectMode = True
      m_CustomEntities = Nothing
      m_CustomEntityIndex = 0

      VisitInCustomSelectMode(lambda.Body)

      m_ExpressionParameters = Nothing

      CustomResultReaderCache.CreateResultFactoryIfNotExists(m_Model.Model, lambda.Body, m_CustomEntities)

      Return (New SqlString(m_Sql.ToString(), m_Parameters), m_CustomEntities)
    End Function

    ''' <summary>
    ''' Visits expression.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Public Overrides Function Visit(node As Expression) As Expression
      Return MyBase.Visit(node)
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
            Return VisitStartsWithMethodCall(node)
          Case NameOf(String.EndsWith)
            Return VisitEndsWithMethodCall(node)
          Case NameOf(String.Contains)
            Return VisitContainsMethodCall(node)
          Case NameOf(String.Concat)
            Return VisitConcatMethodCall(node)
        End Select
      End If

      If node.Method.DeclaringType Is GetType(FormattableStringFactory) Then
        If node.Method.Name = NameOf(FormattableStringFactory.Create) Then
          Return VisitCreateFormattableStringMethodCall(node)
        End If
      End If

      If node.Method.IsStatic Then
        If GetType(SqlHelper).IsAssignableFrom(node.Method.DeclaringType) Then
          Return VisitSqlHelperMethodCall(node)
        End If
      End If

      If node.Method.DeclaringType Is GetType(Enumerable) Then
        ' arrays
        If node.Method.Name = NameOf(Enumerable.Contains) Then
          Return VisitArrayEnumerableContainsMethodCall(node)
        End If
      End If

      If GetType(IEnumerable).IsAssignableFrom(node.Method.DeclaringType) Then
        ' lists, etc. (IEnumerable without Contains are filtered out)
        If node.Method.Name = NameOf(Enumerable.Contains) Then
          Return VisitEnumerableContainsMethodCall(node)
        End If
      End If

      ' node.Object is null when static indexer is called
      If node.Method.IsSpecialName AndAlso node.Object IsNot Nothing AndAlso node.Object.NodeType = ExpressionType.Parameter AndAlso node.Method.Name.StartsWith("set_") Then
        ' this is needed when we pass Action (in UPDATE SET... expressions)
        ' TODO: SIP - investigate how indexers behave and whether they can cause a problem here
        Return VisitEntityPropertySetter(node)
      End If

      Return VisitAndEvaluate(node)
    End Function

    ''' <summary>
    ''' Visits starts with method call.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitStartsWithMethodCall(node As MethodCallExpression) As Expression
      Me.Visit(node.Object)

      m_Sql.Append(" LIKE ")

      If m_Builder.DialectProvider.Formatter.LikeWildcardsInParameter Then
        m_CurrentLikeParameterFormat = "{0}%"
        Me.Visit(node.Arguments(0))

        If m_CurrentLikeParameterFormat IsNot Nothing Then
          Throw New Exception("Only constant string or evaluable expression is allowed as StartsWith parameter.")
        End If
      Else
        Me.Visit(node.Arguments(0))
        m_Sql.Append(" + '%'")
      End If

      Return node
    End Function

    ''' <summary>
    ''' Visits end with method call.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitEndsWithMethodCall(node As MethodCallExpression) As Expression
      Me.Visit(node.Object)

      If m_Builder.DialectProvider.Formatter.LikeWildcardsInParameter Then
        m_Sql.Append(" LIKE ")

        m_CurrentLikeParameterFormat = "%{0}"
        Me.Visit(node.Arguments(0))

        If m_CurrentLikeParameterFormat IsNot Nothing Then
          Throw New Exception("Only constant string or evaluable expression is allowed as EndsWith parameter.")
        End If
      Else
        m_Sql.Append(" LIKE '%' + ")
        Me.Visit(node.Arguments(0))
      End If

      Return node
    End Function

    ''' <summary>
    ''' Visits contains method call.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitContainsMethodCall(node As MethodCallExpression) As Expression
      Me.Visit(node.Object)

      If m_Builder.DialectProvider.Formatter.LikeWildcardsInParameter Then
        m_Sql.Append(" LIKE ")

        m_CurrentLikeParameterFormat = "%{0}%"
        Me.Visit(node.Arguments(0))

        If m_CurrentLikeParameterFormat IsNot Nothing Then
          Throw New Exception("Only constant string or evaluable expression is allowed as Contains parameter.")
        End If
      Else
        m_Sql.Append(" LIKE '%' + ")
        Me.Visit(node.Arguments(0))
        m_Sql.Append(" + '%'")
      End If

      Return node
    End Function

    ''' <summary>
    ''' Visits concat method call.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitConcatMethodCall(node As MethodCallExpression) As Expression
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
        Me.Visit(expressions(i))

        If Not i = count - 1 Then
          m_Sql.Append(" ")
          m_Sql.Append(m_Builder.DialectProvider.Formatter.StringConcatenationOperator)
          m_Sql.Append(" ")
        End If
      Next

      Return node
    End Function

    ''' <summary>
    ''' Visits create <see cref="FormattableString"/> method call.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitCreateFormattableStringMethodCall(node As MethodCallExpression) As Expression
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
      Dim format = DirectCast(getSqlFormatMethod.Invoke(Nothing, {node.Method, dialectProvider}), String)

      Dim args = New StringBuilder(node.Arguments.Count - 1) {}
      Dim sqlBuilder = m_Sql

      For i = 0 To node.Arguments.Count - 1
        m_Sql = New StringBuilder()
        Visit(node.Arguments(i))
        args(i) = m_Sql
      Next

      m_Sql = sqlBuilder

      m_Sql.AppendFormat(format, args)

      Return node
    End Function

    ''' <summary>
    ''' Visits array enumerable contains method call.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitArrayEnumerableContainsMethodCall(node As MethodCallExpression) As Expression
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
    ''' Visits enumerable contains method call.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitEnumerableContainsMethodCall(node As MethodCallExpression) As Expression
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

      AppendEntityMemberAccess(propertyName, entityIndex)

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
          ' this is primitive look ahead optimization for negation of HasValue calls (see also VisitMember(node As MemberExpression) method)
          ' note that nested negations won't be optimized
          Dim compensateForIgnoredNegation = False

          If node.Operand.NodeType = ExpressionType.MemberAccess Then
            Dim operandNode = DirectCast(node.Operand, MemberExpression)
            Dim operandExpression = operandNode.Expression

            If operandExpression IsNot Nothing AndAlso operandExpression.NodeType = ExpressionType.MemberAccess AndAlso Nullable.GetUnderlyingType(operandExpression.Type) IsNot Nothing AndAlso operandNode.Member.Name = "HasValue" Then
              Dim memberNode = DirectCast(operandExpression, MemberExpression)

              If memberNode.Expression IsNot Nothing AndAlso memberNode.Expression.NodeType = ExpressionType.Parameter Then
                compensateForIgnoredNegation = True
              End If
            End If
          End If

          If compensateForIgnoredNegation Then
            m_CompensateForIgnoredNegation = True
          Else
            m_Sql.Append(" NOT ")
          End If

        Case ExpressionType.Convert, ExpressionType.ConvertChecked

        Case Else
          Throw New NotSupportedException($"The unary operator '{node.NodeType}' is not supported.")
      End Select

      Me.Visit(node.Operand)

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

      m_Sql.Append("(")

      Me.Visit(left)

      Select Case nodeType
        Case ExpressionType.[And], ExpressionType.[AndAlso]
          m_Sql.Append(" AND ")

        Case ExpressionType.[Or], ExpressionType.[OrElse]
          m_Sql.Append(" OR ")

        Case ExpressionType.Equal
          If IsNullConstant(right) Then
            m_Sql.Append(" IS ")
          Else
            m_Sql.Append(" = ")
          End If

        Case ExpressionType.NotEqual
          If IsNullConstant(right) Then
            m_Sql.Append(" IS NOT ")
          Else
            m_Sql.Append(" <> ")
          End If

        Case ExpressionType.LessThan
          m_Sql.Append(" < ")

        Case ExpressionType.LessThanOrEqual
          m_Sql.Append(" <= ")

        Case ExpressionType.GreaterThan
          m_Sql.Append(" > ")

        Case ExpressionType.GreaterThanOrEqual
          m_Sql.Append(" >= ")

        Case ExpressionType.Add, ExpressionType.AddChecked
          m_Sql.Append(" + ")

        Case ExpressionType.Subtract, ExpressionType.SubtractChecked
          m_Sql.Append(" - ")

        Case ExpressionType.Multiply, ExpressionType.MultiplyChecked
          m_Sql.Append(" * ")

        Case ExpressionType.Divide
          m_Sql.Append(" / ")

        Case ExpressionType.Modulo
          m_Sql.Append(" % ")

        Case Else
          Throw New NotSupportedException($"The binary Operator '{nodeType}' is not supported.")
      End Select

      Me.Visit(right)

      m_Sql.Append(")")

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

          Case GetType(DateTime)
            AppendNewParameter(node.Value)

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
            Throw New NotSupportedException($"The constant for '{node.Value}' is not supported.")
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
      Dim index = m_ExpressionParameters.IndexOf(node)

      If m_EntityIndexHints Is Nothing Then
        ' this should not happen, because IJoin should be used when index hints are not available
        Throw New Exception("Unable to match expression parameter with entity.")
      End If

      If index < 0 OrElse m_EntityIndexHints.Length <= index Then
        Throw New Exception($"None or unambiguous match of entity of type '{node.Type}'. Use IJoin instead?")
      End If

      Dim entityIndex = m_EntityIndexHints(index)

      AppendEntityMembersAccess(entityIndex)

      Return node
    End Function

    ''' <summary>
    ''' Visits member.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Protected Overrides Function VisitMember(node As MemberExpression) As Expression
      ' special handling for: entityParameter.Property, entityParameter.Property.Value, entityParameter.Property.HasValue, joinParameter.T1.Property, joinParameter.T1.Property.Value, joinParameter.T1.Property.HasValue, ...
      ' other expressions are evaluated

      Dim isEntityMemberAccess = False
      Dim propertyName As String = Nothing
      Dim entityIndex As Int32 = -1
      Dim isNullableHasValueAccess = False

      Dim currentNode = node
      Dim exp = currentNode.Expression

      If exp IsNot Nothing Then
        ' this is hot path; checking IsValueType before calling Nullable.GetUnderlyingType seems to be faster for most use cases
        If exp.Type.IsValueType AndAlso Nullable.GetUnderlyingType(exp.Type) IsNot Nothing Then
          If currentNode.Member.Name = "Value" Then
            currentNode = DirectCast(exp, MemberExpression)
            exp = currentNode.Expression
          ElseIf currentNode.Member.Name = "HasValue" Then
            isNullableHasValueAccess = True
            currentNode = DirectCast(exp, MemberExpression)
            exp = currentNode.Expression
          End If
        End If

        propertyName = currentNode.Member.Name

        If exp.NodeType = ExpressionType.Parameter Then
          If m_ExpressionParametersType = ExpressionParametersType.IJoin Then
            entityIndex = Helpers.Common.GetEntityIndexFromJoinMemberName(node.Member.Name)
            AppendEntityMembersAccess(entityIndex)
            Return node
          Else
            isEntityMemberAccess = True

            Dim index = m_ExpressionParameters.IndexOf(DirectCast(exp, ParameterExpression))

            If m_EntityIndexHints Is Nothing Then
              ' this should not happen, because IJoin should be used when index hints are not available
              Throw New Exception("Unable to match expression parameter with entity.")
            End If

            If index < 0 OrElse m_EntityIndexHints.Length <= index Then
              Dim declaringType = currentNode.Member.DeclaringType
              Throw New Exception($"None or unambiguous match of entity of type '{declaringType}'. Use IJoin instead?")
            End If

            entityIndex = m_EntityIndexHints(index)
          End If

        ElseIf exp.NodeType = ExpressionType.MemberAccess Then
          Dim parent = DirectCast(exp, MemberExpression)

          If parent.Expression IsNot Nothing AndAlso parent.Expression.NodeType = ExpressionType.Parameter Then
            isEntityMemberAccess = True

            entityIndex = Helpers.Common.GetEntityIndexFromJoinMemberName(parent.Member.Name)
          End If
        End If
      End If

      If isEntityMemberAccess Then
        AppendEntityMemberAccess(propertyName, entityIndex)

        If isNullableHasValueAccess Then
          If m_CompensateForIgnoredNegation Then
            m_Sql.Append(" IS NULL")
            m_CompensateForIgnoredNegation = False
          Else
            m_Sql.Append(" IS NOT NULL")
          End If
        End If

        Return node
      Else
        Return VisitAndEvaluate(node)
      End If
    End Function

    ''' <summary>
    ''' Visits new.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Protected Overrides Function VisitNew(node As NewExpression) As Expression
      If IsValueTuple(node.Type) Then
        ' TODO: SIP - does it make sense to support nullable ValueTuples as well?
        Return VisitValueTupleOrAnonymousType(node, True)
      ElseIf IsAnonymousType(node.Type) Then
        Return VisitValueTupleOrAnonymousType(node, False)
      Else
        Return VisitAndEvaluate(node)
      End If
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
    ''' Visits ValueTuple or anonymous type.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <param name="isValueTuple"></param>
    ''' <returns></returns>
    Private Function VisitValueTupleOrAnonymousType(node As NewExpression, isValueTuple As Boolean) As Expression
      Dim count = node.Arguments.Count

      If m_InCustomSelectMode Then
        m_CustomEntities = New CustomSqlEntity(count - 1) {}
      End If

      Dim entities = m_Model.GetEntities().Select(Function(x) x.Entity.EntityType).ToList()

      ' NOTE: this will fail for nested ValueTuples, so we are limited to max 7 fields. Is it worth to support nesting?

      For i = 0 To count - 1
        Dim arg = node.Arguments(i)
        Dim type = arg.Type
        Dim entityIndex = entities.IndexOf(type)
        Dim isEntity = Not entityIndex = -1

        m_CustomEntityIndex = i

        Visit(arg)

        If m_InCustomSelectMode Then
          If isEntity Then
            m_CustomEntities(i) = New CustomSqlEntity(i, entityIndex, type)
          Else
            Dim columnAlias = CreateColumnAlias(i)
            m_Sql.Append(" ")
            m_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, columnAlias)
            m_CustomEntities(i) = New CustomSqlEntity(i, type)
          End If
        End If

        If Not i = count - 1 Then
          m_Sql.Append(", ")
        End If
      Next

      Return node
    End Function

    ''' <summary>
    ''' Visits in custom select mode.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function VisitInCustomSelectMode(node As Expression) As Expression
      If node.NodeType = ExpressionType.New Then
        Return Visit(node)
      Else
        m_CustomEntities = New CustomSqlEntity(0) {}

        Dim entities = m_Model.GetEntities().Select(Function(x) x.Entity.EntityType).ToList()

        Dim type = node.Type
        Dim entityIndex = entities.IndexOf(type)
        Dim isEntity = Not entityIndex = -1

        m_CustomEntityIndex = 0

        Visit(node)

        If isEntity Then
          m_CustomEntities(0) = New CustomSqlEntity(0, entityIndex, type)
        Else
          Dim columnAlias = CreateColumnAlias(0)
          m_Sql.Append(" ")
          m_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, columnAlias)
          m_CustomEntities(0) = New CustomSqlEntity(0, type)
        End If

        Return node
      End If
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
    ''' Checks whether type is ValueTuple.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Private Function IsValueTuple(type As Type) As Boolean
      If Not type.IsGenericType Then
        Return False
      End If

      type = type.GetGenericTypeDefinition()

      If type Is GetType(ValueTuple) Then
        Return True
      ElseIf type Is GetType(ValueTuple(Of )) Then
        Return True
      ElseIf type Is GetType(ValueTuple(Of ,)) Then
        Return True
      ElseIf type Is GetType(ValueTuple(Of ,,)) Then
        Return True
      ElseIf type Is GetType(ValueTuple(Of ,,,)) Then
        Return True
      ElseIf type Is GetType(ValueTuple(Of ,,,,)) Then
        Return True
      ElseIf type Is GetType(ValueTuple(Of ,,,,,)) Then
        Return True
      ElseIf type Is GetType(ValueTuple(Of ,,,,,,)) Then
        Return True
      ElseIf type Is GetType(ValueTuple(Of ,,,,,,,)) Then
        Return True
      End If

      Return False
    End Function

    ''' <summary>
    ''' Checks wheter type is an anonymous type.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Private Function IsAnonymousType(type As Type) As Boolean
      ' via https://stackoverflow.com/questions/2483023/how-to-test-if-a-type-is-anonymous
      ' and https://elegantcode.com/2011/06/24/detecting-anonymous-types-on-mono/
      Return Attribute.IsDefined(type, GetType(CompilerGeneratedAttribute), False) AndAlso
             type.IsGenericType AndAlso
             (type.Name.Contains("AnonymousType") OrElse type.Name.Contains("AnonType")) AndAlso
             (type.Name.StartsWith("<>") OrElse type.Name.StartsWith("VB$")) AndAlso
             (type.Attributes And TypeAttributes.NotPublic) = TypeAttributes.NotPublic
    End Function

    ''' <summary>
    ''' Checks whether expression is a null constant.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function IsNullConstant(node As Expression) As Boolean
      Return node.NodeType = ExpressionType.Constant AndAlso DirectCast(node, ConstantExpression).Value Is Nothing
    End Function

    ''' <summary>
    ''' Strips quotes.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Function StripQuotes(node As Expression) As Expression
      While node.NodeType = ExpressionType.Quote
        node = DirectCast(node, UnaryExpression).Operand
      End While

      Return node
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
    ''' Creates column alias.
    ''' </summary>
    ''' <param name="index1"></param>
    ''' <param name="index2"></param>
    ''' <returns></returns>
    Private Function CreateColumnAlias(index1 As Int32, index2 As Int32) As String
      Return "C" & index1.ToString(Globalization.CultureInfo.InvariantCulture) & "_" & index2.ToString(Globalization.CultureInfo.InvariantCulture)
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
    ''' Appends entity member access.
    ''' </summary>
    ''' <param name="propertyName"></param>
    ''' <param name="entityIndex"></param>
    Private Sub AppendEntityMemberAccess(propertyName As String, entityIndex As Int32)
      Dim entity = m_Model.GetEntity(entityIndex)
      Dim isIgnored = entity.IsIgnored
      Dim prop = entity.Entity.GetProperty(propertyName)

      If isIgnored Then
        ' NOTE: currently, we append NULL if table is ignored. This could be solved better for SELECT clauses.
        ' We could omit column in SQL and instead of using reader, set value directly to Nothing (default).
        m_Sql.Append("NULL")
      ElseIf Not m_UseTableNamesOrAliases Then
        m_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, prop.ColumnName)
      ElseIf m_UseAliases Then
        Dim tableAlias = m_Model.GetTableAlias(entity.Index)
        m_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, tableAlias)
        m_Sql.Append(".")
        m_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, prop.ColumnName)
      Else
        ' NOTE: this is not used right now
        m_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, entity.Entity.TableName)
        m_Sql.Append(".")
        m_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, prop.ColumnName)
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

      Dim isIgnored = entity.IsIgnored
      Dim properties = entity.Entity.GetProperties()
      Dim columnCount = entity.GetColumnCount()
      Dim columnIndex = 0

      For propertyIndex = 0 To properties.Count - 1
        If entity.IncludedColumns(propertyIndex) Then
          If isIgnored Then
            ' NOTE: currently, we append NULL if table is ignored. This could be solved better for SELECT clauses.
            ' We could omit columns in SQL and instead of using entity reader, set value directly to Nothing (default).
            m_Sql.Append("NULL")
          Else
            m_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, entity.TableAlias)
            m_Sql.Append(".")
            m_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, properties(propertyIndex).ColumnName)
          End If

          If m_InCustomSelectMode Then
            Dim columnAlias = CreateColumnAlias(m_CustomEntityIndex, columnIndex)
            m_Sql.Append(" ")
            m_Builder.DialectProvider.Formatter.AppendIdentifier(m_Sql, columnAlias)
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

  End Class
End Namespace