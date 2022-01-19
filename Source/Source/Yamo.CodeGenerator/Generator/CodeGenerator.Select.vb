Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateSelect(builder As CodeBuilder, entityCount As Int32)
      GenerateSelectAll(builder, entityCount)
      builder.AppendLine()

      GenerateSelectCount(builder, entityCount)
      builder.AppendLine()

      For i = 1 To entityCount
        GenerateSelectWithPredicateWithOneEntity(builder, i, entityCount)
        builder.AppendLine()
      Next

      If 1 < entityCount Then
        If entityCount < 8 Then
          GenerateSelectWithPredicateWithAllEntities(builder, entityCount)
          builder.AppendLine()
        End If

        GenerateSelectWithPredicateWithIJoin(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateInternalSelect(builder, entityCount)
    End Sub

    Protected Sub GenerateSelectAll(builder As CodeBuilder, entityCount As Int32)
      Dim comment = If(entityCount = 1, "Adds SELECT clause with all columns of the table (entity).", "Adds SELECT clause with all columns of all tables (entities).")
      AddComment(builder, comment, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))
      Dim genericTypes = String.Join(", ", GetGenericNames(entityCount).Select(Function(x) $"GetType({x})"))

      builder.Indent().AppendLine($"Public Function SelectAll() As SelectedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Me.Builder.AddSelectAll({genericTypes})")
      builder.Indent().AppendLine($"Return New SelectedSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateSelectCount(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds SELECT COUNT(*) clause, executes SQL query and returns the result."
      AddComment(builder, comment, returns:="")

      builder.Indent().AppendLine("Public Function SelectCount() As Int32").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddSelectCount()")
      builder.Indent().AppendLine("Dim query = Me.Builder.CreateQuery()")
      builder.Indent().AppendLine("Return Me.Executor.QueryFirstOrDefault(Of Int32)(query)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateSelectWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds SELECT clause with custom columns selection."
      Dim typeParams = {"TResult"}
      Dim params = {"selector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)

      builder.Indent().AppendLine($"Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of {generic}, TResult))) As CustomSelectSqlExpression(Of TResult)").PushIndent()
      builder.Indent().AppendLine($"Return InternalSelect(Of TResult)(selector, {GetEntityIndexHintsForEntity(index - 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateSelectWithPredicateWithAllEntities(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds SELECT clause with custom columns selection."
      Dim typeParams = {"TResult"}
      Dim params = {"selector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of {generics}, TResult))) As CustomSelectSqlExpression(Of TResult)").PushIndent()
      builder.Indent().AppendLine($"Return InternalSelect(Of TResult)(selector, {GetEntityIndexHintsForAllEntities(entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateSelectWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds SELECT clause with custom columns selection."
      Dim typeParams = {"TResult"}
      Dim params = {"selector"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of Join(Of {generics}), TResult))) As CustomSelectSqlExpression(Of TResult)").PushIndent()
      builder.Indent().AppendLine("Return InternalSelect(Of TResult)(selector, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalSelect(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds SELECT clause with custom columns selection."
      Dim typeParams = {"TResult"}
      Dim params = {"selector", "entityIndexHints"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      builder.Indent().AppendLine("Private Function InternalSelect(Of TResult)(selector As Expression, entityIndexHints As Int32()) As CustomSelectSqlExpression(Of TResult)").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddSelect(selector, entityIndexHints)")
      builder.Indent().AppendLine("Return New CustomSelectSqlExpression(Of TResult)(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace