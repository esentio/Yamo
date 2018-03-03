Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectCountTests
    Inherits Yamo.Test.Tests.SelectCountTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace