''' <summary>
''' Defines what columns will be included in the SELECT clause.<br/>
''' This can be additionally tweaked by calling Include() and Exclude() methods.
''' </summary>
Public Enum SelectColumnsBehavior
  ''' <summary>
  ''' Include only columns that are necessary to create result entities.<br/>
  ''' I.e. columns of the main entity and all joined entities necessary to fill relationship navigation properties.
  ''' </summary>
  ExcludeNonRequiredColumns = 0
  ''' <summary>
  ''' Include columns of all entities in the query, even if they are not used to create the result.<br/>
  ''' I.e. columns of the joined entities that do not fill any relationship navigation property will be included as well.
  ''' </summary>
  SelectAllColumns = 1
End Enum
