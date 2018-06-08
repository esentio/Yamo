Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  Public Class SqlModel

    Public ReadOnly Model As Model

    Private m_Entities As List(Of SqlEntity)

    Private m_CustomEntities As CustomSqlEntity()

    Public Sub New(model As Model)
      Me.Model = model
      m_Entities = New List(Of SqlEntity)
    End Sub

    Public Sub SetMainTable(Of T)()
      If m_Entities.Any() Then
        Throw New InvalidOperationException("Main table already set.")
      End If

      Dim entity = Me.Model.GetEntity(GetType(T))
      Dim index = m_Entities.Count
      Dim tableAlias = "T" & index.ToString(Globalization.CultureInfo.InvariantCulture)

      m_Entities.Add(New SqlEntity(entity, tableAlias, index))
    End Sub

    Public Sub AddJoinedTable(Of T)(Optional relationship As SqlEntityRelationship = Nothing)
      If Not m_Entities.Any() Then
        Throw New InvalidOperationException("Main table isn't set yet.")
      End If

      Dim entity = Me.Model.GetEntity(GetType(T))
      Dim index = m_Entities.Count
      Dim tableAlias = "T" & index.ToString(Globalization.CultureInfo.InvariantCulture)

      m_Entities.Add(New SqlEntity(entity, tableAlias, index, relationship))
    End Sub

    Public Function GetFirstEntity() As SqlEntity
      Return m_Entities(0)
    End Function

    Public Function GetLastEntity() As SqlEntity
      Return m_Entities(m_Entities.Count - 1)
    End Function

    Public Function GetEntity(index As Int32) As SqlEntity
      Return m_Entities(index)
    End Function

    Public Function GetEntities() As SqlEntity()
      Return m_Entities.ToArray()
    End Function

    Public Function ContainsJoins() As Boolean
      Return 1 < m_Entities.Count
    End Function

    Public Function GetTableAlias(index As Int32) As String
      Return m_Entities(index).TableAlias
    End Function

    Public Function GetFirstTableAlias() As String
      Return m_Entities(0).TableAlias
    End Function

    Public Function GetLastTableAlias() As String
      Return m_Entities(m_Entities.Count - 1).TableAlias
    End Function

    Public Function GetEntityCount() As Int32
      Return m_Entities.Count
    End Function

    Public Sub SetCustomEntities(entities As CustomSqlEntity())
      m_CustomEntities = entities
    End Sub

    Public Function GetCustomEntities() As CustomSqlEntity()
      Return m_CustomEntities.ToArray()
    End Function

  End Class
End Namespace