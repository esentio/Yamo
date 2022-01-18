Imports System.Diagnostics.CodeAnalysis

''' <summary>
''' Metadata defining 14 entities used in JOIN statements.
''' </summary>
''' <typeparam name="TTable1"></typeparam>
''' <typeparam name="TTable2"></typeparam>
''' <typeparam name="TTable3"></typeparam>
''' <typeparam name="TTable4"></typeparam>
''' <typeparam name="TTable5"></typeparam>
''' <typeparam name="TTable6"></typeparam>
''' <typeparam name="TTable7"></typeparam>
''' <typeparam name="TTable8"></typeparam>
''' <typeparam name="TTable9"></typeparam>
''' <typeparam name="TTable10"></typeparam>
''' <typeparam name="TTable11"></typeparam>
''' <typeparam name="TTable12"></typeparam>
''' <typeparam name="TTable13"></typeparam>
''' <typeparam name="TTable14"></typeparam>
Public Class Join(Of TTable1, TTable2, TTable3, TTable4, TTable5, TTable6, TTable7, TTable8, TTable9, TTable10, TTable11, TTable12, TTable13, TTable14)
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
  ''' Gets 3rd entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T3 As TTable3

  ''' <summary>
  ''' Gets 4th entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T4 As TTable4

  ''' <summary>
  ''' Gets 5th entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T5 As TTable5

  ''' <summary>
  ''' Gets 6th entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T6 As TTable6

  ''' <summary>
  ''' Gets 7th entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T7 As TTable7

  ''' <summary>
  ''' Gets 8th entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T8 As TTable8

  ''' <summary>
  ''' Gets 9th entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T9 As TTable9

  ''' <summary>
  ''' Gets 10th entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T10 As TTable10

  ''' <summary>
  ''' Gets 11th entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T11 As TTable11

  ''' <summary>
  ''' Gets 12th entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T12 As TTable12

  ''' <summary>
  ''' Gets 13th entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T13 As TTable13

  ''' <summary>
  ''' Gets 14th entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T14 As TTable14

  ''' <summary>
  ''' Creates new instance of <see cref="Join(Of TTable1, TTable2, TTable3, TTable4, TTable5, TTable6, TTable7, TTable8, TTable9, TTable10, TTable11, TTable12, TTable13, TTable14)"/>.
  ''' </summary>
  ''' <param name="table1"></param>
  ''' <param name="table2"></param>
  ''' <param name="table3"></param>
  ''' <param name="table4"></param>
  ''' <param name="table5"></param>
  ''' <param name="table6"></param>
  ''' <param name="table7"></param>
  ''' <param name="table8"></param>
  ''' <param name="table9"></param>
  ''' <param name="table10"></param>
  ''' <param name="table11"></param>
  ''' <param name="table12"></param>
  ''' <param name="table13"></param>
  ''' <param name="table14"></param>
  Sub New(<DisallowNull> table1 As TTable1, <DisallowNull> table2 As TTable2, <DisallowNull> table3 As TTable3, <DisallowNull> table4 As TTable4, <DisallowNull> table5 As TTable5, <DisallowNull> table6 As TTable6, <DisallowNull> table7 As TTable7, <DisallowNull> table8 As TTable8, <DisallowNull> table9 As TTable9, <DisallowNull> table10 As TTable10, <DisallowNull> table11 As TTable11, <DisallowNull> table12 As TTable12, <DisallowNull> table13 As TTable13, <DisallowNull> table14 As TTable14)
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
    Me.T14 = table14
  End Sub

End Class
