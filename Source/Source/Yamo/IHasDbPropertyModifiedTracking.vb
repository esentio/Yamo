Public Interface IHasDbPropertyModifiedTracking

  Function IsAnyDbPropertyModified() As Boolean

  Function IsDbPropertyModified(propertyName As String) As Boolean

  Sub ResetDbPropertyModifiedTracking()

End Interface
