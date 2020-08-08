Imports System.Text

Namespace Internal.Helpers

  ''' <summary>
  ''' Text related helpers.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class Text

    ''' <summary>
    ''' Creates new instance of <see cref="Text"/>.
    ''' </summary>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Appends joined values.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sb"></param>
    ''' <param name="separator"></param>
    ''' <param name="values"></param>
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
