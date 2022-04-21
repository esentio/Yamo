''' <summary>
''' Defines how should non model entity instances be created since we cannot detect their presence in the resultset based on the primary key.<br/>
''' Note that non model entity probably won't have relationship defined in model either and hence instances will be created only if relationship is defined explicitly using As() method.
''' </summary>
Public Enum NonModelEntityCreationBehavior
  ''' <summary>
  ''' If possible, infer the behavior from the subquery result. Otherwise, use <see cref="NullIfAllColumnsAreNull"/> behavior.
  ''' </summary>
  InferOrNullIfAllColumnsAreNull = 0
  ''' <summary>
  ''' Do not create an instance unless there is at least one related column value in the resultset that doesn't equal to <see cref="DBNull"/>.
  ''' </summary>
  NullIfAllColumnsAreNull = 1
  ''' <summary>
  ''' Always create an instance, even if all related columns in the resultset contain <see cref="DBNull"/> value.
  ''' </summary>
  AlwaysCreateInstance = 2
End Enum
