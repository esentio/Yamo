Namespace Internal.Query

  ''' <summary>
  ''' Represents SQL query/statement data.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class Query

    ''' <summary>
    ''' Gets SQL string.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Sql As String

    ''' <summary>
    ''' Gets SQL parameters.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Parameters As IReadOnlyList(Of SqlParameter)

    ''' <summary>
    ''' Creates new instance of <see cref="Query"/><br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sql"></param>
    Sub New(sql As SqlString)
      Me.Sql = sql.Sql
      Me.Parameters = sql.Parameters
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="Query"/><br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="parameters"></param>
    Sub New(sql As String, parameters As IReadOnlyList(Of SqlParameter))
      Me.Sql = sql
      Me.Parameters = parameters
    End Sub

  End Class
End Namespace