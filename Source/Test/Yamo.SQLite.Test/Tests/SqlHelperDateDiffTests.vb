Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SqlHelperDateDiffTests
    Inherits Yamo.Test.Tests.SqlHelperDateDiffTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace