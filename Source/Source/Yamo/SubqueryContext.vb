Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Expressions
Imports Yamo.Internal.Query

''' <summary>
''' A <see cref="SubqueryContext"/> instance represents a context for creating SQL subquery.
''' </summary>
Public Class SubqueryContext

  ''' <summary>
  ''' Gets context.
  ''' </summary>
  ''' <returns></returns>
  Friend ReadOnly Property DbContext As DbContext

  ''' <summary>
  ''' Gets query executor.
  ''' </summary>
  ''' <returns></returns>
  Friend ReadOnly Property Executor As QueryExecutor

  ''' <summary>
  ''' Gets parameter index offset.
  ''' </summary>
  ''' <returns></returns>
  Friend ReadOnly Property ParameterIndexOffset As Int32

  ''' <summary>
  ''' Initializes a new instance of <see cref="SubqueryContext"/>.
  ''' </summary>
  ''' <param name="context"></param>
  ''' <param name="executor"></param>
  ''' <param name="parameterIndexOffset"></param>
  Friend Sub New(context As DbContext, executor As QueryExecutor, parameterIndexOffset As Int32)
    Me.DbContext = context
    Me.Executor = executor
    Me.ParameterIndexOffset = parameterIndexOffset
  End Sub

  ''' <summary>
  ''' Starts building SQL SELECT statement.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <returns></returns>
  Public Function From(Of T)() As SelectSqlExpression(Of T)
    Return New SelectSqlExpression(Of T)(Me)
  End Function

  ''' <summary>
  ''' Starts building SQL SELECT statement.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="tableSourceFactory"></param>
  ''' <param name="behavior"></param>
  ''' <returns></returns>
  Public Function From(Of T)(<DisallowNull> tableSourceFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of T)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.NullIfAllColumnsAreNull) As SelectSqlExpression(Of T)
    Return New SelectSqlExpression(Of T)(Me, tableSourceFactory, behavior)
  End Function

  ''' <summary>
  ''' Starts building SQL SELECT statement.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="tableSource"></param>
  ''' <returns></returns>
  Public Function From(Of T)(<DisallowNull> tableSource As FormattableString) As SelectSqlExpression(Of T)
    Return New SelectSqlExpression(Of T)(Me, tableSource)
  End Function

  ''' <summary>
  ''' Starts building SQL SELECT statement.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="tableSource"></param>
  ''' <param name="parameters"></param>
  ''' <returns></returns>
  Public Function From(Of T)(<DisallowNull> tableSource As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As SelectSqlExpression(Of T)
    Return New SelectSqlExpression(Of T)(Me, tableSource, parameters)
  End Function

End Class
