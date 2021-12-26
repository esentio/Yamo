Public Interface ITestEnvironment

  Function CreateDbContext() As BaseTestDbContext

  Sub InitializeDatabase()

  Sub UninitializeDatabase()

  Function CreateRawValueComparer() As RawValueComparer

End Interface
