Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Base class for SQL results.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public MustInherit Class SqlResultBase

    ''' <summary>
    ''' Gets type of the result.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ResultType As Type

    ''' <summary>
    ''' Creates new instance of <see cref="SqlResultBase"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="resultType"></param>
    Public Sub New(resultType As Type)
      Me.ResultType = resultType
    End Sub

  End Class
End Namespace