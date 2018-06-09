Public Class UnitTestDbContext
  Inherits BaseTestDbContext

  Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
    optionsBuilder.UseUnitTestSetup()
  End Sub

End Class
