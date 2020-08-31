Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectDistinctTests
    Inherits Yamo.Test.Tests.SelectDistinctTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace