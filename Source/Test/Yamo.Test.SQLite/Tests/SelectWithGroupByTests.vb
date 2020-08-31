Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithGroupByTests
    Inherits Yamo.Test.Tests.SelectWithGroupByTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace