Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL result of a model entity.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class EntitySqlResult
    Inherits SqlResultBase

    ''' <summary>
    ''' Gets related SQL entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Entity As SqlEntity

    ''' <summary>
    ''' Creates new instance of <see cref="EntitySqlResult"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    Public Sub New(entity As SqlEntity)
      MyBase.New(entity.Entity.EntityType)
      Me.Entity = entity
    End Sub

  End Class
End Namespace