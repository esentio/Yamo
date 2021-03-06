﻿Namespace Model

  Public MustInherit Class PropertyModifiedTrackingBase
    Implements IHasDbPropertyModifiedTracking

    ' NOTE: this is very naive and memory consuming implementation of IHasDbPropertyModifiedTracking

    Private m_Modified As HashSet(Of String) = New HashSet(Of String)

    Protected Sub MarkPropertyAsModified(propertyName As String)
      m_Modified.Add(propertyName)
    End Sub

    Public Function IsAnyDbPropertyModified() As Boolean Implements IHasDbPropertyModifiedTracking.IsAnyDbPropertyModified
      Return m_Modified.Any()
    End Function

    Public Function IsDbPropertyModified(propertyName As String) As Boolean Implements IHasDbPropertyModifiedTracking.IsDbPropertyModified
      Return m_Modified.Contains(propertyName)
    End Function

    Public Sub ResetDbPropertyModifiedTracking() Implements IHasDbPropertyModifiedTracking.ResetDbPropertyModifiedTracking
      m_Modified.Clear()
    End Sub

  End Class
End Namespace
