Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class InsertTests
    Inherits Yamo.Test.Tests.InsertTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace