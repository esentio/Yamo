Imports System.Diagnostics.CodeAnalysis

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL related data of a result included to an entity.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SqlEntityIncludedResult

    ' TODO: SIP - structure instead?

    ''' <summary>
    ''' Gets SQL select expression of included column(s).<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Sql As String

    ''' <summary>
    ''' Gets property name of an entity, that should be filled with the result value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property PropertyName As String

    ''' <summary>
    ''' Gets SQL result.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Result As SqlResultBase

    ''' <summary>
    ''' Creates new instance of <see cref="SqlEntityIncludedResult"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="propertyName"></param>
    ''' <param name="result"></param>
    Public Sub New(<DisallowNull> sql As String, <DisallowNull> propertyName As String, <DisallowNull> result As SqlResultBase)
      Me.Sql = sql
      Me.PropertyName = propertyName
      Me.Result = result
    End Sub

  End Class
End Namespace