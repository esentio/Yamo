Namespace Internal.Query

  Public Class Query

    Private m_Sql As String

    Public ReadOnly Property Sql As String
      Get
        Return m_Sql
      End Get
    End Property

    Private m_Parameters As List(Of SqlParameter)
    Public ReadOnly Property Parameters As List(Of SqlParameter)
      Get
        Return m_Parameters
      End Get
    End Property

    Sub New(sql As SqlString)
      m_Sql = sql.Sql
      m_Parameters = sql.Parameters
    End Sub

    Sub New(sql As String, parameters As List(Of SqlParameter))
      m_Sql = sql
      m_Parameters = parameters
    End Sub

  End Class
End Namespace