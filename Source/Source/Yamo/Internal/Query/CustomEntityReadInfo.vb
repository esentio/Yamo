Imports System.Data
Imports Yamo.Infrastructure
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Metadata

Namespace Internal.Query

  ''' <summary>
  ''' Represents info used to read custom entity from SQL result.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class CustomEntityReadInfo
    Inherits BaseReadInfo

    ''' <summary>
    ''' Gets whether custom entity relates to an entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsEntity As Boolean

    ''' <summary>
    ''' Gets index of the reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ReaderIndex As Int32

    ''' <summary>
    ''' Gets SQL entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Entity As SqlEntity

    ''' <summary>
    ''' Gets entity reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property EntityReader As Func(Of IDataReader, Int32, Boolean(), Object)

    ''' <summary>
    ''' Gets contains primary key reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ContainsPKReader As Func(Of IDataReader, Int32, Int32(), Boolean)

    ''' <summary>
    ''' Gets primary key offsets.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property PKOffsets As Int32()

    ''' <summary>
    ''' Gets value type reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ValueTypeReader As Object

    ''' <summary>
    ''' Creates new instance of <see cref="CustomEntityReadInfo"/>.
    ''' </summary>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Creates new instances of <see cref="CustomEntityReadInfo"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Creates new instances of <see cref="CustomEntityReadInfo"/> for generic type.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
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
      Dim readerIndex = 0

      For i = 0 To args.Length - 1
        Dim argType = args(i)

        If Helpers.Types.IsProbablyModel(argType) Then
          Dim entity = model.TryGetEntity(argType)

          If entity Is Nothing Then
            Throw New NotSupportedException($"Type '{argType}' is not supported. Only reference types defined in model are supported.")
          End If

          Dim sqlEntity = New SqlEntity(entity)
          result(i) = Create(dialectProvider, model, sqlEntity, readerIndex)
          readerIndex += sqlEntity.GetColumnCount()
        Else
          result(i) = Create(dialectProvider, model, argType, readerIndex)
          readerIndex += 1
        End If
      Next

      Return result
    End Function

    ''' <summary>
    ''' Creates new instances of <see cref="CustomEntityReadInfo"/> for model type.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Shared Function CreateForModelType(dialectProvider As SqlDialectProvider, model As Model, type As Type) As CustomEntityReadInfo()
      Dim entity = model.TryGetEntity(type)

      If entity Is Nothing Then
        Throw New NotSupportedException($"Type '{type}' is not supported. Only reference types defined in model are supported.")
      End If

      Return {Create(dialectProvider, model, New SqlEntity(entity), 0)}
    End Function

    ''' <summary>
    ''' Creates new instance of <see cref="CustomEntityReadInfo"/>.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="entity"></param>
    ''' <param name="readerIndex"></param>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Creates new instance of <see cref="CustomEntityReadInfo"/>.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="type"></param>
    ''' <param name="readerIndex"></param>
    ''' <returns></returns>
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