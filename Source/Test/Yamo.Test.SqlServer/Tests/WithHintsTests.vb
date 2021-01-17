Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class WithHintsTests
    Inherits Yamo.Test.Tests.WithHintsTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

    Protected Overrides Function GetSelectTableHints() As String
      Return "WITH (NOLOCK) /* select hint */"
    End Function

    Protected Overrides Function GetInsertTableHints() As String
      Return "WITH (TABLOCK) /* insert hint */"
    End Function

    Protected Overrides Function GetUpdateTableHints() As String
      Return "WITH (TABLOCK) /* update hint */"
    End Function

    Protected Overrides Function GetDeleteTableHints() As String
      Return "WITH (TABLOCK) /* delete hint */"
    End Function

    Protected Overrides Function GetSoftDeleteTableHints() As String
      Return "WITH (TABLOCK) /* soft delete hint */"
    End Function

  End Class
End Namespace