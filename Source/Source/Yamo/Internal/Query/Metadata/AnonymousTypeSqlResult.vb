Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL result of an anonymous type.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class AnonymousTypeSqlResult
    Inherits SqlResultBase

    ''' <summary>
    ''' Gets nested SQL results which represent anonymous object properties.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Items As SqlResultBase()

    ''' <summary>
    ''' Creates new instance of <see cref="AnonymousTypeSqlResult"/>.<br/>
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