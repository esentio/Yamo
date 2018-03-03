Public Class Join(Of TTable1, TTable2, TTable3, TTable4, TTable5, TTable6, TTable7)
  Implements IJoin

  Public ReadOnly Property T1 As TTable1

  Public ReadOnly Property T2 As TTable2

  Public ReadOnly Property T3 As TTable3

  Public ReadOnly Property T4 As TTable4

  Public ReadOnly Property T5 As TTable5

  Public ReadOnly Property T6 As TTable6

  Public ReadOnly Property T7 As TTable7

  Sub New(table1 As TTable1, table2 As TTable2, table3 As TTable3, table4 As TTable4, table5 As TTable5, table6 As TTable6, table7 As TTable7)
    Me.T1 = table1
    Me.T2 = table2
    Me.T3 = table3
    Me.T4 = table4
    Me.T5 = table5
    Me.T6 = table6
    Me.T7 = table7
  End Sub

End Class
