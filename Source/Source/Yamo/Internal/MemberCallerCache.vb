Imports System.Diagnostics.CodeAnalysis
Imports System.Reflection
Imports Yamo.Infrastructure

Namespace Internal

  ''' <summary>
  ''' Member caller cache.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class MemberCallerCache

    ' NOTE: caching as it is now will prevent unloading collectible assemblies if type from such assembly is used. Do we care about this?
    ' https://stackoverflow.com/questions/6601502/caching-reflection-data
    ' https://msdn.microsoft.com/en-us/library/dd554932(VS.100).aspx

    ''' <summary>
    ''' Stores cache instances.
    ''' </summary>
    Private Shared m_Instance As MemberCallerCache

    ''' <summary>
    ''' Stores cached field caller instances.<br/>
    ''' <br/>
    ''' Key: field type, <see cref="FieldInfo"/> instance.<br/>
    ''' Value: <see cref="Func(Of Object, Object)"/> delegate, where parameter is object and return value is field value of that object.<br/>
    ''' </summary>
    Private m_FieldCallers As Dictionary(Of (Type, FieldInfo), Func(Of Object, Object))

    ''' <summary>
    ''' Stores cached static field caller instances.<br/>
    ''' <br/>
    ''' Key: field type, <see cref="FieldInfo"/> instance.<br/>
    ''' Value: <see cref="Func(Of Object, Object)"/> delegate, where return value is static field value.<br/>
    ''' </summary>
    Private m_StaticFieldCallers As Dictionary(Of (Type, FieldInfo), Func(Of Object))

    ''' <summary>
    ''' Stores cached property caller instances.<br/>
    ''' <br/>
    ''' Key: field type, <see cref="PropertyInfo"/> instance.<br/>
    ''' Value: <see cref="Func(Of Object, Object)"/> delegate, where parameter is object and return value is property value of that object.<br/>
    ''' </summary>
    Private m_PropertyCallers As Dictionary(Of (Type, PropertyInfo), Func(Of Object, Object))

    ''' <summary>
    ''' Stores cached static property caller instances.<br/>
    ''' <br/>
    ''' Key: field type, <see cref="PropertyInfo"/> instance.<br/>
    ''' Value: <see cref="Func(Of Object, Object)"/> delegate, where return value is static property value.<br/>
    ''' </summary>
    Private m_StaticPropertyCallers As Dictionary(Of (Type, PropertyInfo), Func(Of Object))

    ''' <summary>
    ''' Stores cached method caller instances.<br/>
    ''' <br/>
    ''' Key: field type, <see cref="MethodInfo"/> instance.<br/>
    ''' Value: <see cref="Func(Of Object, Object)"/> delegate, where parameter is object and return value is output from method of that object.<br/>
    ''' </summary>
    Private m_MethodCallers As Dictionary(Of (Type, MethodInfo), Func(Of Object, Object))

    ''' <summary>
    ''' Stores cached static method caller instances.<br/>
    ''' <br/>
    ''' Key: field type, <see cref="MethodInfo"/> instance.<br/>
    ''' Value: <see cref="Func(Of Object, Object)"/> delegate, where return value is output from static method.<br/>
    ''' </summary>
    Private m_StaticMethodCallers As Dictionary(Of (Type, MethodInfo), Func(Of Object))

    ''' <summary>
    ''' Initializes <see cref="MemberCallerCache"/> related static data.
    ''' </summary>
    Shared Sub New()
      m_Instance = New MemberCallerCache
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="MemberCallerCache"/>.
    ''' </summary>
    Private Sub New()
      m_FieldCallers = New Dictionary(Of (Type, FieldInfo), Func(Of Object, Object))
      m_StaticFieldCallers = New Dictionary(Of (Type, FieldInfo), Func(Of Object))
      m_PropertyCallers = New Dictionary(Of (Type, PropertyInfo), Func(Of Object, Object))
      m_StaticPropertyCallers = New Dictionary(Of (Type, PropertyInfo), Func(Of Object))
      m_MethodCallers = New Dictionary(Of (Type, MethodInfo), Func(Of Object, Object))
      m_StaticMethodCallers = New Dictionary(Of (Type, MethodInfo), Func(Of Object))
    End Sub

    ''' <summary>
    ''' Gets field caller.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="fieldInfo"></param>
    ''' <returns></returns>
    Public Shared Function GetFieldCaller(<DisallowNull> type As Type, <DisallowNull> fieldInfo As FieldInfo) As Func(Of Object, Object)
      Return m_Instance.GetOrCreateFieldCaller(type, fieldInfo)
    End Function

    ''' <summary>
    ''' Gets static field caller.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="fieldInfo"></param>
    ''' <returns></returns>
    Public Shared Function GetStaticFieldCaller(<DisallowNull> type As Type, <DisallowNull> fieldInfo As FieldInfo) As Func(Of Object)
      Return m_Instance.GetOrCreateStaticFieldCaller(type, fieldInfo)
    End Function

    ''' <summary>
    ''' Gets property caller.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="propertyInfo"></param>
    ''' <returns></returns>
    Public Shared Function GetPropertyCaller(<DisallowNull> type As Type, <DisallowNull> propertyInfo As PropertyInfo) As Func(Of Object, Object)
      Return m_Instance.GetOrCreatePropertyCaller(type, propertyInfo)
    End Function

    ''' <summary>
    ''' Gets static property caller.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="propertyInfo"></param>
    ''' <returns></returns>
    Public Shared Function GetStaticPropertyCaller(<DisallowNull> type As Type, <DisallowNull> propertyInfo As PropertyInfo) As Func(Of Object)
      Return m_Instance.GetOrCreateStaticPropertyCaller(type, propertyInfo)
    End Function

    ''' <summary>
    ''' Gets method caller.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="methodInfo"></param>
    ''' <returns></returns>
    Public Shared Function GetMethodCaller(<DisallowNull> type As Type, <DisallowNull> methodInfo As MethodInfo) As Func(Of Object, Object)
      Return m_Instance.GetOrCreateMethodCaller(type, methodInfo)
    End Function

    ''' <summary>
    ''' Gets static method caller.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="methodInfo"></param>
    ''' <returns></returns>
    Public Shared Function GetStaticMethodCaller(<DisallowNull> type As Type, <DisallowNull> methodInfo As MethodInfo) As Func(Of Object)
      Return m_Instance.GetOrCreateStaticMethodCaller(type, methodInfo)
    End Function

    ''' <summary>
    ''' Gets or creates field caller.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="fieldInfo"></param>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Gets or creates static field caller.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="fieldInfo"></param>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Gets or creates property caller.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="propertyInfo"></param>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Gets or creates static property caller.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="propertyInfo"></param>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Gets or creates method caller.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="methodInfo"></param>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Gets or creates static method caller.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="methodInfo"></param>
    ''' <returns></returns>
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