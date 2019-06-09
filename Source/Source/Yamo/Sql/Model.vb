Imports System.Reflection
Imports Yamo.Infrastructure

Namespace Sql

  Public Class Model
    Inherits SqlHelper

    Public Shared Function Columns(Of T)(Optional tableAlias As String = Nothing) As ModelInfo
      Return New ModelInfo(GetType(T), tableAlias)
    End Function

    Public Overloads Shared Function GetSqlFormat(method As MethodInfo, dialectProvider As SqlDialectProvider) As String
      Throw New InvalidOperationException("This method is not intended to be called in this SQL helper.")
    End Function

  End Class
End Namespace
