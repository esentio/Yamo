Imports Yamo.Metadata

Namespace Infrastructure

  Public Class AliasedEntityColumnData
    Inherits ColumnDataBase

    Private m_Entity As Entity

    Sub New(entity As Entity)
      m_Entity = entity
    End Sub

    Public Overrides Function GetColumnCount() As Int32
      Return m_Entity.GetPropertiesCount()
    End Function

    Public Overrides Function GetExpectedColumnNames() As String()
      Return {}
    End Function

    Public Overrides Function GetNotExpectedColumnNames() As String()
      Return m_Entity.GetProperties().Select(Function(x) x.ColumnName).ToArray()
    End Function
  End Class
End Namespace
