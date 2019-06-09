Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class QueryFirstOrDefaultTests
    Inherits Yamo.Test.Tests.QueryFirstOrDefaultTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace