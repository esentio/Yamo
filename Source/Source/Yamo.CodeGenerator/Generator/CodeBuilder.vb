Imports System.Text

Namespace Generator

  Public Class CodeBuilder

    Public Property CurrentIndent() As Int32

    Private ReadOnly Property Sb As StringBuilder

    Private ReadOnly Property Indentation As String

    Sub New(indentation As String)
      Me.CurrentIndent = 0
      Me.Sb = New StringBuilder
      Me.Indentation = indentation
    End Sub

    Public Function PushIndent() As CodeBuilder
      Me.CurrentIndent += 1
      Return Me
    End Function

    Public Function PopIndent() As CodeBuilder
      If 0 < Me.CurrentIndent Then
        Me.CurrentIndent -= 1
      End If

      Return Me
    End Function

    Public Function Append(text As String) As CodeBuilder
      Me.Sb.Append(text)
      Return Me
    End Function

    Public Function AppendLine(text As String) As CodeBuilder
      Me.Sb.AppendLine(text)
      Return Me
    End Function

    Public Sub AppendLine()
      Me.Sb.AppendLine()
    End Sub

    Public Function Indent() As CodeBuilder
      Me.Sb.Append(GetIndentation(Me.CurrentIndent))
      Return Me
    End Function

    Public Function Indent(indentCount As Int32) As CodeBuilder
      Me.Sb.Append(GetIndentation(indentCount))
      Return Me
    End Function

    Private Function GetIndentation(indent As Int32) As String
      Return String.Join("", Enumerable.Repeat(Me.Indentation, indent))
    End Function

    Public Overrides Function ToString() As String
      Return Me.Sb.ToString()
    End Function

  End Class
End Namespace