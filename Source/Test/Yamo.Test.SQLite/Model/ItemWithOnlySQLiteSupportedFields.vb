Namespace Model

  Public Class ItemWithOnlySQLiteSupportedFields

    Public Property Id As Int32

    Public Property DateOnlyColumn As DateOnly

    Public Property DateOnlyColumnNull As DateOnly?

    Public Property TimeOnlyColumn As TimeOnly

    Public Property TimeOnlyColumnNull As TimeOnly?

    Public Property Nchar1Column As Char

    Public Property Nchar1ColumnNull As Char?

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot ItemWithOnlySQLiteSupportedFields Then
        Return False
      Else
        Dim o = DirectCast(obj, ItemWithOnlySQLiteSupportedFields)

        If Not Object.Equals(Me.Id, o.Id) Then Return False
        If Not Object.Equals(Me.DateOnlyColumn, o.DateOnlyColumn) Then Return False
        If Not Object.Equals(Me.DateOnlyColumnNull, o.DateOnlyColumnNull) Then Return False
        If Not Object.Equals(Me.TimeOnlyColumn, o.TimeOnlyColumn) Then Return False
        If Not Object.Equals(Me.TimeOnlyColumnNull, o.TimeOnlyColumnNull) Then Return False
        If Not Object.Equals(Me.Nchar1Column, o.Nchar1Column) Then Return False
        If Not Object.Equals(Me.Nchar1ColumnNull, o.Nchar1ColumnNull) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return HashCode.Combine(Me.Id, Me.DateOnlyColumn, Me.DateOnlyColumnNull, Me.TimeOnlyColumn, Me.TimeOnlyColumnNull, Me.Nchar1Column, Me.Nchar1ColumnNull)
    End Function

    Public Function ToRawValues() As Object()
      Dim value = New Object() {
        Me.Id,
        Me.DateOnlyColumn,
        Me.DateOnlyColumnNull,
        Me.TimeOnlyColumn,
        Me.TimeOnlyColumnNull,
        Me.Nchar1Column,
        Me.Nchar1ColumnNull
      }

      For i = 0 To value.Length - 1
        If value(i) Is Nothing Then
          value(i) = DBNull.Value
        End If
      Next

      Return value
    End Function

  End Class
End Namespace