Imports Yamo.Expressions.Builders

''' <summary>
''' Interface for SQL SELECT statement filter provider.
''' </summary>
Public Interface ISelectFilterProvider

  ''' <summary>
  ''' Adds condition(s) to WHERE clause.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="builder"></param>
  Sub AddWhere(Of T)(builder As SelectSqlExpressionBuilder)

End Interface
