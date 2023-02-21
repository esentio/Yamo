Namespace Model

  Public Class ItemWithInitialization
    Implements IInitializable

    Public Property Id As Int32

    Public Property Description As String

    Private m_IsInitialized As Boolean = False
    Public ReadOnly Property IsInitialized As Boolean
      Get
        Return m_IsInitialized
      End Get
    End Property

    Sub New()
    End Sub

    Sub New(id As Int32)
      Me.Id = id
    End Sub

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot ItemWithInitialization Then
        Return False
      Else
        Dim o = DirectCast(obj, ItemWithInitialization)

        If Not Object.Equals(Me.Id, o.Id) Then Return False
        If Not Object.Equals(Me.Description, o.Description) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return HashCode.Combine(Me.Id, Me.Description)
    End Function

    Public Sub Initialize() Implements IInitializable.Initialize
      If m_IsInitialized Then
        Throw New InvalidOperationException("Already initialized.")
      End If

      m_IsInitialized = True
    End Sub

  End Class
End Namespace
