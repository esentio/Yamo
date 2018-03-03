Namespace Sql

  Public Interface IInternalSqlHelper

    ReadOnly Property SqlHelperType As Type

    Function GetSqlFormat(methodName As String) As String

  End Interface
End Namespace