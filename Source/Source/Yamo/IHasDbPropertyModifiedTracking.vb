''' <summary>
''' Interface for model entities that defines change tracking of properties mapped to the database.
''' Implement this interface to turn off updating of all mapped properties/columns during UPDATE statements.
''' </summary>
Public Interface IHasDbPropertyModifiedTracking

  ''' <summary>
  ''' Checks if any property mapped to the database has been changed.
  ''' </summary>
  ''' <returns></returns>
  Function IsAnyDbPropertyModified() As Boolean

  ''' <summary>
  ''' Checks if a particular property mapped to the database has been changed.
  ''' </summary>
  ''' <param name="propertyName"></param>
  ''' <returns></returns>
  Function IsDbPropertyModified(propertyName As String) As Boolean

  ''' <summary>
  ''' Marks all properties mapped to the database as not changed.
  ''' </summary>
  Sub ResetDbPropertyModifiedTracking()

End Interface
