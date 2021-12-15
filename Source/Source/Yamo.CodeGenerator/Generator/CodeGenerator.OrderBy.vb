Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateOrderBy(builder As CodeBuilder, entityCount As Int32)
      For i = 1 To entityCount
        GenerateOrderByWithPredicateWithOneEntity(builder, i, entityCount)
        builder.AppendLine()
      Next

      If 1 < entityCount Then
        GenerateOrderByWithPredicateWithIJoin(builder, entityCount)
        builder.AppendLine()
      End If

      For i = 1 To entityCount
        GenerateOrderByDescendingWithPredicateWithOneEntity(builder, i, entityCount)
        builder.AppendLine()
      Next

      If 1 < entityCount Then
        GenerateOrderByDescendingWithPredicateWithIJoin(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateInternalOrderBy(builder, entityCount)
    End Sub

    Protected Sub GenerateOrderByWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds ORDER BY clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of {generic}, TKey))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()

      If entityCount = 1 Then
        builder.Indent().AppendLine($"Return InternalOrderBy(Of TKey)(keySelector, True)").PopIndent()
      Else
        builder.Indent().AppendLine($"Return InternalOrderBy(Of TKey)(keySelector, {GetEntityIndexHintsForEntity(index - 1)}, True)").PopIndent()
      End If

      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateOrderByWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds ORDER BY clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of {generics}), TKey))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalOrderBy(Of TKey)(keySelector, Nothing, True)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateOrderByDescendingWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds ORDER BY DESC clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of {generic}, TKey))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()

      If entityCount = 1 Then
        builder.Indent().AppendLine($"Return InternalOrderBy(Of TKey)(keySelector, False)").PopIndent()
      Else
        builder.Indent().AppendLine($"Return InternalOrderBy(Of TKey)(keySelector, {GetEntityIndexHintsForEntity(index - 1)}, False)").PopIndent()
      End If

      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateOrderByDescendingWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds ORDER BY DESC clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of Join(Of {generics}), TKey))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalOrderBy(Of TKey)(keySelector, Nothing, False)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalOrderBy(builder As CodeBuilder, entityCount As Int32)
      If entityCount = 1 Then
        Dim comment = "Adds ORDER BY clause."
        Dim typeParams = {"TKey"}
        Dim params = {"keySelector", "ascending"}
        AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

        Dim generics = String.Join(", ", GetGenericNames(entityCount))

        builder.Indent().AppendLine($"Private Function InternalOrderBy(Of TKey)(keySelector As Expression(Of Func(Of {generics}, TKey)), ascending As Boolean) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
        builder.Indent().AppendLine("Me.Builder.AddOrderBy(keySelector, {0}, ascending)")
        builder.Indent().AppendLine($"Return New OrderedSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
        builder.Indent().AppendLine("End Function")
      Else
        Dim comment = "Adds ORDER BY clause."
        Dim typeParams = {"TKey"}
        Dim params = {"keySelector", "entityIndexHints", "ascending"}
        AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

        Dim generics = String.Join(", ", GetGenericNames(entityCount))

        builder.Indent().AppendLine($"Private Function InternalOrderBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
        builder.Indent().AppendLine("Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)")
        builder.Indent().AppendLine($"Return New OrderedSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
        builder.Indent().AppendLine("End Function")
      End If
    End Sub

  End Class
End Namespace