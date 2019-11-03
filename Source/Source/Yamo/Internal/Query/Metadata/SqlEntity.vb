Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL related entity data.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SqlEntity

    ' TODO: SIP - structure instead?

    ''' <summary>
    ''' Gets entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Entity As Entity

    ''' <summary>
    ''' Gets table alias.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TableAlias As String

    ''' <summary>
    ''' Gets entity index.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Index As Int32

    ''' <summary>
    ''' Gets SQL entity relationship.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Relationship As SqlEntityRelationship

    ''' <summary>
    ''' Gets whether entity is excluded from select statement.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsExcluded As Boolean

    ''' <summary>
    ''' Gets included columns.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IncludedColumns As Boolean()

    ''' <summary>
    ''' Creates new instance of <see cref="SqlEntity"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    Sub New(entity As Entity)
      Me.New(entity, "", -1)
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="SqlEntity"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <param name="tableAlias"></param>
    ''' <param name="index"></param>
    Sub New(entity As Entity, tableAlias As String, index As Int32)
      Me.Entity = entity
      Me.TableAlias = tableAlias
      Me.Index = index
      Me.Relationship = Nothing
      Me.IsExcluded = False

      Dim lastIndex = Me.Entity.GetPropertiesCount() - 1
      Dim includedColumns = New Boolean(lastIndex) {}

      For i = 0 To lastIndex
        includedColumns(i) = True
      Next

      Me.IncludedColumns = includedColumns
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="SqlEntity"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <param name="tableAlias"></param>
    ''' <param name="index"></param>
    ''' <param name="relationship"></param>
    Sub New(entity As Entity, tableAlias As String, index As Int32, relationship As SqlEntityRelationship)
      Me.New(entity, tableAlias, index)
      Me.Relationship = relationship
    End Sub

    ''' <summary>
    ''' Sets relationship.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="relationship"></param>
    Public Sub SetRelationship(relationship As SqlEntityRelationship)
      Me._Relationship = relationship
    End Sub

    ''' <summary>
    ''' Excludes this entity from select statement.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    Public Sub Exclude()
      Me._IsExcluded = True
    End Sub

    ''' <summary>
    ''' Gets count of included columns.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetColumnCount() As Int32
      If Me.IsExcluded Then
        Return 0
      End If

      Dim count = 0

      For i = 0 To Me.IncludedColumns.Length - 1
        If Me.IncludedColumns(i) Then
          count += 1
        End If
      Next

      Return count
    End Function

  End Class
End Namespace