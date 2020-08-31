Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SqlHelperModelTests
    Inherits Yamo.Test.Tests.SqlHelperModelTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace