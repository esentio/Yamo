Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SqlHelperAggregateTests
    Inherits Yamo.Test.Tests.SqlHelperAggregateTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

    <TestMethod()>
    Public Overrides Sub SelectUsingStdev()
      ' do nothing
      ' STDEV in SQLite only works with extension-functions.c and Microsoft.Data.SQLite doesn't support it
    End Sub

    <TestMethod()>
    Public Overrides Sub SelectUsingStdevDistinct()
      ' do nothing
      ' STDEV in SQLite only works with extension-functions.c and Microsoft.Data.SQLite doesn't support it
    End Sub

  End Class
End Namespace