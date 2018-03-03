Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class DeleteTests
    Inherits Yamo.Test.Tests.DeleteTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace