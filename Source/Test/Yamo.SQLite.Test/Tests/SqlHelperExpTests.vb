Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SqlHelperExpTests
    Inherits Yamo.Test.Tests.SqlHelperExpTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

    <TestMethod()>
    Public Overrides Sub SelectUsingIIf()
      ' do nothing
      ' TODO: work since SQLite 3.32.0, which was released on 22 May 2020 - update SQLite and test this!
      ' For support of SQLite 3.32.0 see: https://github.com/ericsink/SQLitePCL.raw/issues/350
    End Sub

  End Class
End Namespace