Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithJoinTests
    Inherits Yamo.Test.Tests.SelectWithJoinTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace