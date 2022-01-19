Imports System.Data
Imports System.Data.Common
Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports System.Reflection
Imports Yamo.Metadata

Namespace Infrastructure

  ''' <summary>
  ''' Entity reader factory.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class EntityReaderFactory
    Inherits ReaderFactoryBase

    ''' <summary>
    ''' Creates an entity reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Overridable Function CreateReader(<DisallowNull> dataReaderType As Type, <DisallowNull> model As Model, <DisallowNull> entityType As Type) As Func(Of DbDataReader, Int32, Boolean(), Object)
      Dim readerParam = Expression.Parameter(GetType(DbDataReader), "reader")
      Dim indexParam = Expression.Parameter(GetType(Int32), "index")
      Dim includedColumnsParam = Expression.Parameter(GetType(Boolean()), "includedColumns")
      Dim parameters = {readerParam, indexParam, includedColumnsParam}

      Dim readerVariable = Expression.Variable(dataReaderType, "r")
      Dim entityVariable = Expression.Variable(entityType, "entityObj")

      Dim entity = model.GetEntity(entityType)
      Dim properties = entity.GetProperties()

      Dim expressions = New List(Of Expression)(properties.Count + 3)

      expressions.Add(Expression.Assign(readerVariable, Expression.Convert(readerParam, dataReaderType)))
      expressions.Add(Expression.Assign(entityVariable, Expression.[New](entityType)))

      For i = 0 To properties.Count - 1
        Dim prop = properties(i)

        Dim varProp = Expression.Property(entityVariable, prop.Name)

        Dim underlyingNullableType = Nullable.GetUnderlyingType(prop.PropertyType)

        Dim getMethodForType = GetDbDataReaderGetMethodForType(dataReaderType, prop.PropertyType)
        Dim getValueCall As Expression

        If getMethodForType.IsGeneric Then
          Dim genericType = If(underlyingNullableType Is Nothing, prop.PropertyType, underlyingNullableType)
          getValueCall = Expression.Call(readerVariable, getMethodForType.Method, {genericType}, indexParam)
        Else
          getValueCall = Expression.Call(readerVariable, getMethodForType.Method, Nothing, indexParam)
        End If

        If getMethodForType.Convert Then
          If underlyingNullableType Is Nothing Then
            getValueCall = Expression.Convert(getValueCall, prop.PropertyType)
          Else
            getValueCall = Expression.Convert(getValueCall, underlyingNullableType)
          End If
        End If

        Dim propAssignNull = Expression.Assign(varProp, GetDefaultValue(prop))

        Dim includeExpressions = New Expression(1) {}

        If prop.PropertyType Is GetType(String) OrElse prop.PropertyType Is GetType(Byte()) Then
          Dim propAssign = Expression.Assign(varProp, getValueCall)
          Dim isDBNullCall = Expression.Call(readerVariable, "IsDBNull", Nothing, indexParam)
          Dim isDBNullCond = Expression.IfThenElse(isDBNullCall, propAssignNull, propAssign)
          includeExpressions(0) = isDBNullCond
        ElseIf underlyingNullableType Is Nothing Then
          Dim propAssign = Expression.Assign(varProp, getValueCall)
          includeExpressions(0) = propAssign
        Else
          Dim isDBNullCall = Expression.Call(readerVariable, "IsDBNull", Nothing, indexParam)
          Dim nullableConstructor = prop.PropertyType.GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, CallingConventions.HasThis, {underlyingNullableType}, Array.Empty(Of ParameterModifier)())
          Dim propAssign = Expression.Assign(varProp, Expression.[New](nullableConstructor, getValueCall))
          Dim isDBNullCond = Expression.IfThenElse(isDBNullCall, propAssignNull, propAssign)
          includeExpressions(0) = isDBNullCond
        End If

        includeExpressions(1) = Expression.AddAssign(indexParam, Expression.Constant(1))

        Dim includedCheck = Expression.ArrayIndex(includedColumnsParam, Expression.Constant(i))
        Dim includedCond = Expression.IfThenElse(includedCheck, Expression.Block(includeExpressions), propAssignNull)
        expressions.Add(includedCond)
      Next

      expressions.Add(entityVariable)

      Dim body = Expression.Block({readerVariable, entityVariable}, expressions)

      Dim reader = Expression.Lambda(Of Func(Of DbDataReader, Int32, Boolean(), Object))(body, parameters)
      Return reader.Compile()
    End Function

    ''' <summary>
    ''' Creates a reader which provides information whether result contains primary key value of an entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Overridable Function CreateContainsPKReader(<DisallowNull> dataReaderType As Type, <DisallowNull> model As Model, <DisallowNull> entityType As Type) As Func(Of DbDataReader, Int32, Int32(), Boolean)
      Dim readerParam = Expression.Parameter(GetType(DbDataReader), "reader")
      Dim indexParam = Expression.Parameter(GetType(Int32), "index")
      Dim pkOffsetsParam = Expression.Parameter(GetType(Int32()), "pkOffsets")
      Dim parameters = {readerParam, indexParam, pkOffsetsParam}

      Dim readerVariable = Expression.Variable(dataReaderType, "r")

      Dim expressions = New List(Of Expression)(2)

      expressions.Add(Expression.Assign(readerVariable, Expression.Convert(readerParam, dataReaderType)))

      Dim entity = model.GetEntity(entityType)
      Dim keyPropertiesCount = entity.GetKeyPropertiesCount()

      If keyPropertiesCount = 0 Then
        Throw New Exception($"Missing PK definition for entity '{entityType}'.")
      End If

      Dim orParts = New Queue(Of Expression)

      For i = 0 To keyPropertiesCount - 1
        Dim offset = Expression.ArrayIndex(pkOffsetsParam, Expression.Constant(i))
        Dim readIndexArg = Expression.Add(indexParam, offset)
        Dim isDBNullCall = Expression.Call(readerVariable, "IsDBNull", Nothing, readIndexArg)
        orParts.Enqueue(Expression.Not(isDBNullCall))
      Next

      Dim cond = orParts.Dequeue()

      While 0 < orParts.Count
        cond = Expression.OrElse(cond, orParts.Dequeue())
      End While

      expressions.Add(cond)

      Dim body = Expression.Block({readerVariable}, expressions)

      Dim reader = Expression.Lambda(Of Func(Of DbDataReader, Int32, Int32(), Boolean))(body, parameters)
      Return reader.Compile()
    End Function

    ''' <summary>
    ''' Creates a primary key reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Overridable Function CreatePKReader(<DisallowNull> dataReaderType As Type, <DisallowNull> model As Model, <DisallowNull> entityType As Type) As Func(Of DbDataReader, Int32, Int32(), Object)
      Dim readerParam = Expression.Parameter(GetType(DbDataReader), "reader")
      Dim indexParam = Expression.Parameter(GetType(Int32), "index")
      Dim pkOffsetsParam = Expression.Parameter(GetType(Int32()), "pkOffsets")
      Dim parameters = {readerParam, indexParam, pkOffsetsParam}

      Dim variables = New List(Of ParameterExpression)
      Dim expressions = New List(Of Expression)

      Dim readerVariable = Expression.Variable(dataReaderType, "r")
      variables.Add(readerVariable)

      expressions.Add(Expression.Assign(readerVariable, Expression.Convert(readerParam, dataReaderType)))

      Dim pkVariable = Expression.Variable(GetType(Object), "pk")
      variables.Add(pkVariable)

      Dim entity = model.GetEntity(entityType)
      Dim keyProperties = entity.GetKeyProperties()

      If keyProperties.Count = 0 Then
        Throw New Exception($"Missing PK definition for entity '{entityType}'.")

      ElseIf keyProperties.Count = 1 Then
        ' return (single) PK column
        AssignPKToVariable(readerVariable, indexParam, pkOffsetsParam, pkVariable, expressions)

      ElseIf 8 < keyProperties.Count Then
        Throw New Exception("Maximum of 8 primary key columns is supported.")

      Else
        ' return ValueTuple that consists of all PK columns
        Dim partialPKVariables = New ParameterExpression(keyProperties.Count - 1) {}
        Dim typeArguments = New Type(keyProperties.Count - 1) {}

        For i = 0 To keyProperties.Count - 1
          Dim partialPKVariable = Expression.Variable(GetType(Object))

          AssignPKPartToVariable(i, readerVariable, indexParam, pkOffsetsParam, partialPKVariable, expressions)

          partialPKVariables(i) = partialPKVariable
          typeArguments(i) = GetType(Object)
        Next

        Dim tupleValue = Expression.Call(GetType(ValueTuple), "Create", typeArguments, partialPKVariables)
        Dim assignPK = Expression.Assign(pkVariable, Expression.Convert(tupleValue, GetType(Object)))

        variables.AddRange(partialPKVariables)
        expressions.Add(assignPK)
      End If

      expressions.Add(pkVariable)

      Dim body = Expression.Block(variables, expressions)

      Dim reader = Expression.Lambda(Of Func(Of DbDataReader, Int32, Int32(), Object))(body, parameters)
      Return reader.Compile()
    End Function

    ''' <summary>
    ''' Assign primary key to a variable.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="readerVariable"></param>
    ''' <param name="indexParam"></param>
    ''' <param name="pkOffsetsParam"></param>
    ''' <param name="variable"></param>
    ''' <param name="expressions"></param>
    Protected Sub AssignPKToVariable(<DisallowNull> readerVariable As ParameterExpression, <DisallowNull> indexParam As ParameterExpression, <DisallowNull> pkOffsetsParam As ParameterExpression, <DisallowNull> variable As ParameterExpression, <DisallowNull> expressions As List(Of Expression))
      Dim offset = Expression.ArrayIndex(pkOffsetsParam, Expression.Constant(0))
      Dim readIndexArg = Expression.Add(indexParam, offset)
      Dim readValueCall = Expression.Call(readerVariable, "GetValue", Nothing, readIndexArg)
      Dim varAssignReadValue = Expression.Assign(variable, readValueCall)

      expressions.Add(varAssignReadValue)

      ' because we check for null later, convert any DBNull to null
      Dim isDBNullCall = Expression.Equal(variable, Expression.Constant(DBNull.Value))
      Dim varAssignNullValue = Expression.Assign(variable, Expression.Constant(Nothing))

      Dim cond = Expression.IfThen(isDBNullCall, varAssignNullValue)

      expressions.Add(cond)
    End Sub

    ''' <summary>
    ''' Assing primary key part to a variable.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <param name="readerVariable"></param>
    ''' <param name="indexParam"></param>
    ''' <param name="pkOffsetsParam"></param>
    ''' <param name="variable"></param>
    ''' <param name="expressions"></param>
    Protected Sub AssignPKPartToVariable(index As Int32, <DisallowNull> readerVariable As ParameterExpression, <DisallowNull> indexParam As ParameterExpression, <DisallowNull> pkOffsetsParam As ParameterExpression, <DisallowNull> variable As ParameterExpression, <DisallowNull> expressions As List(Of Expression))
      Dim offset = Expression.ArrayIndex(pkOffsetsParam, Expression.Constant(index))
      Dim readIndexArg = Expression.Add(indexParam, offset)
      Dim readValueCall = Expression.Call(readerVariable, "GetValue", Nothing, readIndexArg)
      Dim varAssignReadValue = Expression.Assign(variable, readValueCall)

      expressions.Add(varAssignReadValue)
    End Sub

    ''' <summary>
    ''' Create reader for values generated by the database.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Overridable Function CreateDbGeneratedValuesReader(<DisallowNull> dataReaderType As Type, <DisallowNull> model As Model, <DisallowNull> entityType As Type) As Action(Of DbDataReader, Int32, Object)
      Dim readerParam = Expression.Parameter(GetType(DbDataReader), "reader")
      Dim indexParam = Expression.Parameter(GetType(Int32), "index")
      Dim entityParam = Expression.Parameter(GetType(Object), "entity")
      Dim parameters = {readerParam, indexParam, entityParam}

      Dim readerVariable = Expression.Variable(dataReaderType, "r")
      Dim entityVariable = Expression.Variable(entityType, "entityObj")

      Dim expressions = New List(Of Expression)

      expressions.Add(Expression.Assign(readerVariable, Expression.Convert(readerParam, dataReaderType)))
      expressions.Add(Expression.Assign(entityVariable, Expression.Convert(entityParam, entityType)))

      Dim entity = model.GetEntity(entityType)
      Dim identityOrDefaultValueProperties = entity.GetIdentityOrDefaultValueProperties()

      For i = 0 To identityOrDefaultValueProperties.Count - 1
        Dim prop = identityOrDefaultValueProperties(i)

        Dim getIndexArg = Expression.Add(indexParam, Expression.Constant(i))
        Dim varProp = Expression.Property(entityVariable, prop.Name)

        Dim underlyingNullableType = Nullable.GetUnderlyingType(prop.PropertyType)

        Dim getMethodForType = GetDbDataReaderGetMethodForType(dataReaderType, prop.PropertyType)
        Dim getValueCall As Expression

        If getMethodForType.IsGeneric Then
          Dim genericType = If(underlyingNullableType Is Nothing, prop.PropertyType, underlyingNullableType)
          getValueCall = Expression.Call(readerVariable, getMethodForType.Method, {genericType}, getIndexArg)
        Else
          getValueCall = Expression.Call(readerVariable, getMethodForType.Method, Nothing, getIndexArg)
        End If

        If getMethodForType.Convert Then
          If underlyingNullableType Is Nothing Then
            getValueCall = Expression.Convert(getValueCall, prop.PropertyType)
          Else
            getValueCall = Expression.Convert(getValueCall, underlyingNullableType)
          End If
        End If

        Dim propAssignNull = Expression.Assign(varProp, GetDefaultValue(prop))

        If prop.PropertyType Is GetType(String) OrElse prop.PropertyType Is GetType(Byte()) Then
          Dim propAssign = Expression.Assign(varProp, getValueCall)
          Dim isDBNullCall = Expression.Call(readerVariable, "IsDBNull", Nothing, getIndexArg)
          Dim cond = Expression.IfThenElse(isDBNullCall, propAssignNull, propAssign)
          expressions.Add(cond)
        ElseIf underlyingNullableType Is Nothing Then
          Dim propAssign = Expression.Assign(varProp, getValueCall)
          expressions.Add(propAssign)
        Else
          Dim isDBNullCall = Expression.Call(readerVariable, "IsDBNull", Nothing, getIndexArg)
          Dim nullableConstructor = prop.PropertyType.GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, CallingConventions.HasThis, {underlyingNullableType}, Array.Empty(Of ParameterModifier)())
          Dim propAssign = Expression.Assign(varProp, Expression.[New](nullableConstructor, getValueCall))
          Dim cond = Expression.IfThenElse(isDBNullCall, propAssignNull, propAssign)
          expressions.Add(cond)
        End If
      Next

      Dim body = Expression.Block({readerVariable, entityVariable}, expressions)

      Dim reader = Expression.Lambda(Of Action(Of DbDataReader, Int32, Object))(body, parameters)
      Return reader.Compile()
    End Function

    ''' <summary>
    ''' Gets expression with a default value of property type.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="prop"></param>
    ''' <returns></returns>
    Protected Function GetDefaultValue(<DisallowNull> prop As [Property]) As Expression
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

    ''' <summary>
    ''' Gets DbDataReader GetX method for reading specified type from the database.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Protected Overridable Function GetDbDataReaderGetMethodForType(<DisallowNull> dataReaderType As Type, <DisallowNull> type As Type) As (Method As String, IsGeneric As Boolean, Convert As Boolean)
      Select Case type
        Case GetType(String)
          Return ("GetString", False, False)
        Case GetType(Int16), GetType(Int16?)
          Return ("GetInt16", False, False)
        Case GetType(Int32), GetType(Int32?)
          Return ("GetInt32", False, False)
        Case GetType(Int64), GetType(Int64?)
          Return ("GetInt64", False, False)
        Case GetType(Boolean), GetType(Boolean?)
          Return ("GetBoolean", False, False)
        Case GetType(Guid), GetType(Guid?)
          Return ("GetGuid", False, False)
        Case GetType(DateTime), GetType(DateTime?)
          Return ("GetDateTime", False, False)
        Case GetType(TimeSpan), GetType(TimeSpan?)
          Return ("GetTimeSpan", False, False)
        Case GetType(DateTimeOffset), GetType(DateTimeOffset?)
          Return ("GetDateTimeOffset", False, False)
#If NET6_0_OR_GREATER Then
        Case GetType(DateOnly), GetType(DateOnly?)
          Return ("GetFieldValue", True, False)
        Case GetType(TimeOnly), GetType(TimeOnly?)
          Return ("GetFieldValue", True, False)
#End If
        Case GetType(Decimal), GetType(Decimal?)
          Return ("GetDecimal", False, False)
        Case GetType(Double), GetType(Double?)
          Return ("GetDouble", False, False)
        Case GetType(Single), GetType(Single?)
          Return ("GetFloat", False, False)
        Case GetType(Byte())
          Return ("GetValue", False, True)
        Case GetType(Byte), GetType(Byte?)
          Return ("GetByte", False, False)
        Case Else
          Throw New NotSupportedException($"Reading value of type '{type}' is not supported.")
      End Select
    End Function

  End Class
End Namespace