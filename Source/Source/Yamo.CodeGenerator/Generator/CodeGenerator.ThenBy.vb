Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateThenBy(builder As CodeBuilder, entityCount As Int32)
      Dim limit = 8

      If entityCount < limit Then
        For i = 1 To entityCount
          GenerateThenByWithPredicateWithOneEntity(builder, i, entityCount)
          builder.AppendLine()
        Next
      End If

      If 1 < entityCount Then
        GenerateThenByWithPredicateWithIJoin(builder, entityCount)
        builder.AppendLine()
      End If

      If entityCount < limit Then
        For i = 1 To entityCount
          GenerateThenByDescendingWithPredicateWithOneEntity(builder, i, entityCount)
          builder.AppendLine()
        Next
      End If

      If 1 < entityCount Then
        GenerateThenByDescendingWithPredicateWithIJoin(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateInternalOrderByForThenBy(builder, entityCount)
    End Sub

    Protected Sub GenerateThenByWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds &lt;column&gt; to ORDER BY clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of {generic}, TKey))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()

      If entityCount = 1 Then
        builder.Indent().AppendLine($"Return InternalOrderBy(Of TKey)(keySelector, True)").PopIndent()
      Else
        builder.Indent().AppendLine($"Return InternalOrderBy(Of TKey)(keySelector, {GetEntityIndexHintsForEntity(index - 1)}, True)").PopIndent()
      End If

      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateThenByWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds &lt;column&gt; to ORDER BY clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of {generics}), TKey))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalOrderBy(Of TKey)(keySelector, Nothing, True)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateThenByDescendingWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds &lt;column&gt; DESC to ORDER BY clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of {generic}, TKey))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()

      If entityCount = 1 Then
        builder.Indent().AppendLine($"Return InternalOrderBy(Of TKey)(keySelector, False)").PopIndent()
      Else
        builder.Indent().AppendLine($"Return InternalOrderBy(Of TKey)(keySelector, {GetEntityIndexHintsForEntity(index - 1)}, False)").PopIndent()
      End If

      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateThenByDescendingWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds &lt;column&gt; DESC to ORDER BY clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of Join(Of {generics}), TKey))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalOrderBy(Of TKey)(keySelector, Nothing, False)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalOrderByForThenBy(builder As CodeBuilder, entityCount As Int32)
      ' TODO: SIP - simplify?
      If entityCount = 1 Then
        Dim comment = "Adds ORDER BY clause."
        Dim typeParams = {"TKey"}
        Dim params = {"keySelector", "ascending"}
        AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

        Dim generics = String.Join(", ", GetGenericNames(entityCount))

        builder.Indent().AppendLine($"Private Function InternalOrderBy(Of TKey)(keySelector As Expression(Of Func(Of {generics}, TKey)), ascending As Boolean) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
        builder.Indent().AppendLine("Me.Builder.AddOrderBy(keySelector, {0}, ascending)")
        builder.Indent().AppendLine("Return Me").PopIndent()
        builder.Indent().AppendLine("End Function")
      Else
        Dim comment = "Adds ORDER BY clause."
        Dim typeParams = {"TKey"}
        Dim params = {"keySelector", "entityIndexHints", "ascending"}
        AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

        Dim generics = String.Join(", ", GetGenericNames(entityCount))

        builder.Indent().AppendLine($"Private Function InternalOrderBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
        builder.Indent().AppendLine("Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)")
        builder.Indent().AppendLine("Return Me").PopIndent()
        builder.Indent().AppendLine("End Function")
      End If
    End Sub

  End Class
End Namespace