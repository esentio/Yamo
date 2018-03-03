Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class ExecuteScalarTests
    Inherits Yamo.Test.Tests.ExecuteScalarTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace