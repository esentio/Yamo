Namespace Model

  Public Class ItemWithAllSupportedValues

    Public Property Id As Guid

    Public Property UniqueidentifierColumn As Guid

    Public Property UniqueidentifierColumnNull As Guid?

    Public Property Nvarchar50Column As String

    Public Property Nvarchar50ColumnNull As String

    Public Property NvarcharMaxColumn As String

    Public Property NvarcharMaxColumnNull As String

    Public Property BitColumn As Boolean

    Public Property BitColumnNull As Boolean?

    Public Property SmallintColumn As Int16

    Public Property SmallintColumnNull As Int16?

    Public Property IntColumn As Int32

    Public Property IntColumnNull As Int32?

    Public Property BigintColumn As Int64

    Public Property BigintColumnNull As Int64?

    Public Property RealColumn As Single

    Public Property RealColumnNull As Single?

    Public Property FloatColumn As Double

    Public Property FloatColumnNull As Double?

    Public Property Numeric10and3Column As Decimal

    Public Property Numeric10and3ColumnNull As Decimal?

    Public Property Numeric15and0Column As Decimal

    Public Property Numeric15and0ColumnNull As Decimal?

    Public Property DateColumn As DateTime

    Public Property DateColumnNull As DateTime?

    Public Property DatetimeColumn As DateTime

    Public Property DatetimeColumnNull As DateTime?

    Public Property Varbinary50Column As Byte()

    Public Property Varbinary50ColumnNull As Byte()

    Public Property VarbinaryMaxColumn As Byte()

    Public Property VarbinaryMaxColumnNull As Byte()

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot ItemWithAllSupportedValues Then
        Return False
      Else
        Dim o = DirectCast(obj, ItemWithAllSupportedValues)

        If Not Object.Equals(Me.Id, o.Id) Then Return False
        If Not Object.Equals(Me.UniqueidentifierColumn, o.UniqueidentifierColumn) Then Return False
        If Not Object.Equals(Me.UniqueidentifierColumnNull, o.UniqueidentifierColumnNull) Then Return False
        If Not Object.Equals(Me.Nvarchar50Column, o.Nvarchar50Column) Then Return False
        If Not Object.Equals(Me.Nvarchar50ColumnNull, o.Nvarchar50ColumnNull) Then Return False
        If Not Object.Equals(Me.NvarcharMaxColumn, o.NvarcharMaxColumn) Then Return False
        If Not Object.Equals(Me.NvarcharMaxColumnNull, o.NvarcharMaxColumnNull) Then Return False
        If Not Object.Equals(Me.BitColumn, o.BitColumn) Then Return False
        If Not Object.Equals(Me.BitColumnNull, o.BitColumnNull) Then Return False
        If Not Object.Equals(Me.SmallintColumn, o.SmallintColumn) Then Return False
        If Not Object.Equals(Me.SmallintColumnNull, o.SmallintColumnNull) Then Return False
        If Not Object.Equals(Me.IntColumn, o.IntColumn) Then Return False
        If Not Object.Equals(Me.IntColumnNull, o.IntColumnNull) Then Return False
        If Not Object.Equals(Me.BigintColumn, o.BigintColumn) Then Return False
        If Not Object.Equals(Me.BigintColumnNull, o.BigintColumnNull) Then Return False
        If Not Object.Equals(Me.RealColumn, o.RealColumn) Then Return False
        If Not Object.Equals(Me.RealColumnNull, o.RealColumnNull) Then Return False
        If Not Object.Equals(Me.FloatColumn, o.FloatColumn) Then Return False
        If Not Object.Equals(Me.FloatColumnNull, o.FloatColumnNull) Then Return False
        If Not Object.Equals(Me.Numeric10and3Column, o.Numeric10and3Column) Then Return False
        If Not Object.Equals(Me.Numeric10and3ColumnNull, o.Numeric10and3ColumnNull) Then Return False
        If Not Object.Equals(Me.Numeric15and0Column, o.Numeric15and0Column) Then Return False
        If Not Object.Equals(Me.Numeric15and0ColumnNull, o.Numeric15and0ColumnNull) Then Return False
        If Not Object.Equals(Me.DateColumn, o.DateColumn) Then Return False
        If Not Object.Equals(Me.DateColumnNull, o.DateColumnNull) Then Return False
        If Not Object.Equals(Me.DatetimeColumn, o.DatetimeColumn) Then Return False
        If Not Object.Equals(Me.DatetimeColumnNull, o.DatetimeColumnNull) Then Return False
        If Not Helpers.Compare.AreByteArraysEqual(Me.Varbinary50Column, o.Varbinary50Column) Then Return False
        If Not Helpers.Compare.AreByteArraysEqual(Me.Varbinary50ColumnNull, o.Varbinary50ColumnNull) Then Return False
        If Not Helpers.Compare.AreByteArraysEqual(Me.VarbinaryMaxColumn, o.VarbinaryMaxColumn) Then Return False
        If Not Helpers.Compare.AreByteArraysEqual(Me.VarbinaryMaxColumnNull, o.VarbinaryMaxColumnNull) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Dim hashCode = New HashCode()

      hashCode.Add(Me.Id)
      hashCode.Add(Me.UniqueidentifierColumn)
      hashCode.Add(Me.UniqueidentifierColumnNull)
      hashCode.Add(Me.Nvarchar50Column)
      hashCode.Add(Me.Nvarchar50ColumnNull)
      hashCode.Add(Me.NvarcharMaxColumn)
      hashCode.Add(Me.NvarcharMaxColumnNull)
      hashCode.Add(Me.BitColumn)
      hashCode.Add(Me.BitColumnNull)
      hashCode.Add(Me.SmallintColumn)
      hashCode.Add(Me.SmallintColumnNull)
      hashCode.Add(Me.IntColumn)
      hashCode.Add(Me.IntColumnNull)
      hashCode.Add(Me.BigintColumn)
      hashCode.Add(Me.BigintColumnNull)
      hashCode.Add(Me.RealColumn)
      hashCode.Add(Me.RealColumnNull)
      hashCode.Add(Me.FloatColumn)
      hashCode.Add(Me.FloatColumnNull)
      hashCode.Add(Me.Numeric10and3Column)
      hashCode.Add(Me.Numeric10and3ColumnNull)
      hashCode.Add(Me.Numeric15and0Column)
      hashCode.Add(Me.Numeric15and0ColumnNull)
      hashCode.Add(Me.DateColumn)
      hashCode.Add(Me.DateColumnNull)
      hashCode.Add(Me.DatetimeColumn)
      hashCode.Add(Me.DatetimeColumnNull)

      If Me.Varbinary50Column Is Nothing Then
        hashCode.Add(Of Byte())(Nothing)
      Else
        For Each b In Me.Varbinary50Column.Cast(Of Object)
          hashCode.Add(b)
        Next
      End If

      If Me.Varbinary50ColumnNull Is Nothing Then
        hashCode.Add(Of Byte())(Nothing)
      Else
        For Each b In Me.Varbinary50ColumnNull.Cast(Of Object)
          hashCode.Add(b)
        Next
      End If

      If Me.VarbinaryMaxColumn Is Nothing Then
        hashCode.Add(Of Byte())(Nothing)
      Else
        For Each b In Me.VarbinaryMaxColumn.Cast(Of Object)
          hashCode.Add(b)
        Next
      End If

      If Me.VarbinaryMaxColumnNull Is Nothing Then
        hashCode.Add(Of Byte())(Nothing)
      Else
        For Each b In Me.VarbinaryMaxColumnNull.Cast(Of Object)
          hashCode.Add(b)
        Next
      End If

      Return hashCode.ToHashCode()
    End Function

    Public Function ToRawValues() As Object()
      Dim value = New Object() {
        Me.Id,
        Me.UniqueidentifierColumn,
        Me.UniqueidentifierColumnNull,
        Me.Nvarchar50Column,
        Me.Nvarchar50ColumnNull,
        Me.NvarcharMaxColumn,
        Me.NvarcharMaxColumnNull,
        Me.BitColumn,
        Me.BitColumnNull,
        Me.SmallintColumn,
        Me.SmallintColumnNull,
        Me.IntColumn,
        Me.IntColumnNull,
        Me.BigintColumn,
        Me.BigintColumnNull,
        Me.RealColumn,
        Me.RealColumnNull,
        Me.FloatColumn,
        Me.FloatColumnNull,
        Me.Numeric10and3Column,
        Me.Numeric10and3ColumnNull,
        Me.Numeric15and0Column,
        Me.Numeric15and0ColumnNull,
        Me.DateColumn,
        Me.DateColumnNull,
        Me.DatetimeColumn,
        Me.DatetimeColumnNull,
        Me.Varbinary50Column,
        Me.Varbinary50ColumnNull,
        Me.VarbinaryMaxColumn,
        Me.VarbinaryMaxColumnNull
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
