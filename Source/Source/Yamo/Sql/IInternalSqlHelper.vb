Imports System.Reflection

Namespace Sql

  ''' <summary>
  ''' Interface to define internal and platform specific SQL helper classes.
  ''' </summary>
  Public Interface IInternalSqlHelper

    ''' <summary>
    ''' Type of SQL helper that uses this internal SQL helper class.
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property SqlHelperType As Type

    ''' <summary>
    ''' Returns SQL format string that is appended to final SQL statement.
    ''' </summary>
    ''' <param name="method"></param>
    ''' <returns></returns>
    Function GetSqlFormat(method As MethodInfo) As String

  End Interface
End Namespace