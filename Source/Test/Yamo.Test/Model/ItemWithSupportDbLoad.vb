Namespace Model

  Public Class ItemWithSupportDbLoad
    Implements ISupportDbLoad

    Public Property Id As Int32

    Public Property Description As String

    Public Property IntValue As Int32

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

    Sub New()
    End Sub

    Sub New(id As Int32)
      Me.Id = id
    End Sub

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot ItemWithSupportDbLoad Then
        Return False
      Else
        Dim o = DirectCast(obj, ItemWithSupportDbLoad)

        If Not Object.Equals(Me.Id, o.Id) Then Return False
        If Not Object.Equals(Me.Description, o.Description) Then Return False
        If Not Object.Equals(Me.IntValue, o.IntValue) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return HashCode.Combine(Me.Id, Me.Description, Me.IntValue)
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
