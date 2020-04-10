Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateGroupBy(builder As CodeBuilder, entityCount As Int32)
      Dim limit = 8

      If entityCount < limit Then
        For i = 1 To entityCount
          GenerateGroupByWithPredicateWithOneEntity(builder, i, entityCount)
          builder.AppendLine()
        Next
      End If

      If 1 < entityCount Then
        If entityCount < 8 Then
          GenerateGroupByWithPredicateWithAllEntities(builder, entityCount)
          builder.AppendLine()
        End If

        GenerateGroupByWithPredicateWithIJoin(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateInternalGroupBy(builder, entityCount)
    End Sub

    Protected Sub GenerateGroupByWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds GROUP BY clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function GroupBy(Of TKey)(keySelector As Expression(Of Func(Of {generic}, TKey))) As GroupedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalGroupBy(Of TKey)(keySelector, {GetEntityIndexHintsForEntity(index - 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateGroupByWithPredicateWithAllEntities(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds GROUP BY clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function GroupBy(Of TKey)(keySelector As Expression(Of Func(Of {generics}, TKey))) As GroupedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalGroupBy(Of TKey)(keySelector, {GetEntityIndexHintsForAllEntities(entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateGroupByWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds GROUP BY clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function GroupBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of {generics}), TKey))) As GroupedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalGroupBy(Of TKey)(keySelector, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalGroupBy(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds GROUP BY clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector", "entityIndexHints"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalGroupBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32()) As GroupedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddGroupBy(keySelector, entityIndexHints)")
      builder.Indent().AppendLine($"Return New GroupedSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace