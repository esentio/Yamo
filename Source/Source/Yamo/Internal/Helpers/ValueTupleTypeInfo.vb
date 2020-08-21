Namespace Internal.Helpers

  ''' <summary>
  ''' Value tuple and nullable value tuple type info.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class ValueTupleTypeInfo

    ''' <summary>
    ''' Gets (nullable) value tuple type.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ValueTupleType As Type

    ''' <summary>
    ''' Gets flattened ValueTuple arguments.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FlattenedArguments As List(Of Type)

    ''' <summary>
    ''' Gets nested ValueTuple constructor infos.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CtorInfos As List(Of CtorInfo)

    ''' <summary>
    ''' Creates new instance of <see cref="ValueTupleTypeInfo"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="valueTupleType"></param>
    ''' <param name="flattenedArguments"></param>
    ''' <param name="ctorInfos"></param>
    Public Sub New(valueTupleType As Type, flattenedArguments As List(Of Type), ctorInfos As List(Of CtorInfo))
      Me.ValueTupleType = valueTupleType
      Me.FlattenedArguments = flattenedArguments
      Me.CtorInfos = ctorInfos
    End Sub

  End Class
End Namespace
