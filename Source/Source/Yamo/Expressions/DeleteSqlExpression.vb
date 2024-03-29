﻿Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents SQL DELETE statement.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  Public Class DeleteSqlExpression(Of T)
    Inherits DeleteSqlExpressionBase

    ''' <summary>
    ''' Stores whether soft delete is used.
    ''' </summary>
    Private m_SoftDelete As Boolean

    ''' <summary>
    ''' Creates new instance of <see cref="DeleteSqlExpression(Of T)"/>.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="softDelete"></param>
    ''' <param name="tableNameOverride"></param>
    Friend Sub New(context As DbContext, softDelete As Boolean, Optional tableNameOverride As String = Nothing)
      MyBase.New(context, New DeleteSqlExpressionBuilder(context, GetType(T), softDelete, tableNameOverride), New QueryExecutor(context))
      m_SoftDelete = softDelete
    End Sub

    ''' <summary>
    ''' Adds table hint(s).
    ''' </summary>
    ''' <param name="tableHints"></param>
    ''' <returns></returns>
    Public Function WithHints(<DisallowNull> tableHints As String) As WithHintsDeleteSqlExpression(Of T)
      Me.Builder.SetTableHints(tableHints)
      Return New WithHintsDeleteSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor, m_SoftDelete)
    End Function

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