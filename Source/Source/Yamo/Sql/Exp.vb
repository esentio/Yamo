Imports System.Linq.Expressions
Imports System.Reflection
Imports Yamo.Infrastructure

Namespace Sql

  ''' <summary>
  ''' Expressions related SQL helper methods.
  ''' </summary>
  Public Class Exp
    Inherits SqlHelper

    ''' <summary>
    ''' Converts an expression to specific .NET type. Does not translate to SQL.<br/>
    ''' If expression is read, it forces using DbDataReader.Get* method specific for that .NET type.<br/>
    ''' Useful for converting to and from nullable types.<br/> 
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="expression"></param>
    ''' <returns></returns>
    Public Shared Function [As](Of T)(expression As Object) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Use expression provided as a raw SQL string.<br/>
    ''' If expression is read, it forces using DbDataReader.Get* method specific for that .NET type.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="expression"></param>
    ''' <returns></returns>
    Public Shared Function Raw(Of T)(expression As FormattableString) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Use expression provided as a raw SQL string.<br/>
    ''' If expression is read, it forces using DbDataReader.Get* method specific for that .NET type.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="expression"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Shared Function Raw(Of T)(expression As RawSqlString, ParamArray parameters() As Object) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to COALESCE(&lt;expression1&gt;, &lt;expression2&gt; ...) expression/function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="expression1"></param>
    ''' <param name="expression2"></param>
    ''' <param name="expressions"></param>
    ''' <returns></returns>
    Public Shared Function Coalesce(Of T)(expression1 As Object, expression2 As Object, ParamArray expressions() As Object) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to ISNULL(&lt;expression&gt;, &lt;replacementValue&gt;) function call. If ISNULL function is not available on the platform, IFNULL function is used instead.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="expression"></param>
    ''' <param name="replacementValue"></param>
    ''' <returns></returns>
    Public Shared Function IsNull(Of T)(expression As Object, replacementValue As Object) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to IFNULL(&lt;expression&gt;, &lt;replacementValue&gt;) function call. If IFNULL function is not available on the platform, ISNULL function is used instead.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="expression"></param>
    ''' <param name="replacementValue"></param>
    ''' <returns></returns>
    Public Shared Function IfNull(Of T)(expression As Object, replacementValue As Object) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to NULLIF(&lt;expression1&gt;, &lt;expression2&gt;) expression/function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="expression1"></param>
    ''' <param name="expression2"></param>
    ''' <returns></returns>
    Public Shared Function NullIf(Of T)(expression1 As Object, expression2 As Object) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to IIF(&lt;expression&gt;, &lt;trueValue&gt;, &lt;falseValue&gt;) expression/function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="expression"></param>
    ''' <param name="trueValue"></param>
    ''' <param name="falseValue"></param>
    ''' <returns></returns>
    Public Shared Function IIf(Of T)(expression As Boolean, trueValue As Object, falseValue As Object) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Returns SQL format string that is appended to final SQL statement.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="method"></param>
    ''' <param name="dialectProvider"></param>
    ''' <returns></returns>
    Public Overloads Shared Function GetSqlFormat(method As MethodCallExpression, dialectProvider As SqlDialectProvider) As SqlFormat
      Select Case method.Method.Name
        Case NameOf(Exp.As)
          Return New SqlFormat("{0}", method.Arguments)

        Case NameOf(Exp.Raw)
          Return GetSqlFormatForRaw(method)

        Case NameOf(Exp.Coalesce)
          Dim arguments = FlattenArguments(method)
          Dim args = String.Join(", ", Enumerable.Range(0, arguments.Count).Select(Function(x) "{" & x.ToString(Globalization.CultureInfo.InvariantCulture) & "}"))
          Return New SqlFormat("COALESCE(" & args & ")", arguments)

        Case NameOf(Exp.IsNull), NameOf(Exp.IfNull)
          Throw New NotSupportedException($"Method '{method.Method.Name}' should be implemented in platform specific SQL helper.")

        Case NameOf(Exp.NullIf)
          Return New SqlFormat("NULLIF({0}, {1})", method.Arguments)

        Case NameOf(Exp.IIf)
          ' NOTE: work since SQLite 3.32.0, which was released on 22 May 2020
          Return New SqlFormat("IIF({0}, {1}, {2})", method.Arguments)

        Case Else
          Throw New NotSupportedException($"Method '{method.Method.Name}' is not supported.")
      End Select
    End Function

    ''' <summary>
    ''' Returns SQL format string for <see cref="Raw(Of T)(FormattableString)"/> and <see cref="Raw(Of T)(RawSqlString, Object())"/> methods.
    ''' </summary>
    ''' <param name="method"></param>
    ''' <returns></returns>
    Private Shared Function GetSqlFormatForRaw(method As MethodCallExpression) As SqlFormat
      Dim arg = method.Arguments(0)

      If arg.NodeType = ExpressionType.Call AndAlso arg.Type Is GetType(FormattableString) Then
        Dim createMethodExp = DirectCast(arg, MethodCallExpression)

        If createMethodExp.Method.Name = "Create" AndAlso createMethodExp.Arguments.Count = 2 Then
          Dim formatParamExp = createMethodExp.Arguments(0)
          Dim argumentsParamExp = createMethodExp.Arguments(1)

          If formatParamExp.NodeType = ExpressionType.Constant Then
            Dim format = DirectCast(DirectCast(formatParamExp, ConstantExpression).Value, String)

            If argumentsParamExp.NodeType = ExpressionType.NewArrayInit Then
              Return New SqlFormat(format, DirectCast(argumentsParamExp, NewArrayExpression).Expressions)
            ElseIf argumentsParamExp.NodeType = ExpressionType.NewArrayBounds Then
              Return New SqlFormat(format, {})
            End If
          End If
        End If

      ElseIf arg.NodeType = ExpressionType.Convert AndAlso arg.Type Is GetType(RawSqlString) Then
        Dim stringConstantExp = DirectCast(arg, UnaryExpression).Operand

        If stringConstantExp.NodeType = ExpressionType.Constant Then
          Dim format = DirectCast(DirectCast(stringConstantExp, ConstantExpression).Value, String)

          If method.Arguments.Count = 2 AndAlso method.Arguments(1).NodeType = ExpressionType.NewArrayInit Then
            Return New SqlFormat(format, DirectCast(method.Arguments(1), NewArrayExpression).Expressions)
          Else
            Return New SqlFormat(format, {})
          End If
        End If
      End If

      Throw New NotSupportedException($"Method '{method.Method.Name}' is not used correctly. Use only String or FormattableString as a method parameter. String has to be declared directly in method call. Using variable as a parameter is not supported.")
    End Function
  End Class
End Namespace
