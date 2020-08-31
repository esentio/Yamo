Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithHavingTests
    Inherits Yamo.Test.Tests.SelectWithHavingTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace