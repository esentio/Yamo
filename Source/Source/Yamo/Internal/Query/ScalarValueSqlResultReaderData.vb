Namespace Internal.Query

  ''' <summary>
  ''' Represents reader data for scalar values.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class ScalarValueSqlResultReaderData
    Inherits ReaderDataBase

    ''' <summary>
    ''' Gets reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Reader As Object

    ''' <summary>
    ''' Creates new instance of <see cref="ScalarValueSqlResultReaderData"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="readerIndex"></param>
    ''' <param name="reader"></param>
    Public Sub New(readerIndex As Int32, reader As Object)
      MyBase.New(readerIndex)
      Me.Reader = reader
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