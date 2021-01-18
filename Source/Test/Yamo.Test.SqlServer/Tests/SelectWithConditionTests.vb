Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithConditionTests
    Inherits Yamo.Test.Tests.SelectWithConditionTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

    Protected Overrides Function GetTableHints1() As String
      Return "WITH (NOLOCK) /* hint 1 */"
    End Function

    Protected Overrides Function GetTableHints2() As String
      Return "WITH (NOLOCK) /* hint 2 */"
    End Function

  End Class
End Namespace