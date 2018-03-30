Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class CustomSelectTests
    Inherits TestsBase

    <TestMethod()>
    Public Overridable Sub CustomSelect()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues

      InsertItems(item1)

      ' TODO: SIP - implement

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) (x.Nvarchar50Column, x.IntColumn, Item:=x))
        'FirstOrDefault()

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) New With {x.Nvarchar50Column, x.IntColumn, .Item = x})
        'FirstOrDefault()



        Assert.Fail()

      End Using
    End Sub

  End Class
End Namespace
