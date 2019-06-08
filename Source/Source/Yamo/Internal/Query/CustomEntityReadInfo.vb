Imports System.Data
Imports Yamo.Infrastructure
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Metadata

Namespace Internal.Query

  Public Class CustomEntityReadInfo
    Inherits BaseReadInfo

    Public ReadOnly Property IsEntity As Boolean

    Public ReadOnly Property ReaderIndex As Int32

    Public ReadOnly Property Entity As SqlEntity

    Public ReadOnly Property EntityReader As Func(Of IDataReader, Int32, Boolean(), Object)

    Public ReadOnly Property ContainsPKReader As Func(Of IDataReader, Int32, Int32(), Boolean)

    Public ReadOnly Property PKOffsets As Int32()

    Public ReadOnly Property ValueTypeReader As Object

    Private Sub New()
    End Sub

    Public Shared Function Create(dialectProvider As SqlDialectProvider, model As SqlModel) As CustomEntityReadInfo()
      Dim entities = model.GetEntities()
      Dim customEntities = model.GetCustomEntities()

      Dim result = New CustomEntityReadInfo(customEntities.Length - 1) {}
      Dim readerIndex = 0

      For i = 0 To customEntities.Length - 1
        Dim customEntity = customEntities(i)

        If customEntity.IsEntity Then
          Dim entity = entities(customEntity.EntityIndex)
          result(i) = Create(dialectProvider, model.Model, entity, readerIndex)
          readerIndex += entity.GetColumnCount()
        Else
          result(i) = Create(dialectProvider, model.Model, customEntity.Type, readerIndex)
          readerIndex += 1
        End If
      Next

      Return result
    End Function

    Public Shared Function CreateForGenericType(dialectProvider As SqlDialectProvider, model As Model, type As Type) As CustomEntityReadInfo()
      Dim underlyingNullableType = Nullable.GetUnderlyingType(type)

      If underlyingNullableType IsNot Nothing Then
        type = underlyingNullableType
      End If

      If Not type.IsGenericType Then
        Throw New ArgumentException($"Type '{type}' is not generic type.")
      End If

      Dim args = type.GetGenericArguments()
      Dim result = New CustomEntityReadInfo(args.Length - 1) {}

      For i = 0 To args.Length - 1
        result(i) = Create(dialectProvider, model, args(i), i)
      Next

      Return result
    End Function

    Private Shared Function Create(dialectProvider As SqlDialectProvider, model As Model, entity As SqlEntity, readerIndex As Int32) As CustomEntityReadInfo
      Dim readInfo = New CustomEntityReadInfo

      readInfo._IsEntity = True
      readInfo._ReaderIndex = readerIndex
      readInfo._Entity = entity
      readInfo._EntityReader = EntityReaderCache.GetReader(dialectProvider, model, entity.Entity.EntityType)
      readInfo._ContainsPKReader = EntityReaderCache.GetContainsPKReader(dialectProvider, model, entity.Entity.EntityType)
      readInfo._PKOffsets = GetPKOffsets(entity)
      readInfo._ValueTypeReader = Nothing

      Return readInfo
    End Function

    Private Shared Function Create(dialectProvider As SqlDialectProvider, model As Model, type As Type, readerIndex As Int32) As CustomEntityReadInfo
      Dim readInfo = New CustomEntityReadInfo

      readInfo._IsEntity = False
      readInfo._ReaderIndex = readerIndex
      readInfo._Entity = Nothing
      readInfo._EntityReader = Nothing
      readInfo._ContainsPKReader = Nothing
      readInfo._PKOffsets = Nothing
      readInfo._ValueTypeReader = ValueTypeReaderCache.GetReader(dialectProvider, model, type)

      Return readInfo
    End Function

  End Class
End Namespace