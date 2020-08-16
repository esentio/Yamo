Imports Microsoft.Extensions.Configuration
Imports Yamo.CodeGenerator.Generator

Module Program

  Sub Main(args As String())
    Dim config = New ConfigurationBuilder().AddJsonFile("appsettings.json", True, True).Build()

    Dim indentation = config("Indentation")
    Dim maxEntityCount = Int32.Parse(config("MaxEntityCount"))
    Dim outputFolder = config("OutputFolder")

    ' generate code

    Dim customDistinctSelectSqlExpressionCodeGenerator = New CustomDistinctSelectSqlExpressionCodeGenerator(indentation, outputFolder)
    customDistinctSelectSqlExpressionCodeGenerator.Generate()

    Dim customSelectSqlExpressionCodeGenerator = New CustomSelectSqlExpressionCodeGenerator(indentation, outputFolder)
    customSelectSqlExpressionCodeGenerator.Generate()

    Dim distinctSelectSqlExpressionCodeGenerator = New DistinctSelectSqlExpressionCodeGenerator(indentation, outputFolder)
    distinctSelectSqlExpressionCodeGenerator.Generate()

    Dim filteredSelectSqlExpressionCodeGenerator = New FilteredSelectSqlExpressionCodeGenerator(indentation, maxEntityCount, outputFolder)
    filteredSelectSqlExpressionCodeGenerator.Generate()

    Dim groupedSelectSqlExpressionCodeGenerator = New GroupedSelectSqlExpressionCodeGenerator(indentation, maxEntityCount, outputFolder)
    groupedSelectSqlExpressionCodeGenerator.Generate()

    Dim havingSelectSqlExpressionCodeGenerator = New HavingSelectSqlExpressionCodeGenerator(indentation, maxEntityCount, outputFolder)
    havingSelectSqlExpressionCodeGenerator.Generate()

    Dim joinedSelectSqlExpressionCodeGenerator = New JoinedSelectSqlExpressionCodeGenerator(indentation, maxEntityCount, outputFolder)
    joinedSelectSqlExpressionCodeGenerator.Generate()

    Dim joinSelectSqlExpressionCodeGenerator = New JoinSelectSqlExpressionCodeGenerator(indentation, maxEntityCount, outputFolder)
    joinSelectSqlExpressionCodeGenerator.Generate()

    Dim limitedSelectSqlExpressionCodeGenerator = New LimitedSelectSqlExpressionCodeGenerator(indentation, maxEntityCount, outputFolder)
    limitedSelectSqlExpressionCodeGenerator.Generate()

    Dim orderedSelectSqlExpressionCodeGenerator = New OrderedSelectSqlExpressionCodeGenerator(indentation, maxEntityCount, outputFolder)
    orderedSelectSqlExpressionCodeGenerator.Generate()

    Dim selectedSelectSqlExpressionCodeGenerator = New SelectedSelectSqlExpressionCodeGenerator(indentation, maxEntityCount, outputFolder)
    selectedSelectSqlExpressionCodeGenerator.Generate()

    Dim selectSqlExpressionCodeGenerator = New SelectSqlExpressionCodeGenerator(indentation, maxEntityCount, outputFolder)
    selectSqlExpressionCodeGenerator.Generate()

  End Sub
End Module
