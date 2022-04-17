Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL related model data for update statement.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class UpdateSqlModel
    Inherits SqlModelBase

    ''' <summary>
    ''' Gets main SQL entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Overloads ReadOnly Property MainEntity As EntityBasedSqlEntity
      Get
        Return m_MainEntity
      End Get
    End Property

    ''' <summary>
    ''' Stores main SQL entity.
    ''' </summary>
    Private m_MainEntity As EntityBasedSqlEntity

    ''' <summary>
    ''' Creates new instance of <see cref="UpdateSqlModel"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="mainEntity"></param>
    Public Sub New(<DisallowNull> mainEntity As Entity)
      MyBase.New()
      m_MainEntity = SetMainEntity(mainEntity, False)
    End Sub

  End Class
End Namespace