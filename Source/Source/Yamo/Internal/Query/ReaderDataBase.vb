Namespace Internal.Query

  ''' <summary>
  ''' Base class for reader data used to read values from SQL result.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public MustInherit Class ReaderDataBase

    ''' <summary>
    ''' Gets index of the reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ReaderIndex As Int32

    ''' <summary>
    ''' Creates new instance of <see cref="ReaderDataBase"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="readerIndex"></param>
    Protected Sub New(readerIndex As Int32)
      Me.ReaderIndex = readerIndex
    End Sub

    ''' <summary>
    ''' Gets count of columns in the resultset.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function GetColumnCount() As Int32

  End Class
End Namespace