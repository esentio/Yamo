Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Internal.Query

''' <summary>
''' A <see cref="Subquery(Of T)"/> instance represents SQL subquery.
''' </summary>
''' <typeparam name="T"></typeparam>
Public Class Subquery(Of T)

  ''' <summary>
  ''' SQL subquery.
  ''' </summary>
  ''' <returns></returns>
  Friend ReadOnly Property Query As SelectQuery

  ''' <summary>
  ''' Creates new instance of <see cref="Subquery(Of T)"/>.
  ''' </summary>
  ''' <param name="query"></param>
  Friend Sub New(query As SelectQuery)
    Me.Query = query
  End Sub

End Class
