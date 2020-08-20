Imports System.Linq.Expressions
Imports System.Reflection
Imports Yamo.Infrastructure

Namespace Infrastructure

  ''' <summary>
  ''' Entity SQL string provider factory.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SQLiteEntitySqlStringProviderFactory
    Inherits EntitySqlStringProviderFactory

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
    Protected Overrides Function GetInsertWhenUseDbIdentityAndDefaults(tableName As Expression, declareColumns As List(Of String), outputColumnNames As List(Of String), columnNames As List(Of String), parameterNames As List(Of String)) As Expression
      Dim part1 = Expression.Constant("INSERT INTO ", GetType(String))
      Dim part3 = Expression.Constant($" ({String.Join(", ", columnNames)}) VALUES ({String.Join(", ", parameterNames)});
SELECT last_insert_rowid()", GetType(String))

      Dim concatMethod = GetType(String).GetMethod("Concat", BindingFlags.Public Or BindingFlags.Static, Nothing, {GetType(String), GetType(String), GetType(String)}, Array.Empty(Of ParameterModifier)())
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
    Protected Overrides Function GetInsertWhenNotUseDbIdentityAndDefaults(tableName As Expression, hasIdentityColumn As Boolean, columnNames As List(Of String), parameterNames As List(Of String)) As Expression
      Dim part1 = Expression.Constant("INSERT INTO ", GetType(String))
      Dim part3 = Expression.Constant($" ({String.Join(", ", columnNames)}) VALUES ({String.Join(", ", parameterNames)})", GetType(String))

      Dim concatMethod = GetType(String).GetMethod("Concat", BindingFlags.Public Or BindingFlags.Static, Nothing, {GetType(String), GetType(String), GetType(String)}, Array.Empty(Of ParameterModifier)())
      Return Expression.Call(concatMethod, part1, tableName, part3)
    End Function

    ''' <summary>
    ''' Gets whether insert is supported when using defaults.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Protected Overrides Function IsInsertWhenUseDefaultsSupported() As Boolean
      Return False
    End Function

  End Class
End Namespace