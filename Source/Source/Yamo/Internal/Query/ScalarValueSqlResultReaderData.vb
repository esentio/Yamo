﻿Imports Yamo.Internal.Query.Metadata

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
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <param name="reader"></param>
    Public Sub New(sqlResult As ScalarValueSqlResult, readerIndex As Int32, reader As Object)
      MyBase.New(sqlResult, readerIndex)
      Me.Reader = reader
    End Sub

  End Class
End Namespace