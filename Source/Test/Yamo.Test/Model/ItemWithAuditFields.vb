Namespace Model

  Public Class ItemWithAuditFields
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

    Private m_Created As DateTime
    Public Property Created() As DateTime
      Get
        Return m_Created
      End Get
      Set(ByVal value As DateTime)
        If Not m_Created = value Then
          m_Created = value
          MarkPropertyAsModified(NameOf(Me.Created))
        End If
      End Set
    End Property

    Private m_CreatedUserId As Int32
    Public Property CreatedUserId() As Int32
      Get
        Return m_CreatedUserId
      End Get
      Set(ByVal value As Int32)
        If Not m_CreatedUserId = value Then
          m_CreatedUserId = value
          MarkPropertyAsModified(NameOf(Me.CreatedUserId))
        End If
      End Set
    End Property

    Private m_Modified As DateTime?
    Public Property Modified() As DateTime?
      Get
        Return m_Modified
      End Get
      Set(ByVal value As DateTime?)
        If Not Object.Equals(m_Modified, value) Then
          m_Modified = value
          MarkPropertyAsModified(NameOf(Me.Modified))
        End If
      End Set
    End Property

    Private m_ModifiedUserId As Int32?
    Public Property ModifiedUserId() As Int32?
      Get
        Return m_ModifiedUserId
      End Get
      Set(ByVal value As Int32?)
        If Not Object.Equals(m_ModifiedUserId, value) Then
          m_ModifiedUserId = value
          MarkPropertyAsModified(NameOf(Me.ModifiedUserId))
        End If
      End Set
    End Property

    Private m_Deleted As DateTime?
    Public Property Deleted() As DateTime?
      Get
        Return m_Deleted
      End Get
      Set(ByVal value As DateTime?)
        If Not Object.Equals(m_Deleted, value) Then
          m_Deleted = value
          MarkPropertyAsModified(NameOf(Me.Deleted))
        End If
      End Set
    End Property

    Private m_DeletedUserId As Int32? = Nothing
    Public Property DeletedUserId() As Int32?
      Get
        Return m_DeletedUserId
      End Get
      Set(ByVal value As Int32?)
        If Not Object.Equals(m_DeletedUserId, value) Then
          m_DeletedUserId = value
          MarkPropertyAsModified(NameOf(Me.DeletedUserId))
        End If
      End Set
    End Property

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot ItemWithAuditFields Then
        Return False
      Else
        Dim o = DirectCast(obj, ItemWithAuditFields)

        If Not Object.Equals(Me.Id, o.Id) Then Return False
        If Not Object.Equals(Me.Description, o.Description) Then Return False
        If Not Object.Equals(Me.Created, o.Created) Then Return False
        If Not Object.Equals(Me.CreatedUserId, o.CreatedUserId) Then Return False
        If Not Object.Equals(Me.Modified, o.Modified) Then Return False
        If Not Object.Equals(Me.ModifiedUserId, o.ModifiedUserId) Then Return False
        If Not Object.Equals(Me.Deleted, o.Deleted) Then Return False
        If Not Object.Equals(Me.DeletedUserId, o.DeletedUserId) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return Helpers.Compare.GetHashCode(Me.Id, Me.Description, Me.Created, Me.CreatedUserId, Me.Modified, Me.ModifiedUserId, Me.Deleted, Me.DeletedUserId)
    End Function

  End Class
End Namespace
