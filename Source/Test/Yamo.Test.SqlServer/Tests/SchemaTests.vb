Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SchemaTests
    Inherits Yamo.Test.Tests.SchemaTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

  End Class
End Namespace