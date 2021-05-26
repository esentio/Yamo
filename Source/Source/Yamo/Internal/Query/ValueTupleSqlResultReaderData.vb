Imports Yamo.Internal.Query.Metadata

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
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <param name="items"></param>
    Public Sub New(sqlResult As ValueTupleSqlResult, readerIndex As Int32, items As ReaderDataBase())
      MyBase.New(sqlResult, readerIndex)
      Me.Items = items
    End Sub

  End Class
End Namespace