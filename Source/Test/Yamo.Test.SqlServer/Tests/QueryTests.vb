Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class QueryTests
    Inherits Yamo.Test.Tests.QueryTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace