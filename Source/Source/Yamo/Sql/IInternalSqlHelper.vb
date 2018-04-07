Imports System.Reflection

Namespace Sql

  Public Interface IInternalSqlHelper

    ReadOnly Property SqlHelperType As Type

    Function GetSqlFormat(method As MethodInfo) As String

  End Interface
End Namespace