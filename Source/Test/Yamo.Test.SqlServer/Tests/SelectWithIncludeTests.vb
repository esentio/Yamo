Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithIncludeTests
    Inherits Yamo.Test.Tests.SelectWithIncludeTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace