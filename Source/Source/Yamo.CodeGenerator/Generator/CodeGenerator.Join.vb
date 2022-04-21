Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateJoin(builder As CodeBuilder, entityCount As Int32)
      Dim limit = 7

      If entityCount < limit Then
        For i = 1 To entityCount
          GenerateJoinWithPredicateWithOneEntity(builder, i, entityCount)
          builder.AppendLine()
        Next
      End If

      If entityCount = 2 Then
        GenerateJoinWithPredicateWithAllEntities(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateJoinWithPredicateWithIJoin(builder, entityCount)
      builder.AppendLine()

      GenerateJoinWithNoParameters(builder, entityCount)
      builder.AppendLine()

      GenerateJoinWithSubquery(builder, entityCount)
      builder.AppendLine()

      GenerateJoinWithFormattableString(builder, entityCount)
      builder.AppendLine()

      GenerateJoinWithRawSqlString(builder, entityCount)
      builder.AppendLine()

      If entityCount < limit Then
        For i = 1 To entityCount
          GenerateLeftJoinWithPredicateWithOneEntity(builder, i, entityCount)
          builder.AppendLine()
        Next
      End If

      If entityCount = 2 Then
        GenerateLeftJoinWithPredicateWithAllEntities(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateLeftJoinWithPredicateWithIJoin(builder, entityCount)
      builder.AppendLine()

      GenerateLeftJoinWithNoParameters(builder, entityCount)
      builder.AppendLine()

      GenerateLeftJoinWithSubquery(builder, entityCount)
      builder.AppendLine()

      GenerateLeftJoinWithFormattableString(builder, entityCount)
      builder.AppendLine()

      GenerateLeftJoinWithRawSqlString(builder, entityCount)
      builder.AppendLine()

      If entityCount < limit Then
        For i = 1 To entityCount
          GenerateRightJoinWithPredicateWithOneEntity(builder, i, entityCount)
          builder.AppendLine()
        Next
      End If

      If entityCount = 2 Then
        GenerateRightJoinWithPredicateWithAllEntities(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateRightJoinWithPredicateWithIJoin(builder, entityCount)
      builder.AppendLine()

      GenerateRightJoinWithNoParameters(builder, entityCount)
      builder.AppendLine()

      GenerateRightJoinWithSubquery(builder, entityCount)
      builder.AppendLine()

      GenerateRightJoinWithFormattableString(builder, entityCount)
      builder.AppendLine()

      GenerateRightJoinWithRawSqlString(builder, entityCount)
      builder.AppendLine()

      If entityCount < limit Then
        For i = 1 To entityCount
          GenerateFullJoinWithPredicateWithOneEntity(builder, i, entityCount)
          builder.AppendLine()
        Next
      End If

      If entityCount = 2 Then
        GenerateFullJoinWithPredicateWithAllEntities(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateFullJoinWithPredicateWithIJoin(builder, entityCount)
      builder.AppendLine()

      GenerateFullJoinWithNoParameters(builder, entityCount)
      builder.AppendLine()

      GenerateFullJoinWithSubquery(builder, entityCount)
      builder.AppendLine()

      GenerateFullJoinWithFormattableString(builder, entityCount)
      builder.AppendLine()

      GenerateFullJoinWithRawSqlString(builder, entityCount)
      builder.AppendLine()

      GenerateCrossJoin(builder, entityCount)
      builder.AppendLine()

      GenerateCrossJoinWithSubquery(builder, entityCount)
      builder.AppendLine()

      GenerateCrossJoinWithFormattableString(builder, entityCount)
      builder.AppendLine()

      GenerateCrossJoinWithRawSqlString(builder, entityCount)
      builder.AppendLine()

      GenerateInternalJoinWithPredicate(builder, entityCount)
      builder.AppendLine()

      GenerateInternalJoin(builder, entityCount)
      builder.AppendLine()

      GenerateInternalJoinWithSubquery(builder, entityCount)
      builder.AppendLine()

      GenerateInternalJoinWithFormattableString(builder, entityCount)
      builder.AppendLine()

      GenerateInternalJoinWithRawSqlString(builder, entityCount)
    End Sub

    Protected Sub GenerateJoinWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds INNER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Join(Of TJoined)(<DisallowNull> predicate As Expression(Of Func(Of {generic}, TJoined, Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine($"Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, {GetEntityIndexHintsForEntities(index - 1, entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateJoinWithPredicateWithAllEntities(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds INNER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Join(Of TJoined)(<DisallowNull> predicate As Expression(Of Func(Of {generics}, TJoined, Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine($"Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, {GetEntityIndexHintsForAllEntities(entityCount + 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateJoinWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds INNER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Join(Of TJoined)(<DisallowNull> predicate As Expression(Of Func(Of Join(Of {generics}, TJoined), Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateJoinWithNoParameters(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds INNER JOIN clause."
      Dim typeParams = {"TJoined"}
      AddComment(builder, comment, typeParams:=typeParams, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Join(Of TJoined)() As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.Inner)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateJoinWithSubquery(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds INNER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"tableSourceFactory"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Join(Of TJoined)(<DisallowNull> tableSourceFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of TJoined))) As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.Inner, tableSourceFactory)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateJoinWithFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds INNER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"tableSource"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Join(Of TJoined)(<DisallowNull> tableSource As FormattableString) As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.Inner, tableSource)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateJoinWithRawSqlString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds INNER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"tableSource", "parameters"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Join(Of TJoined)(<DisallowNull> tableSource As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.Inner, tableSource, parameters)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateLeftJoinWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds LEFT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function LeftJoin(Of TJoined)(<DisallowNull> predicate As Expression(Of Func(Of {generic}, TJoined, Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine($"Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, {GetEntityIndexHintsForEntities(index - 1, entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateLeftJoinWithPredicateWithAllEntities(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds LEFT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function LeftJoin(Of TJoined)(<DisallowNull> predicate As Expression(Of Func(Of {generics}, TJoined, Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine($"Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, {GetEntityIndexHintsForAllEntities(entityCount + 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateLeftJoinWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds LEFT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function LeftJoin(Of TJoined)(<DisallowNull> predicate As Expression(Of Func(Of Join(Of {generics}, TJoined), Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateLeftJoinWithNoParameters(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds LEFT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      AddComment(builder, comment, typeParams:=typeParams, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function LeftJoin(Of TJoined)() As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.LeftOuter)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateLeftJoinWithSubquery(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds LEFT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"tableSourceFactory"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function LeftJoin(Of TJoined)(<DisallowNull> tableSourceFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of TJoined))) As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.LeftOuter, tableSourceFactory)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateLeftJoinWithFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds LEFT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"tableSource"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function LeftJoin(Of TJoined)(<DisallowNull> tableSource As FormattableString) As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.LeftOuter, tableSource)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateLeftJoinWithRawSqlString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds LEFT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"tableSource", "parameters"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function LeftJoin(Of TJoined)(<DisallowNull> tableSource As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.LeftOuter, tableSource, parameters)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateRightJoinWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds RIGHT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function RightJoin(Of TJoined)(<DisallowNull> predicate As Expression(Of Func(Of {generic}, TJoined, Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine($"Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, {GetEntityIndexHintsForEntities(index - 1, entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateRightJoinWithPredicateWithAllEntities(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds RIGHT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function RightJoin(Of TJoined)(<DisallowNull> predicate As Expression(Of Func(Of {generics}, TJoined, Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine($"Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, {GetEntityIndexHintsForAllEntities(entityCount + 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateRightJoinWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds RIGHT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function RightJoin(Of TJoined)(<DisallowNull> predicate As Expression(Of Func(Of Join(Of {generics}, TJoined), Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateRightJoinWithNoParameters(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds RIGHT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      AddComment(builder, comment, typeParams:=typeParams, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function RightJoin(Of TJoined)() As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.RightOuter)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateRightJoinWithSubquery(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds RIGHT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"tableSourceFactory"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function RightJoin(Of TJoined)(<DisallowNull> tableSourceFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of TJoined))) As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.RightOuter, tableSourceFactory)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateRightJoinWithFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds RIGHT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"tableSource"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function RightJoin(Of TJoined)(<DisallowNull> tableSource As FormattableString) As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.RightOuter, tableSource)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateRightJoinWithRawSqlString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds RIGHT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"tableSource", "parameters"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function RightJoin(Of TJoined)(<DisallowNull> tableSource As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.RightOuter, tableSource, parameters)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateFullJoinWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds FULL OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function FullJoin(Of TJoined)(<DisallowNull> predicate As Expression(Of Func(Of {generic}, TJoined, Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine($"Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, {GetEntityIndexHintsForEntities(index - 1, entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateFullJoinWithPredicateWithAllEntities(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds FULL OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function FullJoin(Of TJoined)(<DisallowNull> predicate As Expression(Of Func(Of {generics}, TJoined, Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine($"Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, {GetEntityIndexHintsForAllEntities(entityCount + 1)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateFullJoinWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds FULL OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function FullJoin(Of TJoined)(<DisallowNull> predicate As Expression(Of Func(Of Join(Of {generics}, TJoined), Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateFullJoinWithNoParameters(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds FULL OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      AddComment(builder, comment, typeParams:=typeParams, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function FullJoin(Of TJoined)() As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.FullOuter)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateFullJoinWithSubquery(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds FULL OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"tableSourceFactory"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function FullJoin(Of TJoined)(<DisallowNull> tableSourceFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of TJoined))) As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.FullOuter, tableSourceFactory)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateFullJoinWithFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds FULL OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"tableSource"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function FullJoin(Of TJoined)(<DisallowNull> tableSource As FormattableString) As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.FullOuter, tableSource)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateFullJoinWithRawSqlString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds FULL OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"tableSource", "parameters"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function FullJoin(Of TJoined)(<DisallowNull> tableSource As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.FullOuter, tableSource, parameters)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCrossJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds CROSS JOIN clause."
      Dim typeParams = {"TJoined"}
      AddComment(builder, comment, typeParams:=typeParams, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function CrossJoin(Of TJoined)() As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.CrossJoin, Nothing, {0, 1})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCrossJoinWithSubquery(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds CROSS JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"tableSourceFactory"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function CrossJoin(Of TJoined)(<DisallowNull> tableSourceFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of TJoined))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddJoin(Of TJoined)(Me.Executor, JoinType.CrossJoin, tableSourceFactory)")
      builder.Indent().AppendLine("Me.Builder.AddOn(Of TJoined)(Nothing, {0, 1})")
      builder.Indent().AppendLine($"Return New JoinedSelectSqlExpression(Of {generics}, TJoined)(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCrossJoinWithFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds CROSS JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"tableSource"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function CrossJoin(Of TJoined)(<DisallowNull> tableSource As FormattableString) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddJoin(Of TJoined)(JoinType.CrossJoin, tableSource)")
      builder.Indent().AppendLine("Me.Builder.AddOn(Of TJoined)(Nothing, {0, 1})")
      builder.Indent().AppendLine($"Return New JoinedSelectSqlExpression(Of {generics}, TJoined)(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCrossJoinWithRawSqlString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds CROSS JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"tableSource", "parameters"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function CrossJoin(Of TJoined)(<DisallowNull> tableSource As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddJoin(Of TJoined)(JoinType.CrossJoin, tableSource, parameters)")
      builder.Indent().AppendLine("Me.Builder.AddOn(Of TJoined)(Nothing, {0, 1})")
      builder.Indent().AppendLine($"Return New JoinedSelectSqlExpression(Of {generics}, TJoined)(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalJoinWithPredicate(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"joinType", "predicate", "entityIndexHints"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalJoin(Of TJoined)(joinType As JoinType, predicate As Expression, entityIndexHints As Int32()) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddJoin(Of TJoined)(joinType, predicate, entityIndexHints)")
      builder.Indent().AppendLine($"Return New JoinedSelectSqlExpression(Of {generics}, TJoined)(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"joinType"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalJoin(Of TJoined)(joinType As JoinType) As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddJoin(Of TJoined)(joinType)")
      builder.Indent().AppendLine($"Return New JoinSelectSqlExpression(Of {generics}, TJoined)(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalJoinWithSubquery(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"joinType", "tableSourceFactory"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalJoin(Of TJoined)(joinType As JoinType, tableSourceFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of TJoined))) As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddJoin(Of TJoined)(Me.Executor, joinType, tableSourceFactory)")
      builder.Indent().AppendLine($"Return New JoinSelectSqlExpression(Of {generics}, TJoined)(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalJoinWithFormattableString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"joinType", "tableSource"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalJoin(Of TJoined)(joinType As JoinType, tableSource As FormattableString) As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddJoin(Of TJoined)(joinType, tableSource)")
      builder.Indent().AppendLine($"Return New JoinSelectSqlExpression(Of {generics}, TJoined)(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalJoinWithRawSqlString(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"joinType", "tableSource", "parameters"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalJoin(Of TJoined)(joinType As JoinType, tableSource As RawSqlString, ParamArray parameters() As Object) As JoinSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Me.Builder.AddJoin(Of TJoined)(joinType, tableSource, parameters)")
      builder.Indent().AppendLine($"Return New JoinSelectSqlExpression(Of {generics}, TJoined)(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace