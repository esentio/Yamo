﻿Imports System.Linq.Expressions
Imports System.Reflection

Namespace Infrastructure

  ' TODO: SIP - add documentation to this class.
  Public Class MemberCallerFactory

    ' NOTE: for properties and methods, [Delegate].CreateDelegate seems a bit faster than compiled lambda, but we cannot use generics.
    ' https://www.c-sharpcorner.com/article/boosting-up-the-reflection-performance-in-c-sharp/
    ' https://blog.slaks.net/2015-06-26/code-snippets-fast-property-access-reflection/
    ' https://www.codeproject.com/Articles/14560/Fast-Dynamic-Property-Field-Accessors
    ' https://stackoverflow.com/questions/724143/how-do-i-create-a-delegate-for-a-net-property

    Public Shared Function CreateCaller(valueType As Type, fieldInfo As FieldInfo) As Func(Of Object, Object)
      Dim valueParam = Expression.Parameter(GetType(Object), "value")
      Dim resultExpression = Expression.Convert(Expression.Field(Expression.Convert(valueParam, valueType), fieldInfo), GetType(Object))
      Dim getter = Expression.Lambda(Of Func(Of Object, Object))(resultExpression, valueParam)
      Return getter.Compile()
    End Function

    Public Shared Function CreateStaticCaller(valueType As Type, fieldInfo As FieldInfo) As Func(Of Object)
      Dim resultExpression = Expression.Convert(Expression.Field(Nothing, fieldInfo), GetType(Object))
      Dim getter = Expression.Lambda(Of Func(Of Object))(resultExpression)
      Return getter.Compile()
    End Function

    Public Shared Function CreateCaller(valueType As Type, propertyInfo As PropertyInfo) As Func(Of Object, Object)
      Dim valueParam = Expression.Parameter(GetType(Object), "value")
      Dim resultExpression = Expression.Convert(Expression.Property(Expression.Convert(valueParam, valueType), propertyInfo), GetType(Object))
      Dim getter = Expression.Lambda(Of Func(Of Object, Object))(resultExpression, valueParam)
      Return getter.Compile()
    End Function

    Public Shared Function CreateStaticCaller(valueType As Type, propertyInfo As PropertyInfo) As Func(Of Object)
      Dim resultExpression = Expression.Convert(Expression.Property(Nothing, propertyInfo), GetType(Object))
      Dim getter = Expression.Lambda(Of Func(Of Object))(resultExpression)
      Return getter.Compile()
    End Function

    Public Shared Function CreateCaller(valueType As Type, methodInfo As MethodInfo) As Func(Of Object, Object)
      Dim valueParam = Expression.Parameter(GetType(Object), "value")
      Dim resultExpression = Expression.Convert(Expression.Call(Expression.Convert(valueParam, valueType), methodInfo), GetType(Object))
      Dim getter = Expression.Lambda(Of Func(Of Object, Object))(resultExpression, valueParam)
      Return getter.Compile()
    End Function

    Public Shared Function CreateStaticCaller(valueType As Type, methodInfo As MethodInfo) As Func(Of Object)
      Dim resultExpression = Expression.Convert(Expression.Call(methodInfo), GetType(Object))
      Dim getter = Expression.Lambda(Of Func(Of Object))(resultExpression)
      Return getter.Compile()
    End Function

  End Class
End Namespace