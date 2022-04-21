Imports System.Diagnostics.CodeAnalysis
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
    Public Sub New(<DisallowNull> sqlResult As ValueTupleSqlResult, readerIndex As Int32, <DisallowNull> items As ReaderDataBase())
      ' NOTE: in some cases, honoring NullIfAllColumnsAreNull is not necessary and AlwaysCreateInstance could be used.
      ' I.e. when ValueTuple is not nullable. Default is an empty value tuple and not null in that case.
      ' However, we simply cannot only check nullability here. For example, if the result is an include call and we are
      ' setting the value to a property of type Object, we still expect null to be set.
      ' Also, it is questionable, if using AlwaysCreateInstance would be faster. It might even be slower (depending on the result).
      MyBase.New(sqlResult, readerIndex, Not sqlResult.CreationBehavior = NonModelEntityCreationBehavior.AlwaysCreateInstance)
      Me.Items = items
    End Sub

  End Class
End Namespace