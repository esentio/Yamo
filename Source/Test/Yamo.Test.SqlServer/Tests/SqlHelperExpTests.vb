Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SqlHelperExpTests
    Inherits Yamo.Test.Tests.SqlHelperExpTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace