Namespace Helpers

  ''' <summary>
  ''' Compare helper functions
  ''' </summary>
  ''' <remarks></remarks>
  Public Class Compare

    ''' <summary>
    ''' Determine if two byte arrays are equal
    ''' </summary>
    ''' <param name="array1"></param>
    ''' <param name="array2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function AreByteArraysEqual(array1() As Byte, array2() As Byte) As Boolean
      If array1 Is Nothing AndAlso array2 Is Nothing Then Return True
      If array1 Is Nothing OrElse array2 Is Nothing Then Return False
      If Not array1.Length = array2.Length Then Return False

      For i = 0 To array1.Length - 1
        If Not array1(i) = array2(i) Then Return False
      Next

      Return True
    End Function

  End Class
End Namespace