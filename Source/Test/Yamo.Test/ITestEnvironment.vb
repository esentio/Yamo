Public Interface ITestEnvironment

  Function CreateDbContext() As BaseTestDbContext

  Sub InitializeDatabase()

  Sub UninitializeDatabase()

End Interface
