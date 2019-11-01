Imports System.Text

Namespace Internal.Helpers

  ' TODO: SIP - add documentation to this class.
  Public Class Text

    Private Sub New()
    End Sub

    Public Shared Sub AppendJoin(sb As StringBuilder, separator As String, values As List(Of String))
      Dim count = values.Count

      For i = 0 To count - 2
        sb.Append(values(i))
        sb.Append(separator)
      Next

      sb.Append(values(count - 1))
    End Sub

  End Class
End Namespace
