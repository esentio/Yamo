Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class PropertyModifiedTrackingTests
    Inherits Yamo.Test.Tests.PropertyModifiedTrackingTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace