Public Class Join(Of TTable1, TTable2)
  Implements IJoin

  Public ReadOnly Property T1 As TTable1

  Public ReadOnly Property T2 As TTable2

  Sub New(table1 As TTable1, table2 As TTable2)
    Me.T1 = table1
    Me.T2 = table2
  End Sub

End Class
