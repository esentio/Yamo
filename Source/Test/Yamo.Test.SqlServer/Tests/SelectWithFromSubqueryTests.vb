Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithFromSubqueryTests
    Inherits Yamo.Test.Tests.SelectWithFromSubqueryTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace