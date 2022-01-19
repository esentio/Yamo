Imports System.Diagnostics.CodeAnalysis
Imports System.Reflection

Namespace Internal.Helpers

  ''' <summary>
  ''' Constructor info.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Structure CtorInfo

    ''' <summary>
    ''' Gets constructor info.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ConstructorInfo As ConstructorInfo

    ''' <summary>
    ''' Gets cached parameter count for <see cref="ConstructorInfo"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ParameterCount As Int32

    ''' <summary>
    ''' Creates new instance of <see cref="CtorInfo"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="constructorInfo"></param>
    ''' <param name="parameterCount"></param>
    Public Sub New(<DisallowNull> constructorInfo As ConstructorInfo, parameterCount As Int32)
      Me.ConstructorInfo = constructorInfo
      Me.ParameterCount = parameterCount
    End Sub

  End Structure
End Namespace
