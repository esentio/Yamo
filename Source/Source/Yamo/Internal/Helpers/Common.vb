Namespace Internal.Helpers

  ' TODO: SIP - add documentation to this class.
  Public Class Common

    Private Sub New()
    End Sub

    Public Shared Function GetEntityIndexFromJoinMemberName(name As String) As Int32
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
        Case Else
          Throw New NotSupportedException($"There is no entity index mapping for '{name}' member name.")
      End Select
    End Function

  End Class
End Namespace
