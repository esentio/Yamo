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

      GenerateCrossJoin(builder, entityCount)
      builder.AppendLine()

      GenerateInternalJoin(builder, entityCount)
    End Sub

    Protected Sub GenerateJoinWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds INNER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of {generic}, TJoined, Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine($"Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, {GetEntityIndexHintsForEntities(index - 1, entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateJoinWithPredicateWithAllEntities(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds INNER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of {generics}, TJoined, Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      ' TODO: SIP - there probably should be index hints; check and fix everywhere (in every join)
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateJoinWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds INNER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of Join(Of {generics}, TJoined), Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateLeftJoinWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds LEFT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of {generic}, TJoined, Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine($"Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, {GetEntityIndexHintsForEntities(index - 1, entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateLeftJoinWithPredicateWithAllEntities(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds LEFT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of {generics}, TJoined, Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateLeftJoinWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds LEFT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of Join(Of {generics}, TJoined), Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateRightJoinWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds RIGHT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of {generic}, TJoined, Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine($"Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, {GetEntityIndexHintsForEntities(index - 1, entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateRightJoinWithPredicateWithAllEntities(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds RIGHT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of {generics}, TJoined, Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateRightJoinWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds RIGHT OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of Join(Of {generics}, TJoined), Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateFullJoinWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Adds FULL OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of {generic}, TJoined, Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine($"Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, {GetEntityIndexHintsForEntities(index - 1, entityCount)})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateFullJoinWithPredicateWithAllEntities(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds FULL OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of {generics}, TJoined, Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, Nothing)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateFullJoinWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Adds FULL OUTER JOIN clause."
      Dim typeParams = {"TJoined"}
      Dim params = {"predicate"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of Join(Of {generics}, TJoined), Boolean))) As JoinedSelectSqlExpression(Of {generics}, TJoined)").PushIndent()
      builder.Indent().AppendLine("Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, Nothing)").PopIndent()
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

    Protected Sub GenerateInternalJoin(builder As CodeBuilder, entityCount As Int32)
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

  End Class
End Namespace