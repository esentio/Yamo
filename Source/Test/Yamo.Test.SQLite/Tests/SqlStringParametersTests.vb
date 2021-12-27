Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SqlStringParametersTests
    Inherits Yamo.Test.Tests.SqlStringParametersTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace