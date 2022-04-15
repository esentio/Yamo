Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents SQL SELECT statement with result of type <typeparamref name="T"/> that can act as a subquery.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  Public Interface ISubqueryableSelectSqlExpression(Of T)

    ''' <summary>
    ''' Creates SQL subquery.
    ''' </summary>
    ''' <returns></returns>
    Function ToSubquery() As Subquery(Of T)

  End Interface
End Namespace
