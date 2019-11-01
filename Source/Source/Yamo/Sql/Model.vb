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
    Public Shared Function Columns(Of T)(Optional tableAlias As String = Nothing) As ModelInfo
      Return New ModelInfo(GetType(T), tableAlias)
    End Function

    ''' <summary>
    ''' Returns SQL format string that is appended to final SQL statement.<br/>
    ''' This method is not intended to be called directly.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="method"></param>
    ''' <param name="dialectProvider"></param>
    ''' <returns></returns>
    Public Overloads Shared Function GetSqlFormat(method As MethodInfo, dialectProvider As SqlDialectProvider) As String
      Throw New InvalidOperationException("This method is not intended to be called in this SQL helper.")
    End Function

  End Class
End Namespace
