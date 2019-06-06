Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class ExecuteTests
    Inherits Yamo.Test.Tests.ExecuteTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace