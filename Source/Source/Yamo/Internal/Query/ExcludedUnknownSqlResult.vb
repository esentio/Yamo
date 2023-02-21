Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Internal.Query.Metadata

Namespace Internal.Query

  ''' <summary>
  ''' Represents reader data for values of excluded types with no additional details.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class ExcludedUnknownSqlResultReaderData
    Inherits ReaderDataBase

    ''' <summary>
    ''' Creates new instance of <see cref="ExcludedUnknownSqlResultReaderData"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    Public Sub New(<DisallowNull> sqlResult As ExcludedUnknownSqlResult, readerIndex As Int32)
      MyBase.New(sqlResult, readerIndex, False, Nothing)
    End Sub

  End Class
End Namespace