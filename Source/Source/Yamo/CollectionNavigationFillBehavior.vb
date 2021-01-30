''' <summary>
''' Defines how collection navigation properties are filled during FirstOrDefault() calls.
''' </summary>
Public Enum CollectionNavigationFillBehavior
  ''' <summary>
  ''' Process only first row from the resultset. Any collection navigation property will contain maximum 1 item.
  ''' </summary>
  ProcessOnlyFirstRow = 1
  ''' <summary>
  ''' Process the resultset until it contains the same main entity. If resultset is sorted properly and all rows related to main entity are grouped together, all collection navigation properties will be filled with all related items.
  ''' </summary>
  ProcessUntilMainEntityChange = 2
  ''' <summary>
  ''' Process the whole resultset. All collection navigation properties will be filled with all related items, no matter how the resultset is sorted.
  ''' </summary>
  ProcessAllRows = 3
End Enum
