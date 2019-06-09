Imports System.Linq.Expressions

Namespace Internal.Helpers

  Public Class Expressions

    Private Sub New()
    End Sub

    Public Shared Function CreateDebugWriteLine(message As String) As Expression
      Dim debugWriteLineMethodInfo = GetType(Diagnostics.Debug).GetMethod("WriteLine", {GetType(String)})
      Return Expression.Call(debugWriteLineMethodInfo, Expression.Constant(message))
    End Function

  End Class
End Namespace
