Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithLimitTests
    Inherits Yamo.Test.Tests.SelectWithLimitTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SqlServerTestEnvironment.Create()
    End Function

    <TestMethod()>
    Public Overrides Sub SelectUnorderedWithLimitAndOffset()
      Try
        MyBase.SelectUnorderedWithLimitAndOffset()
        Assert.Fail()
      Catch ex As Exception
        If Not ex.Message.Contains("Incorrect syntax near 'OFFSET'.") Then
          Assert.Fail(ex.Message)
        End If
      End Try
    End Sub

  End Class
End Namespace