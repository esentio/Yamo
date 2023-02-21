Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SupportDbLoadTests
    Inherits Yamo.Test.Tests.SupportDbLoadTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace