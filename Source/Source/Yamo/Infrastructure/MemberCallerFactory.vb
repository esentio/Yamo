Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports System.Reflection

Namespace Infrastructure

  ''' <summary>
  ''' Member caller factory.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class MemberCallerFactory

    ' NOTE: for properties and methods, [Delegate].CreateDelegate seems a bit faster than compiled lambda, but we cannot use generics.
    ' https://www.c-sharpcorner.com/article/boosting-up-the-reflection-performance-in-c-sharp/
    ' https://blog.slaks.net/2015-06-26/code-snippets-fast-property-access-reflection/
    ' https://www.codeproject.com/Articles/14560/Fast-Dynamic-Property-Field-Accessors
    ' https://stackoverflow.com/questions/724143/how-do-i-create-a-delegate-for-a-net-property

    ''' <summary>
    ''' Creates caller.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="valueType"></param>
    ''' <param name="fieldInfo"></param>
    ''' <returns></returns>
    Public Shared Function CreateCaller(<DisallowNull> valueType As Type, <DisallowNull> fieldInfo As FieldInfo) As Func(Of Object, Object)
      Dim valueParam = Expression.Parameter(GetType(Object), "value")
      Dim resultExpression = Expression.Convert(Expression.Field(Expression.Convert(valueParam, valueType), fieldInfo), GetType(Object))
      Dim getter = Expression.Lambda(Of Func(Of Object, Object))(resultExpression, valueParam)
      Return getter.Compile()
    End Function

    ''' <summary>
    ''' Creates static caller.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="valueType"></param>
    ''' <param name="fieldInfo"></param>
    ''' <returns></returns>
    Public Shared Function CreateStaticCaller(<DisallowNull> valueType As Type, <DisallowNull> fieldInfo As FieldInfo) As Func(Of Object)
      Dim resultExpression = Expression.Convert(Expression.Field(Nothing, fieldInfo), GetType(Object))
      Dim getter = Expression.Lambda(Of Func(Of Object))(resultExpression)
      Return getter.Compile()
    End Function

    ''' <summary>
    ''' Creates caller.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="valueType"></param>
    ''' <param name="propertyInfo"></param>
    ''' <returns></returns>
    Public Shared Function CreateCaller(<DisallowNull> valueType As Type, <DisallowNull> propertyInfo As PropertyInfo) As Func(Of Object, Object)
      Dim valueParam = Expression.Parameter(GetType(Object), "value")
      Dim resultExpression = Expression.Convert(Expression.Property(Expression.Convert(valueParam, valueType), propertyInfo), GetType(Object))
      Dim getter = Expression.Lambda(Of Func(Of Object, Object))(resultExpression, valueParam)
      Return getter.Compile()
    End Function

    ''' <summary>
    ''' Creates static caller.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="valueType"></param>
    ''' <param name="propertyInfo"></param>
    ''' <returns></returns>
    Public Shared Function CreateStaticCaller(<DisallowNull> valueType As Type, <DisallowNull> propertyInfo As PropertyInfo) As Func(Of Object)
      Dim resultExpression = Expression.Convert(Expression.Property(Nothing, propertyInfo), GetType(Object))
      Dim getter = Expression.Lambda(Of Func(Of Object))(resultExpression)
      Return getter.Compile()
    End Function

    ''' <summary>
    ''' Creates caller.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="valueType"></param>
    ''' <param name="methodInfo"></param>
    ''' <returns></returns>
    Public Shared Function CreateCaller(<DisallowNull> valueType As Type, <DisallowNull> methodInfo As MethodInfo) As Func(Of Object, Object)
      Dim valueParam = Expression.Parameter(GetType(Object), "value")
      Dim resultExpression = Expression.Convert(Expression.Call(Expression.Convert(valueParam, valueType), methodInfo), GetType(Object))
      Dim getter = Expression.Lambda(Of Func(Of Object, Object))(resultExpression, valueParam)
      Return getter.Compile()
    End Function

    ''' <summary>
    ''' Creates static caller.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="valueType"></param>
    ''' <param name="methodInfo"></param>
    ''' <returns></returns>
    Public Shared Function CreateStaticCaller(<DisallowNull> valueType As Type, <DisallowNull> methodInfo As MethodInfo) As Func(Of Object)
      Dim resultExpression = Expression.Convert(Expression.Call(methodInfo), GetType(Object))
      Dim getter = Expression.Lambda(Of Func(Of Object))(resultExpression)
      Return getter.Compile()
    End Function

  End Class
End Namespace