Namespace Infrastructure

  Public Class NonModelEntityColumnData
    Inherits ColumnDataBase

    Private m_ColumnCount As Int32

    Sub New(columnCount As Int32)
      m_ColumnCount = columnCount
    End Sub

    Public Overrides Function GetColumnCount() As Int32
      Return m_ColumnCount
    End Function

    Public Overrides Function GetExpectedColumnNames() As String()
      Return {}
    End Function

    Public Overrides Function GetNotExpectedColumnNames() As String()
      Return {}
    End Function
  End Class
End Namespace
