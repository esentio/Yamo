Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateExclude(builder As CodeBuilder, entityCount As Int32)
      Dim limit = 8

      If entityCount < limit Then
        For i = 1 To entityCount
          GenerateExcludeWithPredicateWithOneEntity(builder, i, entityCount)
          builder.AppendLine()
        Next
      End If

      If 1 < entityCount Then
        GenerateExcludeWithPredicateWithIJoin(builder, entityCount)
        builder.AppendLine()
      End If

      If 1 < entityCount Then
        For i = 2 To entityCount
          GenerateExcludeTable(builder, i, entityCount)
          builder.AppendLine()
        Next

        GenerateInternalExcludeWithPredicate(builder, entityCount)
        builder.AppendLine()

        GenerateInternalExcludeWithIndex(builder, entityCount)
      Else
        GenerateInternalExcludeWithPredicate(builder, entityCount)
      End If
    End Sub

    Protected Sub GenerateExcludeWithPredicateWithOneEntity(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = "Excludes &lt;column&gt; from SELECT clause."
      Dim typeParams = {"TProperty"}
      Dim params = {"propertyExpression"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Exclude(Of TProperty)(propertyExpression As Expression(Of Func(Of {generic}, TProperty))) As SelectedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalExclude(propertyExpression)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateExcludeWithPredicateWithIJoin(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Excludes &lt;column&gt; from SELECT clause."
      Dim typeParams = {"TProperty"}
      Dim params = {"propertyExpression"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Exclude(Of TProperty)(propertyExpression As Expression(Of Func(Of Join(Of {generics}), TProperty))) As SelectedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Return InternalExclude(propertyExpression)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateExcludeTable(builder As CodeBuilder, index As Int32, entityCount As Int32)
      Dim comment = $"Excludes all columns of {GetOrdinal(index)} table (entity) from SELECT clause."
      AddComment(builder, comment, returns:="")

      Dim generic = GetGenericName(index, index = entityCount)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function Exclude{generic}() As SelectedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine($"Return InternalExclude({(index - 1).ToInvariantString()})").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalExcludeWithPredicate(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Excludes &lt;column&gt; from SELECT clause."
      Dim params = {"propertyExpression"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalExclude(propertyExpression As Expression) As SelectedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.ExcludeSelected(propertyExpression)")
      builder.Indent().AppendLine("Return Me").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateInternalExcludeWithIndex(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Excludes all columns of the table (entity) from SELECT clause."
      Dim params = {"entityIndex"}
      AddComment(builder, comment, params:=params, returns:="")

      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Private Function InternalExclude(entityIndex As Int32) As SelectedSelectSqlExpression(Of {generics})").PushIndent()
      builder.Indent().AppendLine("Me.Builder.ExcludeSelected(entityIndex)")
      builder.Indent().AppendLine("Return Me").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace