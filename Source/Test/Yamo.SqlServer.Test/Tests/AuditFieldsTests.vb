Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class AuditFieldsTests
    Inherits Yamo.Test.Tests.AuditFieldsTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace