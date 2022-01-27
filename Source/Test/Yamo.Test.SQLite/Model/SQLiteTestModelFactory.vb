Imports Yamo.Test.Model

Namespace Model

  Public Class SQLiteTestModelFactory
    Inherits ModelFactory
    Public Overridable Function CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues() As ItemWithOnlySQLiteSupportedFields
      Return New ItemWithOnlySQLiteSupportedFields With {
        .DateOnlyColumn = Helpers.Calendar.DateOnlyNow(),
        .DateOnlyColumnNull = Nothing,
        .TimeOnlyColumn = Helpers.Calendar.TimeOnlyNow(),
        .TimeOnlyColumnNull = Nothing,
        .Nchar1Column = "a"c,
        .Nchar1ColumnNull = Nothing
      }
    End Function

    Public Overridable Function CreateItemWithOnlySQLiteSupportedFieldsWithMinValues() As ItemWithOnlySQLiteSupportedFields
      Return New ItemWithOnlySQLiteSupportedFields With {
        .DateOnlyColumn = Helpers.Calendar.GetSqlServerMinDateAsDateOnly(),
        .DateOnlyColumnNull = Helpers.Calendar.GetSqlServerMinDateAsDateOnly(),
        .TimeOnlyColumn = Helpers.Calendar.GetSqlServerMinTimeAsTimeOnly(),
        .TimeOnlyColumnNull = Helpers.Calendar.GetSqlServerMinTimeAsTimeOnly(),
        .Nchar1Column = Char.MinValue,
        .Nchar1ColumnNull = Nothing
      }
    End Function

    Public Overridable Function CreateItemWithOnlySQLiteSupportedFieldsWithMaxValues() As ItemWithOnlySQLiteSupportedFields
      Return New ItemWithOnlySQLiteSupportedFields With {
        .DateOnlyColumn = Helpers.Calendar.GetSqlServerMaxDateAsDateOnly(),
        .DateOnlyColumnNull = Helpers.Calendar.GetSqlServerMaxDateAsDateOnly(),
        .TimeOnlyColumn = Helpers.Calendar.GetSqlServerMaxTimeAsTimeOnly(),
        .TimeOnlyColumnNull = Helpers.Calendar.GetSqlServerMaxTimeAsTimeOnly(),
        .Nchar1Column = Char.MaxValue,
        .Nchar1ColumnNull = Char.MaxValue
      }
    End Function

  End Class
End Namespace
