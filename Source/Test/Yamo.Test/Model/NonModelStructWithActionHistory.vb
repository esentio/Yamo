Namespace Model

  Public Structure NonModelStructWithActionHistory
    Implements IInitializable
    Implements IHasDbPropertyModifiedTracking
    Implements ISupportDbLoad

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

    Private m_RelatedItem As Object
    Public Property RelatedItem() As Object
      Get
        Return m_RelatedItem
      End Get
      Set(ByVal value As Object)
        m_RelatedItem = value
        MarkRelationshipPropertyAsSet(NameOf(Me.RelatedItem))
      End Set
    End Property

    Private m_RelatedItems As List(Of Object)
    Public Property RelatedItems() As List(Of Object)
      Get
        Return m_RelatedItems
      End Get
      Set(ByVal value As List(Of Object))
        m_RelatedItems = value
        MarkRelationshipPropertyAsSet(NameOf(Me.RelatedItems))
      End Set
    End Property

    Private m_Modified As HashSet(Of String)

    Private m_History As List(Of ActionValue)

    Private Sub EnsureInitialization()
      If m_Modified Is Nothing Then
        m_Modified = New HashSet(Of String)
        m_History = New List(Of ActionValue) From {ActionValue.Created}
      End If
    End Sub

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot NonModelStructWithActionHistory Then
        Return False
      Else
        Dim o = DirectCast(obj, NonModelStructWithActionHistory)

        If Not Me.IntValue = o.IntValue Then Return False
        If Not Me.StringValue = o.StringValue Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return HashCode.Combine(Me.IntValue, Me.StringValue)
    End Function

    Public Sub Initialize() Implements IInitializable.Initialize
      EnsureInitialization()
      m_History.Add(ActionValue.InitializeCalled)
    End Sub

    Private Sub MarkDbPropertyAsModified(propertyName As String)
      EnsureInitialization()
      m_Modified.Add(propertyName)
      m_History.Add(ActionValue.ModifyDbPropertyValue)
    End Sub

    Private Sub MarkRelationshipPropertyAsSet(propertyName As String)
      EnsureInitialization()
      m_Modified.Add(propertyName)
      m_History.Add(ActionValue.SetRelationshipProperty)
    End Sub

    Private Sub MarkIncludePropertyAsSet(propertyName As String)
      EnsureInitialization()
      m_Modified.Add(propertyName)
      m_History.Add(ActionValue.SetIncludeProperty)
    End Sub

    Public Function IsAnyDbPropertyModified() As Boolean Implements IHasDbPropertyModifiedTracking.IsAnyDbPropertyModified
      EnsureInitialization()
      Return m_Modified.Any()
    End Function

    Public Function IsDbPropertyModified(propertyName As String) As Boolean Implements IHasDbPropertyModifiedTracking.IsDbPropertyModified
      EnsureInitialization()
      Return m_Modified.Contains(propertyName)
    End Function

    Public Sub ResetDbPropertyModifiedTracking() Implements IHasDbPropertyModifiedTracking.ResetDbPropertyModifiedTracking
      EnsureInitialization()
      m_Modified.Clear()
      m_History.Add(ActionValue.ResetDbPropertyModifiedTrackingCalled)
    End Sub

    Public Sub BeginLoad() Implements ISupportDbLoad.BeginLoad
      EnsureInitialization()
      m_History.Add(ActionValue.BeginLoadCalled)
    End Sub

    Public Sub EndLoad() Implements ISupportDbLoad.EndLoad
      EnsureInitialization()
      m_History.Add(ActionValue.EndLoadCalled)
    End Sub

    Public Function GetActionHistory() As ActionValue()
      EnsureInitialization()
      Return m_History.ToArray()
    End Function

  End Structure
End Namespace
