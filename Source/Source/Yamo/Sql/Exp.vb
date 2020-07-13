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

  End Class
End Namespace
