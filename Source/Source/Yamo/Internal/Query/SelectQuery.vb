Imports Yamo.Internal.Query.Metadata

Namespace Internal.Query

  ''' <summary>
  ''' Represents select query data.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SelectQuery
    Inherits Query

    ''' <summary>
    ''' Gets SQL model.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Model As SqlModel

    ''' <summary>
    ''' Creates new instance of <see cref="SelectQuery"/><br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="model"></param>
    Sub New(sql As SqlString, model As SqlModel)
      MyBase.New(sql)
      Me.Model = model
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="SelectQuery"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="parameters"></param>
    ''' <param name="model"></param>
    Sub New(sql As String, parameters As List(Of SqlParameter), model As SqlModel)
      MyBase.New(sql, parameters)
      Me.Model = model
    End Sub

  End Class
End Namespace