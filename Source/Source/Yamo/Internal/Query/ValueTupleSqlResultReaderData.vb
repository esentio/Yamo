Namespace Internal.Query

  ''' <summary>
  ''' Represents reader data for value tuple values.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class ValueTupleSqlResultReaderData
    Inherits ReaderDataBase

    ''' <summary>
    ''' Gets reader data of value tuple elements.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Items As ReaderDataBase()

    ''' <summary>
    ''' Creates new instance of <see cref="ValueTupleSqlResultReaderData"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="readerIndex"></param>
    ''' <param name="items"></param>
    Public Sub New(readerIndex As Int32, items As ReaderDataBase())
      MyBase.New(readerIndex)
      Me.Items = items
    End Sub

    ''' <summary>
    ''' Gets count of columns in the resultset.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function GetColumnCount() As Int32
      Return Me.Items.Sum(Function(x) x.GetColumnCount())
    End Function

  End Class
End Namespace