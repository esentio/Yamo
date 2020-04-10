Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateToList(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Executes SQL query and returns list of records."
      AddComment(builder, comment, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function ToList() As List(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Dim query = Me.Builder.CreateQuery()")
      builder.Indent().AppendLine($"Return Me.Executor.ReadList(Of {generic})(query)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCustomToList(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Executes SQL query and returns list of records."
      AddComment(builder, comment, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function ToList() As List(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Dim query = Me.Builder.CreateQuery()")
      builder.Indent().AppendLine($"Return Me.Executor.ReadCustomList(Of {generic})(query)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace