Namespace Internal

  Public Class ReaderEntityValueCache

    Private m_Cache As Dictionary(Of Int32, Object)()

    Sub New(count As Int32)
      m_Cache = New Dictionary(Of Int32, Object)(count - 1) {}

      For i = 0 To count - 1
        m_Cache(i) = New Dictionary(Of Int32, Object)
      Next
    End Sub

    Public Function Contains(entityIndex As Int32, key As Int32) As Boolean
      Return m_Cache(entityIndex).ContainsKey(key)
    End Function

    Public Function GetValue(entityIndex As Int32, key As Int32) As Object
      Return m_Cache(entityIndex)(key)
    End Function

    Public Sub AddValue(entityIndex As Int32, key As Int32, value As Object)
      m_Cache(entityIndex)(key) = value
    End Sub

  End Class
End Namespace