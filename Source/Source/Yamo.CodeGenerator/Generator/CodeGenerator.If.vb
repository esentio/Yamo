Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateIf(builder As CodeBuilder, entityCount As Int32, Optional useOverloads As Boolean = False)
      Dim comment = "Conditionally builds the expression."
      Dim typeParams = {"TResult"}
      Dim params = {"condition", "[then]", "otherwise"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public{If(useOverloads, " Overloads", "")} Function [If](Of TResult)(condition As Boolean, [then] As Func(Of {GetFullClassName(entityCount)}, TResult), Optional otherwise As Func(Of {GetFullClassName(entityCount)}, TResult) = Nothing) As TResult").PushIndent()
      builder.Indent().AppendLine("Dim result As TResult")
      builder.AppendLine()
      builder.Indent().AppendLine("If condition Then").PushIndent()
      builder.Indent().AppendLine("result = [then].Invoke(Me)").PopIndent()
      builder.Indent().AppendLine("ElseIf otherwise Is Nothing Then").PushIndent()
      builder.Indent().AppendLine("Me.Builder.StartConditionalIgnoreMode()")
      builder.Indent().AppendLine("result = [then].Invoke(Me)")
      builder.Indent().AppendLine("Me.Builder.EndConditionalIgnoreMode()").PopIndent()
      builder.Indent().AppendLine("Else").PushIndent()
      builder.Indent().AppendLine("result = otherwise.Invoke(Me)").PopIndent()
      builder.Indent().AppendLine("End If")
      builder.AppendLine()
      builder.Indent().AppendLine("Return result").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateIfWithMandatoryOtherwise(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Conditionally builds the expression."
      Dim typeParams = {"TResult"}
      Dim params = {"condition", "[then]", "otherwise"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      Dim generic = GetGenericName(1, entityCount = 1)
      Dim generics = String.Join(", ", GetGenericNames(entityCount))

      builder.Indent().AppendLine($"Public Function [If](Of TResult)(condition As Boolean, [then] As Func(Of {GetFullClassName(entityCount)}, TResult), otherwise As Func(Of {GetFullClassName(entityCount)}, TResult)) As TResult").PushIndent()
      builder.Indent().AppendLine("If condition Then").PushIndent()
      builder.Indent().AppendLine("Return [then].Invoke(Me)").PopIndent()
      builder.Indent().AppendLine("Else").PushIndent()
      builder.Indent().AppendLine("Return otherwise.Invoke(Me)").PopIndent()
      builder.Indent().AppendLine("End If").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace