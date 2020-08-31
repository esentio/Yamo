Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithSqlStringWhereTests
    Inherits Yamo.Test.Tests.SelectWithSqlStringWhereTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace