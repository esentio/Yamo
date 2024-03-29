﻿Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateOrderBy(builder As CodeBuilder, entityCount As Int32)
      For i = 1 To entityCount
        GenerateOrderByWithPredicateWithOneEntity(builder, i, entityCount)
        builder.AppendLine()

        GenerateOrderByWithPredicateWithOneEntityReturningFormattableString(builder, i, entityCount)
        builder.AppendLine()
      Next

      If 1 < entityCount Then
        GenerateOrderByWithPredicateWithIJoin(builder, entityCount)
        builder.AppendLine()

        GenerateOrderByWithPredicateWithIJoinReturningFormattableString(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateOrderByWithString(builder, entityCount)
      builder.AppendLine()

      If entityCount = 1 Then
        GenerateOrderByWithProviderWithOneEntity(builder, entityCount)
        builder.AppendLine()
      Else
        GenerateOrderByWithProviderWithIJoin(builder, entityCount)
        builder.AppendLine()
      End If

      For i = 1 To entityCount
        GenerateOrderByDescendingWithPredicateWithOneEntity(builder, i, entityCount)
        builder.AppendLine()

        GenerateOrderByDescendingWithPredicateWithOneEntityReturningFormattableString(builder, i, entityCount)
        builder.AppendLine()
      Next

      If 1 < entityCount Then
        GenerateOrderByDescendingWithPredicateWithIJoin(builder, entityCount)
        builder.AppendLine()

        GenerateOrderByDescendingWithPredicateWithIJoinReturningFormattableString(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateOrderByDescendingWithString(builder, entityCount)
      builder.AppendLine()

      GenerateInternalOrderBy(builder, entityCount)
    End Sub

    Protected Sub GenerateOrderByWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds ORDER BY clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function OrderBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of {generic}, TKey))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalOrderBy(keySelector, {GetEntityIndexHintsForEntity(index - 1)}, True)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateOrderByWithPredicateWithOneEntityReturningFormattableString(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds ORDER BY clause."
      Dim params = {"keySelector"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function OrderBy(<DisallowNull> keySelector As Expression(Of Func(Of {generic}, FormattableString))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalOrderBy(keySelector, {GetEntityIndexHintsForEntity(index - 1)}, True)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateOrderByWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds ORDER BY clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function OrderBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of {generics}), TKey))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalOrderBy(keySelector, Nothing, True)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateOrderByWithPredicateWithIJoinReturningFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds ORDER BY clause."
      Dim params = {"keySelector"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function OrderBy(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of {generics}), FormattableString))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalOrderBy(keySelector, Nothing, True)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateOrderByWithString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds ORDER BY clause."
      Dim params = {"predicate", "parameters"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function OrderBy(<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddOrderBy(predicate, True, parameters)")
      builder.Indent().AppendLine($"Return New OrderedSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateOrderByWithProviderWithOneEntity(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds ORDER BY clause."
      Dim params = {"provider"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function OrderBy(<DisallowNull> provider As ISelectSortProvider) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"provider.AddOrderBy(Of {generics})(Me.Builder)")
      builder.Indent().AppendLine($"Return New OrderedSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateOrderByWithProviderWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds ORDER BY clause."
      Dim params = {"provider"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function OrderBy(<DisallowNull> provider As ISelectSortProvider) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"provider.AddOrderBy(Of Join(Of {generics}))(Me.Builder)")
      builder.Indent().AppendLine($"Return New OrderedSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateOrderByDescendingWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds ORDER BY DESC clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function OrderByDescending(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of {generic}, TKey))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalOrderBy(keySelector, {GetEntityIndexHintsForEntity(index - 1)}, False)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateOrderByDescendingWithPredicateWithOneEntityReturningFormattableString(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds ORDER BY DESC clause."
      Dim params = {"keySelector"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function OrderByDescending(<DisallowNull> keySelector As Expression(Of Func(Of {generic}, FormattableString))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalOrderBy(keySelector, {GetEntityIndexHintsForEntity(index - 1)}, False)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateOrderByDescendingWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds ORDER BY DESC clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function OrderByDescending(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of {generics}), TKey))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalOrderBy(keySelector, Nothing, False)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateOrderByDescendingWithPredicateWithIJoinReturningFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds ORDER BY DESC clause."
      Dim params = {"keySelector"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function OrderByDescending(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of {generics}), FormattableString))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalOrderBy(keySelector, Nothing, False)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateOrderByDescendingWithString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds ORDER BY DESC clause."
      Dim params = {"predicate", "parameters"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function OrderByDescending(<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddOrderBy(predicate, False, parameters)")
      builder.Indent().AppendLine($"Return New OrderedSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalOrderBy(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds ORDER BY clause."
      Dim params = {"keySelector", "entityIndexHints", "ascending"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalOrderBy(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)")
      builder.Indent().AppendLine($"Return New OrderedSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace