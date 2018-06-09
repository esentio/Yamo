Imports System.IO
Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class BaseTests

    Protected ReadOnly Property TestEnvironment As ITestEnvironment

    Protected Property ModelFactory As ModelFactory

    Sub New()
      Me.TestEnvironment = CreateTestEnvironment()
      Me.ModelFactory = New ModelFactory
    End Sub

    <TestInitialize()>
    Public Sub Initialize()
      OnInitialize()
    End Sub

    Public Overridable Sub OnInitialize()
    End Sub

    <TestCleanup()>
    Public Sub Uninitialize()
      OnUninitialize()
    End Sub

    Public Overridable Sub OnUninitialize()
    End Sub

    Protected MustOverride Function CreateTestEnvironment() As ITestEnvironment

    Protected Overridable Function CreateDbContext() As BaseTestDbContext
      Return Me.TestEnvironment.CreateDbContext()
    End Function

  End Class
End Namespace