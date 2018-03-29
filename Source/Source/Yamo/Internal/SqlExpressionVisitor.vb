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

  Public Class SqlExpressionVisitor
    Inherits ExpressionVisitor

    Private m_Builder As SqlExpressionBuilderBase

    Private m_Model As SqlModel

    Private m_ExpressionParameters As ReadOnlyCollection(Of ParameterExpression)

    Private m_EntityIndexHints As Int32()

    Private m_Sql As StringBuilder

    Private m_Parameters As List(Of SqlParameter)

    Private m_ParameterIndex As Int32

    Private m_UseAliases As Boolean

    Private m_UseTableNamesOrAliases As Boolean

    Private m_CompensateForIgnoredNegation As Boolean

    Private m_CurrentLikeParameterFormat As String

    Public Sub New(builder As SqlExpressionBuilderBase, model As SqlModel)
      m_Builder = builder
      m_Model = model
    End Sub

    ' entityIndexHints might be nothing!!! - napisat do komentarov aj pre volajuce metody
    Public Function Translate(expression As Expression, entityIndexHints As Int32(), parameterIndex As Int32, useAliases As Boolean, useTableNamesOrAliases As Boolean) As SqlString
      If TypeOf expression IsNot LambdaExpression Then
        Throw New ArgumentException("Expression must be of type LambdaExpression.")
      End If

      Dim lambda = DirectCast(expression, LambdaExpression)

      m_ExpressionParameters = lambda.Parameters
      m_EntityIndexHints = entityIndexHints
      m_Sql = New StringBuilder()
      m_Parameters = New List(Of SqlParameter)
      m_ParameterIndex = parameterIndex
      m_UseAliases = useAliases
      m_UseTableNamesOrAliases = useTableNamesOrAliases
      m_CompensateForIgnoredNegation = False
      m_CurrentLikeParameterFormat = Nothing

      Visit(expression)

      m_ExpressionParameters = Nothing

      Return New SqlString(m_Sql.ToString(), m_Parameters)
    End Function

    Public Overrides Function Visit(node As Expression) As Expression
      Return MyBase.Visit(node)
    End Function

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

      If node.Method.IsSpecialName AndAlso node.Object.NodeType = ExpressionType.Parameter AndAlso node.Method.Name.StartsWith("set_") Then
        ' this is needed when we pass Action (in UPDATE SET... expressions)
        ' TODO: SIP - investigate how indexers behave
        Return VisitEntityPropertySetter(node)
      End If

      Return VisitAndEvaluate(node)
    End Function

    Protected Function VisitStartsWithMethodCall(node As MethodCallExpression) As Expression
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

    Protected Function VisitEndsWithMethodCall(node As MethodCallExpression) As Expression
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

    Protected Function VisitContainsMethodCall(node As MethodCallExpression) As Expression
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

    Protected Function VisitConcatMethodCall(node As MethodCallExpression) As Expression
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

    Protected Function VisitCreateFormattableStringMethodCall(node As MethodCallExpression) As Expression
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

    Private Function VisitSqlHelperMethodCall(node As MethodCallExpression) As Expression
      Dim getSqlFormatMethod = node.Method.DeclaringType.GetMethod(NameOf(SqlHelper.GetSqlFormat), BindingFlags.Public Or BindingFlags.Static)
      Dim format = DirectCast(getSqlFormatMethod.Invoke(Nothing, {node.Method.Name, m_Builder.DialectProvider}), String)

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

    Private Function VisitEntityPropertySetter(node As MethodCallExpression) As Expression
      Dim propertyName = node.Method.Name.Substring(4) ' trim "set_"
      Dim entityIndex = 0 ' should be always 0 in so far allowed scenarios (UPDATE SET) and m_EntityIndexHints should always be set

      AppendEntityMemberAccess(propertyName, entityIndex)

      m_Sql.Append(" = ")

      Visit(node.Arguments(0))

      Return node
    End Function

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

    Protected Overrides Function VisitParameter(node As ParameterExpression) As Expression
      Return node
    End Function

    Protected Overrides Function VisitMember(node As MemberExpression) As Expression
      ' special handling for: entityParameter.Property, entityParameter.Property.Value, entityParameter.Property.HasValue, joinParameter.T1.Property, joinParameter.T1.Property.Value, joinParameter.T1.Property.HasValue, ...
      ' other expressions are evaluated

      Dim isEntityMemberAccess = False
      Dim propertyName As String = Nothing
      Dim entityIndex As Int32 = -1
      Dim isNullableHasValueAccess = False

      Dim currentNode = node

      If currentNode.Expression IsNot Nothing Then
        If Nullable.GetUnderlyingType(currentNode.Expression.Type) IsNot Nothing Then
          If currentNode.Member.Name = "Value" Then
            currentNode = DirectCast(currentNode.Expression, MemberExpression)
          ElseIf currentNode.Member.Name = "HasValue" Then
            isNullableHasValueAccess = True
            currentNode = DirectCast(currentNode.Expression, MemberExpression)
          End If
        End If

        If currentNode.Expression.NodeType = ExpressionType.Parameter Then
          isEntityMemberAccess = True

          Dim index = m_ExpressionParameters.IndexOf(DirectCast(currentNode.Expression, ParameterExpression))

          If m_EntityIndexHints Is Nothing Then
            ' this should not happen, because IJoin should be used when index hints are not available
            Throw New Exception("Unable to match expression parameter with entity.")
          End If

          If m_EntityIndexHints.Length <= index Then
            Dim declaringType = currentNode.Member.DeclaringType
            Throw New Exception($"None or unambiguous match of entity of type '{declaringType}'. Use IJoin instead?")
          End If

          propertyName = currentNode.Member.Name
          entityIndex = m_EntityIndexHints(index)

        ElseIf currentNode.Expression.NodeType = ExpressionType.MemberAccess Then
          Dim parent = DirectCast(currentNode.Expression, MemberExpression)

          If parent.Expression IsNot Nothing AndAlso parent.Expression.NodeType = ExpressionType.Parameter Then
            isEntityMemberAccess = True

            propertyName = currentNode.Member.Name
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

    Private Sub AppendEntityMemberAccess(propertyName As String, entityIndex As Int32)
      Dim entity = m_Model.GetEntity(entityIndex)
      Dim prop = entity.Entity.GetProperty(propertyName)

      If Not m_UseTableNamesOrAliases Then
        m_Sql.Append(m_Builder.DialectProvider.Formatter.CreateIdentifier(prop.ColumnName))
      ElseIf m_UseAliases Then
        Dim tableAlias = m_Model.GetTableAlias(entity.Index)
        m_Sql.Append($"{m_Builder.DialectProvider.Formatter.CreateIdentifier(tableAlias)}.{m_Builder.DialectProvider.Formatter.CreateIdentifier(prop.ColumnName)}")
      Else
        ' NOTE: this is not used right now
        m_Sql.Append($"{m_Builder.DialectProvider.Formatter.CreateIdentifier(entity.Entity.TableName)}.{m_Builder.DialectProvider.Formatter.CreateIdentifier(prop.ColumnName)}")
      End If
    End Sub

    Protected Overrides Function VisitNew(node As NewExpression) As Expression
      Return VisitAndEvaluate(node)
    End Function

    Protected Overrides Function VisitNewArray(node As NewArrayExpression) As Expression
      Return VisitAndEvaluate(node)
    End Function

    Protected Function VisitAndEvaluate(node As Expression) As Expression
      AppendNewParameter(Evaluate(node))
      Return node
    End Function

    Protected Function Evaluate(node As Expression) As Object
      ' TODO: rewrite and make faster when possible!!! Currently, only simple captured variables are obtained fast.
      ' For everything else, lambda compilation is used, which is slow.
      ' Study:
      ' https://stackoverflow.com/questions/2616638/access-the-value-of-a-member-expression
      ' https://stackoverflow.com/questions/1613239/getting-the-object-out-of-a-memberexpression
      ' https://blogs.msdn.microsoft.com/csharpfaq/2010/03/11/how-can-i-get-objects-and-property-values-from-expression-trees/
      ' https://github.com/Miaplaza/expression-utils
      ' https://stackoverflow.com/questions/38345606/how-can-i-access-the-value-of-a-local-variable-from-within-an-expression-tree

      Dim value As Object = Nothing
      Dim valueSet = False

      ' shortcut for captured variables
      If node.NodeType = ExpressionType.MemberAccess Then
        Dim memberExpression = DirectCast(node, MemberExpression)
        Dim exp = memberExpression.Expression

        If exp Is Nothing Then
          ' shared property/field
          If memberExpression.Member.MemberType = MemberTypes.Property Then
            value = DirectCast(memberExpression.Member, PropertyInfo).GetValue(Nothing)
            valueSet = True
          ElseIf memberExpression.Member.MemberType = MemberTypes.Field Then
            value = DirectCast(memberExpression.Member, FieldInfo).GetValue(Nothing)
            valueSet = True
          End If
        ElseIf exp.NodeType = ExpressionType.Constant Then
          Dim obj = DirectCast(exp, ConstantExpression).Value

          If memberExpression.Member.MemberType = MemberTypes.Property Then
            value = DirectCast(memberExpression.Member, PropertyInfo).GetValue(obj)
            valueSet = True
          ElseIf memberExpression.Member.MemberType = MemberTypes.Field Then
            value = DirectCast(memberExpression.Member, FieldInfo).GetValue(obj)
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

    Protected Function IsNullConstant(node As Expression) As Boolean
      Return node.NodeType = ExpressionType.Constant AndAlso DirectCast(node, ConstantExpression).Value Is Nothing
    End Function

    Private Function StripQuotes(node As Expression) As Expression
      While node.NodeType = ExpressionType.Quote
        node = DirectCast(node, UnaryExpression).Operand
      End While

      Return node
    End Function

    Private Sub AppendNewParameter(value As Object)
      Dim parameterName = m_Builder.CreateParameter(m_ParameterIndex + m_Parameters.Count)

      m_Sql.Append(parameterName)
      m_Parameters.Add(New SqlParameter(parameterName, value))
    End Sub

  End Class
End Namespace