Namespace Internal.Query

  ''' <summary>
  ''' Represents reader data for anonymous type values.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class AnonymousTypeSqlResultReaderData
    Inherits ReaderDataBase

    ''' <summary>
    ''' Gets reader data of anonymous type properties.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Items As ReaderDataBase()

    ''' <summary>
    ''' Creates new instance of <see cref="AnonymousTypeSqlResultReaderData"/>.<br/>
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