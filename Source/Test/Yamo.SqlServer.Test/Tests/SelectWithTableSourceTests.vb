Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithTableSourceTests
    Inherits Yamo.Test.Tests.SelectWithTableSourceTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace