Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL related custom entity data.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class CustomSqlEntity

    ' TODO: SIP - structure instead?

    ''' <summary>
    ''' Gets custom entity index.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Index As Int32

    ''' <summary>
    ''' Gets whether this relates to an entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsEntity As Boolean

    ''' <summary>
    ''' Gets entity index.<br/>
    ''' Return -1 if this doesn't relate to an entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property EntityIndex As Int32

    ''' <summary>
    ''' Gets custom entity type.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Type As Type

    ''' <summary>
    ''' Creates new instance of <see cref="CustomSqlEntity"/> representing simple value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <param name="type"></param>
    Public Sub New(index As Int32, type As Type)
      Me.Index = index
      Me.IsEntity = False
      Me.EntityIndex = -1
      Me.Type = type
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="CustomSqlEntity"/> representing entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <param name="entityIndex"></param>
    ''' <param name="type"></param>
    Public Sub New(index As Int32, entityIndex As Int32, type As Type)
      Me.Index = index
      Me.IsEntity = True
      Me.EntityIndex = entityIndex
      Me.Type = type
    End Sub

  End Class
End Namespace