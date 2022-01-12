Imports Yamo.Test.Model

Namespace Model

  Public Class SQLiteTestModelFactory
    Inherits ModelFactory
    Public Overridable Function CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues() As ItemWithDateAndTimeOnlyFields
      Return New ItemWithDateAndTimeOnlyFields With {
        .DateOnlyColumn = Helpers.Calendar.DateOnlyNow(),
        .DateOnlyColumnNull = Nothing,
        .TimeOnlyColumn = Helpers.Calendar.TimeOnlyNow(),
        .TimeOnlyColumnNull = Nothing
      }
    End Function

    Public Overridable Function CreateItemWithDateAndTimeOnlyFieldsWithMinValues() As ItemWithDateAndTimeOnlyFields
      Return New ItemWithDateAndTimeOnlyFields With {
        .DateOnlyColumn = Helpers.Calendar.GetSqlServerMinDateAsDateOnly(),
        .DateOnlyColumnNull = Helpers.Calendar.GetSqlServerMinDateAsDateOnly(),
        .TimeOnlyColumn = Helpers.Calendar.GetSqlServerMinTimeAsTimeOnly(),
        .TimeOnlyColumnNull = Helpers.Calendar.GetSqlServerMinTimeAsTimeOnly()
      }
    End Function

    Public Overridable Function CreateItemWithDateAndTimeOnlyFieldsWithMaxValues() As ItemWithDateAndTimeOnlyFields
      Return New ItemWithDateAndTimeOnlyFields With {
        .DateOnlyColumn = Helpers.Calendar.GetSqlServerMaxDateAsDateOnly(),
        .DateOnlyColumnNull = Helpers.Calendar.GetSqlServerMaxDateAsDateOnly(),
        .TimeOnlyColumn = Helpers.Calendar.GetSqlServerMaxTimeAsTimeOnly(),
        .TimeOnlyColumnNull = Helpers.Calendar.GetSqlServerMaxTimeAsTimeOnly()
      }
    End Function

  End Class
End Namespace
