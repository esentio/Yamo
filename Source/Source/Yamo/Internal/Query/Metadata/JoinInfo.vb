Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL related join info.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Structure JoinInfo

    ''' <summary>
    ''' Gets join type<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property JoinType As JoinType

    ''' <summary>
    ''' Gets table source.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TableSource As String

    ''' <summary>
    ''' Gets whether joined entity is conditionally ignored.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsIgnored As Boolean

    ''' <summary>
    ''' Creates new instance of <see cref="JoinInfo"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="joinType"></param>
    ''' <param name="isIgnored"></param>
    Sub New(joinType As JoinType, isIgnored As Boolean)
      Me.New(joinType, Nothing, isIgnored)
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="JoinInfo"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="joinType"></param>
    ''' <param name="tableSource"></param>
    ''' <param name="isIgnored"></param>
    Sub New(joinType As JoinType, tableSource As String, isIgnored As Boolean)
      Me.JoinType = joinType
      Me.TableSource = tableSource
      Me.IsIgnored = isIgnored
    End Sub

  End Structure
End Namespace