﻿''' <summary>
''' Metadata defining 7 entities used in JOIN statements.
''' </summary>
''' <typeparam name="TTable1"></typeparam>
''' <typeparam name="TTable2"></typeparam>
''' <typeparam name="TTable3"></typeparam>
''' <typeparam name="TTable4"></typeparam>
''' <typeparam name="TTable5"></typeparam>
''' <typeparam name="TTable6"></typeparam>
''' <typeparam name="TTable7"></typeparam>
Public Class Join(Of TTable1, TTable2, TTable3, TTable4, TTable5, TTable6, TTable7)
  Implements IJoin

  ''' <summary>
  ''' 1st entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T1 As TTable1

  ''' <summary>
  ''' 2nd entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T2 As TTable2

  ''' <summary>
  ''' 3rd entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T3 As TTable3

  ''' <summary>
  ''' 4th entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T4 As TTable4

  ''' <summary>
  ''' 5th entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T5 As TTable5

  ''' <summary>
  ''' 6th entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T6 As TTable6

  ''' <summary>
  ''' 7th entity.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property T7 As TTable7

  ''' <summary>
  ''' Creates new instance of <see cref="Join(Of TTable1, TTable2, TTable3, TTable4, TTable5, TTable6, TTable7)"/>.
  ''' </summary>
  ''' <param name="table1"></param>
  ''' <param name="table2"></param>
  ''' <param name="table3"></param>
  ''' <param name="table4"></param>
  ''' <param name="table5"></param>
  ''' <param name="table6"></param>
  ''' <param name="table7"></param>
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
