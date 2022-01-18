Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateInclude(builder As CodeBuilder, entityCount As Int32)
      For i = 1 To entityCount
        GenerateIncludeWithActionWithOneEntity(builder, i, entityCount)
        builder.AppendLine()
      Next

      For i = 1 To entityCount
        For j = 1 To entityCount
          GenerateIncludeWithKeyValueSelectorsWithOneEntity(builder, i, j, entityCount)
          builder.AppendLine()
        Next
      Next

      If 1 < entityCount Then
        GenerateIncludeWithActionWithIJoin(builder, entityCount)
        builder.AppendLine()

        GenerateIncludeWithKeyValueSelectorsWithIJoin(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateInternalIncludeWithAction(builder, entityCount)
      builder.AppendLine()

      GenerateInternalIncludeWithKeyValueSelectors(builder, entityCount)
    End Sub

    Protected Sub GenerateIncludeWithActionWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Includes &lt;column(s)&gt; to SELECT clause."
      Dim params = {"action"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Include(<DisallowNull> action As Expression(Of Action(Of {generic}))) As SelectedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalInclude(action, {GetEntityIndexHintsForEntity(index - 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateIncludeWithKeyValueSelectorsWithOneEntity(builder As CodeBuilder, index1 As Int32, index2 As Int32, entityCount As Int32)
      Dim comment = "Includes &lt;column(s)&gt; to SELECT clause."
      Dim typeParams = {"TProperty"}
      Dim params = {"keySelector", "valueSelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic1 = GetGenericName(index1, index1 = entityCount)
      Dim generic2 = GetGenericName(index2, index2 = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of {generic1}, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of {generic2}, TProperty))) As SelectedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalInclude(keySelector, valueSelector, {GetEntityIndexHintsForEntity(index1 - 1)}, {GetEntityIndexHintsForEntity(index2 - 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateIncludeWithActionWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Includes &lt;column(s)&gt; to SELECT clause."
      Dim params = {"action"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Include(<DisallowNull> action As Expression(Of Action(Of Join(Of {generics})))) As SelectedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalInclude(action, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateIncludeWithKeyValueSelectorsWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Includes &lt;column(s)&gt; to SELECT clause."
      Dim typeParams = {"TProperty"}
      Dim params = {"keySelector", "valueSelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of {generics}), TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of Join(Of {generics}), TProperty))) As SelectedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalInclude(keySelector, valueSelector, Nothing, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalIncludeWithAction(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Includes &lt;column(s)&gt; to SELECT clause."
      Dim params = {"action", "entityIndexHints"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalInclude(action As Expression, entityIndexHints As Int32()) As SelectedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.IncludeToSelected(action, entityIndexHints)")
      builder.Indent().AppendLine("Return Me").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalIncludeWithKeyValueSelectors(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Includes &lt;column(s)&gt; to SELECT clause."
      Dim params = {"keySelector", "valueSelector", "keySelectorEntityIndexHints", "valueSelectorEntityIndexHints"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalInclude(keySelector As Expression, valueSelector As Expression, keySelectorEntityIndexHints As Int32(), valueSelectorEntityIndexHints As Int32()) As SelectedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.IncludeToSelected(keySelector, valueSelector, keySelectorEntityIndexHints, valueSelectorEntityIndexHints)")
      builder.Indent().AppendLine("Return Me").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace