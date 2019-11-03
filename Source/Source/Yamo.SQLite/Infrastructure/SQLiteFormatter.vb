Imports Yamo.Infrastructure

Namespace Infrastructure

  ''' <summary>
  ''' SQL formatter for SQLite.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SQLiteFormatter
    Inherits SqlFormatter

    ''' <summary>
    ''' Gets whether LIKE wildcards are in a parameter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property LikeWildcardsInParameter As Boolean
      Get
        Return True
      End Get
    End Property

    ''' <summary>
    ''' Gets string concatenation operator.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property StringConcatenationOperator As String
      Get
        Return "||"
      End Get
    End Property

  End Class
End Namespace