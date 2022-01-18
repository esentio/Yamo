Imports System.Diagnostics.CodeAnalysis

Namespace Internal.Helpers

  ''' <summary>
  ''' Miscellaneous helpers.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class Common

    ''' <summary>
    ''' Creates new instance of <see cref="Common"/>.
    ''' </summary>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Gets entity index from join member name.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    Public Shared Function GetEntityIndexFromJoinMemberName(<DisallowNull> name As String) As Int32
      Select Case name
        Case "T1"
          Return 0
        Case "T2"
          Return 1
        Case "T3"
          Return 2
        Case "T4"
          Return 3
        Case "T5"
          Return 4
        Case "T6"
          Return 5
        Case "T7"
          Return 6
        Case "T8"
          Return 7
        Case "T9"
          Return 8
        Case "T10"
          Return 9
        Case "T11"
          Return 10
        Case "T12"
          Return 11
        Case "T13"
          Return 12
        Case "T14"
          Return 13
        Case "T15"
          Return 14
        Case "T16"
          Return 15
        Case "T17"
          Return 16
        Case "T18"
          Return 17
        Case "T19"
          Return 18
        Case "T20"
          Return 19
        Case "T21"
          Return 20
        Case "T22"
          Return 21
        Case "T23"
          Return 22
        Case "T24"
          Return 23
        Case "T25"
          Return 24
        Case Else
          Throw New NotSupportedException($"There is no entity index mapping for '{name}' member name.")
      End Select
    End Function

  End Class
End Namespace
