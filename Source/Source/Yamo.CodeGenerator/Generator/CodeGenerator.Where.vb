Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateWhere(builder As CodeBuilder, entityCount As Int32)
      Dim limit = 8

      If entityCount < limit Then
        For i = 1 To entityCount
          GenerateWhereWithPredicateWithOneEntity(builder, i, entityCount)
          builder.AppendLine()

          GenerateWhereWithPredicateWithOneEntityReturningFormattableString(builder, i, entityCount)
          builder.AppendLine()
        Next
      End If

      If 1 < entityCount Then
        If entityCount < 4 Then
          GenerateWhereWithPredicateWithAllEntities(builder, entityCount)
          builder.AppendLine()

          GenerateWhereWithPredicateWithAllEntitiesReturningFormattableString(builder, entityCount)
          builder.AppendLine()
        End If

        GenerateWhereWithPredicateWithIJoin(builder, entityCount)
        builder.AppendLine()

        GenerateWhereWithPredicateWithIJoinReturningFormattableString(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateWhereWithString(builder, entityCount)
      builder.AppendLine()

      GenerateParameterlessWhere(builder, entityCount)
      builder.AppendLine()

      GenerateInternalWhere(builder, entityCount)
    End Sub

    Protected Sub GenerateWhereWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds WHERE clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Where(predicate As Expression(Of Func(Of {generic}, Boolean))) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalWhere(predicate, {GetEntityIndexHintsForEntity(index - 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereWithPredicateWithOneEntityReturningFormattableString(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds WHERE clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Where(predicate As Expression(Of Func(Of {generic}, FormattableString))) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalWhere(predicate, {GetEntityIndexHintsForEntity(index - 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereWithPredicateWithAllEntities(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds WHERE clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Where(predicate As Expression(Of Func(Of {generics}, Boolean))) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalWhere(predicate, {GetEntityIndexHintsForAllEntities(entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereWithPredicateWithAllEntitiesReturningFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds WHERE clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Where(predicate As Expression(Of Func(Of {generics}, FormattableString))) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalWhere(predicate, {GetEntityIndexHintsForAllEntities(entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds WHERE clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Where(predicate As Expression(Of Func(Of Join(Of {generics}), Boolean))) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalWhere(predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereWithPredicateWithIJoinReturningFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds WHERE clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Where(predicate As Expression(Of Func(Of Join(Of {generics}), FormattableString))) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalWhere(predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereWithString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds WHERE clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Where(predicate As String) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddWhere(predicate)")
      builder.Indent().AppendLine($"Return New FilteredSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateParameterlessWhere(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds WHERE clause."
      AddComment(builder, comment, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Where() As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return New FilteredSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalWhere(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds WHERE clause."
      Dim params = {"predicate", "entityIndexHints"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalWhere(predicate As Expression, entityIndexHints As Int32()) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddWhere(predicate, entityIndexHints)")
      builder.Indent().AppendLine($"Return New FilteredSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace