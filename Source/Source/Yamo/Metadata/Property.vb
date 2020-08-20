Namespace Metadata

  ''' <summary>
  ''' Represents a property of an entity.
  ''' </summary>
  Public Class [Property]

    ''' <summary>
    ''' Gets name of the property.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Name As String

    ''' <summary>
    ''' Gets type of the property.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property PropertyType As Type

    ''' <summary>
    ''' Gets or sets database column name related to this property.
    ''' </summary>
    ''' <returns></returns>
    Public Property ColumnName As String

    ''' <summary>
    ''' Gets or sets whether this property is part of the primary key of an entity.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsKey As Boolean

    ''' <summary>
    ''' Gets or sets whether related database column is an identity column.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsIdentity As Boolean

    ''' <summary>
    ''' Gets or sets whether related database column has a default value.
    ''' </summary>
    ''' <returns></returns>
    Public Property HasDefaultValue As Boolean

    ''' <summary>
    ''' Gets or sets whether this property must have a value assigned or whether <see langword="Nothing"/> is a valid value.<br/>
    ''' A property can only be configured as non-required if it is based on a CLR type that can be assigned <see langword="Nothing"/>.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsRequired As Boolean

    ''' <summary>
    ''' Gets property index.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Index() As Int32
      Get
        Return m_Index
      End Get
    End Property

    ''' <summary>
    ''' Gets an indication whether the value should be automatically set during the entity insert.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SetOnInsert As Boolean
      Get
        Return m_OnInsertFactory IsNot Nothing
      End Get
    End Property

    ''' <summary>
    ''' Gets an indication whether the value should be automatically set during the entity update.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SetOnUpdate As Boolean
      Get
        Return m_OnUpdateFactory IsNot Nothing
      End Get
    End Property

    ''' <summary>
    ''' Gets an indication whether the value should be automatically set during the entity soft delete.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SetOnDelete As Boolean
      Get
        Return m_OnDeleteFactory IsNot Nothing
      End Get
    End Property

    ''' <summary>
    ''' Stores property index.
    ''' </summary>
    Private m_Index As Int32

    ''' <summary>
    ''' Stores on insert value factory method.
    ''' </summary>
    Private m_OnInsertFactory As Object

    ''' <summary>
    ''' Stores on update value factory method.
    ''' </summary>
    Private m_OnUpdateFactory As Object

    ''' <summary>
    ''' Stores on soft delete value factory method.
    ''' </summary>
    Private m_OnDeleteFactory As Object

    ''' <summary>
    ''' Creates new instance of <see cref="[Property]"/>.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="propertyType"></param>
    Sub New(name As String, propertyType As Type)
      Me.Name = name
      Me.PropertyType = propertyType
      Me.ColumnName = name
      Me.IsKey = False
      Me.IsIdentity = False
      Me.HasDefaultValue = False
      Me.IsRequired = GetIsRequiredDefault(propertyType)
      m_Index = -1
      m_OnInsertFactory = Nothing
      m_OnUpdateFactory = Nothing
      m_OnDeleteFactory = Nothing
    End Sub

    ''' <summary>
    ''' Gets default value for <see cref="IsRequired"/> property.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Sets <see cref="IsRequired"/> property if allowed.
    ''' </summary>
    Friend Sub SetIsRequired()
      If Me.PropertyType Is GetType(String) Then
        Me.IsRequired = True
      ElseIf Me.PropertyType Is GetType(Byte()) Then
        Me.IsRequired = True
      Else
        ' ignore otherwise
      End If
    End Sub

    ''' <summary>
    ''' Sets property index.
    ''' </summary>
    ''' <param name="index"></param>
    Friend Sub SetIndex(index As Int32)
      m_Index = index
    End Sub

    ''' <summary>
    ''' Sets on insert value factory method.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="factory"></param>
    Friend Sub SetOnInsertFactory(Of TResult)(factory As Func(Of TResult))
      m_OnInsertFactory = factory
    End Sub

    ''' <summary>
    ''' Sets on insert value factory method.
    ''' </summary>
    ''' <typeparam name="TContext"></typeparam>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="factory"></param>
    Friend Sub SetOnInsertFactory(Of TContext As DbContext, TResult)(factory As Func(Of TContext, TResult))
      m_OnInsertFactory = factory
    End Sub

    ''' <summary>
    ''' Gets on insert value factory method.
    ''' </summary>
    ''' <returns></returns>
    Friend Function GetOnInsertFactory() As Object
      Return m_OnInsertFactory
    End Function

    ''' <summary>
    ''' Sets on update value factory method.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="factory"></param>
    Friend Sub SetOnUpdateFactory(Of TResult)(factory As Func(Of TResult))
      m_OnUpdateFactory = factory
    End Sub

    ''' <summary>
    ''' Sets on update value factory method.
    ''' </summary>
    ''' <typeparam name="TContext"></typeparam>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="factory"></param>
    Friend Sub SetOnUpdateFactory(Of TContext As DbContext, TResult)(factory As Func(Of TContext, TResult))
      m_OnUpdateFactory = factory
    End Sub

    ''' <summary>
    ''' Gets on update value factory method.
    ''' </summary>
    ''' <returns></returns>
    Friend Function GetOnUpdateFactory() As Object
      Return m_OnUpdateFactory
    End Function

    ''' <summary>
    ''' Sets on soft delete value factory method.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="factory"></param>
    Friend Sub SetOnDeleteFactory(Of TResult)(factory As Func(Of TResult))
      m_OnDeleteFactory = factory
    End Sub

    ''' <summary>
    ''' Sets on soft delete value factory method.
    ''' </summary>
    ''' <typeparam name="TContext"></typeparam>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="factory"></param>
    Friend Sub SetOnDeleteFactory(Of TContext As DbContext, TResult)(factory As Func(Of TContext, TResult))
      m_OnDeleteFactory = factory
    End Sub

    ''' <summary>
    ''' Gets on soft delete value factory method.
    ''' </summary>
    ''' <returns></returns>
    Friend Function GetOnDeleteFactory() As Object
      Return m_OnDeleteFactory
    End Function

  End Class
End Namespace