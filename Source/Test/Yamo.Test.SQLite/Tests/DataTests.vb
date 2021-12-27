﻿Imports Yamo.Test
Imports Yamo.Test.Model

Namespace Tests

  <TestClass()>
  Public Class DataTests
    Inherits Yamo.Test.Tests.DataTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace