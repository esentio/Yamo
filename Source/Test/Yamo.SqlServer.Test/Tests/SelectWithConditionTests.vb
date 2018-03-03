Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithConditionTests
    Inherits Yamo.Test.Tests.SelectWithConditionTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace