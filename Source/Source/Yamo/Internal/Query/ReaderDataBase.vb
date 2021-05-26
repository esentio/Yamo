Imports Yamo.Internal.Query.Metadata

Namespace Internal.Query

  ''' <summary>
  ''' Base class for reader data used to read values from SQL result.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public MustInherit Class ReaderDataBase

    ''' <summary>
    ''' Gets SQL result.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SqlResult As SqlResultBase

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
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    Protected Sub New(sqlResult As SqlResultBase, readerIndex As Int32)
      Me.SqlResult = sqlResult
      Me.ReaderIndex = readerIndex
    End Sub

    ''' <summary>
    ''' Gets count of columns in the resultset.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Function GetColumnCount() As Int32
      Return Me.SqlResult.GetColumnCount()
    End Function

  End Class
End Namespace