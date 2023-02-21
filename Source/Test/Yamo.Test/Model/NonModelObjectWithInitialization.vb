Namespace Model

  Public Class NonModelObjectWithInitialization
    Implements IInitializable

    Public Property IntValue As Int32

    Public Property StringValue As String

    Private m_IsInitialized As Boolean = False
    Public ReadOnly Property IsInitialized As Boolean
      Get
        Return m_IsInitialized
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

    Public Sub Initialize() Implements IInitializable.Initialize
      m_IsInitialized = True
    End Sub

  End Class
End Namespace
