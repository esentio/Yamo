Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL result of a value tuple.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class ValueTupleSqlResult
    Inherits SqlResultBase

    ''' <summary>
    ''' Gets nested SQL results which represent value tuple elements.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Items As SqlResultBase()

    ''' <summary>
    ''' Creates new instance of <see cref="ValueTupleSqlResult"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="resultType"></param>
    ''' <param name="items"></param>
    Public Sub New(resultType As Type, items As SqlResultBase())
      MyBase.New(resultType)
      Me.Items = items
    End Sub

  End Class
End Namespace