Imports Yamo.Infrastructure

Namespace Infrastructure

  Public Class SQLiteFormatter
    Inherits SqlFormatter

    Public Overrides ReadOnly Property LikeWildcardsInParameter As Boolean
      Get
        Return True
      End Get
    End Property

    Public Overrides ReadOnly Property StringConcatenationOperator As String
      Get
        Return "||"
      End Get
    End Property

  End Class
End Namespace