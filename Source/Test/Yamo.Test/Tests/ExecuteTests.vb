Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class ExecuteTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    Protected Const German As String = "de"

    <TestMethod()>
    Public Overridable Sub ExecuteUsingFormattableString()
      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)

      InsertItems(label1En, label2En)

      Using db = CreateDbContext()
        Dim result = db.Execute($"UPDATE Label SET Language = {German}")
        Assert.AreEqual(2, result)

        result = db.From(Of Label).Where(Function(o) o.Language = German).SelectCount()
        Assert.AreEqual(2, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteUsingRawSqlString()
      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)

      InsertItems(label1En, label2En)

      Using db = CreateDbContext()
        Dim result = db.Execute("UPDATE Label SET Language = 'de'")
        Assert.AreEqual(2, result)

        result = db.From(Of Label).Where(Function(o) o.Language = German).SelectCount()
        Assert.AreEqual(2, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteUsingRawSqlStringWithParameters()
      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)

      InsertItems(label1En, label2En)

      Using db = CreateDbContext()
        Dim result = db.Execute("UPDATE Label SET Language = {0}", German)
        Assert.AreEqual(2, result)

        result = db.From(Of Label).Where(Function(o) o.Language = German).SelectCount()
        Assert.AreEqual(2, result)
      End Using
    End Sub

  End Class
End Namespace
