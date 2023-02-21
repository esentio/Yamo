Namespace Model

  Public MustInherit Class ActionHistoryBase
    Implements IInitializable
    Implements IHasDbPropertyModifiedTracking
    Implements ISupportDbLoad

    Private m_Modified As HashSet(Of String)

    Private m_History As List(Of ActionValue)

    Sub New()
      m_Modified = New HashSet(Of String)
      m_History = New List(Of ActionValue) From {ActionValue.Created}
    End Sub

    Public Sub Initialize() Implements IInitializable.Initialize
      m_History.Add(ActionValue.InitializeCalled)
    End Sub

    Protected Sub MarkDbPropertyAsModified(propertyName As String)
      m_Modified.Add(propertyName)
      m_History.Add(ActionValue.ModifyDbPropertyValue)
    End Sub

    Protected Sub MarkRelationshipPropertyAsSet(propertyName As String)
      m_Modified.Add(propertyName)
      m_History.Add(ActionValue.SetRelationshipProperty)
    End Sub

    Protected Sub MarkIncludePropertyAsSet(propertyName As String)
      m_Modified.Add(propertyName)
      m_History.Add(ActionValue.SetIncludeProperty)
    End Sub

    Public Function IsAnyDbPropertyModified() As Boolean Implements IHasDbPropertyModifiedTracking.IsAnyDbPropertyModified
      Return m_Modified.Any()
    End Function

    Public Function IsDbPropertyModified(propertyName As String) As Boolean Implements IHasDbPropertyModifiedTracking.IsDbPropertyModified
      Return m_Modified.Contains(propertyName)
    End Function

    Public Sub ResetDbPropertyModifiedTracking() Implements IHasDbPropertyModifiedTracking.ResetDbPropertyModifiedTracking
      m_Modified.Clear()
      m_History.Add(ActionValue.ResetDbPropertyModifiedTrackingCalled)
    End Sub

    Public Sub BeginLoad() Implements ISupportDbLoad.BeginLoad
      m_History.Add(ActionValue.BeginLoadCalled)
    End Sub

    Public Sub EndLoad() Implements ISupportDbLoad.EndLoad
      m_History.Add(ActionValue.EndLoadCalled)
    End Sub

    Public Function GetActionHistory() As ActionValue()
      Return m_History.ToArray()
    End Function

  End Class
End Namespace
