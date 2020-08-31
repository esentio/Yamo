Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class TransactionTests
    Inherits Yamo.Test.Tests.TransactionTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace