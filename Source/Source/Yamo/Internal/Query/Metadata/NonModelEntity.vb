Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents an ad hoc entity that is not part of the database model.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class NonModelEntity

    ''' <summary>
    ''' Gets type representing this entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property EntityType As Type

    ''' <summary>
    ''' Gets SQL result.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns><see langword="Nothing"/> may be returned if object is not fully initialized yet.</returns>
    Public ReadOnly Property SqlResult As <MaybeNull> SqlResultBase

    ''' <summary>
    ''' Stores column names by their corresponding property names.
    ''' </summary>
    Private m_ColumnsDictionary As Dictionary(Of String, String)

    ''' <summary>
    ''' Stores column names.
    ''' </summary>
    Private m_Columns As List(Of String)

    ''' <summary>
    ''' Creates new instance of <see cref="NonModelEntity"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entityType"></param>
    Public Sub New(<DisallowNull> entityType As Type)
      Me.EntityType = entityType
      m_ColumnsDictionary = New Dictionary(Of String, String)
      m_Columns = New List(Of String)
    End Sub

    ''' <summary>
    ''' Gets column name.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="propertyName"></param>
    ''' <returns></returns>
    Public Function GetColumnName(<DisallowNull> propertyName As String) As String
      Return m_ColumnsDictionary(propertyName)
    End Function

    ''' <summary>
    ''' Gets column names.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Public Function GetColumnName(index As Int32) As String
      Return m_Columns(index)
    End Function

    ''' <summary>
    ''' Gets columns count.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetColumnsCount() As Int32
      Return m_Columns.Count
    End Function

    ''' <summary>
    ''' Adds column which value will be used to fill a property of this entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="propertyName"></param>
    ''' <param name="columnName"></param>
    Public Sub AddColumn(<DisallowNull> propertyName As String, <DisallowNull> columnName As String)
      m_ColumnsDictionary(propertyName) = columnName
      m_Columns.Add(columnName)
    End Sub

    ''' <summary>
    ''' Adds column which value won't be used to directly fill a property of this entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="columnName"></param>
    Public Sub AddColumn(<DisallowNull> columnName As String)
      m_Columns.Add(columnName)
    End Sub

    ''' <summary>
    ''' Sets SQL result.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    Public Sub SetSqlResult(<DisallowNull> sqlResult As SqlResultBase)
      Me._SqlResult = sqlResult
    End Sub

  End Class
End Namespace