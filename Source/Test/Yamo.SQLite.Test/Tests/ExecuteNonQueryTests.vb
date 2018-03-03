Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class ExecuteNonQueryTests
    Inherits Yamo.Test.Tests.ExecuteNonQueryTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace