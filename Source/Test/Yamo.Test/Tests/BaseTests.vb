Imports System.IO
Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class TestsBase

    Protected ReadOnly Property TestEnvironment As ITestEnvironment

    Protected Property ModelFactory As ModelFactory

    Sub New()
      Me.TestEnvironment = CreateTestEnvironment()
      Me.ModelFactory = New ModelFactory
    End Sub

    <TestInitialize()>
    Public Overridable Sub Initialize()
      ReinitializeDatabase()
    End Sub

    <TestCleanup()>
    Public Overridable Sub Uninitialize()
      UninitializeDatabase()
    End Sub

    Protected MustOverride Function CreateTestEnvironment() As ITestEnvironment

    Protected Overridable Function CreateDbContext() As BaseTestDbContext
      Return Me.TestEnvironment.CreateDbContext()
    End Function

    Protected Overridable Sub InitializeDatabase()
      Using db = CreateDbContext()
        Dim sql = File.ReadAllText("Sql\DbInitialize.sql")
        db.ExecuteNonQuery(sql)
      End Using
    End Sub

    Protected Overridable Sub UninitializeDatabase()
      Using db = CreateDbContext()
        Dim sql = File.ReadAllText("Sql\DbUninitialize.sql")
        db.ExecuteNonQuery(sql)
      End Using
    End Sub

    Protected Overridable Sub ReinitializeDatabase()
      UninitializeDatabase()
      InitializeDatabase()
    End Sub

    Protected Overridable Sub InsertItems(items As IEnumerable)
      Using db = CreateDbContext()
        For Each item In items
          db.Insert(item)
        Next
      End Using
    End Sub

    Protected Overridable Sub InsertItems(ParamArray items As Object())
      Using db = CreateDbContext()
        For Each item In items
          db.Insert(item)
        Next
      End Using
    End Sub

  End Class
End Namespace