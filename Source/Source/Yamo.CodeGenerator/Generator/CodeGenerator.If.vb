Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub GenerateIf(builder As CodeBuilder, entityCount As Int32, Optional useOverloads As Boolean = False)
      GenerateIfWithOptionalOtherwise(builder, entityCount, useOverloads)
      builder.AppendLine()

      GenerateCreateResultForCondition(builder, entityCount)
      builder.AppendLine()

      GenerateCanCreateResultForCondition(builder, entityCount)
    End Sub

    Protected Sub GenerateIfWithOptionalOtherwise(builder As CodeBuilder, entityCount As Int32, useOverloads As Boolean)
      Dim comment = "Conditionally builds the expression."
      Dim typeParams = {"TResult"}
      Dim params = {"condition", "[then]", "otherwise"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      builder.Indent().AppendLine($"Public{If(useOverloads, " Overloads", "")} Function [If](Of TResult)(condition As Boolean, [then] As Func(Of {GetFullClassName(entityCount)}, TResult), Optional otherwise As Func(Of {GetFullClassName(entityCount)}, TResult) = Nothing) As TResult").PushIndent()
      builder.Indent().AppendLine("If condition Then").PushIndent()
      builder.Indent().AppendLine("Return [then].Invoke(Me)").PopIndent()
      builder.Indent().AppendLine("ElseIf otherwise Is Nothing Then").PushIndent()
      builder.Indent().AppendLine("Return CreateResultForCondition(Of TResult)()").PopIndent()
      builder.Indent().AppendLine("Else").PushIndent()
      builder.Indent().AppendLine("Return otherwise.Invoke(Me)").PopIndent()
      builder.Indent().AppendLine("End If").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateIfWithMandatoryOtherwise(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Conditionally builds the expression."
      Dim typeParams = {"TResult"}
      Dim params = {"condition", "[then]", "otherwise"}
      AddComment(builder, comment, typeParams:=typeParams, params:=params, returns:="")

      builder.Indent().AppendLine($"Public Function [If](Of TResult)(condition As Boolean, [then] As Func(Of {GetFullClassName(entityCount)}, TResult), otherwise As Func(Of {GetFullClassName(entityCount)}, TResult)) As TResult").PushIndent()
      builder.Indent().AppendLine("If condition Then").PushIndent()
      builder.Indent().AppendLine("Return [then].Invoke(Me)").PopIndent()
      builder.Indent().AppendLine("Else").PushIndent()
      builder.Indent().AppendLine("Return otherwise.Invoke(Me)").PopIndent()
      builder.Indent().AppendLine("End If").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCreateResultForCondition(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Creates result for condition if condition is not met."
      Dim typeParams = {"TResult"}
      AddComment(builder, comment, typeParams:=typeParams, returns:="")

      builder.Indent().AppendLine("Private Function CreateResultForCondition(Of TResult)() As TResult").PushIndent()
      builder.Indent().AppendLine("Dim thisType = Me.GetType()")
      builder.Indent().AppendLine("Dim resultType = GetType(TResult)")
      builder.AppendLine()
      builder.Indent().AppendLine("If thisType Is resultType Then").PushIndent()
      builder.Indent().AppendLine("Return DirectCast(DirectCast(Me, Object), TResult)").PopIndent()
      builder.Indent().AppendLine("End If")
      builder.AppendLine()
      builder.Indent().AppendLine("If Not CanCreateResultForCondition(resultType) Then").PushIndent()
      builder.Indent().AppendLine("Throw New InvalidOperationException($""Parameter 'otherwise' in If() method is required for return type '{resultType}'."")").PopIndent()
      builder.Indent().AppendLine("End If")
      builder.AppendLine()

      Dim allowedResults = GetAllowedResultsForCondition()
      Dim allowJoins = allowedResults.Contains(GeneratedClass.JoinSelectSqlExpression)

      If allowJoins Then
        builder.Indent().AppendLine("Dim thisTypeGenericArgumentsCount = thisType.GenericTypeArguments.Length")
        builder.Indent().AppendLine("Dim resultTypeGenericArguments = resultType.GenericTypeArguments")
        builder.Indent().AppendLine("Dim resultTypeGenericArgumentsCount = resultTypeGenericArguments.Length")
        builder.AppendLine()
        builder.Indent().AppendLine("If thisTypeGenericArgumentsCount < resultTypeGenericArgumentsCount Then").PushIndent()
        builder.Indent().AppendLine("For i = thisTypeGenericArgumentsCount To resultTypeGenericArgumentsCount - 1").PushIndent()
        builder.Indent().AppendLine("Me.Builder.AddIgnoredJoin(resultTypeGenericArguments(i))").PopIndent()
        builder.Indent().AppendLine("Next").PopIndent()
        builder.Indent().AppendLine("End If")
        builder.AppendLine()
      End If
      builder.Indent().AppendLine("Return DirectCast(Activator.CreateInstance(resultType, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance, Nothing, {Me.Builder, Me.Executor}, Nothing), TResult)").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

    Protected Sub GenerateCanCreateResultForCondition(builder As CodeBuilder, entityCount As Int32)
      Dim comment = "Checks if result can be created if condition is not met."
      Dim params = {"resultType"}
      AddComment(builder, comment, params:=params, returns:="")

      builder.Indent().AppendLine("Private Function CanCreateResultForCondition(resultType As Type) As Boolean").PushIndent()
      builder.Indent().AppendLine("If Not GetType(SelectSqlExpressionBase).IsAssignableFrom(resultType) Then").PushIndent()
      builder.Indent().AppendLine("Return False").PopIndent()
      builder.Indent().AppendLine("End If")
      builder.AppendLine()
      builder.Indent().AppendLine("If Not resultType.IsGenericType Then").PushIndent()
      builder.Indent().AppendLine("Return False").PopIndent()
      builder.Indent().AppendLine("End If")
      builder.AppendLine()
      builder.Indent().AppendLine("Dim genericType = resultType.GetGenericTypeDefinition()")
      builder.AppendLine()

      Dim allowedResults = GetAllowedResultsForCondition()
      Dim allowJoins = allowedResults.Contains(GeneratedClass.JoinSelectSqlExpression)

      For Each generatedClass In allowedResults
        Dim definition = Me.Definitions.First(Function(x) x.Type = generatedClass)

        Dim min = Math.Max(entityCount, definition.MinEntityCount)
        Dim max = Math.Min(entityCount, definition.MaxEntityCount)

        If allowJoins Then
          max = Math.Max(entityCount, definition.MaxEntityCount)
        End If

        Select Case generatedClass
          Case GeneratedClass.SelectSqlExpression
            If TypeOf Me Is SelectSqlExpressionCodeGenerator OrElse TypeOf Me Is SelectWithHintsSelectSqlExpressionCodeGenerator Then
              min = Math.Max(entityCount + 1, definition.MinEntityCount)
            End If
          Case GeneratedClass.SelectWithHintsSelectSqlExpression
            If Not (TypeOf Me Is SelectSqlExpressionCodeGenerator AndAlso entityCount = 1) Then
              max = -1
            End If
          Case GeneratedClass.JoinSelectSqlExpression
            min = Math.Max(entityCount + 1, definition.MinEntityCount)
          Case GeneratedClass.JoinWithHintsSelectSqlExpression
            If TypeOf Me Is JoinSelectSqlExpressionCodeGenerator Then
              min = entityCount
              max = entityCount
            Else
              min = Math.Max(entityCount + 1, definition.MinEntityCount)
            End If
          Case GeneratedClass.JoinedSelectSqlExpression
            min = Math.Max(entityCount + 1, definition.MinEntityCount)
          Case GeneratedClass.DistinctSelectSqlExpression
            min = 1
            max = 1
          Case GeneratedClass.CustomSelectSqlExpression
            min = 1
            max = 1
          Case GeneratedClass.CustomDistinctSelectSqlExpression
            min = 1
            max = 1
        End Select

        Dim className = GetClassName(generatedClass)

        For i = min To max
          Dim generics = GetGenericOfDefinitionWithoutTypes(i)
          builder.Indent().AppendLine($"If genericType Is GetType({className}{generics}) Then Return True")
        Next
      Next

      builder.AppendLine()
      builder.Indent().AppendLine("Return False").PopIndent()
      builder.Indent().AppendLine("End Function")
    End Sub

  End Class
End Namespace