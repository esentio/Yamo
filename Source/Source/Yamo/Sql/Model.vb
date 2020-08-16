Imports System.Linq.Expressions
Imports System.Reflection
Imports Yamo.Infrastructure

Namespace Sql

  ''' <summary>
  ''' Model related SQL helper methods.
  ''' </summary>
  Public Class Model
    Inherits SqlHelper

    ''' <summary>
    ''' Translates to SQL expression that contains columns of defined entity (table).<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="tableAlias"></param>
    ''' <returns></returns>
    Public Shared Function Columns(Of T)(Optional tableAlias As String = Nothing) As ColumnsModelInfo
      Return New ColumnsModelInfo(GetType(T), tableAlias)
    End Function

    ''' <summary>
    ''' Translates to SQL expression that contains column of defined entity (table).<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="propertyName"></param>
    ''' <param name="tableAlias"></param>
    ''' <returns></returns>
    Public Shared Function Column(Of T)(propertyName As String, Optional tableAlias As String = Nothing) As ColumnModelInfo
      Return New ColumnModelInfo(GetType(T), propertyName, tableAlias)
    End Function

    ''' <summary>
    ''' Translates to SQL expression that contains table name of defined entity.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <returns></returns>
    Public Shared Function Table(Of T)() As TableModelInfo
      Return New TableModelInfo(GetType(T))
    End Function

    ''' <summary>
    ''' Returns SQL format string that is appended to final SQL statement.<br/>
    ''' This method is not intended to be called directly.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="method"></param>
    ''' <param name="dialectProvider"></param>
    ''' <returns></returns>
    Public Overloads Shared Function GetSqlFormat(method As MethodCallExpression, dialectProvider As SqlDialectProvider) As SqlFormat
      Throw New InvalidOperationException("This method is not intended to be called in this SQL helper.")
    End Function

  End Class
End Namespace
