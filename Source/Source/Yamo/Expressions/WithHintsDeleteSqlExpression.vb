﻿Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents SQL DELETE statement with defined table hints.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  Public Class WithHintsDeleteSqlExpression(Of T)
    Inherits DeleteSqlExpressionBase

    ''' <summary>
    ''' Stores whether soft delete is used.
    ''' </summary>
    Private m_SoftDelete As Boolean

    ''' <summary>
    ''' Creates new instance of <see cref="WithHintsDeleteSqlExpression(Of T)"/>.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    ''' <param name="softDelete"></param>
    Friend Sub New(context As DbContext, builder As DeleteSqlExpressionBuilder, executor As QueryExecutor, softDelete As Boolean)
      MyBase.New(context, builder, executor)
      m_SoftDelete = softDelete
    End Sub

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of T, Boolean))) As FilteredDeleteSqlExpression(Of T)
      Me.Builder.AddWhere(predicate)
      Return New FilteredDeleteSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of T, FormattableString))) As FilteredDeleteSqlExpression(Of T)
      Me.Builder.AddWhere(predicate)
      Return New FilteredDeleteSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object) As FilteredDeleteSqlExpression(Of T)
      Me.Builder.AddWhere(predicate, parameters)
      Return New FilteredDeleteSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Executes DELETE statement or UPDATE statement that marks record(s) as (soft) deleted if expression was created with <see cref="DbContext.SoftDelete(Of T)"/> call. Returns the number of affected rows.
    ''' </summary>
    ''' <returns></returns>
    Public Function Execute() As Int32
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.Execute(query)
    End Function

    ''' <summary>
    ''' Executes DELETE statement or UPDATE statement that marks record as (soft) deleted if expression was created with <see cref="DbContext.SoftDelete(Of T)"/> call. Returns the number of affected rows.
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Public Function Execute(<DisallowNull> obj As T) As Int32
      If m_SoftDelete Then
        ' NOTE: this doesn't reset property modified tracking!
        Dim setter = EntityAutoFieldsSetterCache.GetOnDeleteSetter(Me.DbContext.Model, GetEntityType(obj))
        setter(obj, Me.DbContext)

        Dim query = Me.Builder.CreateSoftDeleteQuery(obj)
        Return Me.Executor.Execute(query)
      Else
        Dim query = Me.Builder.CreateDeleteQuery(obj)
        Return Me.Executor.Execute(query)
      End If
    End Function

  End Class
End Namespace