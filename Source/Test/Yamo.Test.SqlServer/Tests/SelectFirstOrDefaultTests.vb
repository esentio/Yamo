Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectFirstOrDefaultTests
    Inherits Yamo.Test.Tests.SelectFirstOrDefaultTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace