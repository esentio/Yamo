Namespace Tests

  Public MustInherit Class BaseUnitTests
    Inherits BaseTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return UnitTestEnvironment.Create()
    End Function

  End Class
End Namespace