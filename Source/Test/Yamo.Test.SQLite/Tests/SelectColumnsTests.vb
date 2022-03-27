Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectColumnsTests
    Inherits Yamo.Test.Tests.SelectColumnsTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace