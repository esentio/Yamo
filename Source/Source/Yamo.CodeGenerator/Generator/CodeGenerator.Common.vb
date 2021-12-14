Namespace Generator

  Partial Public Class CodeGenerator

    Protected Sub AddComment(builder As CodeBuilder, comment As String, Optional typeParams() As String = Nothing, Optional params() As String = Nothing, Optional commentedParams() As (Param As String, Comment As String) = Nothing, Optional returns As String = Nothing)
      If params IsNot Nothing AndAlso commentedParams IsNot Nothing Then
        Throw New Exception("Both params and commentedParams are not allowed.")
      End If

      builder.Indent().AppendLine("''' <summary>")
      builder.Indent().AppendLine("''' " & comment)
      builder.Indent().AppendLine("''' </summary>")

      If typeParams IsNot Nothing Then
        For Each typeParam In typeParams
          builder.Indent().AppendLine($"''' <typeparam name=""{typeParam}""></typeparam>")
        Next
      End If

      If params IsNot Nothing Then
        For Each param In params
          builder.Indent().AppendLine($"''' <param name=""{param}""></param>")
        Next
      ElseIf commentedParams IsNot Nothing Then
        For Each param In commentedParams
          builder.Indent().AppendLine($"''' <param name=""{param.Param}"">{param.Comment}</param>")
        Next
      End If

      If returns IsNot Nothing Then
        builder.Indent().AppendLine($"''' <returns>{returns}</returns>")
      End If
    End Sub

    Protected Function GetGenericName(index As Int32, Optional trimIndexOnFirstEntity As Boolean = False) As String
      Return GetGenericName("T", index, trimIndexOnFirstEntity)
    End Function

    Protected Function GetGenericName(className As String, index As Int32, Optional trimIndexOnFirstEntity As Boolean = False) As String
      If trimIndexOnFirstEntity AndAlso index = 1 Then
        Return className
      Else
        Return className & index.ToInvariantString()
      End If
    End Function

    Protected Function GetGenericNames(entityCount As Int32) As String()
      Return GetGenericNames("T", entityCount)
    End Function

    Protected Function GetGenericNames(className As String, entityCount As Int32) As String()
      If entityCount = 1 Then
        Return {className}
      Else
        Return Enumerable.Range(1, entityCount).Select(Function(x) GetGenericName(className, x)).ToArray()
      End If
    End Function

    Protected Function GetEntityIndexHintsForEntity(index As Int32) As String
      Return "{" & index.ToInvariantString() & "}"
    End Function

    Protected Function GetEntityIndexHintsForEntities(index1 As Int32, index2 As Int32) As String
      Return "{" & index1.ToInvariantString() & ", " & index2.ToInvariantString() & "}"
    End Function

    Protected Function GetEntityIndexHintsForAllEntities(entityCount As Int32) As String
      Return "{" & String.Join(", ", Enumerable.Range(0, entityCount).Select(Function(x) x.ToInvariantString())) & "}"
    End Function

    Protected Function GetGenericOfDefinition(entityCount As Int32) As String
      Return "(Of " & String.Join(", ", GetGenericNames(entityCount)) & ")"
    End Function

    Protected Function GetGenericOfDefinition(className As String, entityCount As Int32) As String
      Return "(Of " & String.Join(", ", GetGenericNames(className, entityCount)) & ")"
    End Function

    Protected Function GetGenericOfDefinitionWithoutTypes(entityCount As Int32) As String
      Return "(Of " & String.Join("", Enumerable.Range(1, entityCount - 1).Select(Function(x) ",")) & ")"
    End Function

    Protected Function GetOrdinal(index As Int32) As String
      If index = 1 Then
        Return "1st"
      ElseIf index = 2 Then
        Return "2nd"
      ElseIf index = 3 Then
        Return "3rd"
      Else
        Return index.ToInvariantString() & "th"
      End If
    End Function

    Protected Function GetGenericsSuffixForFilename(entityCount As Int32) As String
      If 1 < entityCount Then
        Return "Of" & String.Join("", GetGenericNames(entityCount))
      End If

      Return ""
    End Function

  End Class
End Namespace