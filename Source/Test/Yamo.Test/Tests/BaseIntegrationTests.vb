Imports System.IO
Imports System.Text.RegularExpressions

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
      Me.TestEnvironment.InitializeDatabase()
    End Sub

    Protected Overridable Sub UninitializeDatabase()
      Me.TestEnvironment.UninitializeDatabase()
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

    Protected Overridable Sub InsertItemsToArchive(Of T)(table As String, ParamArray items As T())
      Using db = CreateDbContext()
        For Each item In items
          db.Insert(Of T)(table).Execute(item)
        Next
      End Using
    End Sub

  End Class
End Namespace