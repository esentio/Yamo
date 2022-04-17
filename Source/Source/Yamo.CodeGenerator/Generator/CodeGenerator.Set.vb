Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateSet(builder As CodeBuilder, entityCount As Int32)
      GenerateUnionWithSubquery(builder, entityCount)
      builder.AppendLine()

      GenerateUnionWithFormattableString(builder, entityCount)
      builder.AppendLine()

      GenerateUnionWithRawSqlString(builder, entityCount)
      builder.AppendLine()

      GenerateUnionAllWithSubquery(builder, entityCount)
      builder.AppendLine()

      GenerateUnionAllWithFormattableString(builder, entityCount)
      builder.AppendLine()

      GenerateUnionAllWithRawSqlString(builder, entityCount)
      builder.AppendLine()

      GenerateExceptWithSubquery(builder, entityCount)
      builder.AppendLine()

      GenerateExceptWithFormattableString(builder, entityCount)
      builder.AppendLine()

      GenerateExceptWithRawSqlString(builder, entityCount)
      builder.AppendLine()

      GenerateIntersectWithSubquery(builder, entityCount)
      builder.AppendLine()

      GenerateIntersectWithFormattableString(builder, entityCount)
      builder.AppendLine()

      GenerateIntersectWithRawSqlString(builder, entityCount)
      builder.AppendLine()

      GenerateInternalSetWithSubquery(builder, entityCount)
      builder.AppendLine()

      GenerateInternalSetWithFormattableString(builder, entityCount)
      builder.AppendLine()

      GenerateInternalSetWithRawSqlString(builder, entityCount)
    End Sub

    Protected Sub GenerateCustomSet(builder As CodeBuilder, entityCount As Int32)
      GenerateCustomUnionWithSubquery(builder, entityCount)
      builder.AppendLine()

      GenerateCustomUnionWithFormattableString(builder, entityCount)
      builder.AppendLine()

      GenerateCustomUnionWithRawSqlString(builder, entityCount)
      builder.AppendLine()

      GenerateCustomUnionAllWithSubquery(builder, entityCount)
      builder.AppendLine()

      GenerateCustomUnionAllWithFormattableString(builder, entityCount)
      builder.AppendLine()

      GenerateCustomUnionAllWithRawSqlString(builder, entityCount)
      builder.AppendLine()

      GenerateCustomExceptWithSubquery(builder, entityCount)
      builder.AppendLine()

      GenerateCustomExceptWithFormattableString(builder, entityCount)
      builder.AppendLine()

      GenerateCustomExceptWithRawSqlString(builder, entityCount)
      builder.AppendLine()

      GenerateCustomIntersectWithSubquery(builder, entityCount)
      builder.AppendLine()

      GenerateCustomIntersectWithFormattableString(builder, entityCount)
      builder.AppendLine()

      GenerateCustomIntersectWithRawSqlString(builder, entityCount)
      builder.AppendLine()

      GenerateCustomInternalSetWithSubquery(builder, entityCount)
      builder.AppendLine()

      GenerateCustomInternalSetWithFormattableString(builder, entityCount)
      builder.AppendLine()

      GenerateCustomInternalSetWithRawSqlString(builder, entityCount)
    End Sub

    Protected Sub GenerateUnionWithSubquery(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds UNION operator."
      Dim params = {"queryExpressionFactory"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Union(<DisallowNull> queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of {generic}))) As SetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.Union, queryExpressionFactory)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateUnionWithFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds UNION operator."
      Dim params = {"queryExpression"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Union(<DisallowNull> queryExpression As FormattableString) As SetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.Union, queryExpression)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateUnionWithRawSqlString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds UNION operator."
      Dim params = {"queryExpression", "parameters"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Union(<DisallowNull> queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As SetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.Union, queryExpression, parameters)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCustomUnionWithSubquery(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds UNION operator."
      Dim params = {"queryExpressionFactory"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Union(<DisallowNull> queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of {generic}))) As CustomSetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.Union, queryExpressionFactory)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCustomUnionWithFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds UNION operator."
      Dim params = {"queryExpression"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Union(<DisallowNull> queryExpression As FormattableString) As CustomSetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.Union, queryExpression)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCustomUnionWithRawSqlString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds UNION operator."
      Dim params = {"queryExpression", "parameters"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Union(<DisallowNull> queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As CustomSetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.Union, queryExpression, parameters)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateUnionAllWithSubquery(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds UNION ALL operator."
      Dim params = {"queryExpressionFactory"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function UnionAll(<DisallowNull> queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of {generic}))) As SetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.UnionAll, queryExpressionFactory)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateUnionAllWithFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds UNION ALL operator."
      Dim params = {"queryExpression"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function UnionAll(<DisallowNull> queryExpression As FormattableString) As SetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.UnionAll, queryExpression)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateUnionAllWithRawSqlString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds UNION ALL operator."
      Dim params = {"queryExpression", "parameters"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function UnionAll(<DisallowNull> queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As SetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.UnionAll, queryExpression, parameters)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCustomUnionAllWithSubquery(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds UNION ALL operator."
      Dim params = {"queryExpressionFactory"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function UnionAll(<DisallowNull> queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of {generic}))) As CustomSetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.UnionAll, queryExpressionFactory)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCustomUnionAllWithFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds UNION ALL operator."
      Dim params = {"queryExpression"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function UnionAll(<DisallowNull> queryExpression As FormattableString) As CustomSetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.UnionAll, queryExpression)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCustomUnionAllWithRawSqlString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds UNION ALL operator."
      Dim params = {"queryExpression", "parameters"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function UnionAll(<DisallowNull> queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As CustomSetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.UnionAll, queryExpression, parameters)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateExceptWithSubquery(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds EXCEPT operator."
      Dim params = {"queryExpressionFactory"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Except(<DisallowNull> queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of {generic}))) As SetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.Except, queryExpressionFactory)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateExceptWithFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds EXCEPT operator."
      Dim params = {"queryExpression"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Except(<DisallowNull> queryExpression As FormattableString) As SetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.Except, queryExpression)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateExceptWithRawSqlString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds EXCEPT operator."
      Dim params = {"queryExpression", "parameters"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Except(<DisallowNull> queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As SetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.Except, queryExpression, parameters)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCustomExceptWithSubquery(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds EXCEPT operator."
      Dim params = {"queryExpressionFactory"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Except(<DisallowNull> queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of {generic}))) As CustomSetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.Except, queryExpressionFactory)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCustomExceptWithFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds EXCEPT operator."
      Dim params = {"queryExpression"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Except(<DisallowNull> queryExpression As FormattableString) As CustomSetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.Except, queryExpression)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCustomExceptWithRawSqlString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds EXCEPT operator."
      Dim params = {"queryExpression", "parameters"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Except(<DisallowNull> queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As CustomSetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.Except, queryExpression, parameters)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateIntersectWithSubquery(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds INTERSECT operator."
      Dim params = {"queryExpressionFactory"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Intersect(<DisallowNull> queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of {generic}))) As SetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.Intersect, queryExpressionFactory)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateIntersectWithFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds INTERSECT operator."
      Dim params = {"queryExpression"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Intersect(<DisallowNull> queryExpression As FormattableString) As SetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.Intersect, queryExpression)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateIntersectWithRawSqlString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds INTERSECT operator."
      Dim params = {"queryExpression", "parameters"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Intersect(<DisallowNull> queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As SetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.Intersect, queryExpression, parameters)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCustomIntersectWithSubquery(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds INTERSECT operator."
      Dim params = {"queryExpressionFactory"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Intersect(<DisallowNull> queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of {generic}))) As CustomSetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.Intersect, queryExpressionFactory)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCustomIntersectWithFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds INTERSECT operator."
      Dim params = {"queryExpression"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Intersect(<DisallowNull> queryExpression As FormattableString) As CustomSetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.Intersect, queryExpression)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCustomIntersectWithRawSqlString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds INTERSECT operator."
      Dim params = {"queryExpression", "parameters"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Public Function Intersect(<DisallowNull> queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As CustomSetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine("Return InternalSet(SetOperator.Intersect, queryExpression, parameters)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalSetWithSubquery(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds set operator."
      Dim params = {"setOperator", "queryExpressionFactory"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Private Function InternalSet(setOperator As SetOperator, queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of {generic}))) As SetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine($"Me.Builder.AddSet(Of {generic})(Me.Executor, setOperator, queryExpressionFactory)")
      builder.Indent().AppendLine($"Return New SetSelectSqlExpression(Of {generic})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalSetWithFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds set operator."
      Dim params = {"setOperator", "queryExpression"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Private Function InternalSet(setOperator As SetOperator, queryExpression As FormattableString) As SetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine($"Me.Builder.AddSet(Of {generic})(setOperator, queryExpression)")
      builder.Indent().AppendLine($"Return New SetSelectSqlExpression(Of {generic})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalSetWithRawSqlString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds set operator."
      Dim params = {"setOperator", "queryExpression", "parameters"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Private Function InternalSet(setOperator As SetOperator, queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As SetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine($"Me.Builder.AddSet(Of {generic})(setOperator, queryExpression, parameters)")
      builder.Indent().AppendLine($"Return New SetSelectSqlExpression(Of {generic})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCustomInternalSetWithSubquery(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds set operator."
      Dim params = {"setOperator", "queryExpressionFactory"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Private Function InternalSet(setOperator As SetOperator, queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of {generic}))) As CustomSetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine($"Me.Builder.AddSet(Of {generic})(Me.Executor, setOperator, queryExpressionFactory)")
      builder.Indent().AppendLine($"Return New CustomSetSelectSqlExpression(Of {generic})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCustomInternalSetWithFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds set operator."
      Dim params = {"setOperator", "queryExpression"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Private Function InternalSet(setOperator As SetOperator, queryExpression As FormattableString) As CustomSetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine($"Me.Builder.AddSet(Of {generic})(setOperator, queryExpression)")
      builder.Indent().AppendLine($"Return New CustomSetSelectSqlExpression(Of {generic})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCustomInternalSetWithRawSqlString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds set operator."
      Dim params = {"setOperator", "queryExpression", "parameters"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)

      builder.Indent().AppendLine($"Private Function InternalSet(setOperator As SetOperator, queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As CustomSetSelectSqlExpression(Of {generic})").PushIndent()
      builder.Indent().AppendLine($"Me.Builder.AddSet(Of {generic})(setOperator, queryExpression, parameters)")
      builder.Indent().AppendLine($"Return New CustomSetSelectSqlExpression(Of {generic})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace