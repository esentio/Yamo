Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithHintsTests
    Inherits Yamo.Test.Tests.SelectWithHintsTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

    Protected Overrides Function GetTableHints() As String
      Return "NOT INDEXED /* hint 1 */"
    End Function

  End Class
End Namespace