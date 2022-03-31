Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithJoinSubqueryTests
    Inherits Yamo.Test.Tests.SelectWithJoinSubqueryTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace