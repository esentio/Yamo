Namespace Model

  Public MustInherit Class PropertyModifiedTrackingBase
    Implements IHasPropertyModifiedTracking

    ' NOTE: this is very naive and memory consuming implementation of IHasPropertyModifiedTracking

    Private m_Modified As HashSet(Of String) = New HashSet(Of String)

    Protected Sub MarkPropertyAsModified(propertyName As String)
      m_Modified.Add(propertyName)
    End Sub

    Public Function IsAnyPropertyModified() As Boolean Implements IHasPropertyModifiedTracking.IsAnyPropertyModified
      Return m_Modified.Any()
    End Function

    Public Function IsPropertyModified(propertyName As String) As Boolean Implements IHasPropertyModifiedTracking.IsPropertyModified
      Return m_Modified.Contains(propertyName)
    End Function

    Public Sub ResetPropertyModifiedTracking() Implements IHasPropertyModifiedTracking.ResetPropertyModifiedTracking
      m_Modified.Clear()
    End Sub

  End Class
End Namespace
