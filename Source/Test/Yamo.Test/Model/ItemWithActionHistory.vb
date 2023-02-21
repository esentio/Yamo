Namespace Model

  Public Class ItemWithActionHistory
    Inherits ActionHistoryBase

    Private m_Id As Int32
    Public Property Id() As Int32
      Get
        Return m_Id
      End Get
      Set(ByVal value As Int32)
        If Not m_Id = value Then
          m_Id = value
          MarkDbPropertyAsModified(NameOf(Me.Id))
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
          MarkDbPropertyAsModified(NameOf(Me.Description))
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
          MarkDbPropertyAsModified(NameOf(Me.IntValue))
        End If
      End Set
    End Property

    Private m_IncludedValue As Int32
    Public Property IncludedValue() As Int32
      Get
        Return m_IncludedValue
      End Get
      Set(ByVal value As Int32)
        m_IncludedValue = value
        MarkIncludePropertyAsSet(NameOf(Me.IncludedValue))
      End Set
    End Property

    Private m_RelatedItem As Object = Nothing
    Public Property RelatedItem() As Object
      Get
        Return m_RelatedItem
      End Get
      Set(ByVal value As Object)
        m_RelatedItem = value
        MarkRelationshipPropertyAsSet(NameOf(Me.RelatedItem))
      End Set
    End Property

    Private m_RelatedItems As List(Of Object) = Nothing
    Public Property RelatedItems() As List(Of Object)
      Get
        Return m_RelatedItems
      End Get
      Set(ByVal value As List(Of Object))
        m_RelatedItems = value
        MarkRelationshipPropertyAsSet(NameOf(Me.RelatedItems))
      End Set
    End Property

    Sub New()
    End Sub

    Sub New(id As Int32)
      m_Id = id
    End Sub

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot ItemWithActionHistory Then
        Return False
      Else
        Dim o = DirectCast(obj, ItemWithActionHistory)

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
