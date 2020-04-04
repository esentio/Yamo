Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateHavingAnd(builder As CodeBuilder, entityCount As Int32)
      Dim limit = 8

      If entityCount < limit Then
        For i = 1 To entityCount
          GenerateHavingAndWithPredicateWithOneEntity(builder, i, entityCount)
          builder.AppendLine()

          GenerateHavingAndWithPredicateWithOneEntityReturningFormattableString(builder, i, entityCount)
          builder.AppendLine()
        Next
      End If

      If 1 < entityCount Then
        If entityCount < 4 Then
          GenerateHavingAndWithPredicateWithAllEntities(builder, entityCount)
          builder.AppendLine()

          GenerateHavingAndWithPredicateWithAllEntitiesReturningFormattableString(builder, entityCount)
          builder.AppendLine()
        End If

        GenerateHavingAndWithPredicateWithIJoin(builder, entityCount)
        builder.AppendLine()

        GenerateHavingAndWithPredicateWithIJoinReturningFormattableString(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateHavingAndWithString(builder, entityCount)
      builder.AppendLine()

      GenerateInternalHavingForHavingAnd(builder, entityCount)
    End Sub

    Protected Sub GenerateHavingAndWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds AND condition to HAVING clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [And](predicate As Expression(Of Func(Of {generic}, Boolean))) As HavingSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalHaving(predicate, {GetEntityIndexHintsForEntity(index - 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateHavingAndWithPredicateWithOneEntityReturningFormattableString(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds AND condition to HAVING clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [And](predicate As Expression(Of Func(Of {generic}, FormattableString))) As HavingSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalHaving(predicate, {GetEntityIndexHintsForEntity(index - 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateHavingAndWithPredicateWithAllEntities(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds AND condition to HAVING clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [And](predicate As Expression(Of Func(Of {generics}, Boolean))) As HavingSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalHaving(predicate, {GetEntityIndexHintsForAllEntities(entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateHavingAndWithPredicateWithAllEntitiesReturningFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds AND condition to HAVING clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [And](predicate As Expression(Of Func(Of {generics}, FormattableString))) As HavingSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalHaving(predicate, {GetEntityIndexHintsForAllEntities(entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateHavingAndWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds AND condition to HAVING clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [And](predicate As Expression(Of Func(Of Join(Of {generics}), Boolean))) As HavingSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalHaving(predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateHavingAndWithPredicateWithIJoinReturningFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds AND condition to HAVING clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [And](predicate As Expression(Of Func(Of Join(Of {generics}), FormattableString))) As HavingSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalHaving(predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateHavingAndWithString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds AND condition to HAVING clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [And](predicate As String) As HavingSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddHaving(predicate)")
      builder.Indent().AppendLine("Return Me").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalHavingForHavingAnd(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds HAVING clause."
      Dim params = {"predicate", "entityIndexHints"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalHaving(predicate As Expression, entityIndexHints As Int32()) As HavingSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddHaving(predicate, entityIndexHints)")
      builder.Indent().AppendLine("Return Me").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace