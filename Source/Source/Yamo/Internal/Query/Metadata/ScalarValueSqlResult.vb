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

    ''' <summary>
    ''' Gets count of columns in the resultset.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function GetColumnCount() As Int32
      Return 1
    End Function

  End Class
End Namespace