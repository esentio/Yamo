Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateWhereAnd(builder As CodeBuilder, entityCount As Int32)
      For i = 1 To entityCount
        GenerateWhereAndWithPredicateWithOneEntity(builder, i, entityCount)
        builder.AppendLine()

        GenerateWhereAndWithPredicateWithOneEntityReturningFormattableString(builder, i, entityCount)
        builder.AppendLine()
      Next

      If 1 < entityCount Then
        If entityCount < 4 Then
          GenerateWhereAndWithPredicateWithAllEntities(builder, entityCount)
          builder.AppendLine()

          GenerateWhereAndWithPredicateWithAllEntitiesReturningFormattableString(builder, entityCount)
          builder.AppendLine()
        End If

        GenerateWhereAndWithPredicateWithIJoin(builder, entityCount)
        builder.AppendLine()

        GenerateWhereAndWithPredicateWithIJoinReturningFormattableString(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateWhereAndWithString(builder, entityCount)
      builder.AppendLine()

      If entityCount = 1 Then
        GenerateWhereAndWithProviderWithOneEntity(builder, entityCount)
        builder.AppendLine()
      Else
        GenerateWhereAndWithProviderWithIJoin(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateInternalWhereForWhereAnd(builder, entityCount)
    End Sub

    Protected Sub GenerateWhereAndWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds AND condition to WHERE clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [And](<DisallowNull> predicate As Expression(Of Func(Of {generic}, Boolean))) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalWhere(predicate, {GetEntityIndexHintsForEntity(index - 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereAndWithPredicateWithOneEntityReturningFormattableString(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds AND condition to WHERE clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [And](<DisallowNull> predicate As Expression(Of Func(Of {generic}, FormattableString))) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalWhere(predicate, {GetEntityIndexHintsForEntity(index - 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereAndWithPredicateWithAllEntities(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds AND condition to WHERE clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [And](<DisallowNull> predicate As Expression(Of Func(Of {generics}, Boolean))) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalWhere(predicate, {GetEntityIndexHintsForAllEntities(entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereAndWithPredicateWithAllEntitiesReturningFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds AND condition to WHERE clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [And](<DisallowNull> predicate As Expression(Of Func(Of {generics}, FormattableString))) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalWhere(predicate, {GetEntityIndexHintsForAllEntities(entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereAndWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds AND condition to WHERE clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [And](<DisallowNull> predicate As Expression(Of Func(Of Join(Of {generics}), Boolean))) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalWhere(predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereAndWithPredicateWithIJoinReturningFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds AND condition to WHERE clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [And](<DisallowNull> predicate As Expression(Of Func(Of Join(Of {generics}), FormattableString))) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalWhere(predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereAndWithString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds AND condition to WHERE clause."
      Dim params = {"predicate", "parameters"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [And](<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddWhere(predicate, parameters)")
      builder.Indent().AppendLine("Return Me").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereAndWithProviderWithOneEntity(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds AND condition(s) to WHERE clause."
      Dim params = {"provider"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [And](<DisallowNull> provider As ISelectFilterProvider) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"provider.AddWhere(Of {generics})(Me.Builder)")
      builder.Indent().AppendLine("Return Me").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereAndWithProviderWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds AND condition(s) to WHERE clause."
      Dim params = {"provider"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [And](<DisallowNull> provider As ISelectFilterProvider) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"provider.AddWhere(Of Join(Of {generics}))(Me.Builder)")
      builder.Indent().AppendLine("Return Me").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalWhereForWhereAnd(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds WHERE clause."
      Dim params = {"predicate", "entityIndexHints"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalWhere(predicate As Expression, entityIndexHints As Int32()) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddWhere(predicate, entityIndexHints)")
      builder.Indent().AppendLine("Return Me").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace