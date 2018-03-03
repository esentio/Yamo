Public Class Join(Of TTable1, TTable2, TTable3, TTable4)
  Implements IJoin

  Public ReadOnly Property T1 As TTable1

  Public ReadOnly Property T2 As TTable2

  Public ReadOnly Property T3 As TTable3

  Public ReadOnly Property T4 As TTable4

  Sub New(table1 As TTable1, table2 As TTable2, table3 As TTable3, table4 As TTable4)
    Me.T1 = table1
    Me.T2 = table2
    Me.T3 = table3
    Me.T4 = table4
  End Sub

End Class
