Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Base class for SQL related entity data.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public MustInherit Class SqlEntityBase

    ''' <summary>
    ''' Gets type of model representing this SQL entity.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property EntityType As Type

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
    Public ReadOnly Property Relationship As <MaybeNull> SqlEntityRelationship

    ''' <summary>
    ''' Gets whether entity is excluded from select clause.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsExcluded As Boolean

    ''' <summary>
    ''' Gets whether entity is conditionally ignored
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsIgnored As Boolean

    ''' <summary>
    ''' Gets whether entity is excluded or conditionally ignored
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsExcludedOrIgnored As Boolean
      Get
        Return Me.IsExcluded OrElse Me.IsIgnored
      End Get
    End Property

    ''' <summary>
    ''' Gets included columns.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IncludedColumns As Boolean()

    ''' <summary>
    ''' Gets included SQL results.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns>List of included results or <see langword="Nothing"/> if no additional results are included.</returns>
    Public ReadOnly Property IncludedSqlResults As <MaybeNull> List(Of SqlEntityIncludedResult)

    ''' <summary>
    ''' Creates new instance of <see cref="SqlEntityBase"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entityType"></param>
    ''' <param name="tableAlias"></param>
    ''' <param name="index"></param>
    ''' <param name="columnsCount"></param>
    Sub New(<DisallowNull> entityType As Type, <DisallowNull> tableAlias As String, index As Int32, columnsCount As Int32)
      Me.EntityType = entityType
      Me.TableAlias = tableAlias
      Me.Index = index
      Me.Relationship = Nothing
      Me.IsExcluded = False
      Me.IsIgnored = False

      Dim lastIndex = columnsCount - 1
      Dim includedColumns = New Boolean(lastIndex) {}

      For i = 0 To lastIndex
        includedColumns(i) = True
      Next

      Me.IncludedColumns = includedColumns
      Me.IncludedSqlResults = Nothing
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="SqlEntityBase"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entityType"></param>
    ''' <param name="tableAlias"></param>
    ''' <param name="index"></param>
    ''' <param name="columnsCount"></param>
    ''' <param name="relationship"></param>
    ''' <param name="isIgnored"></param>
    Sub New(<DisallowNull> entityType As Type, <DisallowNull> tableAlias As String, index As Int32, columnsCount As Int32, relationship As SqlEntityRelationship, isIgnored As Boolean)
      Me.New(entityType, tableAlias, index, columnsCount)
      Me.Relationship = relationship
      Me.IsIgnored = isIgnored
    End Sub

    ''' <summary>
    ''' Sets relationship.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="relationship"></param>
    Public Sub SetRelationship(<DisallowNull> relationship As SqlEntityRelationship)
      Me._Relationship = relationship
    End Sub

    ''' <summary>
    ''' Excludes this entity from select clause.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    Public Sub Exclude()
      Me._IsExcluded = True
    End Sub

    ''' <summary>
    ''' Gets column name.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="propertyName"></param>
    ''' <returns></returns>
    Public MustOverride Function GetColumnName(<DisallowNull> propertyName As String) As String

    ''' <summary>
    ''' Gets column names.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function GetColumnNames() As List(Of String)

    ''' <summary>
    ''' Gets count of included columns. Only columns representing entity properties are counted, not columns representing included result(s).<br/>
    ''' This returns non-zero count, even if table is ignored (unless the whole table is excluded).<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="ignoreExclusion"></param>
    ''' <returns></returns>
    Public Function GetColumnCount(Optional ignoreExclusion As Boolean = False) As Int32
      If Me.IsExcluded AndAlso Not ignoreExclusion Then
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

    ''' <summary>
    ''' Adds included SQL result.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="includedSqlResult"></param>
    Public Sub AddIncludedSqlResult(<DisallowNull> includedSqlResult As SqlEntityIncludedResult)
      ' TODO: SIP - implement subquery - only for entitybased?
      If Me.IncludedSqlResults Is Nothing Then
        Me._IncludedSqlResults = New List(Of SqlEntityIncludedResult)
      End If

      Me.IncludedSqlResults.Add(includedSqlResult)
    End Sub

  End Class
End Namespace