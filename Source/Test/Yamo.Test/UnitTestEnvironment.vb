Public Class UnitTestEnvironment
  Implements ITestEnvironment

  Public Shared Function Create() As UnitTestEnvironment
    Return New UnitTestEnvironment
  End Function

  Public Function CreateDbContext() As BaseTestDbContext Implements ITestEnvironment.CreateDbContext
    Return New UnitTestDbContext
  End Function

End Class
