Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithExplicitRelationship
    Inherits Yamo.Test.Tests.SelectWithExplicitRelationship

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

  End Class
End Namespace