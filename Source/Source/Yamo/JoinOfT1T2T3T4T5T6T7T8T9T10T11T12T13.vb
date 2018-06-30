Public Class Join(Of TTable1, TTable2, TTable3, TTable4, TTable5, TTable6, TTable7, TTable8, TTable9, TTable10, TTable11, TTable12, TTable13)
  Implements IJoin

  Public ReadOnly Property T1 As TTable1

  Public ReadOnly Property T2 As TTable2

  Public ReadOnly Property T3 As TTable3

  Public ReadOnly Property T4 As TTable4

  Public ReadOnly Property T5 As TTable5

  Public ReadOnly Property T6 As TTable6

  Public ReadOnly Property T7 As TTable7

  Public ReadOnly Property T8 As TTable8

  Public ReadOnly Property T9 As TTable9

  Public ReadOnly Property T10 As TTable10

  Public ReadOnly Property T11 As TTable11

  Public ReadOnly Property T12 As TTable12

  Public ReadOnly Property T13 As TTable13

  Sub New(table1 As TTable1, table2 As TTable2, table3 As TTable3, table4 As TTable4, table5 As TTable5, table6 As TTable6, table7 As TTable7, table8 As TTable8, table9 As TTable9, table10 As TTable10, table11 As TTable11, table12 As TTable12, table13 As TTable13)
    Me.T1 = table1
    Me.T2 = table2
    Me.T3 = table3
    Me.T4 = table4
    Me.T5 = table5
    Me.T6 = table6
    Me.T7 = table7
    Me.T8 = table8
    Me.T9 = table9
    Me.T10 = table10
    Me.T11 = table11
    Me.T12 = table12
    Me.T13 = table13
  End Sub

End Class
