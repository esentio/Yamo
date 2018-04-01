Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class CustomSelectTests
    Inherits Yamo.Test.Tests.CustomSelectTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace