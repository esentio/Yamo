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
  ''' Post processor factory.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class PostProcessorFactory
    Inherits ReaderFactoryBase

    ''' <summary>
    ''' Creates post processor.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <returns>Returns <see langword="Nothing"/> if post processing is not necessary.</returns>
    Public Shared Function TryCreatePostProcessor(<DisallowNull> sqlResult As SqlResultBase) As <MaybeNull> Action(Of Object)
      If TypeOf sqlResult Is AdHocTypeSqlResult Then
        Return CreatePostProcessorInternal(DirectCast(sqlResult, AdHocTypeSqlResult))

      ElseIf TypeOf sqlResult Is AnonymousTypeSqlResult Then
        Return Nothing

      ElseIf TypeOf sqlResult Is ValueTupleSqlResult Then
        Return Nothing

      ElseIf TypeOf sqlResult Is EntitySqlResult Then
        Return CreatePostProcessorInternal(DirectCast(sqlResult, EntitySqlResult))

      ElseIf TypeOf sqlResult Is ScalarValueSqlResult Then
        Return Nothing

      Else
        Throw New NotSupportedException($"SQL result of type {sqlResult.GetType()} is not supported.")
      End If
    End Function

    ''' <summary>
    ''' Creates post processor.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Private Shared Function CreatePostProcessorInternal(sqlResult As SqlResultBase) As Action(Of Object)
      Dim resultType = sqlResult.ResultType
      Dim actualType = resultType

      Dim isNullable = False
      Dim underlyingNullableType = Nullable.GetUnderlyingType(actualType)

      If underlyingNullableType IsNot Nothing Then
        isNullable = True
        actualType = underlyingNullableType
      End If

      Dim isIhdpmtType = GetType(IHasDbPropertyModifiedTracking).IsAssignableFrom(actualType)
      Dim isIsdlType = GetType(ISupportDbLoad).IsAssignableFrom(actualType)

      If isNullable Then
        If isIhdpmtType OrElse isIsdlType Then
          Return CreatePostProcessorForNullableType(sqlResult, resultType, isIhdpmtType, isIsdlType)
        End If
      Else
        If isIhdpmtType AndAlso isIsdlType Then
          Return AddressOf CallResetDbPropertyModifiedTrackingAndEndLoad
        ElseIf isIhdpmtType Then
          Return AddressOf CallResetDbPropertyModifiedTracking
        ElseIf isIsdlType Then
          Return AddressOf CallEndLoad
        End If
      End If

      Return Nothing
    End Function

    Private Shared Function CreatePostProcessorForNullableType(sqlResult As SqlResultBase, type As Type, isIhdpmtType As Boolean, isIsdlType As Boolean) As Action(Of Object)
      Dim valueParam = Expression.Parameter(GetType(Object), "value")
      Dim parameters = {valueParam}

      Dim valueVar = Expression.Variable(type, "valueCasted")
      Dim variables = {valueVar}

      Dim valueAssign = Expression.Assign(valueVar, Expression.Convert(valueParam, type))

      Dim ifTestExpression = Expression.Property(valueVar, "HasValue")
      Dim ifTrueBlockExpressions = New List(Of Expression)(2)

      If isIhdpmtType Then
        Dim ihdpmtType = GetType(IHasDbPropertyModifiedTracking)
        Dim rdpmtMethodInfo = ihdpmtType.GetMethod(NameOf(IHasDbPropertyModifiedTracking.ResetDbPropertyModifiedTracking))
        Dim rdpmtCast = Expression.Convert(Expression.Property(valueVar, "Value"), ihdpmtType)
        Dim rdpmtCall = Expression.Call(rdpmtCast, rdpmtMethodInfo)
        ifTrueBlockExpressions.Add(rdpmtCall)
      End If

      If isIsdlType Then
        Dim isdlType = GetType(ISupportDbLoad)
        Dim elMethodInfo = isdlType.GetMethod(NameOf(ISupportDbLoad.EndLoad))
        Dim elCast = Expression.Convert(Expression.Property(valueVar, "Value"), isdlType)
        Dim elCall = Expression.Call(elCast, elMethodInfo)
        ifTrueBlockExpressions.Add(elCall)
      End If

      Dim ifExpression = Expression.IfThen(ifTestExpression, Expression.Block(ifTrueBlockExpressions))

      Dim body = Expression.Block(variables, {valueAssign, ifExpression})

      Dim postProcessor = Expression.Lambda(body, parameters)
      Return DirectCast(postProcessor.Compile(), Action(Of Object))
    End Function

    ''' <summary>
    ''' Calls <see cref="IHasDbPropertyModifiedTracking.ResetDbPropertyModifiedTracking"/> and <see cref="ISupportDbLoad.EndLoad"/> methods.
    ''' </summary>
    ''' <param name="value"></param>
    Private Shared Sub CallResetDbPropertyModifiedTrackingAndEndLoad(value As Object)
      DirectCast(value, IHasDbPropertyModifiedTracking).ResetDbPropertyModifiedTracking()
      DirectCast(value, ISupportDbLoad).EndLoad()
    End Sub

    ''' <summary>
    ''' Calls <see cref="IHasDbPropertyModifiedTracking.ResetDbPropertyModifiedTracking"/> method.
    ''' </summary>
    ''' <param name="value"></param>
    Private Shared Sub CallResetDbPropertyModifiedTracking(value As Object)
      DirectCast(value, IHasDbPropertyModifiedTracking).ResetDbPropertyModifiedTracking()
    End Sub

    ''' <summary>
    ''' Calls <see cref="ISupportDbLoad.EndLoad"/> method.
    ''' </summary>
    ''' <param name="value"></param>
    Private Shared Sub CallEndLoad(value As Object)
      DirectCast(value, ISupportDbLoad).EndLoad()
    End Sub

  End Class
End Namespace