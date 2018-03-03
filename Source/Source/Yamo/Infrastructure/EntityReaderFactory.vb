Imports System.Data
Imports System.Linq.Expressions
Imports System.Reflection
Imports Yamo.Metadata

Namespace Infrastructure

  Public Class EntityReaderFactory
    Inherits ReaderFactoryBase

    Public Overridable Function CreateReader(model As Model, entityType As Type) As Func(Of IDataReader, Int32, BitArray, Object)
      Dim readerParam = Expression.Parameter(GetType(IDataRecord), "reader") ' this has to be IDataRecord, otherwise Expression.Call() cannot find the method
      Dim indexParam = Expression.Parameter(GetType(Int32), "index")
      Dim includedColumnsParam = Expression.Parameter(GetType(BitArray), "includedColumns")

      Dim parameters = {readerParam, indexParam, includedColumnsParam}

      Dim entityVariable = Expression.Variable(entityType, "entityObj")

      Dim expressions = New List(Of Expression)

      expressions.Add(Expression.Assign(entityVariable, Expression.[New](entityType)))

      Dim bitArrayItemPropertyInfo = GetType(BitArray).GetProperty("Item")

      Dim entity = model.GetEntity(entityType)
      Dim i = 0
      For Each prop In entity.GetProperties()
        Dim varProp = Expression.Property(entityVariable, prop.Name)

        Dim readMethodForType = GetReadMethodForType(prop.PropertyType)
        Dim readValueCall As Expression = Expression.Call(readerParam, readMethodForType.Method, Nothing, indexParam)

        If readMethodForType.Convert Then
          readValueCall = Expression.Convert(readValueCall, prop.PropertyType)
        End If

        Dim propAssignNull = Expression.Assign(varProp, GetDefaultValue(prop))

        Dim underlyingNullableType = Nullable.GetUnderlyingType(prop.PropertyType)

        Dim includeExpressions = New List(Of Expression)

        If prop.PropertyType Is GetType(String) OrElse prop.PropertyType Is GetType(Byte()) Then
          Dim propAssign = Expression.Assign(varProp, readValueCall)
          Dim isDBNullCall = Expression.Call(readerParam, "IsDBNull", Nothing, indexParam)
          Dim isDBNullCond = Expression.IfThenElse(isDBNullCall, propAssignNull, propAssign)
          includeExpressions.Add(isDBNullCond)
        ElseIf underlyingNullableType Is Nothing Then
          Dim propAssign = Expression.Assign(varProp, readValueCall)
          includeExpressions.Add(propAssign)
        Else
          Dim isDBNullCall = Expression.Call(readerParam, "IsDBNull", Nothing, indexParam)
          Dim nullableConstructor = prop.PropertyType.GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, CallingConventions.HasThis, {underlyingNullableType}, New ParameterModifier(0) {})
          Dim propAssign = Expression.Assign(varProp, Expression.[New](nullableConstructor, readValueCall))
          Dim isDBNullCond = Expression.IfThenElse(isDBNullCall, propAssignNull, propAssign)
          includeExpressions.Add(isDBNullCond)
        End If

        includeExpressions.Add(Expression.AddAssign(indexParam, Expression.Constant(1)))

        Dim includedCheck = Expression.MakeIndex(includedColumnsParam, bitArrayItemPropertyInfo, {Expression.Constant(i)})
        Dim includedCond = Expression.IfThenElse(includedCheck, Expression.Block(includeExpressions), propAssignNull)
        expressions.Add(includedCond)

        i += 1
      Next

      expressions.Add(entityVariable)

      Dim body = Expression.Block({entityVariable}, expressions)

      Dim reader = Expression.Lambda(Of Func(Of IDataReader, Int32, BitArray, Object))(body, parameters)
      Return reader.Compile()
    End Function

    Public Overridable Function CreateContainsPKReader(model As Model, entityType As Type) As Func(Of IDataReader, Int32, Int32(), Boolean)
      Dim readerParam = Expression.Parameter(GetType(IDataRecord), "reader") ' this has to be IDataRecord, otherwise Expression.Call() cannot find the method
      Dim indexParam = Expression.Parameter(GetType(Int32), "index")
      Dim pkOffsetsParam = Expression.Parameter(GetType(Int32()), "pkOffsets")
      Dim parameters = {readerParam, indexParam, pkOffsetsParam}

      Dim entity = model.GetEntity(entityType)
      Dim keyPropertiesCount = entity.GetKeyPropertiesCount()

      If keyPropertiesCount = 0 Then
        Throw New Exception($"Missing PK definition for entity '{entityType}'.")
      End If

      Dim orParts = New Queue(Of Expression)

      For i = 0 To keyPropertiesCount - 1
        Dim offset = Expression.ArrayIndex(pkOffsetsParam, Expression.Constant(i))
        Dim readIndexArg = Expression.Add(indexParam, offset)
        Dim isDBNullCall = Expression.Call(readerParam, "IsDBNull", Nothing, readIndexArg)
        orParts.Enqueue(Expression.Not(isDBNullCall))
      Next

      Dim cond = orParts.Dequeue()

      While 0 < orParts.Count
        cond = Expression.OrElse(cond, orParts.Dequeue())
      End While

      Dim reader = Expression.Lambda(Of Func(Of IDataReader, Int32, Int32(), Boolean))(cond, parameters)
      Return reader.Compile()
    End Function

    Public Overridable Function CreatePKReader(model As Model, entityType As Type) As Func(Of IDataReader, Int32, Int32(), Int32)
      Dim readerParam = Expression.Parameter(GetType(IDataRecord), "reader") ' this has to be IDataRecord, otherwise Expression.Call() cannot find the method
      Dim indexParam = Expression.Parameter(GetType(Int32), "index")
      Dim pkOffsetsParam = Expression.Parameter(GetType(Int32()), "pkOffsets")
      Dim parameters = {readerParam, indexParam, pkOffsetsParam}

      Dim hashCodeVariable = Expression.Variable(GetType(Int32), "hashCode")

      Dim variables = New List(Of ParameterExpression) From {hashCodeVariable}
      Dim expressions = New List(Of Expression)

      Dim dbNullHashCode = Expression.Constant(DBNull.Value.GetHashCode())

      Dim entity = model.GetEntity(entityType)
      Dim keyProperties = entity.GetKeyProperties()

      If keyProperties.Count = 0 Then
        Throw New Exception($"Missing PK definition for entity '{entityType}'.")

      ElseIf keyProperties.Count = 1 Then
        ' return GetHashCode of (single) PK column
        Dim propInfo = keyProperties(0)
        AssignHashCodeToVariable(propInfo.Property, 0, readerParam, indexParam, pkOffsetsParam, hashCodeVariable, dbNullHashCode, expressions)

      ElseIf 8 < keyProperties.Count Then
        Throw New Exception("Maximum of 8 primary key columns is supported.")

      Else
        ' return GetHashCode of ValueTuple that consists of all PK columns
        ' using ValueTuple is just simple workaround until .NET becomes System.HashCode: https://github.com/dotnet/corefx/issues/14354

        Dim partialHashCodeVariables = New ParameterExpression(keyProperties.Count - 1) {}
        Dim typeArguments = New Type(keyProperties.Count - 1) {}

        For i = 0 To keyProperties.Count - 1
          Dim partialHashCodeVariable = Expression.Variable(GetType(Int32))
          Dim propInfo = keyProperties(i)

          AssignHashCodeToVariable(propInfo.Property, i, readerParam, indexParam, pkOffsetsParam, partialHashCodeVariable, dbNullHashCode, expressions)

          partialHashCodeVariables(i) = partialHashCodeVariable
          typeArguments(i) = GetType(Int32)
        Next

        Dim tupleValue = Expression.Call(GetType(ValueTuple), "Create", typeArguments, partialHashCodeVariables)
        Dim hashCode = Expression.Call(tupleValue, "GetHashCode", Nothing)
        Dim assignHashCode = Expression.Assign(hashCodeVariable, hashCode)

        variables.AddRange(partialHashCodeVariables)
        expressions.Add(assignHashCode)
      End If

      expressions.Add(hashCodeVariable)

      Dim body = Expression.Block(variables, expressions)

      Dim reader = Expression.Lambda(Of Func(Of IDataReader, Int32, Int32(), Int32))(body, parameters)
      Return reader.Compile()
    End Function

    Protected Sub AssignHashCodeToVariable(prop As [Property], index As Int32, readerParam As ParameterExpression, indexParam As ParameterExpression, pkOffsetsParam As ParameterExpression, variable As ParameterExpression, dbNullHashCode As Expression, expressions As List(Of Expression))
      Dim offset = Expression.ArrayIndex(pkOffsetsParam, Expression.Constant(index))
      Dim readIndexArg = Expression.Add(indexParam, offset)
      Dim readValueCall = Expression.Call(readerParam, GetReadMethodForType(prop.PropertyType).Method, Nothing, readIndexArg) ' NOTE: Convert can be ignored here
      Dim readValueHashCode = Expression.Call(readValueCall, "GetHashCode", Nothing)
      Dim varAssignReadValueHashCode = Expression.Assign(variable, readValueHashCode)
      Dim varAssignDBNullHashCode = Expression.Assign(variable, dbNullHashCode)
      Dim isDBNullCall = Expression.Call(readerParam, "IsDBNull", Nothing, readIndexArg)
      Dim cond = Expression.IfThenElse(isDBNullCall, varAssignDBNullHashCode, varAssignReadValueHashCode)

      Dim underlyingNullableType = Nullable.GetUnderlyingType(prop.PropertyType)

      If prop.PropertyType Is GetType(String) Then
        expressions.Add(cond)
      ElseIf underlyingNullableType Is Nothing Then
        expressions.Add(varAssignReadValueHashCode)
      Else
        expressions.Add(cond)
      End If
    End Sub

    Public Overridable Function CreateDbGeneratedValuesReader(model As Model, entityType As Type) As Action(Of IDataReader, Int32, Object)
      Dim readerParam = Expression.Parameter(GetType(IDataRecord), "reader") ' this has to be IDataRecord, otherwise Expression.Call() cannot find the method
      Dim indexParam = Expression.Parameter(GetType(Int32), "index")
      Dim entityParam = Expression.Parameter(GetType(Object), "entity")
      Dim parameters = {readerParam, indexParam, entityParam}

      Dim entityVariable = Expression.Variable(entityType, "entityObj")

      Dim expressions = New List(Of Expression)
      expressions.Add(Expression.Assign(entityVariable, Expression.Convert(entityParam, entityType)))

      Dim entity = model.GetEntity(entityType)
      Dim i = 0
      For Each prop In entity.GetIdentityOrDefaultValueProperties().Select(Function(x) x.Property)
        Dim readIndexArg = Expression.Add(indexParam, Expression.Constant(i))
        Dim varProp = Expression.Property(entityVariable, prop.Name)

        Dim readMethodForType = GetReadMethodForType(prop.PropertyType)
        Dim readValueCall As Expression = Expression.Call(readerParam, readMethodForType.Method, Nothing, readIndexArg)

        If readMethodForType.Convert Then
          readValueCall = Expression.Convert(readValueCall, prop.PropertyType)
        End If

        Dim propAssignNull = Expression.Assign(varProp, GetDefaultValue(prop))

        Dim underlyingNullableType = Nullable.GetUnderlyingType(prop.PropertyType)

        If prop.PropertyType Is GetType(String) OrElse prop.PropertyType Is GetType(Byte()) Then
          Dim propAssign = Expression.Assign(varProp, readValueCall)
          Dim isDBNullCall = Expression.Call(readerParam, "IsDBNull", Nothing, readIndexArg)
          Dim cond = Expression.IfThenElse(isDBNullCall, propAssignNull, propAssign)
          expressions.Add(cond)
        ElseIf underlyingNullableType Is Nothing Then
          Dim propAssign = Expression.Assign(varProp, readValueCall)
          expressions.Add(propAssign)
        Else
          Dim isDBNullCall = Expression.Call(readerParam, "IsDBNull", Nothing, readIndexArg)
          Dim nullableConstructor = prop.PropertyType.GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, CallingConventions.HasThis, {underlyingNullableType}, New ParameterModifier(0) {})
          Dim propAssign = Expression.Assign(varProp, Expression.[New](nullableConstructor, readValueCall))
          Dim cond = Expression.IfThenElse(isDBNullCall, propAssignNull, propAssign)
          expressions.Add(cond)
        End If

        i += 1
      Next

      Dim body = Expression.Block({entityVariable}, expressions)

      Dim reader = Expression.Lambda(Of Action(Of IDataReader, Int32, Object))(body, parameters)
      Return reader.Compile()
    End Function

    Protected Function GetDefaultValue(prop As [Property]) As Expression
      Dim type = prop.PropertyType

      If prop.IsRequired Then
        If type Is GetType(String) Then
          Return Expression.Constant("")
        ElseIf type Is GetType(Byte()) Then
          Return Expression.Constant(New Byte() {})
        End If
      End If

      Return Expression.Default(prop.PropertyType)
    End Function

  End Class
End Namespace