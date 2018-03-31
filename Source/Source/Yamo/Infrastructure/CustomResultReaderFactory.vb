Imports System.Data
Imports System.Linq.Expressions
Imports System.Reflection
Imports Yamo.Metadata

Namespace Infrastructure

  Public Class CustomResultReaderFactory
    Inherits ReaderFactoryBase

    Public Shared Function CreateResultFactory(node As NewExpression) As Object
      Dim valuesParam = Expression.Parameter(GetType(Object()), "values")
      Dim parameters = {valuesParam}

      Dim expressions = New List(Of Expression)

      Dim arguments = New List(Of Expression)

      For i = 0 To node.Arguments.Count - 1
        Dim paramIndex = Expression.ArrayIndex(valuesParam, Expression.Constant(i))
        Dim paramConvert = Expression.Convert(paramIndex, node.Arguments(i).Type)
        arguments.Add(paramConvert)
      Next

      Dim newExp = Expression.[New](node.Constructor, arguments)

      expressions.Add(newExp)

      Dim body = Expression.Block({}, expressions)

      Dim reader = Expression.Lambda(body, parameters)
      Return reader.Compile()
    End Function

  End Class
End Namespace