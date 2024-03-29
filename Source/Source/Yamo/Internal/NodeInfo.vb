﻿Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions

Namespace Internal

  ''' <summary>
  ''' Stores expression node with additional info.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class NodeInfo

    ' TODO: SIP - is really IsCompare, IsCoalesce, IsConditional, ... necessary? Would't it be better to check it when needed (during boolean expansion)?.

    ''' <summary>
    ''' Gets node.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Node As Expression

    ''' <summary>
    ''' Gets whether node represents negation (<see cref="ExpressionType.[Not]"/>).<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsNegation As Boolean

    ''' <summary>
    ''' Gets whether node represents negation (<see cref="ExpressionType.[Not]"/>) that should be ignored in its current location and processed differently (elsewhere).<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsIgnoredNegation As Boolean

    ''' <summary>
    ''' Gets whether node represents equality or inequality comparison (<see cref="ExpressionType.Equal"/> or <see cref="ExpressionType.NotEqual"/>).<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsCompare As Boolean

    ''' <summary>
    ''' Gets whether node represents coalesce operation (<see cref="ExpressionType.Coalesce"/>).<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsCoalesce As Boolean

    ''' <summary>
    ''' Gets whether node represents conditional operation (<see cref="ExpressionType.Conditional"/>).<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsConditional As Boolean

    ''' <summary>
    ''' Gets whether node represents <see cref="Nullable(Of T).Value"/> access.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsNullableValueAccess As Boolean

    ''' <summary>
    ''' Gets whether node represents <see cref="Nullable(Of T).HasValue"/> access.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsNullableHasValueAccess As Boolean

    ''' <summary>
    ''' Gets whether node represents cast or conversion operation.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsConvert As Boolean

    ''' <summary>
    ''' Gets whether node represents <see cref="Nullable(Of T)"/> constructor.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsNullableConstructor As Boolean

    ''' <summary>
    ''' Gets whether node represents object initialization.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsMemberInit As Boolean

    ''' <summary>
    ''' Creates new instance of <see cref="NodeInfo"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="node"></param>
    Public Sub New(<DisallowNull> node As Expression)
      Me.Node = node
      Me.IsNegation = False
      Me.IsIgnoredNegation = False
      Me.IsCompare = False
      Me.IsCoalesce = False
      Me.IsConditional = False
      Me.IsNullableValueAccess = False
      Me.IsNullableHasValueAccess = False
      Me.IsConvert = False
      Me.IsNullableConstructor = False
      Me.IsMemberInit = False
    End Sub

  End Class
End Namespace