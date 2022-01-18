Imports System.Diagnostics.CodeAnalysis

Namespace Sql

  ''' <summary>
  ''' Stores info about table of a model.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Structure TableModelInfo

    ''' <summary>
    ''' Type of model entity (table).
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Model As Type

    ''' <summary>
    ''' Creates new instance of <see cref="TableModelInfo"/>.
    ''' </summary>
    ''' <param name="model"></param>
    Public Sub New(<DisallowNull> model As Type)
      Me.Model = model
    End Sub

  End Structure
End Namespace
