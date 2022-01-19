Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateAs(builder As CodeBuilder, entityCount As Int32)
      For i = 1 To entityCount - 1
        GenerateAsWithPredicateWithOneEntity(builder, i, entityCount)
        builder.AppendLine()
      Next

      If 2 < entityCount Then
        GenerateAsWithPredicateWithIJoin(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateInternalAs(builder, entityCount)
    End Sub

    Protected Sub GenerateAsWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Specifies last joined entity."
      Dim typeParams = {"TProperty"}
      Dim params = {"relationship"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [As](Of TProperty)(<DisallowNull> relationship As Expression(Of Func(Of {generic}, TProperty))) As SelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalAs(relationship)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateAsWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Specifies last joined entity."
      Dim typeParams = {"TProperty"}
      Dim params = {"relationship"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics1 = String.Join(", ", GetGenericNames(entityCount - 1))
      Dim generics2 = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [As](Of TProperty)(<DisallowNull> relationship As Expression(Of Func(Of Join(Of {generics1}), TProperty))) As SelectSqlExpression(Of {generics2})").PushIndent()
      builder.Indent().AppendLine("Return InternalAs(relationship)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalAs(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Specifies last joined entity."
      Dim params = {"relationship"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalAs(relationship As Expression) As SelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.SetLastJoinRelationship(relationship)")
      builder.Indent().AppendLine($"Return New SelectSqlExpression(Of {generics})(Me.Builder, Me.Executor)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace