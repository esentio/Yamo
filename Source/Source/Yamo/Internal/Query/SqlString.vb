Namespace Internal.Query

  ''' <summary>
  ''' Represents SQL string.
  ''' </summary>
  Public Structure SqlString

    ''' <summary>
    ''' Gets SQL string.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Sql As String

    ''' <summary>
    ''' Gets SQL parameters.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Parameters As IReadOnlyList(Of SqlParameter)

    ''' <summary>
    ''' Creates new instance of <see cref="SqlString"/>.
    ''' </summary>
    ''' <param name="sql"></param>
    Sub New(sql As String)
      Me.Sql = sql
      Me.Parameters = Array.Empty(Of SqlParameter)()
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="SqlString"/>.
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="parameters"></param>
    Sub New(sql As String, parameters As IReadOnlyList(Of SqlParameter))
      Me.Sql = sql
      Me.Parameters = parameters
    End Sub

  End Structure
End Namespace