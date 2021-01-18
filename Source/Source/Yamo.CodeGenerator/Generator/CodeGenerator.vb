Imports System.IO
Imports System.Text

Namespace Generator

  Public MustInherit Class CodeGenerator

    Protected ReadOnly Property Indentation As String

    Protected ReadOnly Property OutputFolder As String

    Protected ReadOnly Property Definition As GeneratedClassDefinition

    Protected ReadOnly Property Definitions As List(Of GeneratedClassDefinition)

    Public Sub New(indentation As String, outputFolder As String, definition As GeneratedClassDefinition, definitions As List(Of GeneratedClassDefinition))
      Me.Indentation = indentation
      Me.OutputFolder = outputFolder
      Me.Definition = definition
      Me.Definitions = definitions
    End Sub

    Public Sub Generate()
      For i = Me.Definition.MinEntityCount To Me.Definition.MaxEntityCount
        Generate(i)
      Next
    End Sub

    Protected Sub Generate(entityCount As Int32)
      Dim builder = CreateCodeBuilder()

      Generate(builder, entityCount)

      Dim filename = GetFilename(entityCount)
      CreateFile(filename, builder.ToString())
    End Sub

    Protected Function GetClassName() As String
      Return GetClassName(Me.Definition.Type)
    End Function

    Protected Function GetClassName(type As GeneratedClass) As String
      Select Case type
        Case GeneratedClass.SelectSqlExpression
          Return "SelectSqlExpression"
        Case GeneratedClass.WithHintsSelectSqlExpression
          Return "WithHintsSelectSqlExpression"
        Case GeneratedClass.JoinSelectSqlExpression
          Return "JoinSelectSqlExpression"
        Case GeneratedClass.JoinWithHintsSelectSqlExpression
          Return "JoinWithHintsSelectSqlExpression"
        Case GeneratedClass.JoinedSelectSqlExpression
          Return "JoinedSelectSqlExpression"
        Case GeneratedClass.FilteredSelectSqlExpression
          Return "FilteredSelectSqlExpression"
        Case GeneratedClass.GroupedSelectSqlExpression
          Return "GroupedSelectSqlExpression"
        Case GeneratedClass.HavingSelectSqlExpression
          Return "HavingSelectSqlExpression"
        Case GeneratedClass.OrderedSelectSqlExpression
          Return "OrderedSelectSqlExpression"
        Case GeneratedClass.LimitedSelectSqlExpression
          Return "LimitedSelectSqlExpression"
        Case GeneratedClass.SelectedSelectSqlExpression
          Return "SelectedSelectSqlExpression"
        Case GeneratedClass.DistinctSelectSqlExpression
          Return "DistinctSelectSqlExpression"
        Case GeneratedClass.CustomSelectSqlExpression
          Return "CustomSelectSqlExpression"
        Case GeneratedClass.CustomDistinctSelectSqlExpression
          Return "CustomDistinctSelectSqlExpression"
        Case Else
          Throw New NotSupportedException()
      End Select
    End Function

    Protected MustOverride Function GetAllowedResultsForCondition() As GeneratedClass()

    Protected Function GetFullClassName(entityCount As Int32) As String
      Return GetClassName() & GetGenericOfDefinition(entityCount)
    End Function

    Protected Function GetFilename(entityCount As Int32) As String
      Return GetClassName() & GetGenericsSuffixForFilename(entityCount) & ".vb"
    End Function

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