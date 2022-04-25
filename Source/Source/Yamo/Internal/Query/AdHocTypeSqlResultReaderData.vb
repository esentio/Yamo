﻿Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Internal.Query.Metadata

Namespace Internal.Query

  ''' <summary>
  ''' Represents reader data for ad hoc type values.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class AdHocTypeSqlResultReaderData
    Inherits ReaderDataBase

    ''' <summary>
    ''' Gets reader data of ad hoc type constructor arguments.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CtorArguments As ReaderDataBase()

    ''' <summary>
    ''' Gets reader data of ad hoc type member inits.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MemberInits As ReaderDataBase()

    ''' <summary>
    ''' Creates new instance of <see cref="AdHocTypeSqlResultReaderData"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <param name="ctorArguments"></param>
    ''' <param name="memberInits"></param>
    Public Sub New(<DisallowNull> sqlResult As AdHocTypeSqlResult, readerIndex As Int32, <DisallowNull> ctorArguments As ReaderDataBase(), <DisallowNull> memberInits As ReaderDataBase())
      MyBase.New(sqlResult, readerIndex, Not sqlResult.CreationBehavior = NonModelEntityCreationBehavior.AlwaysCreateInstance)
      Me.CtorArguments = ctorArguments
      Me.MemberInits = memberInits
    End Sub

  End Class
End Namespace