Namespace Internal.Query

  Public Structure ChainKey

    Private ReadOnly m_Pks As Object()

    Sub New(pks As Object())
      m_Pks = pks
    End Sub

    Public Overrides Function GetHashCode() As Int32
      ' using ValueTuple is just simple workaround until .NET becomes System.HashCode

      Dim length = m_Pks.Length

      If length = 1 Then
        Return If(m_Pks(0) Is Nothing, 0, m_Pks(0).GetHashCode())
      ElseIf length = 2 Then
        Return (m_Pks(0), m_Pks(1)).GetHashCode()
      ElseIf length = 3 Then
        Return (m_Pks(0), m_Pks(1), m_Pks(2)).GetHashCode()
      ElseIf length = 4 Then
        Return (m_Pks(0), m_Pks(1), m_Pks(2), m_Pks(3)).GetHashCode()
      ElseIf length = 5 Then
        Return (m_Pks(0), m_Pks(1), m_Pks(2), m_Pks(3), m_Pks(4)).GetHashCode()
      ElseIf length = 6 Then
        Return (m_Pks(0), m_Pks(1), m_Pks(2), m_Pks(3), m_Pks(4), m_Pks(5)).GetHashCode()
      ElseIf length = 7 Then
        Return (m_Pks(0), m_Pks(1), m_Pks(2), m_Pks(3), m_Pks(4), m_Pks(5), m_Pks(6)).GetHashCode()
      Else
        Throw New NotSupportedException("Too much joins.")
      End If
    End Function

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