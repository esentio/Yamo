Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithSqlStringConditionTests
    Inherits Yamo.Test.Tests.SelectWithSqlStringConditionTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace