Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL related model data for delete statement.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class DeleteSqlModel
    Inherits SqlModelBase

    ''' <summary>
    ''' Creates new instance of <see cref="DeleteSqlModel"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="mainEntityType"></param>
    Public Sub New(model As Model, mainEntityType As Type)
      MyBase.New(model, mainEntityType)
    End Sub

  End Class
End Namespace