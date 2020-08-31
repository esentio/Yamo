Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SqlHelperAggregateTests
    Inherits Yamo.Test.Tests.SqlHelperAggregateTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace