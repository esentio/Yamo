Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateDistinct(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds DISTINCT clause."
      AddComment(builder, comment, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Distinct() As DistinctSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddDistinct()")
      builder.Indent().AppendLine($"Return New DistinctSelectSqlExpression(Of {generic})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCustomDistinct(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds DISTINCT clause."
      AddComment(builder, comment, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Distinct() As CustomDistinctSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddDistinct()")
      builder.Indent().AppendLine($"Return New CustomDistinctSelectSqlExpression(Of {generic})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace