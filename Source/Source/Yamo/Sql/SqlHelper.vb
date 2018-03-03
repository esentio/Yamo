Imports Yamo.Infrastructure

Namespace Sql

  Public MustInherit Class SqlHelper

    Protected Sub New()
    End Sub

    Public Shared Function GetSqlFormat(methodName As String, dialectProvider As SqlDialectProvider) As String
      Throw New Exception("This method has to be overloaded.")
    End Function

  End Class
End Namespace