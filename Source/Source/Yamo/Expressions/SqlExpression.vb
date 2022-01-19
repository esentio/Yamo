Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents SQL statement.
  ''' </summary>
  Public Class SqlExpression
    Inherits SqlExpressionBase

    ''' <summary>
    ''' Gets builder.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Protected Property Builder As SqlExpressionBuilder

    ''' <summary>
    ''' Gets query executor.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Protected Property Executor As QueryExecutor

    ''' <summary>
    ''' Creates new instance of <see cref="SqlExpression"/>.
    ''' </summary>
    ''' <param name="context"></param>
    Friend Sub New(context As DbContext)
      Me.Builder = New SqlExpressionBuilder(context)
      Me.Executor = New QueryExecutor(context)
    End Sub

    ''' <summary>
    ''' Executes SQL query and returns the number of affected rows.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    Public Function Execute(<DisallowNull> sql As FormattableString) As Int32
      Dim query = Me.Builder.CreateQuery(sql)
      Return Me.Executor.Execute(query)
    End Function

    ''' <summary>
    ''' Executes SQL query and returns the number of affected rows.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function Execute(<DisallowNull> sql As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As Int32
      Dim query = Me.Builder.CreateQuery(sql, parameters)
      Return Me.Executor.Execute(query)
    End Function

    ''' <summary>
    ''' Executes SQL query and returns first record or a default value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    Public Function QueryFirstOrDefault(Of T)(<DisallowNull> sql As FormattableString) As T
      Dim query = Me.Builder.CreateQuery(sql)
      Return Me.Executor.QueryFirstOrDefault(Of T)(query)
    End Function

    ''' <summary>
    ''' Executes SQL query and returns first record or a default value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="sql"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function QueryFirstOrDefault(Of T)(<DisallowNull> sql As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As T
      Dim query = Me.Builder.CreateQuery(sql, parameters)
      Return Me.Executor.QueryFirstOrDefault(Of T)(query)
    End Function

    ''' <summary>
    ''' Executes SQL query and returns list of records.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    Public Function Query(Of T)(<DisallowNull> sql As FormattableString) As List(Of T)
      Dim q = Me.Builder.CreateQuery(sql)
      Return Me.Executor.QueryList(Of T)(q)
    End Function

    ''' <summary>
    ''' Executes SQL query and returns list of records.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="sql"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function Query(Of T)(<DisallowNull> sql As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As List(Of T)
      Dim q = Me.Builder.CreateQuery(sql, parameters)
      Return Me.Executor.QueryList(Of T)(q)
    End Function

  End Class
End Namespace
