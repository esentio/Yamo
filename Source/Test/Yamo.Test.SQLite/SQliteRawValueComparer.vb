Imports Yamo.SQLite.Infrastructure

Public Class SQliteRawValueComparer
  Inherits RawValueComparer

  Private m_Conversion As SQLiteDbValueConversion

  Sub New()
    m_Conversion = New SQLiteDbValueConversion
  End Sub

  Public Overrides Sub AreRawValuesEqual(expected As Object, actual As Object)
    If actual IsNot DBNull.Value Then
      If TypeOf expected Is Guid Then
        actual = m_Conversion.FromDbValue(Of Guid)(actual)
      ElseIf TypeOf expected Is Guid? Then
        actual = m_Conversion.FromDbValue(Of Guid?)(actual)

      ElseIf TypeOf expected Is Boolean Then
        actual = m_Conversion.FromDbValue(Of Boolean)(actual)
      ElseIf TypeOf expected Is Boolean? Then
        actual = m_Conversion.FromDbValue(Of Boolean?)(actual)

      ElseIf TypeOf expected Is Int16 Then
        actual = m_Conversion.FromDbValue(Of Int16)(actual)
      ElseIf TypeOf expected Is Int16? Then
        actual = m_Conversion.FromDbValue(Of Int16?)(actual)

      ElseIf TypeOf expected Is Int32 Then
        actual = m_Conversion.FromDbValue(Of Int32)(actual)
      ElseIf TypeOf expected Is Int32? Then
        actual = m_Conversion.FromDbValue(Of Int32?)(actual)

      ElseIf TypeOf expected Is Single Then
        actual = m_Conversion.FromDbValue(Of Single)(actual)
      ElseIf TypeOf expected Is Single? Then
        actual = m_Conversion.FromDbValue(Of Single?)(actual)

      ElseIf TypeOf expected Is Decimal Then
        actual = m_Conversion.FromDbValue(Of Decimal)(actual)
      ElseIf TypeOf expected Is Decimal? Then
        actual = m_Conversion.FromDbValue(Of Decimal?)(actual)

      ElseIf TypeOf expected Is DateTime Then
        actual = m_Conversion.FromDbValue(Of DateTime)(actual)
      ElseIf TypeOf expected Is DateTime? Then
        actual = m_Conversion.FromDbValue(Of DateTime?)(actual)
      End If
    End If

    MyBase.AreRawValuesEqual(expected, actual)
  End Sub

End Class
