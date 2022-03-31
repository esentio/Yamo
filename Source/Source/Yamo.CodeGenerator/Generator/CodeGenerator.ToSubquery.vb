Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateToSubquery(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Creates SQL subquery."
      AddComment(builder, comment, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function ToSubquery() As Subquery(Of {generic})").PushIndent()
      builder.Indent().AppendLine($"Return Me.Builder.CreateSubquery(Of {generic})()").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace