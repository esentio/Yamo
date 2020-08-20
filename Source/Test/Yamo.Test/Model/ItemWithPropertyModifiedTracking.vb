Namespace Model

  Public Class ItemWithPropertyModifiedTracking
    Inherits PropertyModifiedTrackingBase

    Private m_Id As Int32
    Public Property Id() As Int32
      Get
        Return m_Id
      End Get
      Set(ByVal value As Int32)
        If Not m_Id = value Then
          m_Id = value
          MarkPropertyAsModified(NameOf(Me.Id))
        End If
      End Set
    End Property

    Private m_Description As String
    Public Property Description() As String
      Get
        Return m_Description
      End Get
      Set(ByVal value As String)
        If Not String.Equals(m_Description, value) Then
          m_Description = value
          MarkPropertyAsModified(NameOf(Me.Description))
        End If
      End Set
    End Property

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

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot ItemWithPropertyModifiedTracking Then
        Return False
      Else
        Dim o = DirectCast(obj, ItemWithPropertyModifiedTracking)

        If Not Object.Equals(Me.Id, o.Id) Then Return False
        If Not Object.Equals(Me.Description, o.Description) Then Return False
        If Not Object.Equals(Me.IntValue, o.IntValue) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return HashCode.Combine(Me.Id, Me.Description, Me.IntValue)
    End Function

  End Class
End Namespace
