Imports System.Diagnostics.CodeAnalysis
Imports System.Reflection

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents value that can be used as a dictionary key for ad hoc type SQL result reader.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class AdHocTypeSqlResultReaderKey

    ''' <summary>
    ''' Gets ad hoc type.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Type As Type

    ''' <summary>
    ''' Gets constructor.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Constructor As <MaybeNull> ConstructorInfo

    ''' <summary>
    ''' Gets members.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns>Will return <see langword="Nothing"/> if there are no members to initialize.</returns>
    Public ReadOnly Property Members As <MaybeNull> MemberInfo()

    ''' <summary>
    ''' Creates new instance of <see cref="AdHocTypeSqlResultReaderKey"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="constructor">Might be <see langword="Nothing"/> for structs.</param>
    ''' <param name="members"></param>
    Public Sub New(<DisallowNull> type As Type, constructor As ConstructorInfo, <DisallowNull> members As MemberInfo())
      Me.Type = type
      Me.Constructor = constructor
      Me.Members = members
    End Sub

    ''' <summary>
    ''' Determines whether the specified object is equal to the current object.
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot AdHocTypeSqlResultReaderKey Then
        Return False
      Else
        Dim o = DirectCast(obj, AdHocTypeSqlResultReaderKey)

        If Not Object.Equals(Me.Type, o.Type) Then Return False
        If Not Object.Equals(Me.Constructor, o.Constructor) Then Return False
        If Me.Members Is Nothing AndAlso o.Members IsNot Nothing Then Return False
        If Me.Members IsNot Nothing AndAlso o.Members Is Nothing Then Return False

        If Me.Members IsNot Nothing Then
          If Not Me.Members.Length = o.Members.Length Then Return False

          For i = 0 To Me.Members.Length - 1
            If Not Me.Members(i) = o.Members(i) Then Return False
          Next
        End If

        Return True
      End If
    End Function

    ''' <summary>
    ''' Serves as the hash function.
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function GetHashCode() As Int32
      Dim h = New HashCode()

      h.Add(Me.Type)
      h.Add(Me.Constructor)

      If Me.Members IsNot Nothing Then
        For i = 0 To Me.Members.Length - 1
          h.Add(Me.Members(i))
        Next
      End If

      Return h.ToHashCode()
    End Function

  End Class
End Namespace