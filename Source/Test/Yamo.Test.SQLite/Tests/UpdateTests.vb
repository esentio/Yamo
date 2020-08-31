Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class UpdateTests
    Inherits Yamo.Test.Tests.UpdateTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace