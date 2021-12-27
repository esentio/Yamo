Imports System.Data
Imports System.Data.Common

Public Interface ITestEnvironment

  Function CreateDbContext() As BaseTestDbContext

  Sub InitializeDatabase()

  Sub UninitializeDatabase()

  Function CreateDbParameter(value As Object, dbType As DbType) As DbParameter

  Function CreateRawValueComparer() As RawValueComparer

End Interface
