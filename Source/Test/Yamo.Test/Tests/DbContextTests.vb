﻿Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class DbContextTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public MustOverride Sub CreateContextWithConnection()

    <TestMethod()>
    Public MustOverride Sub CreateContextWithConnectionFactory()

  End Class
End Namespace
