Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateWithHints(builder As CodeBuilder, entityCount As Int32)
      GenerateWithHintsWithString(builder, entityCount)
    End Sub

    Protected Sub GenerateWithHintsWithString(builder As CodeBuilder, entityCount As Int32)
      If entityCount = 1 Then
        Dim comment = "Adds table hint(s)."
        Dim params = {"tableHints"}
        AddComment(builder, comment, params:=params, returns:="")

        Dim generics = String.Join(", ", GetGenericNames(entityCount))

        builder.Indent().AppendLine($"Public Function WithHints(<DisallowNull> tableHints As String) As WithHintsSelectSqlExpression(Of {generics})").PushIndent()
        builder.Indent().AppendLine("Me.Builder.SetMainTableHints(tableHints)")
        builder.Indent().AppendLine($"Return New WithHintsSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
        builder.Indent().AppendLine("End Function")
      Else
        Dim comment = "Adds table hint(s)."
        Dim params = {"tableHints"}
        AddComment(builder, comment, params:=params, returns:="")

        Dim generics = String.Join(", ", GetGenericNames(entityCount))

        builder.Indent().AppendLine($"Public Function WithHints(<DisallowNull> tableHints As String) As JoinWithHintsSelectSqlExpression(Of {generics})").PushIndent()
        builder.Indent().AppendLine("Me.Builder.SetLastJoinTableHints(tableHints)")
        builder.Indent().AppendLine($"Return New JoinWithHintsSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
        builder.Indent().AppendLine("End Function")
      End If
    End Sub

  End Class
End Namespace