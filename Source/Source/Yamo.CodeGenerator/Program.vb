Imports Microsoft.Extensions.Configuration
Imports Yamo.CodeGenerator.Generator

Module Program

  Sub Main(args As String())
    Dim config = New ConfigurationBuilder().AddJsonFile("appsettings.json", True, True).Build()

    Dim indentation = config("Indentation")
    Dim maxEntityCount = Int32.Parse(config("MaxEntityCount"))
    Dim rootFolder = config("OutputFolder")
    Dim expressionsFolder = IO.Path.Combine(rootFolder, "Expressions")

    Dim selectSqlExpressionDefinition = New GeneratedClassDefinition(GeneratedClass.SelectSqlExpression, maxEntityCount)
    Dim withHintsSqlExpressionDefinition = New GeneratedClassDefinition(GeneratedClass.WithHintsSelectSqlExpression, 1, 1)
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
    Dim setSelectSqlExpressionDefinition = New GeneratedClassDefinition(GeneratedClass.SetSelectSqlExpression, 1)
    Dim customSetSelectSqlExpressionDefinition = New GeneratedClassDefinition(GeneratedClass.CustomSetSelectSqlExpression, 1)
    Dim joinDefinition = New GeneratedClassDefinition(GeneratedClass.Join, 2, maxEntityCount)

    Dim definitions = New List(Of GeneratedClassDefinition) From {
      selectSqlExpressionDefinition,
      withHintsSqlExpressionDefinition,
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
      customDistinctSelectSqlExpressionDefinition,
      setSelectSqlExpressionDefinition,
      customSetSelectSqlExpressionDefinition,
      joinDefinition
    }

    ' generate code

    Dim customDistinctSelectSqlExpressionCodeGenerator = New CustomDistinctSelectSqlExpressionCodeGenerator(indentation, expressionsFolder, customDistinctSelectSqlExpressionDefinition, definitions)
    customDistinctSelectSqlExpressionCodeGenerator.Generate()

    Dim customSelectSqlExpressionCodeGenerator = New CustomSelectSqlExpressionCodeGenerator(indentation, expressionsFolder, customSelectSqlExpressionDefinition, definitions)
    customSelectSqlExpressionCodeGenerator.Generate()

    Dim customSetSelectSqlExpressionCodeGenerator = New CustomSetSelectSqlExpressionCodeGenerator(indentation, expressionsFolder, customSetSelectSqlExpressionDefinition, definitions)
    customSetSelectSqlExpressionCodeGenerator.Generate()

    Dim distinctSelectSqlExpressionCodeGenerator = New DistinctSelectSqlExpressionCodeGenerator(indentation, expressionsFolder, distinctSelectSqlExpressionDefinition, definitions)
    distinctSelectSqlExpressionCodeGenerator.Generate()

    Dim filteredSelectSqlExpressionCodeGenerator = New FilteredSelectSqlExpressionCodeGenerator(indentation, expressionsFolder, filteredSelectSqlExpressionDefinition, definitions)
    filteredSelectSqlExpressionCodeGenerator.Generate()

    Dim groupedSelectSqlExpressionCodeGenerator = New GroupedSelectSqlExpressionCodeGenerator(indentation, expressionsFolder, groupedSelectSqlExpressionDefinition, definitions)
    groupedSelectSqlExpressionCodeGenerator.Generate()

    Dim havingSelectSqlExpressionCodeGenerator = New HavingSelectSqlExpressionCodeGenerator(indentation, expressionsFolder, havingSelectSqlExpressionDefinition, definitions)
    havingSelectSqlExpressionCodeGenerator.Generate()

    Dim joinedSelectSqlExpressionCodeGenerator = New JoinedSelectSqlExpressionCodeGenerator(indentation, expressionsFolder, joinedSelectSqlExpressionDefinition, definitions)
    joinedSelectSqlExpressionCodeGenerator.Generate()

    Dim joinSelectSqlExpressionCodeGenerator = New JoinSelectSqlExpressionCodeGenerator(indentation, expressionsFolder, joinSelectSqlExpressionDefinition, definitions)
    joinSelectSqlExpressionCodeGenerator.Generate()

    Dim joinWithHintsSelectSqlExpressionCodeGenerator = New JoinWithHintsSelectSqlExpressionCodeGenerator(indentation, expressionsFolder, joinWithHintsSelectSqlExpressionDefinition, definitions)
    joinWithHintsSelectSqlExpressionCodeGenerator.Generate()

    Dim limitedSelectSqlExpressionCodeGenerator = New LimitedSelectSqlExpressionCodeGenerator(indentation, expressionsFolder, limitedSelectSqlExpressionDefinition, definitions)
    limitedSelectSqlExpressionCodeGenerator.Generate()

    Dim orderedSelectSqlExpressionCodeGenerator = New OrderedSelectSqlExpressionCodeGenerator(indentation, expressionsFolder, orderedSelectSqlExpressionDefinition, definitions)
    orderedSelectSqlExpressionCodeGenerator.Generate()

    Dim selectedSelectSqlExpressionCodeGenerator = New SelectedSelectSqlExpressionCodeGenerator(indentation, expressionsFolder, selectedSelectSqlExpressionDefinition, definitions)
    selectedSelectSqlExpressionCodeGenerator.Generate()

    Dim selectSqlExpressionCodeGenerator = New SelectSqlExpressionCodeGenerator(indentation, expressionsFolder, selectSqlExpressionDefinition, definitions)
    selectSqlExpressionCodeGenerator.Generate()

    Dim setSelectSqlExpressionCodeGenerator = New SetSelectSqlExpressionCodeGenerator(indentation, expressionsFolder, setSelectSqlExpressionDefinition, definitions)
    setSelectSqlExpressionCodeGenerator.Generate()

    Dim withHintsSelectSqlExpressionCodeGenerator = New WithHintsSelectSqlExpressionCodeGenerator(indentation, expressionsFolder, withHintsSqlExpressionDefinition, definitions)
    withHintsSelectSqlExpressionCodeGenerator.Generate()

    Dim joinCodeGenerator = New JoinCodeGenerator(indentation, rootFolder, joinDefinition, definitions)
    joinCodeGenerator.Generate()

  End Sub
End Module
