''' <summary>
''' Interface for model entities that adds support for notification of filling its properties with values from the database.
''' </summary>
Public Interface ISupportDbLoad

  ''' <summary>
  ''' Signals the object that filling its properties with values from the database is starting.
  ''' </summary>
  Sub BeginLoad()

  ''' <summary>
  ''' Signals the object that filling its properties with values from the database is complete.
  ''' </summary>
  Sub EndLoad()

End Interface
