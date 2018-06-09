Imports System.IO

Namespace Tests

  Public MustInherit Class BaseIntegrationTests
    Inherits BaseTests

    Public Overrides Sub OnInitialize()
      ReinitializeDatabase()
    End Sub

    Public Overrides Sub OnUninitialize()
      UninitializeDatabase()
    End Sub

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