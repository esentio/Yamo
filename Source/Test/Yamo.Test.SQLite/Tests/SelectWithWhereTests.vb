Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithWhereTests
    Inherits Yamo.Test.Tests.SelectWithWhereTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace