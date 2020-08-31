Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectTests
    Inherits Yamo.Test.Tests.SelectTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace