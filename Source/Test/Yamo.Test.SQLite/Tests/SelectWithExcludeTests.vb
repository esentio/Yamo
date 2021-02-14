Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithExcludeTests
    Inherits Yamo.Test.Tests.SelectWithExcludeTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace