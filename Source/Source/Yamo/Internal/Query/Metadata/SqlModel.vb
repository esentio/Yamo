Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL related model data.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SqlModel

    ''' <summary>
    ''' Gets model.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    Public ReadOnly Model As Model

    ''' <summary>
    ''' Store SQL entities.
    ''' </summary>
    Private m_Entities As List(Of SqlEntity)

    ''' <summary>
    ''' Store custom SQL entities.
    ''' </summary>
    Private m_CustomEntities As CustomSqlEntity()

    ''' <summary>
    ''' Creates new instance of <see cref="SqlModel"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    Public Sub New(model As Model)
      Me.Model = model
      m_Entities = New List(Of SqlEntity)
    End Sub

    ''' <summary>
    ''' Sets main table.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    Public Sub SetMainTable(Of T)()
      If Not m_Entities.Count = 0 Then
        Throw New InvalidOperationException("Main table already set.")
      End If

      Dim entity = Me.Model.GetEntity(GetType(T))
      Dim index = m_Entities.Count
      Dim tableAlias = "T" & index.ToString(Globalization.CultureInfo.InvariantCulture)

      m_Entities.Add(New SqlEntity(entity, tableAlias, index))
    End Sub

    ''' <summary>
    ''' Adds joined table.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="relationship"></param>
    Public Sub AddJoinedTable(Of T)(Optional relationship As SqlEntityRelationship = Nothing)
      If m_Entities.Count = 0 Then
        Throw New InvalidOperationException("Main table isn't set yet.")
      End If

      Dim entity = Me.Model.GetEntity(GetType(T))
      Dim index = m_Entities.Count
      Dim tableAlias = "T" & index.ToString(Globalization.CultureInfo.InvariantCulture)

      m_Entities.Add(New SqlEntity(entity, tableAlias, index, relationship, False))
    End Sub

    ''' <summary>
    ''' Adds ignored joined table.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entityType"></param>
    Public Sub AddIgnoredJoinedTable(entityType As Type)
      If m_Entities.Count = 0 Then
        Throw New InvalidOperationException("Main table isn't set yet.")
      End If

      Dim entity = Me.Model.GetEntity(entityType)
      Dim index = m_Entities.Count
      Dim tableAlias = "T" & index.ToString(Globalization.CultureInfo.InvariantCulture)

      m_Entities.Add(New SqlEntity(entity, tableAlias, index, Nothing, True))
    End Sub

    ''' <summary>
    ''' Gets first entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFirstEntity() As SqlEntity
      Return m_Entities(0)
    End Function

    ''' <summary>
    ''' Gets last entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetLastEntity() As SqlEntity
      Return m_Entities(m_Entities.Count - 1)
    End Function

    ''' <summary>
    ''' Gets entity by its index.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Public Function GetEntity(index As Int32) As SqlEntity
      Return m_Entities(index)
    End Function

    ''' <summary>
    ''' Gets all entities.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetEntities() As SqlEntity()
      Return m_Entities.ToArray()
    End Function

    ''' <summary>
    ''' Checks wheter join is used.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function ContainsJoins() As Boolean
      Return 1 < m_Entities.Count
    End Function

    ''' <summary>
    ''' Gets table alias.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Public Function GetTableAlias(index As Int32) As String
      Return m_Entities(index).TableAlias
    End Function

    ''' <summary>
    ''' Gets table alias of the first entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFirstTableAlias() As String
      Return m_Entities(0).TableAlias
    End Function

    ''' <summary>
    ''' Gets table alias of the last entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetLastTableAlias() As String
      Return m_Entities(m_Entities.Count - 1).TableAlias
    End Function

    ''' <summary>
    ''' Get entities count.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetEntityCount() As Int32
      Return m_Entities.Count
    End Function

    ''' <summary>
    ''' Sets custom entities.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entities"></param>
    Public Sub SetCustomEntities(entities As CustomSqlEntity())
      m_CustomEntities = entities
    End Sub

    ''' <summary>
    ''' Gets custom entities.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCustomEntities() As CustomSqlEntity()
      Return m_CustomEntities.ToArray()
    End Function

  End Class
End Namespace