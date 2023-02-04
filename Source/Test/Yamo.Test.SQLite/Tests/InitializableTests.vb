Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class InitializableTests
    Inherits Yamo.Test.Tests.InitializableTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace