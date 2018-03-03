Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  Public Class SqlModel

    Public ReadOnly Model As Model

    Private m_Enitities As List(Of SqlEntity)

    Public Sub New(model As Model)
      Me.Model = model
      m_Enitities = New List(Of SqlEntity)
    End Sub

    Public Sub SetMainTable(Of T)()
      If m_Enitities.Any() Then
        Throw New InvalidOperationException("Main table already set.")
      End If

      Dim entity = Me.Model.GetEntity(GetType(T))
      Dim index = m_Enitities.Count
      Dim tableAlias = $"T{(index).ToString(Globalization.CultureInfo.InvariantCulture)}"

      m_Enitities.Add(New SqlEntity(entity, tableAlias, index))
    End Sub

    Public Sub AddJoinedTable(Of T)(Optional relationship As SqlEntityRelationship = Nothing)
      If Not m_Enitities.Any() Then
        Throw New InvalidOperationException("Main table isn't set yet.")
      End If

      Dim entity = Me.Model.GetEntity(GetType(T))
      Dim index = m_Enitities.Count
      Dim tableAlias = $"T{(index).ToString(Globalization.CultureInfo.InvariantCulture)}"

      m_Enitities.Add(New SqlEntity(entity, tableAlias, index, relationship))
    End Sub

    Public Function GetFirstEntity() As SqlEntity
      Return m_Enitities(0)
    End Function

    Public Function GetLastEntity() As SqlEntity
      Return m_Enitities(m_Enitities.Count - 1)
    End Function

    Public Function GetEntity(index As Int32) As SqlEntity
      Return m_Enitities(index)
    End Function

    Public Function GetEntities() As SqlEntity()
      Return m_Enitities.ToArray()
    End Function

    Public Function ContainsJoins() As Boolean
      Return 1 < m_Enitities.Count
    End Function

    Public Function GetTableAlias(index As Int32) As String
      Return m_Enitities(index).TableAlias
    End Function

    Public Function GetFirstTableAlias() As String
      Return m_Enitities(0).TableAlias
    End Function

    Public Function GetLastTableAlias() As String
      Return m_Enitities(m_Enitities.Count - 1).TableAlias
    End Function

    Public Function GetEntityCount() As Int32
      Return m_Enitities.Count
    End Function

  End Class
End Namespace