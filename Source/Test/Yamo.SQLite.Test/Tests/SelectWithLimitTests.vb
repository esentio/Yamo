Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithLimitTests
    Inherits Yamo.Test.Tests.SelectWithLimitTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace