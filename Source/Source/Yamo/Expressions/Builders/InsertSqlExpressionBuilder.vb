Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Internal
Imports Yamo.Internal.Query

Namespace Expressions.Builders

  ''' <summary>
  ''' Represents insert SQL expression builder.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class InsertSqlExpressionBuilder
    Inherits SqlExpressionBuilderBase

    ''' <summary>
    ''' Stores table name override.
    ''' </summary>
    Private m_TableNameOverride As String

    ''' <summary>
    ''' Stores table hints.
    ''' </summary>
    Private m_TableHints As String

    ''' <summary>
    ''' Creates new instance of <see cref="InsertSqlExpressionBuilder"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="tableNameOverride"></param>
    Public Sub New(<DisallowNull> context As DbContext, tableNameOverride As String)
      MyBase.New(context)
      m_TableNameOverride = tableNameOverride
      m_TableHints = Nothing
    End Sub

    ''' <summary>
    ''' Sets table hint(s).<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="tableHints"></param>
    Public Sub SetTableHints(<DisallowNull> tableHints As String)
      m_TableHints = tableHints
    End Sub

    ''' <summary>
    ''' Creates query.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <param name="useDbIdentityAndDefaults"></param>
    ''' <returns></returns>
    Public Function CreateQuery(<DisallowNull> obj As Object, useDbIdentityAndDefaults As Boolean) As InsertQuery
      Dim entityType = obj.GetType()
      Dim table As String

      If m_TableNameOverride Is Nothing Then
        Dim entity = Me.DbContext.Model.GetEntity(entityType)
        table = Me.DialectProvider.Formatter.CreateIdentifier(entity.TableName, entity.Schema)
      Else
        table = m_TableNameOverride
      End If

      If m_TableHints IsNot Nothing Then
        table = table & " " & m_TableHints
      End If

      Dim provider = EntitySqlStringProviderCache.GetInsertProvider(Me, entityType)
      Dim result = provider(obj, table, useDbIdentityAndDefaults)

      Return New InsertQuery(result.SqlString, result.ReadDbGeneratedValues, obj)
    End Function

  End Class
End Namespace
