Namespace Internal.Query

  ''' <summary>
  ''' Represents chain key (contains all primary keys of all entities in resultset).<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Structure ChainKey

    ''' <summary>
    ''' Stores primary keys.
    ''' </summary>
    Private ReadOnly m_Pks As Object()

    ''' <summary>
    ''' Creates new instance of <see cref="ChainKey"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="pks"></param>
    Sub New(pks As Object())
      m_Pks = pks
    End Sub

    ''' <summary>
    ''' Returns the hash code for this instance.
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function GetHashCode() As Int32
      Dim length = m_Pks.Length

      If length = 1 Then
        Return If(m_Pks(0) Is Nothing, 0, m_Pks(0).GetHashCode())
      ElseIf length = 2 Then
        Return HashCode.Combine(m_Pks(0), m_Pks(1))
      ElseIf length = 3 Then
        Return HashCode.Combine(m_Pks(0), m_Pks(1), m_Pks(2))
      ElseIf length = 4 Then
        Return HashCode.Combine(m_Pks(0), m_Pks(1), m_Pks(2), m_Pks(3))
      ElseIf length = 5 Then
        Return HashCode.Combine(m_Pks(0), m_Pks(1), m_Pks(2), m_Pks(3), m_Pks(4))
      ElseIf length = 6 Then
        Return HashCode.Combine(m_Pks(0), m_Pks(1), m_Pks(2), m_Pks(3), m_Pks(4), m_Pks(5))
      ElseIf length = 7 Then
        Return HashCode.Combine(m_Pks(0), m_Pks(1), m_Pks(2), m_Pks(3), m_Pks(4), m_Pks(5), m_Pks(6))
      ElseIf length = 8 Then
        Return HashCode.Combine(m_Pks(0), m_Pks(1), m_Pks(2), m_Pks(3), m_Pks(4), m_Pks(5), m_Pks(6), m_Pks(7))
      Else
        Dim hashCode = New HashCode

        For i = 0 To length - 1
          hashCode.Add(m_Pks(i))
        Next

        Return hashCode.ToHashCode()
      End If
    End Function

    ''' <summary>
    ''' Indicates whether this instance and a specified object are equal.
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Public Overrides Function Equals(obj As Object) As Boolean
      If TypeOf obj IsNot ChainKey Then Return False

      Dim key = DirectCast(obj, ChainKey)

      If Not m_Pks.Length = key.m_Pks.Length Then
        Return False
      Else
        For i = 0 To m_Pks.Length - 1
          If Not Object.Equals(m_Pks(i), key.m_Pks(i)) Then
            Return False
          End If
        Next
      End If

      Return True
    End Function

  End Structure
End Namespace