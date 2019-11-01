Imports Yamo.Infrastructure

Namespace Infrastructure

  ' TODO: SIP - add documentation to this class.
  Public Class SQLiteEntitySqlStringProviderFactory
    Inherits EntitySqlStringProviderFactory

    Protected Overrides Function GetInsertWhenUseDbIdentityAndDefaults(tableName As String, declareColumns As List(Of String), outputColumnNames As List(Of String), columnNames As List(Of String), parameterNames As List(Of String)) As String
      Return $"INSERT INTO {tableName} ({String.Join(", ", columnNames)}) VALUES ({String.Join(", ", parameterNames)});
SELECT last_insert_rowid()"
    End Function

    Protected Overrides Function GetInsertWhenNotUseDbIdentityAndDefaults(tableName As String, hasIdentityColumn As Boolean, columnNames As List(Of String), parameterNames As List(Of String)) As String
      Return $"INSERT INTO {tableName} ({String.Join(", ", columnNames)}) VALUES ({String.Join(", ", parameterNames)})"
    End Function

    Protected Overrides Function IsInsertWhenUseDefaultsSupported() As Boolean
      Return False
    End Function

  End Class
End Namespace