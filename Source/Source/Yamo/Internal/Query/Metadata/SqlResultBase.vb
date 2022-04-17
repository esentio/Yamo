Imports System.Diagnostics.CodeAnalysis

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Base class for SQL results.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public MustInherit Class SqlResultBase

    ''' <summary>
    ''' Gets type of the result.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ResultType As Type

    ''' <summary>
    ''' Gets entity creation behavior.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Property CreationBehavior As NonModelEntityCreationBehavior

    ''' <summary>
    ''' Creates new instance of <see cref="SqlResultBase"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="resultType"></param>
    Public Sub New(<DisallowNull> resultType As Type)
      Me.ResultType = resultType
      ' NOTE: probably better would be to set the default to NullIfAllColumnsAreNull.
      ' It's like this for backward compatibility (to avoid breaking change in the behavior, since we won't be able to manage it for Include results at the moment).
      Me.CreationBehavior = NonModelEntityCreationBehavior.AlwaysCreateInstance
    End Sub

    ''' <summary>
    ''' Gets count of columns in the resultset.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function GetColumnCount() As Int32

  End Class
End Namespace