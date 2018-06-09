Imports System.Reflection
Imports Yamo.Infrastructure

Namespace Internal

  Public Class MemberCallerCache

    ' NOTE: caching as it is now will prevent unloading collectible assemblies if type from such assembly is used. Do we care about this?
    ' https://stackoverflow.com/questions/6601502/caching-reflection-data
    ' https://msdn.microsoft.com/en-us/library/dd554932(VS.100).aspx

    Private Shared m_Instance As MemberCallerCache

    Private m_FieldCallers As Dictionary(Of (Type, FieldInfo), Func(Of Object, Object))

    Private m_StaticFieldCallers As Dictionary(Of (Type, FieldInfo), Func(Of Object))

    Private m_PropertyCallers As Dictionary(Of (Type, PropertyInfo), Func(Of Object, Object))

    Private m_StaticPropertyCallers As Dictionary(Of (Type, PropertyInfo), Func(Of Object))

    Private m_MethodCallers As Dictionary(Of (Type, MethodInfo), Func(Of Object, Object))

    Private m_StaticMethodCallers As Dictionary(Of (Type, MethodInfo), Func(Of Object))

    Shared Sub New()
      m_Instance = New MemberCallerCache
    End Sub

    Private Sub New()
      m_FieldCallers = New Dictionary(Of (Type, FieldInfo), Func(Of Object, Object))
      m_StaticFieldCallers = New Dictionary(Of (Type, FieldInfo), Func(Of Object))
      m_PropertyCallers = New Dictionary(Of (Type, PropertyInfo), Func(Of Object, Object))
      m_StaticPropertyCallers = New Dictionary(Of (Type, PropertyInfo), Func(Of Object))
      m_MethodCallers = New Dictionary(Of (Type, MethodInfo), Func(Of Object, Object))
      m_StaticMethodCallers = New Dictionary(Of (Type, MethodInfo), Func(Of Object))
    End Sub

    Public Shared Function GetFieldCaller(type As Type, fieldInfo As FieldInfo) As Func(Of Object, Object)
      Return m_Instance.GetOrCreateFieldCaller(type, fieldInfo)
    End Function

    Public Shared Function GetStaticFieldCaller(type As Type, fieldInfo As FieldInfo) As Func(Of Object)
      Return m_Instance.GetOrCreateStaticFieldCaller(type, fieldInfo)
    End Function

    Public Shared Function GetPropertyCaller(type As Type, propertyInfo As PropertyInfo) As Func(Of Object, Object)
      Return m_Instance.GetOrCreatePropertyCaller(type, propertyInfo)
    End Function

    Public Shared Function GetStaticPropertyCaller(type As Type, propertyInfo As PropertyInfo) As Func(Of Object)
      Return m_Instance.GetOrCreateStaticPropertyCaller(type, propertyInfo)
    End Function

    Public Shared Function GetMethodCaller(type As Type, methodInfo As MethodInfo) As Func(Of Object, Object)
      Return m_Instance.GetOrCreateMethodCaller(type, methodInfo)
    End Function

    Public Shared Function GetStaticMethodCaller(type As Type, methodInfo As MethodInfo) As Func(Of Object)
      Return m_Instance.GetOrCreateStaticMethodCaller(type, methodInfo)
    End Function

    Private Function GetOrCreateFieldCaller(type As Type, fieldInfo As FieldInfo) As Func(Of Object, Object)
      Dim caller As Func(Of Object, Object) = Nothing

      Dim key = (type, fieldInfo)

      SyncLock m_FieldCallers
        m_FieldCallers.TryGetValue(key, caller)
      End SyncLock

      If caller Is Nothing Then
        caller = MemberCallerFactory.CreateCaller(type, fieldInfo)
      Else
        Return caller
      End If

      SyncLock m_FieldCallers
        m_FieldCallers(key) = caller
      End SyncLock

      Return caller
    End Function

    Private Function GetOrCreateStaticFieldCaller(type As Type, fieldInfo As FieldInfo) As Func(Of Object)
      Dim caller As Func(Of Object) = Nothing

      Dim key = (type, fieldInfo)

      SyncLock m_StaticFieldCallers
        m_StaticFieldCallers.TryGetValue(key, caller)
      End SyncLock

      If caller Is Nothing Then
        caller = MemberCallerFactory.CreateStaticCaller(type, fieldInfo)
      Else
        Return caller
      End If

      SyncLock m_StaticFieldCallers
        m_StaticFieldCallers(key) = caller
      End SyncLock

      Return caller
    End Function

    Private Function GetOrCreatePropertyCaller(type As Type, propertyInfo As PropertyInfo) As Func(Of Object, Object)
      Dim caller As Func(Of Object, Object) = Nothing

      Dim key = (type, propertyInfo)

      SyncLock m_PropertyCallers
        m_PropertyCallers.TryGetValue(key, caller)
      End SyncLock

      If caller Is Nothing Then
        caller = MemberCallerFactory.CreateCaller(type, propertyInfo)
      Else
        Return caller
      End If

      SyncLock m_PropertyCallers
        m_PropertyCallers(key) = caller
      End SyncLock

      Return caller
    End Function

    Private Function GetOrCreateStaticPropertyCaller(type As Type, propertyInfo As PropertyInfo) As Func(Of Object)
      Dim caller As Func(Of Object) = Nothing

      Dim key = (type, propertyInfo)

      SyncLock m_StaticPropertyCallers
        m_StaticPropertyCallers.TryGetValue(key, caller)
      End SyncLock

      If caller Is Nothing Then
        caller = MemberCallerFactory.CreateStaticCaller(type, propertyInfo)
      Else
        Return caller
      End If

      SyncLock m_StaticPropertyCallers
        m_StaticPropertyCallers(key) = caller
      End SyncLock

      Return caller
    End Function

    Private Function GetOrCreateMethodCaller(type As Type, methodInfo As MethodInfo) As Func(Of Object, Object)
      Dim caller As Func(Of Object, Object) = Nothing

      Dim key = (type, methodInfo)

      SyncLock m_MethodCallers
        m_MethodCallers.TryGetValue(key, caller)
      End SyncLock

      If caller Is Nothing Then
        caller = MemberCallerFactory.CreateCaller(type, methodInfo)
      Else
        Return caller
      End If

      SyncLock m_MethodCallers
        m_MethodCallers(key) = caller
      End SyncLock

      Return caller
    End Function

    Private Function GetOrCreateStaticMethodCaller(type As Type, methodInfo As MethodInfo) As Func(Of Object)
      Dim caller As Func(Of Object) = Nothing

      Dim key = (type, methodInfo)

      SyncLock m_StaticMethodCallers
        m_StaticMethodCallers.TryGetValue(key, caller)
      End SyncLock

      If caller Is Nothing Then
        caller = MemberCallerFactory.CreateStaticCaller(type, methodInfo)
      Else
        Return caller
      End If

      SyncLock m_StaticMethodCallers
        m_StaticMethodCallers(key) = caller
      End SyncLock

      Return caller
    End Function

  End Class
End Namespace