Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class DataTests
    Inherits Yamo.Test.Tests.DataTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace