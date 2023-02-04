''' <summary>
''' Interface for model entities that requires initialization after creation.
''' </summary>
Public Interface IInitializable

  ''' <summary>
  ''' Initializes the instance. Called after entity creation.
  ''' </summary>
  Sub Initialize()

End Interface
