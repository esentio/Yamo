Imports System.Data
Imports System.Linq.Expressions
Imports System.Reflection

' TODO: SIP - will this still be needed in the future. Delete if not.

Namespace Infrastructure

  Public Class ValueTypeReaderFactory
    Inherits ReaderFactoryBase

    Public Overridable Function CreateReader(Of T)() As Func(Of IDataReader, Int32, T)
      Dim type = GetType(T)
      Dim readerParam = Expression.Parameter(GetType(IDataRecord), "reader") ' this has to be IDataRecord, otherwise Expression.Call() cannot find the method
      Dim indexParam = Expression.Parameter(GetType(Int32), "index")
      Dim parameters = {readerParam, indexParam}

      Dim variable = Expression.Variable(type, "value")

      Dim expressions = New List(Of Expression)

      Dim readMethodForType = GetReadMethodForType(type)
      Dim readValueCall As Expression = Expression.Call(readerParam, readMethodForType.Method, Nothing, indexParam)

      If readMethodForType.Convert Then
        readValueCall = Expression.Convert(readValueCall, type)
      End If

      Dim propAssignNull = Expression.Assign(variable, Expression.Default(type))

      Dim underlyingType = Nullable.GetUnderlyingType(type)

      If type Is GetType(String) Then
        Dim propAssign = Expression.Assign(variable, readValueCall)
        Dim isDBNullCall = Expression.Call(readerParam, "IsDBNull", Nothing, indexParam)
        Dim cond = Expression.IfThenElse(isDBNullCall, propAssignNull, propAssign)
        expressions.Add(cond)
      ElseIf underlyingType Is Nothing Then
        Dim propAssign = Expression.Assign(variable, readValueCall)
        expressions.Add(propAssign)
      Else
        Dim isDBNullCall = Expression.Call(readerParam, "IsDBNull", Nothing, indexParam)
        Dim nullableConstructor = type.GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, CallingConventions.HasThis, {underlyingType}, New ParameterModifier(0) {})
        Dim propAssign = Expression.Assign(variable, Expression.[New](nullableConstructor, readValueCall))
        Dim cond = Expression.IfThenElse(isDBNullCall, propAssignNull, propAssign)
        expressions.Add(cond)
      End If

      expressions.Add(variable)

      Dim body = Expression.Block({variable}, expressions)

      Dim reader = Expression.Lambda(Of Func(Of IDataReader, Int32, T))(body, parameters)
      Return reader.Compile()
    End Function

  End Class
End Namespace