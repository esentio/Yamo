Imports System.Diagnostics.CodeAnalysis

Namespace Internal.Query

  ''' <summary>
  ''' Represents reader data for included entity result values.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class IncludedSqlResultReaderData

    ' TODO: SIP - structure instead?

    ''' <summary>
    ''' Gets property setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Setter As Action(Of Object, Object) ' declaring entity, value

    ''' <summary>
    ''' Reader data.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ReaderData As ReaderDataBase

    ''' <summary>
    ''' Creates new instance of <see cref="IncludedSqlResultReaderData"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="setter"></param>
    ''' <param name="readerData"></param>
    Public Sub New(<DisallowNull> setter As Action(Of Object, Object), <DisallowNull> readerData As ReaderDataBase)
      Me.Setter = setter
      Me.ReaderData = readerData
    End Sub

  End Class
End Namespace