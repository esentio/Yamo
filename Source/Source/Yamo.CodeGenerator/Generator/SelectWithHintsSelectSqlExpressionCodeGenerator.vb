Namespace Generator

  Public Class SelectWithHintsSelectSqlExpressionCodeGenerator
    Inherits CodeGenerator

    Public Sub New(indentation As String, outputFolder As String, definition As GeneratedClassDefinition, definitions As List(Of GeneratedClassDefinition))
      MyBase.New(indentation, outputFolder, definition, definitions)
    End Sub

    Protected Overrides Function GetAllowedResultsForCondition() As GeneratedClass()
      Return {
        GeneratedClass.SelectSqlExpression,
        GeneratedClass.JoinSelectSqlExpression,
        GeneratedClass.JoinWithHintsSelectSqlExpression,
        GeneratedClass.JoinedSelectSqlExpression,
        GeneratedClass.FilteredSelectSqlExpression,
        GeneratedClass.GroupedSelectSqlExpression,
        GeneratedClass.HavingSelectSqlExpression,
        GeneratedClass.OrderedSelectSqlExpression,
        GeneratedClass.LimitedSelectSqlExpression
      }
    End Function

    Protected Overrides Sub Generate(builder As CodeBuilder, entityCount As Int32)
      builder.Indent().AppendLine("Imports System.Linq.Expressions")
      builder.Indent().AppendLine("Imports Yamo.Expressions.Builders")
      builder.Indent().AppendLine("Imports Yamo.Internal.Query")
      builder.Indent().AppendLine("Imports Yamo.Internal.Query.Metadata")
      builder.AppendLine()
      builder.Indent().AppendLine("Namespace Expressions").PushIndent()
      builder.AppendLine()

      Dim comment As String
      Dim typeParams As String()

      If entityCount = 1 Then
        comment = "Represents SQL SELECT statement from one table (entity), which has defined table hints."
        typeParams = GetGenericNames(entityCount)
      Else
        Throw New NotSupportedException()
      End If
      AddComment(builder, comment, typeParams:=typeParams)

      builder.Indent().AppendLine($"Public Class {GetFullClassName(entityCount)}").PushIndent()
      builder.Indent().AppendLine("Inherits SelectSqlExpressionBase")
      builder.AppendLine()
      GenerateConstructor(builder, entityCount)
      builder.AppendLine()

      GenerateJoin(builder, entityCount)
      builder.AppendLine()

      GenerateWhere(builder, entityCount)
      builder.AppendLine()

      GenerateGroupBy(builder, entityCount)
      builder.AppendLine()

      GenerateOrderBy(builder, entityCount)
      builder.AppendLine()

      GenerateLimit(builder, entityCount)
      builder.AppendLine()

      GenerateSelect(builder, entityCount)
      builder.AppendLine()

      GenerateIf(builder, entityCount)
      builder.AppendLine()

      builder.PopIndent()
      builder.Indent().AppendLine($"End Class").PopIndent()
      builder.Indent().AppendLine("End Namespace")
    End Sub

    Private Sub GenerateConstructor(builder As CodeBuilder, entityCount As Int32)
      If entityCount = 1 Then
        Dim comment = $"Creates new instance of <see cref=""{GetFullClassName(entityCount)}""/>."
        Dim params = {"builder", "executor"}
        AddComment(builder, comment, params:=params)

        builder.Indent().AppendLine("Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)").PushIndent()
        builder.Indent().AppendLine("MyBase.New(builder, executor)").PopIndent()
        builder.Indent().AppendLine("End Sub")
      Else
        Throw New NotSupportedException()
      End If
    End Sub

  End Class
End Namespace