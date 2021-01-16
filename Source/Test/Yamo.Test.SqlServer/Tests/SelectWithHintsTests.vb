Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithHintsTests
    Inherits Yamo.Test.Tests.SelectWithHintsTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

    Protected Overrides Function GetTableHints() As String
      Return "WITH (NOLOCK) /* hint 1 */"
    End Function

  End Class
End Namespace