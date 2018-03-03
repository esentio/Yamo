Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class ExecuteScalarTests
    Inherits Yamo.Test.Tests.ExecuteScalarTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace