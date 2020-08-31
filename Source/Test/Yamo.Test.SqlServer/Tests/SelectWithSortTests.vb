Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithSortTests
    Inherits Yamo.Test.Tests.SelectWithSortTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace