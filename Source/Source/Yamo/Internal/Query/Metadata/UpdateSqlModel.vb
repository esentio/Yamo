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
    ''' Creates new instance of <see cref="UpdateSqlModel"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="mainEntity"></param>
    Public Sub New(<DisallowNull> model As Model, <DisallowNull> mainEntity As Entity)
      MyBase.New(model, mainEntity)
    End Sub

  End Class
End Namespace