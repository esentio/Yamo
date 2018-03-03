Namespace Helpers

  Public Class Data

    Private Shared m_Random As Random = New Random

    Private Sub New()
    End Sub

    Public Shared Function CreateRandomString(maxLength As Int32) As String
      Dim chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ľščťžýáíéäôúň,.-!?: "

      Dim sb = New Text.StringBuilder()

      Dim count = m_Random.Next(maxLength - 1)

      For i = 0 To count - 1
        sb.Append(chars(m_Random.Next(chars.Length - 1)))
      Next

      Return sb.ToString()
    End Function

    Public Shared Function CreateLargeRandomString() As String
      Dim chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"

      Dim sb = New Text.StringBuilder()

      sb.AppendLine("Lorem ipsum dolor sit amet.")
      sb.AppendLine("ľščťžýáíéäôúň,.-!?:")
      sb.AppendLine(chars)

      Dim count = m_Random.Next(1000, 10000)

      For i = 0 To count - 1
        sb.Append(chars(m_Random.Next(chars.Length - 1)))
      Next

      Return sb.ToString()
    End Function

    Public Shared Function CreateRandomInt32() As Int32
      Return m_Random.Next(Int32.MinValue, Int32.MaxValue)
    End Function

    Public Shared Function CreateRandomDecimal(precision As Int32, scale As Int32) As Decimal
      Dim intLimit = Convert.ToInt32(Math.Pow(10, (precision - scale))) - 1
      Dim intPart = m_Random.Next(-intLimit, intLimit)

      Dim decLimit = Convert.ToInt32(Math.Pow(10, scale)) - 1
      Dim decPart = m_Random.Next(0, decLimit)

      Return intPart + decPart * Convert.ToDecimal(Math.Pow(0.1D, scale))
    End Function

    Public Shared Function CreateRandomPositiveDecimal(precision As Int32, scale As Int32) As Decimal
      Return Math.Abs(CreateRandomDecimal(precision, scale))
    End Function

    Public Shared Function CreateRandomByteArray(length As Int32) As Byte()
      Dim arr = New Byte(length - 1) {}

      m_Random.NextBytes(arr)

      Return arr
    End Function

  End Class
End Namespace
