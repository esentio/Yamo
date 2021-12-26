Public Class RawValueComparer

  Public Overridable Sub AreRawValuesEqual(expected As Object(), actual As Object())
    If expected Is Nothing Then
      Assert.IsNull(actual)
      Return
    End If

    If expected IsNot Nothing Then
      Assert.IsNotNull(actual)
    End If

    Assert.AreEqual(expected.Length, actual.Length)

    For i = 0 To expected.Length - 1
      AreRawValuesEqual(expected(i), actual(i))
    Next
  End Sub

  Public Overridable Sub AreRawValuesEqual(expected As Object, actual As Object)
    If TypeOf expected Is Byte() Then
      If Not Helpers.Compare.AreByteArraysEqual(CType(expected, Byte()), CType(actual, Byte())) Then
        Assert.Fail()
      End If
    Else
      Assert.AreEqual(expected, actual)
    End If
  End Sub

End Class
