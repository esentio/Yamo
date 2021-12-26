Public Class UnitTestEnvironment
  Implements ITestEnvironment

  Public Shared Function Create() As UnitTestEnvironment
    Return New UnitTestEnvironment
  End Function

  Public Function CreateDbContext() As BaseTestDbContext Implements ITestEnvironment.CreateDbContext
    Return New UnitTestDbContext
  End Function

  Public Sub InitializeDatabase() Implements ITestEnvironment.InitializeDatabase
    Throw New NotSupportedException()
  End Sub

  Public Sub UninitializeDatabase() Implements ITestEnvironment.UninitializeDatabase
    Throw New NotSupportedException()
  End Sub

  Public Function CreateRawValueComparer() As RawValueComparer Implements ITestEnvironment.CreateRawValueComparer
    Return New RawValueComparer()
  End Function

End Class
