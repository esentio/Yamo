Namespace Model

  Public Class NonModelObjectWithActionHistory
    Inherits ActionHistoryBase

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

    Private m_StringValue As String
    Public Property StringValue() As String
      Get
        Return m_StringValue
      End Get
      Set(ByVal value As String)
        If Not String.Equals(m_StringValue, value) Then
          m_StringValue = value
          MarkDbPropertyAsModified(NameOf(Me.StringValue))
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

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot NonModelObjectWithActionHistory Then
        Return False
      Else
        Dim o = DirectCast(obj, NonModelObjectWithActionHistory)

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