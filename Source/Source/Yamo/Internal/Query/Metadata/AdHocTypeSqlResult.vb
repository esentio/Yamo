Imports System.Diagnostics.CodeAnalysis
Imports System.Reflection

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL result of an ad hoc type.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class AdHocTypeSqlResult
    Inherits SqlResultBase

    ''' <summary>
    ''' Gets used constructor.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Constructor As <MaybeNull> ConstructorInfo

    ''' <summary>
    ''' Gets nested SQL results which represent ad hoc object constructor arguments.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CtorArguments As SqlResultBase()

    ''' <summary>
    ''' Gets if there are members to be initialized.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HasMemberInits As Boolean
      Get
        Return Me.Members IsNot Nothing
      End Get
    End Property

    ''' <summary>
    ''' Gets members.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns>Will return <see langword="Nothing"/> if there are no members to initialize.</returns>
    Public ReadOnly Property Members As <MaybeNull> MemberInfo()

    ''' <summary>
    ''' Gets nested SQL results which represent ad hoc object member inits.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns>Will return <see langword="Nothing"/> if there are no members to initialize. For <see cref="Nullable(Of T)"/>, it represents members of the underlying type.</returns>
    Public ReadOnly Property MemberInits As <MaybeNull> SqlResultBase()

    ''' <summary>
    ''' Creates new instance of <see cref="AdHocTypeSqlResult"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="resultType"></param>
    ''' <param name="constructor">Might be <see langword="Nothing"/> for structs. For <see cref="Nullable(Of T)"/>, it represents constructor info of the underlying type.</param>
    ''' <param name="ctorArguments"></param>
    Public Sub New(<DisallowNull> resultType As Type, constructor As ConstructorInfo, <DisallowNull> ctorArguments As SqlResultBase())
      MyBase.New(resultType)
      Me.Constructor = constructor
      Me.CtorArguments = ctorArguments
      Me.Members = Nothing
      Me.MemberInits = Nothing
    End Sub

    ''' <summary>
    ''' Sets ad hoc object init members data.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="members"></param>
    ''' <param name="memberInits"></param>
    Public Sub SetMembers(members As MemberInfo(), <DisallowNull> memberInits As SqlResultBase())
      Me._Members = members
      Me._MemberInits = memberInits
    End Sub

    ''' <summary>
    ''' Gets count of columns in the resultset.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function GetColumnCount() As Int32
      Dim sum = Me.CtorArguments.Sum(Function(x) x.GetColumnCount())

      If Me.MemberInits IsNot Nothing Then
        sum += Me.MemberInits.Sum(Function(x) x.GetColumnCount())
      End If

      Return sum
    End Function

    ''' <summary>
    ''' Gets reader key.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetKey() As AdHocTypeSqlResultReaderKey
      Return New AdHocTypeSqlResultReaderKey(Me.ResultType, Me.Constructor, Me.Members)
    End Function

  End Class
End Namespace