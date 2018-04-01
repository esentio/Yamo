Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class DataTests
    Inherits TestsBase

    <TestMethod()>
    Public Overridable Sub SelectRecordWithAllSupportedValues()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMinValues()
      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMaxValues()

      Insert(item1)
      Insert(item2)
      Insert(item3)

      Dim expected = New List(Of ItemWithAllSupportedValues) From {item1, item2, item3}
      Dim result As List(Of ItemWithAllSupportedValues)

      Using db = CreateDbContext()
        result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()
      End Using

      CollectionAssert.AreEquivalent(expected, result)
    End Sub

    Protected Overridable Sub Insert(item As ItemWithAllSupportedValues)
      ' use explicit SQL rather than built-in insert support (that is tested elsewhere)

      Dim sql As FormattableString = $"
      INSERT INTO ItemWithAllSupportedValues
        (Id
        ,UniqueidentifierColumn
        ,UniqueidentifierColumnNull
        ,Nvarchar50Column
        ,Nvarchar50ColumnNull
        ,NvarcharMaxColumn
        ,NvarcharMaxColumnNull
        ,BitColumn
        ,BitColumnNull
        ,SmallintColumn
        ,SmallintColumnNull
        ,IntColumn
        ,IntColumnNull
        ,BigintColumn
        ,BigintColumnNull
        ,RealColumn
        ,RealColumnNull
        ,FloatColumn
        ,FloatColumnNull
        ,Numeric10and3Column
        ,Numeric10and3ColumnNull
        ,Numeric15and0Column
        ,Numeric15and0ColumnNull
        ,DatetimeColumn
        ,DatetimeColumnNull
        ,Varbinary50Column
        ,Varbinary50ColumnNull
        ,VarbinaryMaxColumn
        ,VarbinaryMaxColumnNull)
      VALUES
        ({item.Id}
        ,{item.UniqueidentifierColumn}
        ,{item.UniqueidentifierColumnNull}
        ,{item.Nvarchar50Column}
        ,{item.Nvarchar50ColumnNull}
        ,{item.NvarcharMaxColumn}
        ,{item.NvarcharMaxColumnNull}
        ,{item.BitColumn}
        ,{item.BitColumnNull}
        ,{item.SmallintColumn}
        ,{item.SmallintColumnNull}
        ,{item.IntColumn}
        ,{item.IntColumnNull}
        ,{item.BigintColumn}
        ,{item.BigintColumnNull}
        ,{item.RealColumn}
        ,{item.RealColumnNull}
        ,{item.FloatColumn}
        ,{item.FloatColumnNull}
        ,{item.Numeric10and3Column}
        ,{item.Numeric10and3ColumnNull}
        ,{item.Numeric15and0Column}
        ,{item.Numeric15and0ColumnNull}
        ,{item.DatetimeColumn}
        ,{item.DatetimeColumnNull}
        ,{item.Varbinary50Column}
        ,CAST({item.Varbinary50ColumnNull} AS varbinary(50))
        ,{item.VarbinaryMaxColumn}
        ,CAST({item.VarbinaryMaxColumnNull} AS varbinary(max))
      )"
      ' NOTE: cast is here, because there are problems with inserting NULL values.
      ' Decribed e.g. here: https://stackoverflow.com/questions/11411854/implicit-conversion-from-data-type-nvarchar-to-varbinarymax-is-not-allowed.
      ' Providing correct SqlDbType is not possible, (at least not with current design). Main reason is that we don't know the type when null/Nothing is passed as a parameter.

      Using db = CreateDbContext()
        db.ExecuteNonQuery(sql)
      End Using
    End Sub

  End Class
End Namespace
