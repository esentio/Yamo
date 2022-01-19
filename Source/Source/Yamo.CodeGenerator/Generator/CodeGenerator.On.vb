Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateOn(builder As CodeBuilder, entityCount As Int32)
      Dim limit = 8

      If entityCount < limit Then
        For i = 1 To entityCount - 1
          GenerateOnWithPredicateWithOneEntity(builder, i, entityCount)
          builder.AppendLine()
        Next
      End If

      If entityCount = 3 Then
        GenerateOnWithPredicateWithAllEntities(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateOnWithPredicateWithIJoin(builder, entityCount)
      builder.AppendLine()

      GenerateInternalOn(builder, entityCount)
    End Sub

    Protected Sub GenerateOnWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds ON clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic1 = GetGenericName(index, index = entityCount)
      Dim generic2 = GetGenericName(entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [On](<DisallowNull> predicate As Expression(Of Func(Of {generic1}, {generic2}, Boolean))) As JoinedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalOn(predicate, {GetEntityIndexHintsForEntities(index - 1, entityCount - 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateOnWithPredicateWithAllEntities(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds ON clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [On](<DisallowNull> predicate As Expression(Of Func(Of {generics}, Boolean))) As JoinedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalOn(predicate, {GetEntityIndexHintsForAllEntities(entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateOnWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds ON clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [On](<DisallowNull> predicate As Expression(Of Func(Of Join(Of {generics}), Boolean))) As JoinedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalOn(predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalOn(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds ON clause."
      Dim params = {"predicate", "entityIndexHints"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalOn(predicate As Expression, entityIndexHints As Int32()) As JoinedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Me.Builder.AddOn(Of {generic})(predicate, entityIndexHints)")
      builder.Indent().AppendLine($"Return New JoinedSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace