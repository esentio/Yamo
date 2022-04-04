Imports System.Data
Imports System.Data.Common
Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Internal.Query.Metadata

Namespace Internal.Query

  ''' <summary>
  ''' Represents reader data for entity values.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class EntitySqlResultReaderData
    Inherits ReaderDataBase

    ''' <summary>
    ''' Gets SQL entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Entity As EntityBasedSqlEntity

    ''' <summary>
    ''' Gets entity reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Reader As Func(Of DbDataReader, Int32, Boolean(), Object)

    ''' <summary>
    ''' Gets contains primary key reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns>Might return <see langword="Nothing"/> in scenarios when reading primary key is not necessary.</returns>
    Public ReadOnly Property ContainsPKReader As <MaybeNull> Func(Of DbDataReader, Int32, Int32(), Boolean)

    ''' <summary>
    ''' Gets primary key offsets.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns>Might return <see langword="Nothing"/> in scenarios when reading primary key is not necessary.</returns>
    Public ReadOnly Property PKOffsets As <MaybeNull> Int32()

    ''' <summary>
    ''' Gets primary key reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns>Might return <see langword="Nothing"/> in scenarios when reading primary key is not necessary.</returns>
    Public ReadOnly Property PKReader As <MaybeNull> Func(Of DbDataReader, Int32, Int32(), Object)

    ''' <summary>
    ''' Creates new instance of <see cref="EntitySqlResultReaderData"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <param name="entityReader"></param>
    Public Sub New(<DisallowNull> sqlResult As EntitySqlResult, readerIndex As Int32, <DisallowNull> entityReader As Func(Of DbDataReader, Int32, Boolean(), Object))
      MyBase.New(sqlResult, readerIndex)
      Me.Entity = sqlResult.Entity
      Me.Reader = entityReader
      Me.ContainsPKReader = Nothing
      Me.PKOffsets = Nothing
      Me.PKReader = Nothing
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="EntitySqlResultReaderData"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <param name="entityReader"></param>
    ''' <param name="containsPKReader"></param>
    ''' <param name="pkOffsets"></param>
    Public Sub New(<DisallowNull> sqlResult As EntitySqlResult, readerIndex As Int32, <DisallowNull> entityReader As Func(Of DbDataReader, Int32, Boolean(), Object), containsPKReader As Func(Of DbDataReader, Int32, Int32(), Boolean), pkOffsets As Int32())
      MyBase.New(sqlResult, readerIndex)
      Me.Entity = sqlResult.Entity
      Me.Reader = entityReader
      Me.ContainsPKReader = containsPKReader
      Me.PKOffsets = pkOffsets
      Me.PKReader = Nothing
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="EntitySqlResultReaderData"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <param name="entityReader"></param>
    ''' <param name="containsPKReader"></param>
    ''' <param name="pkOffsets"></param>
    ''' <param name="pkReader"></param>
    Public Sub New(<DisallowNull> sqlResult As EntitySqlResult, readerIndex As Int32, <DisallowNull> entityReader As Func(Of DbDataReader, Int32, Boolean(), Object), containsPKReader As Func(Of DbDataReader, Int32, Int32(), Boolean), pkOffsets As Int32(), pkReader As Func(Of DbDataReader, Int32, Int32(), Object))
      MyBase.New(sqlResult, readerIndex)
      Me.Entity = sqlResult.Entity
      Me.Reader = entityReader
      Me.ContainsPKReader = containsPKReader
      Me.PKOffsets = pkOffsets
      Me.PKReader = pkReader
    End Sub

  End Class
End Namespace