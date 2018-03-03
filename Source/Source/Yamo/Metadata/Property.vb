Namespace Metadata

  Public Class [Property]

    Public ReadOnly Property Name As String

    Public ReadOnly Property PropertyType As Type

    Public Property ColumnName As String

    Public Property IsKey As Boolean

    Public Property IsIdentity As Boolean

    Public Property HasDefaultValue As Boolean

    Public Property IsRequired As Boolean

    Public ReadOnly Property SetOnInsert As Boolean
      Get
        Return m_OnInsertFactory IsNot Nothing
      End Get
    End Property

    Public ReadOnly Property SetOnUpdate As Boolean
      Get
        Return m_OnUpdateFactory IsNot Nothing
      End Get
    End Property

    Public ReadOnly Property SetOnDelete As Boolean
      Get
        Return m_OnDeleteFactory IsNot Nothing
      End Get
    End Property

    Private m_OnInsertFactory As Object

    Private m_OnUpdateFactory As Object

    Private m_OnDeleteFactory As Object

    Sub New(name As String, propertyType As Type)
      Me.Name = name
      Me.PropertyType = propertyType
      Me.ColumnName = name
      Me.IsKey = False
      Me.IsIdentity = False
      Me.HasDefaultValue = False
      Me.IsRequired = GetIsRequiredDefault(propertyType)
      m_OnInsertFactory = Nothing
      m_OnUpdateFactory = Nothing
      m_OnDeleteFactory = Nothing
    End Sub

    Private Function GetIsRequiredDefault(type As Type) As Boolean
      If type Is GetType(String) Then
        Return False
      ElseIf type Is GetType(Byte()) Then
        Return False
      ElseIf Nullable.GetUnderlyingType(type) Is Nothing Then
        Return True
      Else
        Return False
      End If
    End Function

    Friend Sub SetIsRequired()
      If Me.PropertyType Is GetType(String) Then
        Me.IsRequired = True
      ElseIf Me.PropertyType Is GetType(Byte()) Then
        Me.IsRequired = True
      Else
        ' ignore otherwise
      End If
    End Sub

    Friend Sub SetOnInsertFactory(Of TResult)(factory As Func(Of TResult))
      m_OnInsertFactory = factory
    End Sub

    Friend Sub SetOnInsertFactory(Of TContext As DbContext, TResult)(factory As Func(Of TContext, TResult))
      m_OnInsertFactory = factory
    End Sub

    Friend Function GetOnInsertFactory() As Object
      Return m_OnInsertFactory
    End Function

    Friend Sub SetOnUpdateFactory(Of TResult)(factory As Func(Of TResult))
      m_OnUpdateFactory = factory
    End Sub

    Friend Sub SetOnUpdateFactory(Of TContext As DbContext, TResult)(factory As Func(Of TContext, TResult))
      m_OnUpdateFactory = factory
    End Sub

    Friend Function GetOnUpdateFactory() As Object
      Return m_OnUpdateFactory
    End Function

    Friend Sub SetOnDeleteFactory(Of TResult)(factory As Func(Of TResult))
      m_OnDeleteFactory = factory
    End Sub

    Friend Sub SetOnDeleteFactory(Of TContext As DbContext, TResult)(factory As Func(Of TContext, TResult))
      m_OnDeleteFactory = factory
    End Sub

    Friend Function GetOnDeleteFactory() As Object
      Return m_OnDeleteFactory
    End Function

  End Class
End Namespace