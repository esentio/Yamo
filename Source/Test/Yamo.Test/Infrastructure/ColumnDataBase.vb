Namespace Infrastructure

  Public MustInherit Class ColumnDataBase

    Public MustOverride Function GetColumnCount() As Int32

    Public MustOverride Function GetExpectedColumnNames() As String()

    Public MustOverride Function GetNotExpectedColumnNames() As String()

  End Class
End Namespace
