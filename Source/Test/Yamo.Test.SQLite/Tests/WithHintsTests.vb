Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class WithHintsTests
    Inherits Yamo.Test.Tests.WithHintsTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

    Protected Overrides Function GetSelectTableHints() As String
      Return "NOT INDEXED /* select hint */"
    End Function

    Protected Overrides Function GetInsertTableHints() As String
      Return "/* insert hint */"
    End Function

    Protected Overrides Function GetUpdateTableHints() As String
      Return "/* update hint */"
    End Function

    Protected Overrides Function GetDeleteTableHints() As String
      Return "/* delete hint */"
    End Function

    Protected Overrides Function GetSoftDeleteTableHints() As String
      Return "/* soft delete hint */"
    End Function

  End Class
End Namespace