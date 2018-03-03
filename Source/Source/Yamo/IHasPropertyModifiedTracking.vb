Public Interface IHasPropertyModifiedTracking

  Function IsAnyPropertyModified() As Boolean

  Function IsPropertyModified(propertyName As String) As Boolean

  Sub ResetPropertyModifiedTracking()

End Interface
