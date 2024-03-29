﻿Imports System.Diagnostics.CodeAnalysis
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
    ''' Gets whether contains non-null column check should be performed.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ContainsNonNullColumnCheck As Boolean

    ''' <summary>
    ''' Gets post processor action.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns>Returns <see langword="Nothing"/> if post processing is not necessary.</returns>
    Public ReadOnly Property PostProcessor As <MaybeNull> Action(Of Object)

    ''' <summary>
    ''' Creates new instance of <see cref="ReaderDataBase"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <param name="containsNonNullColumnCheck"></param>
    ''' <param name="postProcessor"></param>
    Protected Sub New(<DisallowNull> sqlResult As SqlResultBase, readerIndex As Int32, containsNonNullColumnCheck As Boolean, postProcessor As Action(Of Object))
      Me.SqlResult = sqlResult
      Me.ReaderIndex = readerIndex
      Me.ContainsNonNullColumnCheck = containsNonNullColumnCheck
      Me.PostProcessor = postProcessor
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