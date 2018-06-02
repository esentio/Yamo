Imports Yamo.Internal.Query

Namespace Internal

  Public Class ReaderEntityValueCache

    Private m_Cache As Dictionary(Of ChainKey, Object)()

    Sub New(count As Int32)
      m_Cache = New Dictionary(Of ChainKey, Object)(count - 1) {}

      For i = 0 To count - 1
        m_Cache(i) = New Dictionary(Of ChainKey, Object)
      Next
    End Sub

    Public Function TryGetValue(entityIndex As Int32, ByRef key As ChainKey, ByRef value As Object) As Boolean
      Return m_Cache(entityIndex).TryGetValue(key, value)
    End Function

    Public Sub AddValue(entityIndex As Int32, ByRef key As ChainKey, value As Object)
      m_Cache(entityIndex)(key) = value
    End Sub

  End Class
End Namespace