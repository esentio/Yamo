Imports System.Linq.Expressions
Imports System.Runtime.CompilerServices

Namespace Extensions

  <Extension()>
  Public Module Int32Extensions

    <Extension>
    Public Function ToInvariantString(source As Int32) As String
      Return source.ToString(Globalization.CultureInfo.InvariantCulture)
    End Function

  End Module
End Namespace