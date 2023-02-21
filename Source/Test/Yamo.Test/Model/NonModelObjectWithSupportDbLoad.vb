Namespace Model

  Public Class NonModelObjectWithSupportDbLoad
    Implements ISupportDbLoad

    Public Property IntValue As Int32

    Public Property StringValue As String

    Private m_Status As SupportDbLoadStatusValue = SupportDbLoadStatusValue.Created
    Public ReadOnly Property Status As SupportDbLoadStatusValue
      Get
        Return m_Status
      End Get
    End Property

    Public ReadOnly Property IsLoaded As Boolean
      Get
        Return Me.Status = SupportDbLoadStatusValue.Loaded
      End Get
    End Property

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot NonModelObjectWithInitialization Then
        Return False
      Else
        Dim o = DirectCast(obj, NonModelObjectWithInitialization)

        If Not Object.Equals(Me.IntValue, o.IntValue) Then Return False
        If Not Object.Equals(Me.StringValue, o.StringValue) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return HashCode.Combine(Me.IntValue, Me.StringValue)
    End Function

    Public Sub BeginLoad() Implements ISupportDbLoad.BeginLoad
      If Not m_Status = SupportDbLoadStatusValue.Created Then
        Throw New InvalidOperationException()
      End If

      m_Status = SupportDbLoadStatusValue.Loading
    End Sub

    Public Sub EndLoad() Implements ISupportDbLoad.EndLoad
      If Not m_Status = SupportDbLoadStatusValue.Loading Then
        Throw New InvalidOperationException()
      End If

      m_Status = SupportDbLoadStatusValue.Loaded
    End Sub

  End Class
End Namespace
