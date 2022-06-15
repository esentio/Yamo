Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateWhere(builder As CodeBuilder, entityCount As Int32)
      For i = 1 To entityCount
        GenerateWhereWithPredicateWithOneEntity(builder, i, entityCount)
        builder.AppendLine()

        GenerateWhereWithPredicateWithOneEntityReturningFormattableString(builder, i, entityCount)
        builder.AppendLine()
      Next

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

      If entityCount = 1 Then
        GenerateWhereWithProviderWithOneEntity(builder, entityCount)
        builder.AppendLine()
      Else
        GenerateWhereWithProviderWithIJoin(builder, entityCount)
        builder.AppendLine()
      End If

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

      builder.Indent().AppendLine($"Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of {generic}, Boolean))) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalWhere(predicate, {GetEntityIndexHintsForEntity(index - 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereWithPredicateWithOneEntityReturningFormattableString(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds WHERE clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of {generic}, FormattableString))) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalWhere(predicate, {GetEntityIndexHintsForEntity(index - 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereWithPredicateWithAllEntities(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds WHERE clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of {generics}, Boolean))) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalWhere(predicate, {GetEntityIndexHintsForAllEntities(entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereWithPredicateWithAllEntitiesReturningFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds WHERE clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of {generics}, FormattableString))) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalWhere(predicate, {GetEntityIndexHintsForAllEntities(entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds WHERE clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of Join(Of {generics}), Boolean))) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalWhere(predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereWithPredicateWithIJoinReturningFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds WHERE clause."
      Dim params = {"predicate"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of Join(Of {generics}), FormattableString))) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalWhere(predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereWithString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds WHERE clause."
      Dim params = {"predicate", "parameters"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Where(<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddWhere(predicate, parameters)")
      builder.Indent().AppendLine($"Return New FilteredSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereWithProviderWithOneEntity(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds WHERE clause."
      Dim params = {"provider"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Where(<DisallowNull> provider As ISelectFilterProvider) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"provider.AddWhere(Of {generics})(Me.Builder)")
      builder.Indent().AppendLine($"Return New FilteredSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateWhereWithProviderWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds WHERE clause."
      Dim params = {"provider"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Where(<DisallowNull> provider As ISelectFilterProvider) As FilteredSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"provider.AddWhere(Of Join(Of {generics}))(Me.Builder)")
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