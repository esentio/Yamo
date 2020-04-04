Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateLimit(builder As CodeBuilder, entityCount As Int32)
      GenerateLimitWithCount(builder, entityCount)
      builder.AppendLine()

      GenerateLimitWithCountAndOffset(builder, entityCount)
      builder.AppendLine()

      GenerateInternalLimit(builder, entityCount)
    End Sub

    Protected Sub GenerateLimitWithCount(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used."
      Dim params = {"count"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Limit(count As Int32) As LimitedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalLimit(Nothing, count)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateLimitWithCountAndOffset(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used."
      Dim params = {"offset", "count"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Limit(offset As Int32, count As Int32) As LimitedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalLimit(offset, count)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalLimit(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used."
      Dim params = {"offset", "count"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalLimit(offset As Int32?, count As Int32) As LimitedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddLimit(offset, count)")
      builder.Indent().AppendLine($"Return New LimitedSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace