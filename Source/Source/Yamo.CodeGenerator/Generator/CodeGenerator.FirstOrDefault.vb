﻿Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateFirstOrDefault(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Executes SQL query and returns first record or a default value."
      AddComment(builder, comment, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function FirstOrDefault() As {generic}").PushIndent()
      builder.Indent().AppendLine("Dim query = Me.Builder.CreateQuery()")
      builder.Indent().AppendLine($"Return Me.Executor.ReadFirstOrDefault(Of {generic})(query)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCustomFirstOrDefault(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Executes SQL query and returns first record or a default value."
      AddComment(builder, comment, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function FirstOrDefault() As {generic}").PushIndent()
      builder.Indent().AppendLine("Dim query = Me.Builder.CreateQuery()")
      builder.Indent().AppendLine($"Return Me.Executor.ReadCustomFirstOrDefault(Of {generic})(query)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace