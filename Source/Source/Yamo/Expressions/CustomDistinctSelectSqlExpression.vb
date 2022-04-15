﻿Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents DISTINCT clause in SQL SELECT statement.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  Public Class CustomDistinctSelectSqlExpression(Of T)
    Inherits SelectSqlExpressionBase
    Implements ISubqueryableSelectSqlExpression(Of T)

    ''' <summary>
    ''' Creates new instance of <see cref="CustomDistinctSelectSqlExpression(Of T)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    ''' <summary>
    ''' Creates SQL subquery.
    ''' </summary>
    ''' <returns></returns>
    Public Function ToSubquery() As Subquery(Of T) Implements ISubqueryableSelectSqlExpression(Of T).ToSubquery
      Return Me.Builder.CreateSubquery(Of T)()
    End Function

    ''' <summary>
    ''' Executes SQL query and returns list of records.
    ''' </summary>
    ''' <returns></returns>
    Public Function ToList() As List(Of T)
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadCustomList(Of T)(query)
    End Function

    ''' <summary>
    ''' Executes SQL query and returns first record or a default value.
    ''' </summary>
    ''' <returns></returns>
    Public Function FirstOrDefault() As <MaybeNull> T
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadCustomFirstOrDefault(Of T)(query)
    End Function

  End Class
End Namespace
