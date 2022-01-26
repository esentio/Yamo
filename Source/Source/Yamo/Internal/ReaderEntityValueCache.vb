Imports Yamo.Internal.Query

Namespace Internal

  ''' <summary>
  ''' Reader entity value cache.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class ReaderEntityValueCache

    ''' <summary>
    ''' Stores cached instances.<br/>
    ''' <br/>
    ''' Key: <see cref="ChainKey"/>.<br/>
    ''' Value: entity instance.
    ''' </summary>
    Private m_Cache As Dictionary(Of ChainKey, Object)()

    ''' <summary>
    ''' Creates new instance of <see cref="ReaderEntityValueCache"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="count"></param>
    Sub New(count As Int32)
      m_Cache = New Dictionary(Of ChainKey, Object)(count - 1) {}

      For i = 0 To count - 1
        m_Cache(i) = New Dictionary(Of ChainKey, Object)
      Next
    End Sub

    ''' <summary>
    ''' Tries to get value from cache.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entityIndex"></param>
    ''' <param name="key"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Function TryGetValue(entityIndex As Int32, ByRef key As ChainKey, ByRef value As Object) As Boolean
      Return m_Cache(entityIndex).TryGetValue(key, value)
    End Function

    ''' <summary>
    ''' Adds value to cache.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entityIndex"></param>
    ''' <param name="key"></param>
    ''' <param name="value"></param>
    Public Sub AddValue(entityIndex As Int32, ByRef key As ChainKey, value As Object)
      m_Cache(entityIndex)(key) = value
    End Sub

  End Class
End Namespace