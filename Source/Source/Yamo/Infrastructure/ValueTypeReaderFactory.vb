Imports System.Data
Imports System.Linq.Expressions
Imports System.Reflection

Namespace Infrastructure

  Public Class ValueTypeReaderFactory
    Inherits ReaderFactoryBase

    Public Overridable Function CreateReader(type As Type) As Object
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
      ElseIf type Is GetType(Byte()) Then
        Dim propAssign = Expression.Assign(variable, readValueCall)
        Dim isDBNullCall = Expression.Call(readerParam, "IsDBNull", Nothing, indexParam)
        Dim cond = Expression.IfThenElse(isDBNullCall, propAssignNull, propAssign)
        expressions.Add(cond)
      ElseIf underlyingType Is Nothing Then
        'Dim propAssign = Expression.Assign(variable, readValueCall)
        'expressions.Add(propAssign)
        ' NOTE: we perform IsDBNull check on non-nullable types anyway and return default value. This behavior is
        ' probably more convenient in custom selects than throwing an exception, especially when called from FirstOrDefault.
        ' Also, QueryFirstOrDefault behaves the same way. If this behavior should change/be optional in the future (probably
        ' with introducing First/QueryFirst methods), make sure it is constistent across all use cases.
        Dim propAssign = Expression.Assign(variable, readValueCall)
        Dim isDBNullCall = Expression.Call(readerParam, "IsDBNull", Nothing, indexParam)
        Dim cond = Expression.IfThenElse(isDBNullCall, propAssignNull, propAssign)
        expressions.Add(cond)
      Else
        Dim isDBNullCall = Expression.Call(readerParam, "IsDBNull", Nothing, indexParam)
        Dim nullableConstructor = type.GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, CallingConventions.HasThis, {underlyingType}, New ParameterModifier(0) {})
        Dim propAssign = Expression.Assign(variable, Expression.[New](nullableConstructor, readValueCall))
        Dim cond = Expression.IfThenElse(isDBNullCall, propAssignNull, propAssign)
        expressions.Add(cond)
      End If

      expressions.Add(variable)

      Dim body = Expression.Block({variable}, expressions)

      Dim reader = Expression.Lambda(body, parameters)
      Return reader.Compile()
    End Function

  End Class
End Namespace