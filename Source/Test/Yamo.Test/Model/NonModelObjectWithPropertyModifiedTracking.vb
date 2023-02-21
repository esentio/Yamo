Namespace Model

  Public Class NonModelObjectWithPropertyModifiedTracking
    Inherits PropertyModifiedTrackingBase

    Private m_IntValue As Int32
    Public Property IntValue() As Int32
      Get
        Return m_IntValue
      End Get
      Set(ByVal value As Int32)
        If Not m_IntValue = value Then
          m_IntValue = value
          MarkPropertyAsModified(NameOf(Me.IntValue))
        End If
      End Set
    End Property

    Private m_StringValue As String
    Public Property StringValue() As String
      Get
        Return m_StringValue
      End Get
      Set(ByVal value As String)
        If Not String.Equals(m_StringValue, value) Then
          m_StringValue = value
          MarkPropertyAsModified(NameOf(Me.StringValue))
        End If
      End Set
    End Property

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot NonModelObjectWithPropertyModifiedTracking Then
        Return False
      Else
        Dim o = DirectCast(obj, NonModelObjectWithPropertyModifiedTracking)

        If Not Object.Equals(Me.IntValue, o.IntValue) Then Return False
        If Not Object.Equals(Me.StringValue, o.StringValue) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return HashCode.Combine(Me.IntValue, Me.StringValue)
    End Function

  End Class
End Namespace