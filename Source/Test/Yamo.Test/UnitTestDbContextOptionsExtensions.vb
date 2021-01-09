Imports System.Runtime.CompilerServices
Imports Yamo.Test

Namespace Global.Yamo

  Public Module UnitTestDbContextOptionsExtensions

    <Extension>
    Public Function UseUnitTestSetup(optionsBuilder As DbContextOptionsBuilder) As DbContextOptionsBuilder
      Dim internalBuilder = optionsBuilder.GetInternalBuilder()
      internalBuilder.UseDialectProvider(UnitTestDialectProvider.Instance)
      Return optionsBuilder
    End Function

  End Module
End Namespace