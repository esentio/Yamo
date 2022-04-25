Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Internal.Query.Metadata

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
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <param name="items"></param>
    Public Sub New(<DisallowNull> sqlResult As AnonymousTypeSqlResult, readerIndex As Int32, <DisallowNull> items As ReaderDataBase())
      MyBase.New(sqlResult, readerIndex, Not sqlResult.CreationBehavior = NonModelEntityCreationBehavior.AlwaysCreateInstance)
      Me.Items = items
    End Sub

  End Class
End Namespace