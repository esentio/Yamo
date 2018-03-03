Public Class Join(Of TTable1, TTable2, TTable3)
  Implements IJoin

  Public ReadOnly Property T1 As TTable1

  Public ReadOnly Property T2 As TTable2

  Public ReadOnly Property T3 As TTable3

  Sub New(table1 As TTable1, table2 As TTable2, table3 As TTable3)
    Me.T1 = table1
    Me.T2 = table2
    Me.T3 = table3
  End Sub

End Class
