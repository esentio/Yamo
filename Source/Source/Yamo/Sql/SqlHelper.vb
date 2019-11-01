Imports System.Reflection
Imports Yamo.Infrastructure

Namespace Sql

  ''' <summary>
  ''' Base class for API that provides support for SQL helper methods.
  ''' </summary>
  Public MustInherit Class SqlHelper

    ''' <summary>
    ''' Creates new instance of <see cref="SqlHelper"/>.
    ''' </summary>
    Protected Sub New()
    End Sub

    ''' <summary>
    ''' Returns SQL format string that is appended to final SQL statement.<br/>
    ''' This method has to be overloaded.
    ''' </summary>
    ''' <param name="method">SQL helper method</param>
    ''' <param name="dialectProvider"></param>
    ''' <returns></returns>
    Public Shared Function GetSqlFormat(method As MethodInfo, dialectProvider As SqlDialectProvider) As String
      Throw New Exception("This method has to be overloaded.")
    End Function

  End Class
End Namespace