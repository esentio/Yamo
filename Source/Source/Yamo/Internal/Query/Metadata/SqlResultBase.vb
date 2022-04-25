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
    Public ReadOnly Property CreationBehavior As NonModelEntityCreationBehavior

    ''' <summary>
    ''' Creates new instance of <see cref="SqlResultBase"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="resultType"></param>
    Public Sub New(<DisallowNull> resultType As Type)
      Me.ResultType = resultType
      Me.CreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull
    End Sub

    ''' <summary>
    ''' Sets creation behavior unless <see cref="NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull"/> value is passed as a parameter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="behavior"></param>
    Public Sub UpdateCreationBehavior(behavior As NonModelEntityCreationBehavior)
      ' NOTE: this method is called from custom select and include. Those two are mutually
      ' exclusive, so it's not possible to call this method twice on the same result (i.e. this instance).
      ' However, include can be called multiple times and theoretically on the same result, if
      ' it originates from the subquery. This is a problem, because last call wins and all included
      ' values will follow the same behavior.
      ' Solution is to create new result instance (copy) if needed. However, this is an edge
      ' case and we don't do it for now for the simplicity and allocation reasons.
      If Not behavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull Then
        _CreationBehavior = behavior
      End If
    End Sub

    ''' <summary>
    ''' Gets count of columns in the resultset.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function GetColumnCount() As Int32

  End Class
End Namespace