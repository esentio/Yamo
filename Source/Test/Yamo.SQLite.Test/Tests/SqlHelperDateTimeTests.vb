Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SqlHelperDateTimeTests
    Inherits Yamo.Test.Tests.SqlHelperDateTimeTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace