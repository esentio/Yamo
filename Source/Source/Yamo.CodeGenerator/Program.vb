Imports Microsoft.Extensions.Configuration
Imports Yamo.CodeGenerator.Generator

Module Program

  Sub Main(args As String())
    Dim config = New ConfigurationBuilder().AddJsonFile("appsettings.json", True, True).Build()

    Dim indentation = config("Indentation")
    Dim maxEntityCount = Int32.Parse(config("MaxEntityCount"))
    Dim outputFolder = config("OutputFolder")

    Dim selectSqlExpressionDefinition = New GeneratedClassDefinition(GeneratedClass.SelectSqlExpression, maxEntityCount)
    Dim selectWithHintsSqlExpressionDefinition = New GeneratedClassDefinition(GeneratedClass.SelectWithHintsSelectSqlExpression, 1, 1)
    Dim joinSelectSqlExpressionDefinition = New GeneratedClassDefinition(GeneratedClass.JoinSelectSqlExpression, 2, maxEntityCount)
    Dim joinWithHintsSelectSqlExpressionDefinition = New GeneratedClassDefinition(GeneratedClass.JoinWithHintsSelectSqlExpression, 2, maxEntityCount)
    Dim joinedSelectSqlExpressionDefinition = New GeneratedClassDefinition(GeneratedClass.JoinedSelectSqlExpression, 2, maxEntityCount)
    Dim filteredSelectSqlExpressionDefinition = New GeneratedClassDefinition(GeneratedClass.FilteredSelectSqlExpression, maxEntityCount)
    Dim groupedSelectSqlExpressionDefinition = New GeneratedClassDefinition(GeneratedClass.GroupedSelectSqlExpression, maxEntityCount)
    Dim havingSelectSqlExpressionDefinition = New GeneratedClassDefinition(GeneratedClass.HavingSelectSqlExpression, maxEntityCount)
    Dim orderedSelectSqlExpressionDefinition = New GeneratedClassDefinition(GeneratedClass.OrderedSelectSqlExpression, maxEntityCount)
    Dim limitedSelectSqlExpressionDefinition = New GeneratedClassDefinition(GeneratedClass.LimitedSelectSqlExpression, maxEntityCount)
    Dim selectedSelectSqlExpressionDefinition = New GeneratedClassDefinition(GeneratedClass.SelectedSelectSqlExpression, maxEntityCount)
    Dim distinctSelectSqlExpressionDefinition = New GeneratedClassDefinition(GeneratedClass.DistinctSelectSqlExpression, 1)
    Dim customSelectSqlExpressionDefinition = New GeneratedClassDefinition(GeneratedClass.CustomSelectSqlExpression, 1)
    Dim customDistinctSelectSqlExpressionDefinition = New GeneratedClassDefinition(GeneratedClass.CustomDistinctSelectSqlExpression, 1)

    Dim definitions = New List(Of GeneratedClassDefinition) From {
      selectSqlExpressionDefinition,
      selectWithHintsSqlExpressionDefinition,
      joinSelectSqlExpressionDefinition,
      joinWithHintsSelectSqlExpressionDefinition,
      joinedSelectSqlExpressionDefinition,
      filteredSelectSqlExpressionDefinition,
      groupedSelectSqlExpressionDefinition,
      havingSelectSqlExpressionDefinition,
      orderedSelectSqlExpressionDefinition,
      limitedSelectSqlExpressionDefinition,
      selectedSelectSqlExpressionDefinition,
      distinctSelectSqlExpressionDefinition,
      customSelectSqlExpressionDefinition,
      customDistinctSelectSqlExpressionDefinition
    }

    ' generate code

    Dim customDistinctSelectSqlExpressionCodeGenerator = New CustomDistinctSelectSqlExpressionCodeGenerator(indentation, outputFolder, customDistinctSelectSqlExpressionDefinition, definitions)
    customDistinctSelectSqlExpressionCodeGenerator.Generate()

    Dim customSelectSqlExpressionCodeGenerator = New CustomSelectSqlExpressionCodeGenerator(indentation, outputFolder, customSelectSqlExpressionDefinition, definitions)
    customSelectSqlExpressionCodeGenerator.Generate()

    Dim distinctSelectSqlExpressionCodeGenerator = New DistinctSelectSqlExpressionCodeGenerator(indentation, outputFolder, distinctSelectSqlExpressionDefinition, definitions)
    distinctSelectSqlExpressionCodeGenerator.Generate()

    Dim filteredSelectSqlExpressionCodeGenerator = New FilteredSelectSqlExpressionCodeGenerator(indentation, outputFolder, filteredSelectSqlExpressionDefinition, definitions)
    filteredSelectSqlExpressionCodeGenerator.Generate()

    Dim groupedSelectSqlExpressionCodeGenerator = New GroupedSelectSqlExpressionCodeGenerator(indentation, outputFolder, groupedSelectSqlExpressionDefinition, definitions)
    groupedSelectSqlExpressionCodeGenerator.Generate()

    Dim havingSelectSqlExpressionCodeGenerator = New HavingSelectSqlExpressionCodeGenerator(indentation, outputFolder, havingSelectSqlExpressionDefinition, definitions)
    havingSelectSqlExpressionCodeGenerator.Generate()

    Dim joinedSelectSqlExpressionCodeGenerator = New JoinedSelectSqlExpressionCodeGenerator(indentation, outputFolder, joinedSelectSqlExpressionDefinition, definitions)
    joinedSelectSqlExpressionCodeGenerator.Generate()

    Dim joinSelectSqlExpressionCodeGenerator = New JoinSelectSqlExpressionCodeGenerator(indentation, outputFolder, joinSelectSqlExpressionDefinition, definitions)
    joinSelectSqlExpressionCodeGenerator.Generate()

    Dim joinWithHintsSelectSqlExpressionCodeGenerator = New JoinWithHintsSelectSqlExpressionCodeGenerator(indentation, outputFolder, joinWithHintsSelectSqlExpressionDefinition, definitions)
    joinWithHintsSelectSqlExpressionCodeGenerator.Generate()

    Dim limitedSelectSqlExpressionCodeGenerator = New LimitedSelectSqlExpressionCodeGenerator(indentation, outputFolder, limitedSelectSqlExpressionDefinition, definitions)
    limitedSelectSqlExpressionCodeGenerator.Generate()

    Dim orderedSelectSqlExpressionCodeGenerator = New OrderedSelectSqlExpressionCodeGenerator(indentation, outputFolder, orderedSelectSqlExpressionDefinition, definitions)
    orderedSelectSqlExpressionCodeGenerator.Generate()

    Dim selectedSelectSqlExpressionCodeGenerator = New SelectedSelectSqlExpressionCodeGenerator(indentation, outputFolder, selectedSelectSqlExpressionDefinition, definitions)
    selectedSelectSqlExpressionCodeGenerator.Generate()

    Dim selectSqlExpressionCodeGenerator = New SelectSqlExpressionCodeGenerator(indentation, outputFolder, selectSqlExpressionDefinition, definitions)
    selectSqlExpressionCodeGenerator.Generate()

    Dim selectWithHintsSqlExpressionCodeGenerator = New SelectWithHintsSelectSqlExpressionCodeGenerator(indentation, outputFolder, selectWithHintsSqlExpressionDefinition, definitions)
    selectWithHintsSqlExpressionCodeGenerator.Generate()

  End Sub
End Module
