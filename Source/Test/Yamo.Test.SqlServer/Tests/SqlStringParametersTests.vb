Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SqlStringParametersTests
    Inherits Yamo.Test.Tests.SqlStringParametersTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace