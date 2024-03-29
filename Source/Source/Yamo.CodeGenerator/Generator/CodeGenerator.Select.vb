﻿Namespace Generator

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
      If entityCount = 1 Then
        Dim comment = "Adds SELECT clause with all columns of the table (entity)."
        AddComment(builder, comment, returns:="")

        Dim generics = String.Join(", ", GetGenericNames(entityCount))

        builder.Indent().AppendLine($"Public Function SelectAll() As SelectedSelectSqlExpression(Of {generics})").PushIndent()
        builder.Indent().AppendLine("Me.Builder.AddSelectAll(SelectColumnsBehavior.ExcludeNonRequiredColumns)")
        builder.Indent().AppendLine($"Return New SelectedSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
        builder.Indent().AppendLine("End Function")
      Else
        Dim comment = "Adds SELECT clause with all columns of the tables (entities) used in the query. Behavior parameter controls whether columns of all or only required tables are included."
        Dim params = {"behavior"}
        AddComment(builder, comment, params:=params, returns:="")

        Dim generics = String.Join(", ", GetGenericNames(entityCount))

        builder.Indent().AppendLine($"Public Function SelectAll(Optional behavior As SelectColumnsBehavior = SelectColumnsBehavior.ExcludeNonRequiredColumns) As SelectedSelectSqlExpression(Of {generics})").PushIndent()
        builder.Indent().AppendLine("Me.Builder.AddSelectAll(behavior)")
        builder.Indent().AppendLine($"Return New SelectedSelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
        builder.Indent().AppendLine("End Function")
      End If
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
      Dim params = {"selector", "behavior"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)

      builder.Indent().AppendLine($"Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of {generic}, TResult)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As CustomSelectSqlExpression(Of TResult)").PushIndent()
      builder.Indent().AppendLine($"Return InternalSelect(Of TResult)(selector, {GetEntityIndexHintsForEntity(index - 1)}, behavior)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateSelectWithPredicateWithAllEntities(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds SELECT clause with custom columns selection."
      Dim typeParams = {"TResult"}
      Dim params = {"selector", "behavior"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of {generics}, TResult)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As CustomSelectSqlExpression(Of TResult)").PushIndent()
      builder.Indent().AppendLine($"Return InternalSelect(Of TResult)(selector, {GetEntityIndexHintsForAllEntities(entityCount)}, behavior)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateSelectWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds SELECT clause with custom columns selection."
      Dim typeParams = {"TResult"}
      Dim params = {"selector", "behavior"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of Join(Of {generics}), TResult)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As CustomSelectSqlExpression(Of TResult)").PushIndent()
      builder.Indent().AppendLine("Return InternalSelect(Of TResult)(selector, Nothing, behavior)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalSelect(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds SELECT clause with custom columns selection."
      Dim typeParams = {"TResult"}
      Dim params = {"selector", "entityIndexHints", "behavior"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      builder.Indent().AppendLine("Private Function InternalSelect(Of TResult)(selector As Expression, entityIndexHints As Int32(), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As CustomSelectSqlExpression(Of TResult)").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddSelect(selector, entityIndexHints, behavior)")
      builder.Indent().AppendLine("Return New CustomSelectSqlExpression(Of TResult)(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace