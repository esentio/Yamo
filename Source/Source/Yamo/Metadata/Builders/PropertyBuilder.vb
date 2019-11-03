Namespace Metadata.Builders

  ''' <summary>
  ''' Provides an API for configuring a <see cref="[Property]"/>.
  ''' </summary>
  ''' <typeparam name="TProperty"></typeparam>
  Public Class PropertyBuilder(Of TProperty)

    ''' <summary>
    ''' Stores related entity.
    ''' </summary>
    Private m_Entity As Entity

    ''' <summary>
    ''' Stores related property.
    ''' </summary>
    Private m_Property As [Property]

    ''' <summary>
    ''' Creates new instance of <see cref="PropertyBuilder(Of TProperty)"/>.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <param name="name"></param>
    Sub New(entity As Entity, name As String)
      m_Entity = entity
      m_Property = m_Entity.AddProperty(name, GetType(TProperty))
    End Sub

    ''' <summary>
    ''' Configures the database column name that the property maps to.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    Public Function HasColumnName(name As String) As PropertyBuilder(Of TProperty)
      m_Property.ColumnName = name
      Return Me
    End Function

    ''' <summary>
    ''' Configures whether this property is part of the primary key of an entity.
    ''' </summary>
    ''' <returns></returns>
    Public Function IsKey() As PropertyBuilder(Of TProperty)
      m_Property.IsKey = True
      Return Me
    End Function

    ''' <summary>
    ''' Configures whether related database column is an identity column.
    ''' </summary>
    ''' <returns></returns>
    Public Function IsIdentity() As PropertyBuilder(Of TProperty)
      m_Property.IsIdentity = True
      Return Me
    End Function

    ''' <summary>
    ''' Configures whether related database column has a default value.
    ''' </summary>
    ''' <returns></returns>
    Public Function HasDefaultValue() As PropertyBuilder(Of TProperty)
      m_Property.HasDefaultValue = True
      Return Me
    End Function

    ''' <summary>
    ''' Configures whether this property must have a value assigned or whether <see langword="Nothing"/> is a valid value.<br/>
    ''' A property can only be configured as non-required if it is based on a CLR type that can be assigned <see langword="Nothing"/>.
    ''' </summary>
    ''' <returns></returns>
    Public Function IsRequired() As PropertyBuilder(Of TProperty)
      m_Property.SetIsRequired()
      Return Me
    End Function

    ''' <summary>
    ''' Configures on insert value factory method.
    ''' </summary>
    ''' <param name="factory"></param>
    ''' <returns></returns>
    Public Function SetOnInsertTo(factory As Func(Of TProperty)) As PropertyBuilder(Of TProperty)
      m_Property.SetOnInsertFactory(factory)
      Return Me
    End Function

    ''' <summary>
    ''' Configures on insert value factory method.
    ''' </summary>
    ''' <typeparam name="TContext"></typeparam>
    ''' <param name="factory"></param>
    ''' <returns></returns>
    Public Function SetOnInsertTo(Of TContext As DbContext)(factory As Func(Of TContext, TProperty)) As PropertyBuilder(Of TProperty)
      m_Property.SetOnInsertFactory(factory)
      Return Me
    End Function

    ''' <summary>
    ''' Configures on update value factory method.
    ''' </summary>
    ''' <param name="factory"></param>
    ''' <returns></returns>
    Public Function SetOnUpdateTo(factory As Func(Of TProperty)) As PropertyBuilder(Of TProperty)
      m_Property.SetOnUpdateFactory(factory)
      Return Me
    End Function

    ''' <summary>
    ''' Configures on update value factory method.
    ''' </summary>
    ''' <typeparam name="TContext"></typeparam>
    ''' <param name="factory"></param>
    ''' <returns></returns>
    Public Function SetOnUpdateTo(Of TContext As DbContext)(factory As Func(Of TContext, TProperty)) As PropertyBuilder(Of TProperty)
      m_Property.SetOnUpdateFactory(factory)
      Return Me
    End Function

    ''' <summary>
    ''' Configures on soft delete value factory method.
    ''' </summary>
    ''' <param name="factory"></param>
    ''' <returns></returns>
    Public Function SetOnDeleteTo(factory As Func(Of TProperty)) As PropertyBuilder(Of TProperty)
      m_Property.SetOnDeleteFactory(factory)
      Return Me
    End Function

    ''' <summary>
    ''' Configures on soft delete value factory method.
    ''' </summary>
    ''' <typeparam name="TContext"></typeparam>
    ''' <param name="factory"></param>
    ''' <returns></returns>
    Public Function SetOnDeleteTo(Of TContext As DbContext)(factory As Func(Of TContext, TProperty)) As PropertyBuilder(Of TProperty)
      m_Property.SetOnDeleteFactory(factory)
      Return Me
    End Function

  End Class
End Namespace