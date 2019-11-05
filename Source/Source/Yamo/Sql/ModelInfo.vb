Namespace Sql

  ''' <summary>
  ''' Stores info about model and its alias.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Structure ModelInfo

    ''' <summary>
    ''' Type of model entity (table).
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Model As Type

    ''' <summary>
    ''' Table alias used in SQL expression.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TableAlias As String

    ''' <summary>
    ''' Creates new instance of <see cref="ModelInfo"/>.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="tableAlias"></param>
    Public Sub New(model As Type, tableAlias As String)
      Me.Model = model
      Me.TableAlias = tableAlias
    End Sub

  End Structure
End Namespace
