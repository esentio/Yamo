Imports Yamo.Expressions.Builders

''' <summary>
''' Interface for SQL SELECT statement sort provider.
''' </summary>
Public Interface ISelectSortProvider

  ''' <summary>
  ''' Adds column(s) to ORDER BY clause.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="builder"></param>
  Sub AddOrderBy(Of T)(builder As SelectSqlExpressionBuilder)

End Interface
