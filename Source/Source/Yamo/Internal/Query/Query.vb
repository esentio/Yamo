Namespace Internal.Query

  ''' <summary>
  ''' Represents SQL query/statement data.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class Query

    Private m_Sql As String
    ''' <summary>
    ''' Gets SQL string.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Sql As String
      Get
        Return m_Sql
      End Get
    End Property

    Private m_Parameters As List(Of SqlParameter)
    ''' <summary>
    ''' Gets SQL parameters.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Parameters As List(Of SqlParameter)
      Get
        Return m_Parameters
      End Get
    End Property

    ''' <summary>
    ''' Creates new instance of <see cref="Query"/><br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sql"></param>
    Sub New(sql As SqlString)
      m_Sql = sql.Sql
      m_Parameters = sql.Parameters
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="Query"/><br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="parameters"></param>
    Sub New(sql As String, parameters As List(Of SqlParameter))
      m_Sql = sql
      m_Parameters = parameters
    End Sub

  End Class
End Namespace