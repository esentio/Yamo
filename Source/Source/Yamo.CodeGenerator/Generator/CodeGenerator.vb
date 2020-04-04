Imports System.IO
Imports System.Text

Namespace Generator

  Public MustInherit Class CodeGenerator

    Protected ReadOnly Property Indentation As String

    Protected ReadOnly Property MinEntityCount As Int32

    Protected ReadOnly Property MaxEntityCount As Int32

    Protected ReadOnly Property OutputFolder As String

    Sub New(indentation As String, maxEntityCount As Int32, outputFolder As String)
      Me.New(indentation, 1, maxEntityCount, outputFolder)
    End Sub

    Sub New(indentation As String, minEntityCount As Int32, maxEntityCount As Int32, outputFolder As String)
      Me.Indentation = indentation
      Me.MinEntityCount = minEntityCount
      Me.MaxEntityCount = maxEntityCount
      Me.OutputFolder = outputFolder
    End Sub

    Public Sub Generate()
      For i = Me.MinEntityCount To Me.MaxEntityCount
        Generate(i)
      Next
    End Sub

    Protected Sub Generate(entityCount As Int32)
      Dim builder = CreateCodeBuilder()

      Generate(builder, entityCount)

      Dim filename = GetFilename(entityCount)
      CreateFile(filename, builder.ToString())
    End Sub

    Protected MustOverride Function GetFilename(entityCount As Int32) As String

    Protected MustOverride Sub Generate(builder As CodeBuilder, entityCount As Int32)

    Protected Function CreateCodeBuilder() As CodeBuilder
      Return New CodeBuilder(Me.Indentation)
    End Function

    Protected Sub CreateFile(filename As String, content As String)
      Dim filePath = Path.Combine(Me.OutputFolder, filename)
      File.WriteAllText(filePath, content, Encoding.UTF8)
    End Sub

  End Class
End Namespace