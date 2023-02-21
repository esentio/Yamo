Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class ActionsTests
    Inherits Yamo.Test.Tests.ActionsTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace