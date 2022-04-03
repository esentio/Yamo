﻿Imports System.Diagnostics.CodeAnalysis

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL related join info.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Structure JoinInfo

    ''' <summary>
    ''' Gets join type<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property JoinType As JoinType

    ''' <summary>
    ''' Gets table source (if used).<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TableSource As <MaybeNull> String

    ''' <summary>
    ''' Gets ad hoc non model entity (if used).<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property NonModelEntity As <MaybeNull> NonModelEntity

    ''' <summary>
    ''' Creates new instance of <see cref="JoinInfo"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="joinType"></param>
    Sub New(<DisallowNull> joinType As JoinType)
      Me.New(joinType, Nothing)
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="JoinInfo"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="joinType"></param>
    ''' <param name="tableSource"></param>
    Sub New(<DisallowNull> joinType As JoinType, tableSource As String)
      Me.New(joinType, tableSource, Nothing)
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="JoinInfo"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="joinType"></param>
    ''' <param name="tableSource"></param>
    ''' <param name="nonModelEntity"></param>
    Sub New(<DisallowNull> joinType As JoinType, tableSource As String, nonModelEntity As NonModelEntity)
      Me.JoinType = joinType
      Me.TableSource = tableSource
      Me.NonModelEntity = nonModelEntity
    End Sub

  End Structure
End Namespace