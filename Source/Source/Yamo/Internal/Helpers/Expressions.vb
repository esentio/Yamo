Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions

Namespace Internal.Helpers

  ''' <summary>
  ''' Expressions related helpers.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class Expressions

    ''' <summary>
    ''' Creates new instance of <see cref="Expressions"/>.
    ''' </summary>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Creates <see cref="Diagnostics.Debug.WriteLine(String)"/> expression.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="message"></param>
    ''' <returns></returns>
    Public Shared Function CreateDebugWriteLine(<DisallowNull> message As String) As Expression
      Dim debugWriteLineMethodInfo = GetType(Diagnostics.Debug).GetMethod("WriteLine", {GetType(String)})
      Return Expression.Call(debugWriteLineMethodInfo, Expression.Constant(message))
    End Function

  End Class
End Namespace
