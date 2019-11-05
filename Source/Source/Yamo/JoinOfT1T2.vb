''' <summary>
''' Metadata defining 2 entities used in JOIN statements.
''' </summary>
''' <typeparam name="TTable1"></typeparam>
''' <typeparam name="TTable2"></typeparam>
Public Class Join(Of TTable1, TTable2)
  Implements IJoin

  ''' <summary>
  ''' Gets 1st entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T1 As TTable1

  ''' <summary>
  ''' Gets 2nd entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T2 As TTable2

  ''' <summary>
  ''' Creates new instance of <see cref="Join(Of TTable1, TTable2)"/>.
  ''' </summary>
  ''' <param name="table1"></param>
  ''' <param name="table2"></param>
  Sub New(table1 As TTable1, table2 As TTable2)
    Me.T1 = table1
    Me.T2 = table2
  End Sub

End Class
