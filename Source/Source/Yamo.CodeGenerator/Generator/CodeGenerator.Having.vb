Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateHaving(builder As CodeBuilder, entityCount As Int32)
      For i = 1 To entityCount
        GenerateHavingWithPredicateWithOneEntity(builder, i, entityCount)
        builder.AppendLine()

        GenerateHavingWithPredicateWithOneEntityReturningFormattableString(builder, i, entityCount)
        builder.AppendLine()
      Next

      If 1 < entityCount Then
        If entityCount < 4 Then
          GenerateHavingWithPredicateWithAllEntities(builder, entityCount)
          builder.AppendLine()

          GenerateHavingWithPredicateWithAllEntitiesReturningFormattableString(builder, entityCount)
          builder.AppendLine()
        End If

        GenerateHavingWithPredicateWithIJoin(builder, entityCount)
        builder.AppendLine()

        GenerateHavingWithPredicateWithIJoinReturningFormattableString(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateHavingWithString(builder, entityCount)
      builder.AppendLine()

      GenerateParameterlessHaving(builder, entityCount)
      builder.AppendLine()

      GenerateInternalHaving(builder, entityCount)
    End Sub

    Protected Sub GenerateHavingWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds HAVING clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Having(<DisallowNull> predicate As Expression(Of Func(Of {generic}, Boolean))) As HavingSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalHaving(predicate, {GetEntityIndexHintsForEntity(index - 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateHavingWithPredicateWithOneEntityReturningFormattableString(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds HAVING clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Having(<DisallowNull> predicate As Expression(Of Func(Of {generic}, FormattableString))) As HavingSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalHaving(predicate, {GetEntityIndexHintsForEntity(index - 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateHavingWithPredicateWithAllEntities(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds HAVING clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Having(<DisallowNull> predicate As Expression(Of Func(Of {generics}, Boolean))) As HavingSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalHaving(predicate, {GetEntityIndexHintsForAllEntities(entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateHavingWithPredicateWithAllEntitiesReturningFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds HAVING clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Having(<DisallowNull> predicate As Expression(Of Func(Of {generics}, FormattableString))) As HavingSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalHaving(predicate, {GetEntityIndexHintsForAllEntities(entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateHavingWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds HAVING clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Having(<DisallowNull> predicate As Expression(Of Func(Of Join(Of {generics}), Boolean))) As HavingSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalHaving(predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateHavingWithPredicateWithIJoinReturningFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds HAVING clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Having(<DisallowNull> predicate As Expression(Of Func(Of Join(Of {generics}), FormattableString))) As HavingSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalHaving(predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateHavingWithString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds HAVING clause."
      Dim params = {"predicate", "parameters"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Having(<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object) As HavingSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddHaving(predicate, parameters)")
      builder.Indent().AppendLine($"Return New HavingSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateParameterlessHaving(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds HAVING clause."
      AddComment(builder, comment, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Having() As HavingSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return New HavingSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalHaving(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds HAVING clause."
      Dim params = {"predicate", "entityIndexHints"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalHaving(predicate As Expression, entityIndexHints As Int32()) As HavingSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddHaving(predicate, entityIndexHints)")
      builder.Indent().AppendLine($"Return New HavingSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace