﻿Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithIncludeTests
    Inherits Yamo.Test.Tests.SelectWithIncludeTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace