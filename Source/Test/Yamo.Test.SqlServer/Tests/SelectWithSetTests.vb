Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithSetTests
    Inherits Yamo.Test.Tests.SelectWithSetTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace