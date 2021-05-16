Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL result of a scalar value.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class ScalarValueSqlResult
    Inherits SqlResultBase

    ''' <summary>
    ''' Creates new instance of <see cref="ScalarValueSqlResult"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="resultType"></param>
    Public Sub New(resultType As Type)
      MyBase.New(resultType)
    End Sub

  End Class
End Namespace