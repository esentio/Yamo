Imports System.Data
Imports System.Linq.Expressions
Imports System.Reflection
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Metadata

Namespace Infrastructure

  Public Class CustomResultReaderFactory
    Inherits ReaderFactoryBase

    Public Shared Function CreateResultFactory(node As Expression, customEntities As CustomSqlEntity()) As Object
      Dim readerParam = Expression.Parameter(GetType(IDataReader), "reader") ' this has to be IDataRecord, otherwise Expression.Call() cannot find the method
      Dim customEntityInfosParam = Expression.Parameter(GetType(CustomEntityReadInfo()), "customEntityInfos")
      Dim parameters = {readerParam, customEntityInfosParam}

      Dim variables = New List(Of ParameterExpression)
      Dim expressions = New List(Of Expression)

      Dim arguments = New List(Of Expression)

      For i = 0 To customEntities.Length - 1
        Dim customEntity = customEntities(i)
        Dim type = customEntity.Type

        Dim valueVar = Expression.Variable(type, $"value{i.ToString(Globalization.CultureInfo.InvariantCulture)}")
        variables.Add(valueVar)

        Dim customEntityInfoVar = Expression.Variable(GetType(CustomEntityReadInfo), $"readInfo{i.ToString(Globalization.CultureInfo.InvariantCulture)}")
        variables.Add(customEntityInfoVar)

        Dim customEntityInfoIndex = Expression.ArrayIndex(customEntityInfosParam, Expression.Constant(i))
        expressions.Add(Expression.Assign(customEntityInfoVar, customEntityInfoIndex))

        Dim readerIndexProp = Expression.Property(customEntityInfoVar, NameOf(CustomEntityReadInfo.ReaderIndex))

        If customEntity.IsEntity Then
          Dim entityProp = Expression.Property(customEntityInfoVar, NameOf(CustomEntityReadInfo.Entity))
          Dim includedColumnsProp = Expression.Property(entityProp, NameOf(SqlEntity.IncludedColumns))
          Dim pkOffsetsProp = Expression.Property(customEntityInfoVar, NameOf(CustomEntityReadInfo.PKOffsets))

          Dim containsPKReaderProp = Expression.Property(customEntityInfoVar, NameOf(CustomEntityReadInfo.ContainsPKReader))
          Dim containsPKReaderType = GetType(Func(Of, , ,)).MakeGenericType(GetType(IDataReader), GetType(Int32), GetType(Int32()), GetType(Boolean))
          Dim containsPKReaderInvokeMethodInfo = containsPKReaderType.GetMethod("Invoke", BindingFlags.Public Or BindingFlags.Instance)
          Dim containsPKReaderCall = Expression.Call(containsPKReaderProp, containsPKReaderInvokeMethodInfo, readerParam, readerIndexProp, pkOffsetsProp)

          Dim entityReaderProp = Expression.Property(customEntityInfoVar, NameOf(CustomEntityReadInfo.EntityReader))
          Dim entityReaderType = GetType(Func(Of, , ,)).MakeGenericType(GetType(IDataReader), GetType(Int32), GetType(BitArray), GetType(Object))
          Dim entityReaderInvokeMethodInfo = entityReaderType.GetMethod("Invoke", BindingFlags.Public Or BindingFlags.Instance)
          Dim entityReaderCall = Expression.Call(entityReaderProp, entityReaderInvokeMethodInfo, readerParam, readerIndexProp, includedColumnsProp)
          Dim entityReaderCallCast = Expression.Convert(entityReaderCall, type)

          Dim valueAssign = Expression.Assign(valueVar, entityReaderCallCast)
          Dim valueAssignNull = Expression.Assign(valueVar, Expression.Default(type))

          Dim valueAssignBlock As Expression

          Dim ihpmtType = GetType(IHasPropertyModifiedTracking)
          If ihpmtType.IsAssignableFrom(type) Then
            Dim rpmtMethodInfo = ihpmtType.GetMethod(NameOf(IHasPropertyModifiedTracking.ResetPropertyModifiedTracking))
            Dim rpmtCast = Expression.Convert(valueVar, ihpmtType)
            Dim rpmtCall = Expression.Call(rpmtCast, rpmtMethodInfo)

            valueAssignBlock = Expression.Block(valueAssign, rpmtCall)
          Else
            valueAssignBlock = valueAssign
          End If

          expressions.Add(Expression.IfThenElse(containsPKReaderCall, valueAssignBlock, valueAssignNull))
        Else
          Dim valueTypeReaderProp = Expression.Property(customEntityInfoVar, NameOf(CustomEntityReadInfo.ValueTypeReader))
          Dim valueTypeReaderType = GetType(Func(Of, , )).MakeGenericType(GetType(IDataReader), GetType(Int32), type)
          Dim valueTypeReaderCast = Expression.Convert(valueTypeReaderProp, valueTypeReaderType)
          Dim valueTypeReaderInvokeMethodInfo = valueTypeReaderType.GetMethod("Invoke", BindingFlags.Public Or BindingFlags.Instance)
          Dim valueTypeReaderCall = Expression.Call(valueTypeReaderCast, valueTypeReaderInvokeMethodInfo, readerParam, readerIndexProp)

          Dim valueAssign = Expression.Assign(valueVar, valueTypeReaderCall)
          expressions.Add(valueAssign)
        End If

        arguments.Add(valueVar)
      Next

      If node.NodeType = ExpressionType.New Then
        ' ValueTuple or anonymous type
        Dim newExp = Expression.[New](DirectCast(node, NewExpression).Constructor, arguments)
        expressions.Add(newExp)
      Else
        ' single value or entity
        expressions.Add(variables(0))
      End If

      Dim body = Expression.Block(variables, expressions)

      Dim reader = Expression.Lambda(body, parameters)
      Return reader.Compile()
    End Function

  End Class
End Namespace