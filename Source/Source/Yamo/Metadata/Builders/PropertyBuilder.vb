Namespace Metadata.Builders

  Public Class PropertyBuilder(Of TProperty)

    Private m_Entity As Entity

    Private m_Property As [Property]

    Sub New(entity As Entity, name As String)
      m_Entity = entity
      m_Property = m_Entity.AddProperty(name, GetType(TProperty))
    End Sub

    Public Function HasColumnName(name As String) As PropertyBuilder(Of TProperty)
      m_Property.ColumnName = name
      Return Me
    End Function

    Public Function IsKey() As PropertyBuilder(Of TProperty)
      m_Property.IsKey = True
      Return Me
    End Function

    Public Function IsIdentity() As PropertyBuilder(Of TProperty)
      m_Property.IsIdentity = True
      Return Me
    End Function

    Public Function HasDefaultValue() As PropertyBuilder(Of TProperty)
      m_Property.HasDefaultValue = True
      Return Me
    End Function

    Public Function IsRequired() As PropertyBuilder(Of TProperty)
      m_Property.SetIsRequired()
      Return Me
    End Function

    Public Function SetOnInsertTo(factory As Func(Of TProperty)) As PropertyBuilder(Of TProperty)
      m_Property.SetOnInsertFactory(factory)
      Return Me
    End Function

    Public Function SetOnInsertTo(Of TContext As DbContext)(factory As Func(Of TContext, TProperty)) As PropertyBuilder(Of TProperty)
      m_Property.SetOnInsertFactory(factory)
      Return Me
    End Function

    Public Function SetOnUpdateTo(factory As Func(Of TProperty)) As PropertyBuilder(Of TProperty)
      m_Property.SetOnUpdateFactory(factory)
      Return Me
    End Function

    Public Function SetOnUpdateTo(Of TContext As DbContext)(factory As Func(Of TContext, TProperty)) As PropertyBuilder(Of TProperty)
      m_Property.SetOnUpdateFactory(factory)
      Return Me
    End Function

    Public Function SetOnDeleteTo(factory As Func(Of TProperty)) As PropertyBuilder(Of TProperty)
      m_Property.SetOnDeleteFactory(factory)
      Return Me
    End Function

    Public Function SetOnDeleteTo(Of TContext As DbContext)(factory As Func(Of TContext, TProperty)) As PropertyBuilder(Of TProperty)
      m_Property.SetOnDeleteFactory(factory)
      Return Me
    End Function

  End Class
End Namespace