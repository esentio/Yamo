Imports System.Data
Imports System.Linq.Expressions
Imports System.Reflection
Imports System.Text
Imports Yamo.Expressions.Builders
Imports Yamo.Internal
Imports Yamo.Internal.Query
Imports Yamo.Metadata

Namespace Infrastructure

  ''' <summary>
  ''' Entity SQL string provider factory.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class EntitySqlStringProviderFactory

    ''' <summary>
    ''' Creates insert provider.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Overridable Function CreateInsertProvider(builder As InsertSqlExpressionBuilder, entityType As Type) As Func(Of Object, String, Boolean, CreateInsertSqlStringResult)
      Dim entityParam = Expression.Parameter(GetType(Object), "entity")
      Dim tableParam = Expression.Parameter(GetType(String), "table")
      Dim useDbIdentityAndDefaultsParam = Expression.Parameter(GetType(Boolean), "useDbIdentityAndDefaults")
      Dim parameters = {entityParam, tableParam, useDbIdentityAndDefaultsParam}

      Dim expressions = New List(Of Expression)

      Dim entityVariable = Expression.Variable(entityType, "entityObj")
      expressions.Add(Expression.Assign(entityVariable, Expression.Convert(entityParam, entityType)))

      Dim sqlVariable = Expression.Variable(GetType(String), "sql")

      Dim parametersVariableType = GetType(List(Of SqlParameter))
      Dim parametersVariable = Expression.Variable(parametersVariableType, "parameters")
      expressions.Add(Expression.Assign(parametersVariable, Expression.[New](parametersVariableType)))

      Dim readDbGeneratedValuesVariable = Expression.Variable(GetType(Boolean))

      Dim entity = builder.DbContext.Model.GetEntity(entityType)

      Dim declareColumnsWhenUseDbIdentityAndDefaults = New List(Of String)
      Dim outputColumnNamesWhenUseDbIdentityAndDefaults = New List(Of String)
      Dim columnNamesWhenUseDbIdentityAndDefaults = New List(Of String)
      Dim parameterNamesWhenUseDbIdentityAndDefaults = New List(Of String)
      Dim expressionsWhenUseDbIdentityAndDefaults = New List(Of Expression)

      Dim columnNamesWhenNotUseDbIdentityAndDefaults = New List(Of String)
      Dim parameterNamesWhenNotUseDbIdentityAndDefaults = New List(Of String)
      Dim expressionsWhenNotUseDbIdentityAndDefaults = New List(Of Expression)

      Dim hasIdentityColumn = False
      Dim hasColumnWithDefaultValue = False

      Dim i = 0
      For Each prop In entity.GetProperties()
        Dim p = CreateParameterFromProperty(parametersVariable, entityVariable, prop, i, builder)
        Dim parameterName = p.ParameterName
        Dim parameterAddCall = p.ParameterAddCall

        If prop.IsIdentity Then
          hasIdentityColumn = True
        End If

        If prop.HasDefaultValue Then
          hasColumnWithDefaultValue = True
        End If

        Dim columnName = builder.DialectProvider.Formatter.CreateIdentifier(prop.ColumnName)

        If prop.IsIdentity OrElse prop.HasDefaultValue Then
          Dim declareColumn = columnName & " " & GetDbTypeDefinition(prop.PropertyType)
          Dim outputColumnName = "inserted." & columnName

          declareColumnsWhenUseDbIdentityAndDefaults.Add(declareColumn)
          outputColumnNamesWhenUseDbIdentityAndDefaults.Add(outputColumnName)
        Else
          columnNamesWhenUseDbIdentityAndDefaults.Add(columnName)
          parameterNamesWhenUseDbIdentityAndDefaults.Add(parameterName)
          expressionsWhenUseDbIdentityAndDefaults.Add(parameterAddCall)
        End If

        columnNamesWhenNotUseDbIdentityAndDefaults.Add(columnName)
        parameterNamesWhenNotUseDbIdentityAndDefaults.Add(parameterName)
        expressionsWhenNotUseDbIdentityAndDefaults.Add(parameterAddCall)

        i += 1
      Next

      Dim sqlWhenUseDbIdentityAndDefaults = GetInsertWhenUseDbIdentityAndDefaults(tableParam, declareColumnsWhenUseDbIdentityAndDefaults, outputColumnNamesWhenUseDbIdentityAndDefaults, columnNamesWhenUseDbIdentityAndDefaults, parameterNamesWhenUseDbIdentityAndDefaults)
      Dim sqlVariableAssignWhenUseDbIdentityAndDefaults = Expression.Assign(sqlVariable, sqlWhenUseDbIdentityAndDefaults)
      expressionsWhenUseDbIdentityAndDefaults.Add(sqlVariableAssignWhenUseDbIdentityAndDefaults)

      Dim readDbGeneratedValuesVariableAssignWhenUseDbIdentityAndDefaults = Expression.Assign(readDbGeneratedValuesVariable, Expression.Constant(True, GetType(Boolean)))
      expressionsWhenUseDbIdentityAndDefaults.Add(readDbGeneratedValuesVariableAssignWhenUseDbIdentityAndDefaults)

      Dim sqlWhenNotUseDbIdentityAndDefaults = GetInsertWhenNotUseDbIdentityAndDefaults(tableParam, hasIdentityColumn, columnNamesWhenNotUseDbIdentityAndDefaults, parameterNamesWhenNotUseDbIdentityAndDefaults)
      Dim sqlVariableAssignWhenNotUseDbIdentityAndDefaults = Expression.Assign(sqlVariable, sqlWhenNotUseDbIdentityAndDefaults)
      expressionsWhenNotUseDbIdentityAndDefaults.Add(sqlVariableAssignWhenNotUseDbIdentityAndDefaults)

      Dim readDbGeneratedValuesVariableAssignWhenNotUseDbIdentityAndDefaults = Expression.Assign(readDbGeneratedValuesVariable, Expression.Constant(False, GetType(Boolean)))
      expressionsWhenNotUseDbIdentityAndDefaults.Add(readDbGeneratedValuesVariableAssignWhenNotUseDbIdentityAndDefaults)

      If hasIdentityColumn OrElse hasColumnWithDefaultValue Then
        If hasColumnWithDefaultValue AndAlso Not IsInsertWhenUseDefaultsSupported() Then
          expressionsWhenUseDbIdentityAndDefaults.Insert(0, Expression.Throw(Expression.Constant(New NotSupportedException("Querying default values on insert is not supported in implementation of this SQL dialect. Set 'useDbIdentityAndDefaults' parameter to false or drop 'HasDefaultValue' hint(s) from model if you still want to use useDbIdentityAndDefaults for to identity (autoincrement) column."))))
        End If

        Dim useDbIdentityAndDefaultsCond = Expression.Condition(useDbIdentityAndDefaultsParam, Expression.Block(expressionsWhenUseDbIdentityAndDefaults), Expression.Block(expressionsWhenNotUseDbIdentityAndDefaults))
        expressions.Add(useDbIdentityAndDefaultsCond)
      Else
        expressions.AddRange(expressionsWhenNotUseDbIdentityAndDefaults)
      End If

      Dim sqlStringConstructor = GetType(SqlString).GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, CallingConventions.HasThis, {GetType(String), parametersVariableType}, New ParameterModifier(0) {})
      Dim sqlString = Expression.[New](sqlStringConstructor, sqlVariable, parametersVariable)
      expressions.Add(sqlString)

      Dim resultConstructor = GetType(CreateInsertSqlStringResult).GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, CallingConventions.HasThis, {GetType(SqlString), GetType(Boolean)}, New ParameterModifier(0) {})
      Dim result = Expression.[New](resultConstructor, sqlString, readDbGeneratedValuesVariable)
      expressions.Add(result)

      Dim body = Expression.Block({entityVariable, sqlVariable, parametersVariable, readDbGeneratedValuesVariable}, expressions)

      Dim reader = Expression.Lambda(Of Func(Of Object, String, Boolean, CreateInsertSqlStringResult))(body, parameters)
      Return reader.Compile()
    End Function

    ''' <summary>
    ''' Gets insert statement when using database identity and defaults.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="tableName"></param>
    ''' <param name="declareColumns"></param>
    ''' <param name="outputColumnNames"></param>
    ''' <param name="columnNames"></param>
    ''' <param name="parameterNames"></param>
    ''' <returns></returns>
    Protected Overridable Function GetInsertWhenUseDbIdentityAndDefaults(tableName As Expression, declareColumns As List(Of String), outputColumnNames As List(Of String), columnNames As List(Of String), parameterNames As List(Of String)) As Expression
      Dim part1 = Expression.Constant($"DECLARE @InsertedValues TABLE ({String.Join(", ", declareColumns)})
INSERT INTO ", GetType(String))
      Dim part3 = Expression.Constant($" ({String.Join(", ", columnNames)}) OUTPUT {String.Join(", ", outputColumnNames)} INTO @InsertedValues VALUES ({String.Join(", ", parameterNames)})
SELECT * FROM @InsertedValues", GetType(String))

      Dim concatMethod = GetType(String).GetMethod("Concat", BindingFlags.Public Or BindingFlags.Static, Nothing, {GetType(String), GetType(String), GetType(String)}, {})
      Return Expression.Call(concatMethod, part1, tableName, part3)
    End Function

    ''' <summary>
    ''' Gets insert statement when nor using database identity and defaults.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="tableName"></param>
    ''' <param name="hasIdentityColumn"></param>
    ''' <param name="columnNames"></param>
    ''' <param name="parameterNames"></param>
    ''' <returns></returns>
    Protected Overridable Function GetInsertWhenNotUseDbIdentityAndDefaults(tableName As Expression, hasIdentityColumn As Boolean, columnNames As List(Of String), parameterNames As List(Of String)) As Expression
      If hasIdentityColumn Then
        Dim part1 = Expression.Constant("SET IDENTITY_INSERT ", GetType(String))
        Dim part3 = Expression.Constant(" ON
        INSERT INTO ", GetType(String))
        Dim part5 = Expression.Constant($" ({String.Join(", ", columnNames)}) VALUES ({String.Join(", ", parameterNames)})
        SET IDENTITY_INSERT ", GetType(String))
        Dim part7 = Expression.Constant(" OFF", GetType(String))

        Dim concatMethod = GetType(String).GetMethod("Concat", BindingFlags.Public Or BindingFlags.Static, Nothing, {GetType(String())}, {})
        Return Expression.Call(concatMethod, Expression.NewArrayInit(GetType(String), part1, tableName, part3, tableName, part5, tableName, part7))
      Else
        Dim part1 = Expression.Constant("INSERT INTO ", GetType(String))
        Dim part3 = Expression.Constant($" ({String.Join(", ", columnNames)}) VALUES ({String.Join(", ", parameterNames)})", GetType(String))

        Dim concatMethod = GetType(String).GetMethod("Concat", BindingFlags.Public Or BindingFlags.Static, Nothing, {GetType(String), GetType(String), GetType(String)}, {})
        Return Expression.Call(concatMethod, part1, tableName, part3)
      End If
    End Function

    ''' <summary>
    ''' Gets whether insert is supported when using defaults.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Protected Overridable Function IsInsertWhenUseDefaultsSupported() As Boolean
      Return True
    End Function

    ''' <summary>
    ''' Creates update provider.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Overridable Function CreateUpdateProvider(builder As UpdateSqlExpressionBuilder, entityType As Type) As Func(Of Object, String, Boolean, SqlString)
      If GetType(IHasDbPropertyModifiedTracking).IsAssignableFrom(entityType) Then
        Return CreateUpdateProviderForDbPropertyModifiedTrackingObject(builder, entityType)
      Else
        Return CreateUpdateProviderForSimpleObjects(builder, entityType)
      End If
    End Function

    ''' <summary>
    ''' Creates update provider for simple objects.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Protected Overridable Function CreateUpdateProviderForSimpleObjects(builder As UpdateSqlExpressionBuilder, entityType As Type) As Func(Of Object, String, Boolean, SqlString)
      Dim entityParam = Expression.Parameter(GetType(Object), "entity")
      Dim tableParam = Expression.Parameter(GetType(String), "table")
      Dim forceUpdateAllFieldsParam = Expression.Parameter(GetType(Boolean), "forceUpdateAllFields")
      Dim parameters = {entityParam, tableParam, forceUpdateAllFieldsParam}

      Dim expressions = New List(Of Expression)

      Dim entityVariable = Expression.Variable(entityType, "entityObj")
      expressions.Add(Expression.Assign(entityVariable, Expression.Convert(entityParam, entityType)))

      Dim sqlVariable = Expression.Variable(GetType(String), "sql")

      Dim parametersVariableType = GetType(List(Of SqlParameter))
      Dim parametersVariable = Expression.Variable(parametersVariableType, "parameters")
      expressions.Add(Expression.Assign(parametersVariable, Expression.[New](parametersVariableType)))

      Dim entity = builder.DbContext.Model.GetEntity(entityType)

      Dim sql = New StringBuilder
      Dim setColumns = New List(Of String)
      Dim whereColumns = New List(Of String)

      sql.AppendLine(" SET")

      Dim i = 0
      For Each prop In entity.GetNonKeyProperties().Select(Function(x) x.Property)
        Dim p = CreateParameterFromProperty(parametersVariable, entityVariable, prop, i, builder)
        Dim parameterName = p.ParameterName
        Dim parameterAddCall = p.ParameterAddCall

        expressions.Add(parameterAddCall)
        setColumns.Add(builder.DialectProvider.Formatter.CreateIdentifier(prop.ColumnName) & " = " & parameterName)

        i += 1
      Next

      Helpers.Text.AppendJoin(sql, ", " & Environment.NewLine, setColumns)
      sql.AppendLine()
      sql.AppendLine("WHERE")

      For Each prop In entity.GetKeyProperties().Select(Function(x) x.Property)
        Dim p = CreateParameterFromProperty(parametersVariable, entityVariable, prop, i, builder)
        Dim parameterName = p.ParameterName
        Dim parameterAddCall = p.ParameterAddCall

        expressions.Add(parameterAddCall)
        whereColumns.Add(builder.DialectProvider.Formatter.CreateIdentifier(prop.ColumnName) & " = " & parameterName)

        i += 1
      Next

      Helpers.Text.AppendJoin(sql, " AND " & Environment.NewLine, whereColumns)

      Dim concatMethod = GetType(String).GetMethod("Concat", BindingFlags.Public Or BindingFlags.Static, Nothing, {GetType(String), GetType(String), GetType(String)}, {})
      Dim concatCall = Expression.Call(concatMethod, Expression.Constant("UPDATE ", GetType(String)), tableParam, Expression.Constant(sql.ToString(), GetType(String)))
      Dim sqlVariableAssign = Expression.Assign(sqlVariable, concatCall)
      expressions.Add(sqlVariableAssign)

      Dim sqlStringConstructor = GetType(SqlString).GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, CallingConventions.HasThis, {GetType(String), parametersVariableType}, New ParameterModifier(0) {})
      Dim sqlString = Expression.[New](sqlStringConstructor, sqlVariable, parametersVariable)
      expressions.Add(sqlString)

      Dim body = Expression.Block({entityVariable, sqlVariable, parametersVariable}, expressions)

      Dim reader = Expression.Lambda(Of Func(Of Object, String, Boolean, SqlString))(body, parameters)
      Return reader.Compile()
    End Function

    ''' <summary>
    ''' Create update provider for objects with database property modified tracking.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Protected Overridable Function CreateUpdateProviderForDbPropertyModifiedTrackingObject(builder As UpdateSqlExpressionBuilder, entityType As Type) As Func(Of Object, String, Boolean, SqlString)
      Dim entityParam = Expression.Parameter(GetType(Object), "entity")
      Dim tableParam = Expression.Parameter(GetType(String), "table")
      Dim forceUpdateAllFieldsParam = Expression.Parameter(GetType(Boolean), "forceUpdateAllFields")
      Dim parameters = {entityParam, tableParam, forceUpdateAllFieldsParam}

      Dim expressions = New List(Of Expression)

      Dim entityVariable = Expression.Variable(entityType, "entityObj")
      expressions.Add(Expression.Assign(entityVariable, Expression.Convert(entityParam, entityType)))

      Dim trackingVariable = Expression.Variable(GetType(IHasDbPropertyModifiedTracking), "trackingValue")
      expressions.Add(Expression.Assign(trackingVariable, Expression.Convert(entityParam, GetType(IHasDbPropertyModifiedTracking))))

      Dim sqlVariableType = GetType(StringBuilder)
      Dim sqlVariable = Expression.Variable(GetType(StringBuilder), "sql")
      expressions.Add(Expression.Assign(sqlVariable, Expression.[New](sqlVariableType)))

      Dim setColumnsVariableType = GetType(List(Of String))
      Dim setColumnsVariable = Expression.Variable(setColumnsVariableType, "setColumns")
      expressions.Add(Expression.Assign(setColumnsVariable, Expression.[New](setColumnsVariableType)))

      Dim parametersVariableType = GetType(List(Of SqlParameter))
      Dim parametersVariable = Expression.Variable(parametersVariableType, "parameters")
      expressions.Add(Expression.Assign(parametersVariable, Expression.[New](parametersVariableType)))

      Dim entity = builder.DbContext.Model.GetEntity(entityType)

      Dim whereColumns = New List(Of String)

      Dim appendMethod = GetType(StringBuilder).GetMethod("Append", BindingFlags.Public Or BindingFlags.Instance, Nothing, {GetType(String)}, {})

      expressions.Add(Expression.Call(sqlVariable, appendMethod, Expression.Constant("UPDATE ", GetType(String))))
      expressions.Add(Expression.Call(sqlVariable, appendMethod, tableParam))
      expressions.Add(Expression.Call(sqlVariable, "AppendLine", {}, Expression.Constant(" SET", GetType(String))))

      Dim i = 0
      For Each prop In entity.GetNonKeyProperties().Select(Function(x) x.Property)
        Dim p = CreateParameterFromProperty(parametersVariable, entityVariable, prop, i, builder)
        Dim parameterName = p.ParameterName
        Dim parameterAddCall = p.ParameterAddCall

        Dim modifiedExpressions = New List(Of Expression)

        Dim addSetColumnCall = Expression.Call(setColumnsVariable, "Add", {}, Expression.Constant(builder.DialectProvider.Formatter.CreateIdentifier(prop.ColumnName) & " = " & parameterName, GetType(String)))
        modifiedExpressions.Add(addSetColumnCall)

        modifiedExpressions.Add(parameterAddCall)

        Dim modifiedTest = Expression.OrElse(forceUpdateAllFieldsParam, Expression.Call(trackingVariable, NameOf(IHasDbPropertyModifiedTracking.IsDbPropertyModified), {}, Expression.Constant(prop.Name, GetType(String))))
        Dim modifiedCond = Expression.IfThen(modifiedTest, Expression.Block(modifiedExpressions))

        expressions.Add(modifiedCond)

        i += 1
      Next

      Dim joinMethod = GetType(String).GetMethod("Join", BindingFlags.Public Or BindingFlags.Static, Nothing, {GetType(String), GetType(IEnumerable(Of String))}, {})
      Dim joinCall = Expression.Call(joinMethod, Expression.Constant(", " & Environment.NewLine, GetType(String)), setColumnsVariable)

      expressions.Add(Expression.Call(sqlVariable, "AppendLine", {}, joinCall))

      expressions.Add(Expression.Call(sqlVariable, "AppendLine", {}, Expression.Constant("WHERE", GetType(String))))

      For Each prop In entity.GetKeyProperties().Select(Function(x) x.Property)
        Dim p = CreateParameterFromProperty(parametersVariable, entityVariable, prop, i, builder)
        Dim parameterName = p.ParameterName
        Dim parameterAddCall = p.ParameterAddCall

        expressions.Add(parameterAddCall)
        whereColumns.Add(builder.DialectProvider.Formatter.CreateIdentifier(prop.ColumnName) & " = " & parameterName)

        i += 1
      Next

      expressions.Add(Expression.Call(sqlVariable, appendMethod, Expression.Constant(String.Join(" AND " & Environment.NewLine, whereColumns), GetType(String))))

      Dim sqlStringConstructor = GetType(SqlString).GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, CallingConventions.HasThis, {GetType(String), parametersVariableType}, New ParameterModifier(0) {})
      Dim sqlString = Expression.[New](sqlStringConstructor, Expression.Call(sqlVariable, "ToString", {}), parametersVariable)
      expressions.Add(sqlString)

      Dim body = Expression.Block({entityVariable, trackingVariable, sqlVariable, setColumnsVariable, parametersVariable}, expressions)

      Dim reader = Expression.Lambda(Of Func(Of Object, String, Boolean, SqlString))(body, parameters)
      Return reader.Compile()
    End Function

    ''' <summary>
    ''' Creates delete provider.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Overridable Function CreateDeleteProvider(builder As DeleteSqlExpressionBuilder, entityType As Type) As Func(Of Object, String, SqlString)
      Dim entityParam = Expression.Parameter(GetType(Object), "entity")
      Dim tableParam = Expression.Parameter(GetType(String), "table")
      Dim parameters = {entityParam, tableParam}

      Dim expressions = New List(Of Expression)

      Dim entityVariable = Expression.Variable(entityType, "entityObj")
      expressions.Add(Expression.Assign(entityVariable, Expression.Convert(entityParam, entityType)))

      Dim sqlVariable = Expression.Variable(GetType(String), "sql")

      Dim parametersVariableType = GetType(List(Of SqlParameter))
      Dim parametersVariable = Expression.Variable(parametersVariableType, "parameters")
      expressions.Add(Expression.Assign(parametersVariable, Expression.[New](parametersVariableType)))

      Dim entity = builder.DbContext.Model.GetEntity(entityType)

      Dim sql = New StringBuilder
      Dim whereColumns = New List(Of String)

      sql.AppendLine()
      sql.AppendLine("WHERE")

      Dim i = 0
      For Each prop In entity.GetKeyProperties().Select(Function(x) x.Property)
        Dim p = CreateParameterFromProperty(parametersVariable, entityVariable, prop, i, builder)
        Dim parameterName = p.ParameterName
        Dim parameterAddCall = p.ParameterAddCall

        expressions.Add(parameterAddCall)
        whereColumns.Add(builder.DialectProvider.Formatter.CreateIdentifier(prop.ColumnName) & " = " & parameterName)

        i += 1
      Next

      Helpers.Text.AppendJoin(sql, " AND " & Environment.NewLine, whereColumns)

      Dim concatMethod = GetType(String).GetMethod("Concat", BindingFlags.Public Or BindingFlags.Static, Nothing, {GetType(String), GetType(String), GetType(String)}, {})
      Dim concatCall = Expression.Call(concatMethod, Expression.Constant("DELETE FROM ", GetType(String)), tableParam, Expression.Constant(sql.ToString(), GetType(String)))
      Dim sqlVariableAssign = Expression.Assign(sqlVariable, concatCall)
      expressions.Add(sqlVariableAssign)

      Dim sqlStringConstructor = GetType(SqlString).GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, CallingConventions.HasThis, {GetType(String), parametersVariableType}, New ParameterModifier(0) {})
      Dim sqlString = Expression.[New](sqlStringConstructor, sqlVariable, parametersVariable)
      expressions.Add(sqlString)

      Dim body = Expression.Block({entityVariable, sqlVariable, parametersVariable}, expressions)

      Dim reader = Expression.Lambda(Of Func(Of Object, String, SqlString))(body, parameters)
      Return reader.Compile()
    End Function

    ''' <summary>
    ''' Creates soft delete provider.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Overridable Function CreateSoftDeleteProvider(builder As DeleteSqlExpressionBuilder, entityType As Type) As Func(Of Object, String, SqlString)
      Dim entityParam = Expression.Parameter(GetType(Object), "entity")
      Dim tableParam = Expression.Parameter(GetType(String), "table")
      Dim parameters = {entityParam, tableParam}

      Dim expressions = New List(Of Expression)

      Dim entityVariable = Expression.Variable(entityType, "entityObj")
      expressions.Add(Expression.Assign(entityVariable, Expression.Convert(entityParam, entityType)))

      Dim sqlVariable = Expression.Variable(GetType(String), "sql")

      Dim parametersVariableType = GetType(List(Of SqlParameter))
      Dim parametersVariable = Expression.Variable(parametersVariableType, "parameters")
      expressions.Add(Expression.Assign(parametersVariable, Expression.[New](parametersVariableType)))

      Dim entity = builder.DbContext.Model.GetEntity(entityType)

      Dim sql = New StringBuilder
      Dim setColumns = New List(Of String)
      Dim whereColumns = New List(Of String)

      sql.AppendLine(" SET")

      Dim i = 0
      For Each prop In entity.GetNonKeyProperties().Where(Function(x) x.Property.SetOnDelete).Select(Function(x) x.Property)
        Dim p = CreateParameterFromProperty(parametersVariable, entityVariable, prop, i, builder)
        Dim parameterName = p.ParameterName
        Dim parameterAddCall = p.ParameterAddCall

        expressions.Add(parameterAddCall)
        setColumns.Add(builder.DialectProvider.Formatter.CreateIdentifier(prop.ColumnName) & " = " & parameterName)

        i += 1
      Next

      Helpers.Text.AppendJoin(sql, ", " & Environment.NewLine, setColumns)
      sql.AppendLine()
      sql.AppendLine("WHERE")

      For Each prop In entity.GetKeyProperties().Select(Function(x) x.Property)
        Dim p = CreateParameterFromProperty(parametersVariable, entityVariable, prop, i, builder)
        Dim parameterName = p.ParameterName
        Dim parameterAddCall = p.ParameterAddCall

        expressions.Add(parameterAddCall)
        whereColumns.Add(builder.DialectProvider.Formatter.CreateIdentifier(prop.ColumnName) & " = " & parameterName)

        i += 1
      Next

      Helpers.Text.AppendJoin(sql, " AND " & Environment.NewLine, whereColumns)

      Dim concatMethod = GetType(String).GetMethod("Concat", BindingFlags.Public Or BindingFlags.Static, Nothing, {GetType(String), GetType(String), GetType(String)}, {})
      Dim concatCall = Expression.Call(concatMethod, Expression.Constant("UPDATE ", GetType(String)), tableParam, Expression.Constant(sql.ToString(), GetType(String)))
      Dim sqlVariableAssign = Expression.Assign(sqlVariable, concatCall)
      expressions.Add(sqlVariableAssign)

      Dim sqlStringConstructor = GetType(SqlString).GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, CallingConventions.HasThis, {GetType(String), parametersVariableType}, New ParameterModifier(0) {})
      Dim sqlString = Expression.[New](sqlStringConstructor, sqlVariable, parametersVariable)
      expressions.Add(sqlString)

      Dim body = Expression.Block({entityVariable, sqlVariable, parametersVariable}, expressions)

      Dim reader = Expression.Lambda(Of Func(Of Object, String, SqlString))(body, parameters)
      Return reader.Compile()
    End Function

    ''' <summary>
    ''' Creates soft delete provider without condition.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Overridable Function CreateSoftDeleteWithoutConditionProvider(builder As DeleteSqlExpressionBuilder, entityType As Type) As Func(Of String, Object(), SqlString)
      Dim tableParam = Expression.Parameter(GetType(String), "table")
      Dim valuesParam = Expression.Parameter(GetType(Object()), "values")
      Dim parameters = {tableParam, valuesParam}

      Dim expressions = New List(Of Expression)

      Dim sqlVariable = Expression.Variable(GetType(String), "sql")

      Dim parametersVariableType = GetType(List(Of SqlParameter))
      Dim parametersVariable = Expression.Variable(parametersVariableType, "parameters")
      expressions.Add(Expression.Assign(parametersVariable, Expression.[New](parametersVariableType)))

      Dim entity = builder.DbContext.Model.GetEntity(entityType)

      Dim sql = New StringBuilder
      Dim setColumns = New List(Of String)

      sql.AppendLine(" SET")

      Dim i = 0
      For Each prop In entity.GetNonKeyProperties().Where(Function(x) x.Property.SetOnDelete).Select(Function(x) x.Property)
        Dim value = Expression.ArrayAccess(valuesParam, Expression.Constant(i, GetType(Int32)))
        Dim p = CreateParameter(parametersVariable, value, prop, i, builder)
        Dim parameterName = p.ParameterName
        Dim parameterAddCall = p.ParameterAddCall

        expressions.Add(parameterAddCall)
        setColumns.Add(builder.DialectProvider.Formatter.CreateIdentifier(prop.ColumnName) & " = " & parameterName)

        i += 1
      Next

      Helpers.Text.AppendJoin(sql, ", " & Environment.NewLine, setColumns)

      Dim concatMethod = GetType(String).GetMethod("Concat", BindingFlags.Public Or BindingFlags.Static, Nothing, {GetType(String), GetType(String), GetType(String)}, {})
      Dim concatCall = Expression.Call(concatMethod, Expression.Constant("UPDATE ", GetType(String)), tableParam, Expression.Constant(sql.ToString(), GetType(String)))
      Dim sqlVariableAssign = Expression.Assign(sqlVariable, concatCall)
      expressions.Add(sqlVariableAssign)

      Dim sqlStringConstructor = GetType(SqlString).GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, CallingConventions.HasThis, {GetType(String), parametersVariableType}, New ParameterModifier(0) {})
      Dim sqlString = Expression.[New](sqlStringConstructor, sqlVariable, parametersVariable)
      expressions.Add(sqlString)

      Dim body = Expression.Block({sqlVariable, parametersVariable}, expressions)

      Dim reader = Expression.Lambda(Of Func(Of String, Object(), SqlString))(body, parameters)
      Return reader.Compile()
    End Function

    ''' <summary>
    ''' Creates parameter from property.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="parametersVariable"></param>
    ''' <param name="entityVariable"></param>
    ''' <param name="prop"></param>
    ''' <param name="index"></param>
    ''' <param name="builder"></param>
    ''' <returns></returns>
    Protected Function CreateParameterFromProperty(parametersVariable As Expression, entityVariable As Expression, prop As [Property], index As Int32, builder As SqlExpressionBuilderBase) As (ParameterName As String, ParameterAddCall As Expression)
      Dim parameterValue = Expression.Property(entityVariable, prop.Name)

      Return CreateParameter(parametersVariable, parameterValue, prop, index, builder)
    End Function

    ''' <summary>
    ''' Creates parameter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="parametersVariable"></param>
    ''' <param name="value"></param>
    ''' <param name="prop"></param>
    ''' <param name="index"></param>
    ''' <param name="builder"></param>
    ''' <returns></returns>
    Protected Function CreateParameter(parametersVariable As Expression, value As Expression, prop As [Property], index As Int32, builder As SqlExpressionBuilderBase) As (ParameterName As String, ParameterAddCall As Expression)
      Dim parameterNameValue = builder.CreateParameter(index)
      Dim parameterName = Expression.Constant(parameterNameValue, GetType(String))
      Dim parameterValue = Expression.Convert(value, GetType(Object))

      Dim parameterAddCall As Expression

      Dim dbTypeValue = GetDbType(prop.PropertyType)
      If dbTypeValue.HasValue Then
        Dim dbType = Expression.Constant(dbTypeValue.Value, GetType(DbType))
        Dim parameterConstructor = GetType(SqlParameter).GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, CallingConventions.HasThis, {GetType(String), GetType(Object), GetType(DbType)}, New ParameterModifier(0) {})
        Dim parameter = Expression.[New](parameterConstructor, parameterName, parameterValue, dbType)
        parameterAddCall = Expression.Call(parametersVariable, "Add", {}, parameter)
      Else
        Dim parameterConstructor = GetType(SqlParameter).GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, CallingConventions.HasThis, {GetType(String), GetType(Object)}, New ParameterModifier(0) {})
        Dim parameter = Expression.[New](parameterConstructor, parameterName, parameterValue)
        parameterAddCall = Expression.Call(parametersVariable, "Add", {}, parameter)
      End If

      Return (parameterNameValue, parameterAddCall)
    End Function

    ''' <summary>
    ''' Gets database type.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Protected Overridable Function GetDbType(type As Type) As DbType?
      If type Is GetType(Byte()) Then
        Return DbType.Binary
      Else
        Return Nothing
      End If
    End Function

    ''' <summary>
    ''' Gets database type definition.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Protected Overridable Function GetDbTypeDefinition(type As Type) As String
      Select Case type
        Case GetType(Guid), GetType(Guid?)
          Return "uniqueidentifier"
        Case GetType(Int16), GetType(Int16?)
          Return "smallint"
        Case GetType(Int32), GetType(Int32?)
          Return "int"
        Case GetType(Int64), GetType(Int64?)
          Return "bigint"
        Case Else
          Throw New NotSupportedException($"Type '{type}' is not supported.")
      End Select
    End Function

  End Class
End Namespace