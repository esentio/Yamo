Imports System.Data
Imports System.Data.Common
Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports System.Reflection
Imports Yamo.Internal
Imports Yamo.Internal.Helpers
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Metadata

Namespace Infrastructure

  ''' <summary>
  ''' SQL result reader factory.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SqlResultReaderFactory
    Inherits ReaderFactoryBase

    ''' <summary>
    ''' Creates result factory.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Public Shared Function CreateResultFactory(<DisallowNull> sqlResult As SqlResultBase) As Object
      If TypeOf sqlResult Is AdHocTypeSqlResult Then
        Return CreateResultFactory(DirectCast(sqlResult, AdHocTypeSqlResult))

      ElseIf TypeOf sqlResult Is AnonymousTypeSqlResult Then
        Return CreateResultFactory(DirectCast(sqlResult, AnonymousTypeSqlResult))

      ElseIf TypeOf sqlResult Is ValueTupleSqlResult Then
        Return CreateResultFactory(DirectCast(sqlResult, ValueTupleSqlResult))

      ElseIf TypeOf sqlResult Is EntitySqlResult Then
        Return CreateResultFactory(DirectCast(sqlResult, EntitySqlResult))

      ElseIf TypeOf sqlResult Is ScalarValueSqlResult Then
        Return CreateResultFactory(DirectCast(sqlResult, ScalarValueSqlResult))

      Else
        Throw New NotSupportedException($"SQL result of type {sqlResult.GetType()} is not supported.")
      End If
    End Function

    ''' <summary>
    ''' Creates wrapper for result factory that converts value type result to an object.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="resultType"></param>
    ''' <param name="readerFunction"></param>
    ''' <returns></returns>
    Public Shared Function CreateValueTypeToObjectResultFactoryWrapper(<DisallowNull> resultType As Type, <DisallowNull> readerFunction As Object) As Func(Of DbDataReader, ReaderDataBase, Object)
      Dim readerParam = Expression.Parameter(GetType(DbDataReader), "reader")
      Dim readerDataParam = Expression.Parameter(GetType(ReaderDataBase), "readerData")
      Dim parameters = {readerParam, readerDataParam}

      Dim variables = New List(Of ParameterExpression)
      Dim expressions = New List(Of Expression)

      Dim readerFunctionType = GetType(Func(Of , , )).MakeGenericType(GetType(DbDataReader), GetType(ReaderDataBase), resultType)
      Dim readerFuncVar = Expression.Variable(readerFunctionType, "readerFunc")
      variables.Add(readerFuncVar)
      expressions.Add(Expression.Assign(readerFuncVar, Expression.Convert(Expression.Constant(readerFunction), readerFunctionType)))

      Dim invokeMethodInfo = readerFunctionType.GetMethod("Invoke", BindingFlags.Public Or BindingFlags.Instance)
      Dim invokeCall = Expression.Call(readerFuncVar, invokeMethodInfo, readerParam, readerDataParam)

      expressions.Add(Expression.Convert(invokeCall, GetType(Object)))

      Dim body = Expression.Block(variables, expressions)

      Dim wrapper = Expression.Lambda(body, parameters)
      Return DirectCast(wrapper.Compile(), Func(Of DbDataReader, ReaderDataBase, Object))
    End Function

    ''' <summary>
    ''' Creates result factory.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Private Shared Function CreateResultFactory(sqlResult As AdHocTypeSqlResult) As Object
      Dim readerParam = Expression.Parameter(GetType(DbDataReader), "reader")
      Dim readerDataParam = Expression.Parameter(GetType(ReaderDataBase), "readerData")
      Dim parameters = {readerParam, readerDataParam}

      Dim variables = New List(Of ParameterExpression)
      Dim expressions = New List(Of Expression)
      Dim ctorArguments = New List(Of Expression)
      Dim memberBinds As List(Of MemberBinding) = Nothing

      Dim readerDataVar = AddReaderDataVar(GetType(AdHocTypeSqlResultReaderData), "readerDataVar", readerDataParam, variables, expressions)

      For i = 0 To sqlResult.CtorArguments.Length - 1
        Dim sqlResultItem = sqlResult.CtorArguments(i)

        Dim readerDataItemVarType = GetReaderDataType(sqlResultItem)
        Dim readerDataItemVarName = $"readerDataCtorVar{i.ToString(Globalization.CultureInfo.InvariantCulture)}"
        Dim readerDataItemSource = Expression.Convert(Expression.ArrayIndex(Expression.Property(readerDataVar, NameOf(AdHocTypeSqlResultReaderData.CtorArguments)), Expression.Constant(i)), readerDataItemVarType)
        Dim readerDataItemVar = AddReaderDataVar(readerDataItemVarType, readerDataItemVarName, readerDataItemSource, variables, expressions)

        Dim valueVar = Expression.Variable(sqlResultItem.ResultType, $"ctorValue{i.ToString(Globalization.CultureInfo.InvariantCulture)}")
        variables.Add(valueVar)

        AddSqlResultReads(readerParam, readerDataItemVar, valueVar, sqlResultItem, expressions, ctorArguments)
      Next

      If sqlResult.HasMemberInits Then
        Dim memberInits = New List(Of Expression)
        memberBinds = New List(Of MemberBinding)

        For i = 0 To sqlResult.MemberInits.Length - 1
          Dim sqlResultItem = sqlResult.MemberInits(i)

          Dim readerDataItemVarType = GetReaderDataType(sqlResultItem)
          Dim readerDataItemVarName = $"readerDataMemberInitVar{i.ToString(Globalization.CultureInfo.InvariantCulture)}"
          Dim readerDataItemSource = Expression.Convert(Expression.ArrayIndex(Expression.Property(readerDataVar, NameOf(AdHocTypeSqlResultReaderData.MemberInits)), Expression.Constant(i)), readerDataItemVarType)
          Dim readerDataItemVar = AddReaderDataVar(readerDataItemVarType, readerDataItemVarName, readerDataItemSource, variables, expressions)

          Dim valueVar = Expression.Variable(sqlResultItem.ResultType, $"memberInitValue{i.ToString(Globalization.CultureInfo.InvariantCulture)}")
          variables.Add(valueVar)

          AddSqlResultReads(readerParam, readerDataItemVar, valueVar, sqlResultItem, expressions, memberInits)

          memberBinds.Add(Expression.Bind(sqlResult.Members(i), memberInits(i)))
        Next
      End If

      Dim resultType = sqlResult.ResultType
      Dim constructorType = resultType
      Dim isNullable = False
      Dim underlyingNullableType = Nullable.GetUnderlyingType(sqlResult.ResultType)

      If underlyingNullableType IsNot Nothing Then
        isNullable = True
        constructorType = underlyingNullableType
      End If

      Dim resultConstructorInfo = sqlResult.Constructor
      Dim constructor = If(resultConstructorInfo Is Nothing, Expression.[New](constructorType), Expression.[New](resultConstructorInfo, ctorArguments))

      Dim result As Expression = constructor

      If sqlResult.HasMemberInits Then
        result = Expression.MemberInit(constructor, memberBinds)
      End If

      If isNullable Then
        Dim nullableConstructorInfo = resultType.GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, CallingConventions.HasThis, {underlyingNullableType}, Array.Empty(Of ParameterModifier)())
        result = Expression.[New](nullableConstructorInfo, result)
      End If

      expressions.Add(result)

      Dim body = Expression.Block(variables, expressions)

      Dim reader = Expression.Lambda(body, parameters)
      Return reader.Compile()
    End Function

    ''' <summary>
    ''' Creates result factory.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Private Shared Function CreateResultFactory(sqlResult As AnonymousTypeSqlResult) As Object
      Dim readerParam = Expression.Parameter(GetType(DbDataReader), "reader")
      Dim readerDataParam = Expression.Parameter(GetType(ReaderDataBase), "readerData")
      Dim parameters = {readerParam, readerDataParam}

      Dim variables = New List(Of ParameterExpression)
      Dim expressions = New List(Of Expression)
      Dim ctorArguments = New List(Of Expression)

      Dim readerDataVar = AddReaderDataVar(GetType(AnonymousTypeSqlResultReaderData), "readerDataVar", readerDataParam, variables, expressions)

      For i = 0 To sqlResult.Items.Length - 1
        Dim sqlResultItem = sqlResult.Items(i)

        Dim readerDataItemVarType = GetReaderDataType(sqlResultItem)
        Dim readerDataItemVarName = $"readerDataVar{i.ToString(Globalization.CultureInfo.InvariantCulture)}"
        Dim readerDataItemSource = Expression.Convert(Expression.ArrayIndex(Expression.Property(readerDataVar, NameOf(AnonymousTypeSqlResultReaderData.Items)), Expression.Constant(i)), readerDataItemVarType)
        Dim readerDataItemVar = AddReaderDataVar(readerDataItemVarType, readerDataItemVarName, readerDataItemSource, variables, expressions)

        Dim valueVar = Expression.Variable(sqlResultItem.ResultType, $"value{i.ToString(Globalization.CultureInfo.InvariantCulture)}")
        variables.Add(valueVar)

        AddSqlResultReads(readerParam, readerDataItemVar, valueVar, sqlResultItem, expressions, ctorArguments)
      Next

      Dim resultConstructorInfo = sqlResult.ResultType.GetConstructors()(0)
      expressions.Add(Expression.[New](resultConstructorInfo, ctorArguments))

      Dim body = Expression.Block(variables, expressions)

      Dim reader = Expression.Lambda(body, parameters)
      Return reader.Compile()
    End Function

    ''' <summary>
    ''' Creates result factory.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Private Shared Function CreateResultFactory(sqlResult As ValueTupleSqlResult) As Object
      Dim readerParam = Expression.Parameter(GetType(DbDataReader), "reader")
      Dim readerDataParam = Expression.Parameter(GetType(ReaderDataBase), "readerData")
      Dim parameters = {readerParam, readerDataParam}

      Dim variables = New List(Of ParameterExpression)
      Dim expressions = New List(Of Expression)
      Dim ctorArguments = New List(Of Expression)

      Dim readerDataVar = AddReaderDataVar(GetType(ValueTupleSqlResultReaderData), "readerDataVar", readerDataParam, variables, expressions)

      For i = 0 To sqlResult.Items.Length - 1
        Dim sqlResultItem = sqlResult.Items(i)

        Dim readerDataItemVarType = GetReaderDataType(sqlResultItem)
        Dim readerDataItemVarName = $"readerDataVar{i.ToString(Globalization.CultureInfo.InvariantCulture)}"
        Dim readerDataItemSource = Expression.Convert(Expression.ArrayIndex(Expression.Property(readerDataVar, NameOf(ValueTupleSqlResultReaderData.Items)), Expression.Constant(i)), readerDataItemVarType)
        Dim readerDataItemVar = AddReaderDataVar(readerDataItemVarType, readerDataItemVarName, readerDataItemSource, variables, expressions)

        Dim valueVar = Expression.Variable(sqlResultItem.ResultType, $"value{i.ToString(Globalization.CultureInfo.InvariantCulture)}")
        variables.Add(valueVar)

        AddSqlResultReads(readerParam, readerDataItemVar, valueVar, sqlResultItem, expressions, ctorArguments)
      Next

      Dim valueTupleTypeInfo = Helpers.Types.GetValueTupleTypeInfo(sqlResult.ResultType)

      expressions.Add(CreateValueTupleConstructor(valueTupleTypeInfo.CtorInfos, ctorArguments, 0, 0))

      Dim body = Expression.Block(variables, expressions)

      Dim reader = Expression.Lambda(body, parameters)
      Return reader.Compile()
    End Function

    ''' <summary>
    ''' Creates result factory.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Private Shared Function CreateResultFactory(sqlResult As EntitySqlResult) As Object
      Dim readerParam = Expression.Parameter(GetType(DbDataReader), "reader")
      Dim readerDataParam = Expression.Parameter(GetType(ReaderDataBase), "readerData")
      Dim parameters = {readerParam, readerDataParam}

      Dim variables = New List(Of ParameterExpression)
      Dim expressions = New List(Of Expression)
      Dim ctorArguments = New List(Of Expression)

      Dim readerDataVar = AddReaderDataVar(GetType(EntitySqlResultReaderData), "readerDataVar", readerDataParam, variables, expressions)

      Dim valueVar = Expression.Variable(sqlResult.ResultType, "value")
      variables.Add(valueVar)

      AddSqlResultReads(readerParam, readerDataVar, valueVar, sqlResult, expressions, ctorArguments)

      expressions.Add(valueVar)

      Dim body = Expression.Block(variables, expressions)

      Dim reader = Expression.Lambda(body, parameters)
      Return reader.Compile()
    End Function

    ''' <summary>
    ''' Creates result factory.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Private Shared Function CreateResultFactory(sqlResult As ScalarValueSqlResult) As Object
      Dim readerParam = Expression.Parameter(GetType(DbDataReader), "reader")
      Dim readerDataParam = Expression.Parameter(GetType(ReaderDataBase), "readerData")
      Dim parameters = {readerParam, readerDataParam}

      Dim variables = New List(Of ParameterExpression)
      Dim expressions = New List(Of Expression)
      Dim ctorArguments = New List(Of Expression)

      Dim readerDataVar = AddReaderDataVar(GetType(ScalarValueSqlResultReaderData), "readerDataVar", readerDataParam, variables, expressions)

      Dim valueVar = Expression.Variable(sqlResult.ResultType, "value")
      variables.Add(valueVar)

      AddSqlResultReads(readerParam, readerDataVar, valueVar, sqlResult, expressions, ctorArguments)

      expressions.Add(valueVar)

      Dim body = Expression.Block(variables, expressions)

      Dim reader = Expression.Lambda(body, parameters)
      Return reader.Compile()
    End Function

    ''' <summary>
    ''' Get <see cref="ReaderDataBase"/> type for <see cref="SqlResultBase"/>.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Private Shared Function GetReaderDataType(sqlResult As SqlResultBase) As Type
      If TypeOf sqlResult Is AdHocTypeSqlResult Then
        Return GetType(AdHocTypeSqlResultReaderData)

      ElseIf TypeOf sqlResult Is AnonymousTypeSqlResult Then
        Return GetType(AnonymousTypeSqlResultReaderData)

      ElseIf TypeOf sqlResult Is ValueTupleSqlResult Then
        Return GetType(ValueTupleSqlResultReaderData)

      ElseIf TypeOf sqlResult Is EntitySqlResult Then
        Return GetType(EntitySqlResultReaderData)

      ElseIf TypeOf sqlResult Is ScalarValueSqlResult Then
        Return GetType(ScalarValueSqlResultReaderData)

      Else
        Throw New NotSupportedException($"SQL result of type {sqlResult.GetType()} is not supported.")
      End If
    End Function

    ''' <summary>
    ''' Adds code for read info variable.
    ''' </summary>
    ''' <param name="readerDataType"></param>
    ''' <param name="variableName"></param>
    ''' <param name="source"></param>
    ''' <param name="variables"></param>
    ''' <param name="expressions"></param>
    ''' <returns></returns>
    Private Shared Function AddReaderDataVar(readerDataType As Type, variableName As String, source As Expression, variables As List(Of ParameterExpression), expressions As List(Of Expression)) As ParameterExpression
      Dim readerDataVar = Expression.Variable(readerDataType, variableName)
      variables.Add(readerDataVar)
      expressions.Add(Expression.Assign(readerDataVar, Expression.Convert(source, readerDataType)))
      Return readerDataVar
    End Function

    ''' <summary>
    ''' Adds code to read <see cref="SqlResultBase"/> value.
    ''' </summary>
    ''' <param name="readerParam"></param>
    ''' <param name="readerDataVar"></param>
    ''' <param name="valueVar"></param>
    ''' <param name="sqlResult"></param>
    ''' <param name="expressions"></param>
    ''' <param name="items"></param>
    Private Shared Sub AddSqlResultReads(readerParam As ParameterExpression, readerDataVar As ParameterExpression, valueVar As ParameterExpression, sqlResult As SqlResultBase, expressions As List(Of Expression), items As List(Of Expression))
      Dim type = sqlResult.ResultType

      Dim readerIndexProp = Expression.Property(readerDataVar, NameOf(ReaderDataBase.ReaderIndex))

      If TypeOf sqlResult Is EntitySqlResult Then
        Dim entityProp = Expression.Property(readerDataVar, NameOf(EntitySqlResultReaderData.Entity))
        Dim includedColumnsProp = Expression.Property(entityProp, NameOf(SqlEntityBase.IncludedColumns))
        Dim pkOffsetsProp = Expression.Property(readerDataVar, NameOf(EntitySqlResultReaderData.PKOffsets))

        Dim containsPKReaderProp = Expression.Property(readerDataVar, NameOf(EntitySqlResultReaderData.ContainsPKReader))
        Dim containsPKReaderType = GetType(Func(Of, , ,)).MakeGenericType(GetType(DbDataReader), GetType(Int32), GetType(Int32()), GetType(Boolean))
        Dim containsPKReaderInvokeMethodInfo = containsPKReaderType.GetMethod("Invoke", BindingFlags.Public Or BindingFlags.Instance)
        Dim containsPKReaderCall = Expression.Call(containsPKReaderProp, containsPKReaderInvokeMethodInfo, readerParam, readerIndexProp, pkOffsetsProp)

        Dim entityReaderProp = Expression.Property(readerDataVar, NameOf(EntitySqlResultReaderData.Reader))
        Dim entityReaderType = GetType(Func(Of, , ,)).MakeGenericType(GetType(DbDataReader), GetType(Int32), GetType(Boolean()), GetType(Object))
        Dim entityReaderInvokeMethodInfo = entityReaderType.GetMethod("Invoke", BindingFlags.Public Or BindingFlags.Instance)
        Dim entityReaderCall = Expression.Call(entityReaderProp, entityReaderInvokeMethodInfo, readerParam, readerIndexProp, includedColumnsProp)
        Dim entityReaderCallCast = Expression.Convert(entityReaderCall, type)

        Dim valueAssign = Expression.Assign(valueVar, entityReaderCallCast)
        Dim valueAssignNull = Expression.Assign(valueVar, Expression.Default(type))

        Dim valueAssignBlockExpressions = New List(Of Expression)(3) From {valueAssign}

        Dim iInitType = GetType(IInitializable)
        If iInitType.IsAssignableFrom(type) Then
          Dim initMethodInfo = iInitType.GetMethod(NameOf(IInitializable.Initialize))
          Dim initCast = Expression.Convert(valueVar, iInitType)
          Dim initCall = Expression.Call(initCast, initMethodInfo)
          valueAssignBlockExpressions.Add(initCall)
        End If

        Dim ihdpmtType = GetType(IHasDbPropertyModifiedTracking)
        If ihdpmtType.IsAssignableFrom(type) Then
          Dim rdpmtMethodInfo = ihdpmtType.GetMethod(NameOf(IHasDbPropertyModifiedTracking.ResetDbPropertyModifiedTracking))
          Dim rdpmtCast = Expression.Convert(valueVar, ihdpmtType)
          Dim rdpmtCall = Expression.Call(rdpmtCast, rdpmtMethodInfo)
          valueAssignBlockExpressions.Add(rdpmtCall)
        End If

        Dim valueAssignBlock = Expression.Block(valueAssignBlockExpressions)

        expressions.Add(Expression.IfThenElse(containsPKReaderCall, valueAssignBlock, valueAssignNull))
      ElseIf TypeOf sqlResult Is ScalarValueSqlResult Then
        Dim scalarValueReaderProp = Expression.Property(readerDataVar, NameOf(ScalarValueSqlResultReaderData.Reader))
        Dim scalarValueReaderType = GetType(Func(Of, , )).MakeGenericType(GetType(DbDataReader), GetType(Int32), type)
        Dim scalarValueReaderCast = Expression.Convert(scalarValueReaderProp, scalarValueReaderType)
        Dim scalarValueReaderInvokeMethodInfo = scalarValueReaderType.GetMethod("Invoke", BindingFlags.Public Or BindingFlags.Instance)
        Dim scalarValueReaderCall = Expression.Call(scalarValueReaderCast, scalarValueReaderInvokeMethodInfo, readerParam, readerIndexProp)

        Dim valueAssign = Expression.Assign(valueVar, scalarValueReaderCall)
        expressions.Add(valueAssign)
      Else
        Throw New NotSupportedException($"SQL result of type {sqlResult.GetType()} is not supported.")
      End If

      items.Add(valueVar)
    End Sub

    ''' <summary>
    ''' Creates value tuple constructor.
    ''' </summary>
    ''' <param name="ctorInfos"></param>
    ''' <param name="arguments"></param>
    ''' <param name="ctorIndex"></param>
    ''' <param name="argumentIndex"></param>
    ''' <returns></returns>
    Private Shared Function CreateValueTupleConstructor(ctorInfos As List(Of CtorInfo), arguments As List(Of Expression), ctorIndex As Int32, argumentIndex As Int32) As Expression
      Dim ctorInfo = ctorInfos(ctorIndex)

      If ctorIndex = ctorInfos.Count - 1 Then
        ' last constructor
        Dim args = arguments.Skip(argumentIndex).Take(ctorInfo.ParameterCount)
        Return Expression.[New](ctorInfo.ConstructorInfo, args)
      Else
        Dim take = ctorInfo.ParameterCount - 1
        Dim args = arguments.Skip(argumentIndex).Take(take).ToList()
        args.Add(CreateValueTupleConstructor(ctorInfos, arguments, ctorIndex + 1, argumentIndex + take))
        Return Expression.[New](ctorInfo.ConstructorInfo, args)
      End If
    End Function

  End Class
End Namespace