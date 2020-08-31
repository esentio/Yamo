Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SqlHelperDateTimeTests
    Inherits Yamo.Test.Tests.SqlHelperDateTimeTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace