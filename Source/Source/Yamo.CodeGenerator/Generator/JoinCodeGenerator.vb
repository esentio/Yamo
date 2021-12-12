Namespace Generator

  Public Class JoinCodeGenerator
    Inherits CodeGenerator

    Public Sub New(indentation As String, outputFolder As String, definition As GeneratedClassDefinition, definitions As List(Of GeneratedClassDefinition))
      MyBase.New(indentation, outputFolder, definition, definitions)
    End Sub

    Protected Overrides Function GetAllowedResultsForCondition() As GeneratedClass()
      Return {}
    End Function

    Protected Overrides Sub Generate(builder As CodeBuilder, entityCount As Int32)
      Dim comment = $"Metadata defining {entityCount.ToInvariantString()} entities used in JOIN statements."
      Dim typeParams = GetGenericNames("TTable", entityCount)
      AddComment(builder, comment, typeParams:=typeParams)

      builder.Indent().AppendLine($"Public Class {GetFullClassName("TTable", entityCount)}").PushIndent()
      builder.Indent().AppendLine("Implements IJoin")
      builder.AppendLine()

      GenerateProperties(builder, entityCount)

      GenerateConstructor(builder, entityCount)
      builder.AppendLine()

      builder.PopIndent()
      builder.Indent().AppendLine($"End Class")
    End Sub

    Private Sub GenerateConstructor(builder As CodeBuilder, entityCount As Int32)
      Dim comment = $"Creates new instance of <see cref=""{GetFullClassName("TTable", entityCount)}""/>."
      Dim params = Enumerable.Range(1, entityCount).Select(Function(x) "table" & x.ToInvariantString()).ToArray()
      AddComment(builder, comment, params:=params)

      builder.Indent().Append("Sub New(")

      For i = 1 To entityCount - 1
        builder.Append($"table{i.ToInvariantString()} As TTable{i.ToInvariantString()}, ")
      Next
      builder.AppendLine($"table{entityCount.ToInvariantString()} As TTable{entityCount.ToInvariantString()})").PushIndent()

      For i = 1 To entityCount
        builder.Indent().AppendLine($"Me.T{i.ToInvariantString()} = table{i.ToInvariantString()}")
      Next

      builder.PopIndent()
      builder.Indent().AppendLine("End Sub")
    End Sub

    Private Sub GenerateProperties(builder As CodeBuilder, entityCount As Int32)
      For i = 1 To entityCount
        Dim comment = $"Gets {GetOrdinal(i)} entity."
        Dim returns = ""
        AddComment(builder, comment, returns:=returns)
        builder.Indent().AppendLine($"Public ReadOnly Property T{i.ToInvariantString()} As TTable{i.ToInvariantString()}")
        builder.AppendLine()
      Next
    End Sub

  End Class
End Namespace