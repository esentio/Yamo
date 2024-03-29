﻿Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateThenBy(builder As CodeBuilder, entityCount As Int32)
      For i = 1 To entityCount
        GenerateThenByWithPredicateWithOneEntity(builder, i, entityCount)
        builder.AppendLine()

        GenerateThenByWithPredicateWithOneEntityReturningFormattableString(builder, i, entityCount)
        builder.AppendLine()
      Next

      If 1 < entityCount Then
        GenerateThenByWithPredicateWithIJoin(builder, entityCount)
        builder.AppendLine()

        GenerateThenByWithPredicateWithIJoinReturningFormattableString(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateThenByWithString(builder, entityCount)
      builder.AppendLine()

      If entityCount = 1 Then
        GenerateThenByWithProviderWithOneEntity(builder, entityCount)
        builder.AppendLine()
      Else
        GenerateThenByWithProviderWithIJoin(builder, entityCount)
        builder.AppendLine()
      End If

      For i = 1 To entityCount
        GenerateThenByDescendingWithPredicateWithOneEntity(builder, i, entityCount)
        builder.AppendLine()

        GenerateThenByDescendingWithPredicateWithOneEntityReturningFormattableString(builder, i, entityCount)
        builder.AppendLine()
      Next

      If 1 < entityCount Then
        GenerateThenByDescendingWithPredicateWithIJoin(builder, entityCount)
        builder.AppendLine()

        GenerateThenByDescendingWithPredicateWithIJoinReturningFormattableString(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateThenByDescendingWithString(builder, entityCount)
      builder.AppendLine()

      GenerateInternalOrderByForThenBy(builder, entityCount)
    End Sub

    Protected Sub GenerateThenByWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds &lt;column&gt; to ORDER BY clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function ThenBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of {generic}, TKey))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalOrderBy(keySelector, {GetEntityIndexHintsForEntity(index - 1)}, True)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateThenByWithPredicateWithOneEntityReturningFormattableString(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds &lt;column&gt; to ORDER BY clause."
      Dim params = {"keySelector"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function ThenBy(<DisallowNull> keySelector As Expression(Of Func(Of {generic}, FormattableString))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalOrderBy(keySelector, {GetEntityIndexHintsForEntity(index - 1)}, True)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateThenByWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds &lt;column&gt; to ORDER BY clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function ThenBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of {generics}), TKey))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalOrderBy(keySelector, Nothing, True)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateThenByWithPredicateWithIJoinReturningFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds &lt;column&gt; to ORDER BY clause."
      Dim params = {"keySelector"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function ThenBy(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of {generics}), FormattableString))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalOrderBy(keySelector, Nothing, True)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateThenByWithString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds &lt;column&gt; to ORDER BY clause."
      Dim params = {"predicate", "parameters"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function ThenBy(<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddOrderBy(predicate, True, parameters)")
      builder.Indent().AppendLine("Return Me").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateThenByWithProviderWithOneEntity(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds column(s) to ORDER BY clause."
      Dim params = {"provider"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function ThenBy(<DisallowNull> provider As ISelectSortProvider) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"provider.AddOrderBy(Of {generics})(Me.Builder)")
      builder.Indent().AppendLine("Return Me").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateThenByWithProviderWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds column(s) to ORDER BY clause."
      Dim params = {"provider"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function ThenBy(<DisallowNull> provider As ISelectSortProvider) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"provider.AddOrderBy(Of Join(Of {generics}))(Me.Builder)")
      builder.Indent().AppendLine("Return Me").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateThenByDescendingWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds &lt;column&gt; DESC to ORDER BY clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function ThenByDescending(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of {generic}, TKey))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalOrderBy(keySelector, {GetEntityIndexHintsForEntity(index - 1)}, False)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateThenByDescendingWithPredicateWithOneEntityReturningFormattableString(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds &lt;column&gt; DESC to ORDER BY clause."
      Dim params = {"keySelector"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function ThenByDescending(<DisallowNull> keySelector As Expression(Of Func(Of {generic}, FormattableString))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalOrderBy(keySelector, {GetEntityIndexHintsForEntity(index - 1)}, False)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateThenByDescendingWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds &lt;column&gt; DESC to ORDER BY clause."
      Dim typeParams = {"TKey"}
      Dim params = {"keySelector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function ThenByDescending(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of {generics}), TKey))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalOrderBy(keySelector, Nothing, False)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateThenByDescendingWithPredicateWithIJoinReturningFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds &lt;column&gt; DESC to ORDER BY clause."
      Dim params = {"keySelector"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function ThenByDescending(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of {generics}), FormattableString))) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalOrderBy(keySelector, Nothing, False)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateThenByDescendingWithString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds &lt;column&gt; DESC to ORDER BY clause."
      Dim params = {"predicate", "parameters"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function ThenByDescending(<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddOrderBy(predicate, False, parameters)")
      builder.Indent().AppendLine("Return Me").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalOrderByForThenBy(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds ORDER BY clause."
      Dim params = {"keySelector", "entityIndexHints", "ascending"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalOrderBy(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)")
      builder.Indent().AppendLine("Return Me").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace