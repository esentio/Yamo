Imports System.Linq.Expressions
Imports System.Reflection
Imports Yamo.Infrastructure
Imports Yamo.Sql

Namespace Sql

  ''' <summary>
  ''' Expressions related SQL helper methods.<br/>
  ''' Specific for SQLite.
  ''' </summary>
  Public Class Exp
    Inherits Yamo.Sql.Exp

    ''' <summary>
    ''' Returns SQL format string that is appended to final SQL statement.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="method"></param>
    ''' <param name="dialectProvider"></param>
    ''' <returns></returns>
    Public Overloads Shared Function GetSqlFormat(method As MethodCallExpression, dialectProvider As SqlDialectProvider) As SqlFormat
      Select Case method.Method.Name
        Case NameOf(Exp.IsNull), NameOf(Exp.IfNull)
          Return New SqlFormat("IFNULL({0}, {1})", method.Arguments)
        Case Else
          Return Yamo.Sql.Exp.GetSqlFormat(method, dialectProvider)
      End Select
    End Function

  End Class
End Namespace
