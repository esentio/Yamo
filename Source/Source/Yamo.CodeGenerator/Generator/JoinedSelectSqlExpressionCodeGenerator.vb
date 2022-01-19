Namespace Generator

  Public Class JoinedSelectSqlExpressionCodeGenerator
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
      builder.Indent().AppendLine("Imports System.Diagnostics.CodeAnalysis")
      builder.Indent().AppendLine("Imports System.Linq.Expressions")
      builder.Indent().AppendLine("Imports Yamo.Expressions.Builders")
      builder.Indent().AppendLine("Imports Yamo.Internal.Query")
      builder.AppendLine()
      builder.Indent().AppendLine("Namespace Expressions").PushIndent()
      builder.AppendLine()

      Dim comment = $"Represents SQL SELECT statement from {entityCount.ToInvariantString()} tables (entities)."
      Dim typeParams = GetGenericNames(entityCount)
      AddComment(builder, comment, typeParams:=typeParams)

      builder.Indent().AppendLine($"Public Class {GetFullClassName(entityCount)}").PushIndent()
      builder.Indent().AppendLine($"Inherits SelectSqlExpression{GetGenericOfDefinition(entityCount)}")
      builder.AppendLine()
      GenerateConstructor(builder, entityCount)
      builder.AppendLine()

      GenerateAs(builder, entityCount)
      builder.AppendLine()

      GenerateIf(builder, entityCount, True)
      builder.AppendLine()

      builder.PopIndent()
      builder.Indent().AppendLine($"End Class").PopIndent()
      builder.Indent().AppendLine("End Namespace")
    End Sub

    Private Sub GenerateConstructor(builder As CodeBuilder, entityCount As Int32)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      Dim comment = $"Creates new instance of <see cref=""{GetFullClassName(entityCount)}""/>."
      Dim params = {"builder", "executor"}
      AddComment(builder, comment, params:=params)

      builder.Indent().AppendLine("Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)").PushIndent()
      builder.Indent().AppendLine("MyBase.New(builder, executor)").PopIndent()
      builder.Indent().AppendLine("End Sub")
    End Sub

  End Class
End Namespace