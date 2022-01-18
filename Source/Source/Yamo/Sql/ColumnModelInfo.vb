Imports System.Diagnostics.CodeAnalysis

Namespace Sql

  ''' <summary>
  ''' Stores info about column of a model.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Structure ColumnModelInfo

    ''' <summary>
    ''' Type of model entity (table).
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Model As Type

    ''' <summary>
    ''' Entity property name.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property PropertyName As String

    ''' <summary>
    ''' Table alias used in SQL expression.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TableAlias As <MaybeNull> String

    ''' <summary>
    ''' Creates new instance of <see cref="ColumnModelInfo"/>.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="propertyName"></param>
    ''' <param name="tableAlias"></param>
    Public Sub New(<DisallowNull> model As Type, <DisallowNull> propertyName As String, tableAlias As String)
      Me.Model = model
      Me.TableAlias = tableAlias
      Me.PropertyName = propertyName
    End Sub

  End Structure
End Namespace
