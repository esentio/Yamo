Namespace Metadata

  ''' <summary>
  ''' Represents an entity in database model.
  ''' </summary>
  Public Class Entity

    ''' <summary>
    ''' Stores properties metadata by their property names.
    ''' </summary>
    Private m_PropertiesDictionary As Dictionary(Of String, [Property])

    ''' <summary>
    ''' Stores properties metadata.
    ''' </summary>
    Private m_Properties As List(Of [Property])

    ''' <summary>
    ''' Stores relation navigations by name of the properties they are defined on.
    ''' </summary>
    Private m_RelationshipNavigations As Dictionary(Of String, RelationshipNavigation)

    ''' <summary>
    ''' Gets type of model representing this entity.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property EntityType As Type

    ''' <summary>
    ''' Gets or sets table name of the entity.
    ''' </summary>
    ''' <returns></returns>
    Public Property TableName As String

    ''' <summary>
    ''' Gets or sets schema of the entity.
    ''' </summary>
    ''' <returns></returns>
    Public Property Schema As String

    ''' <summary>
    ''' Creates new instance of <see cref="Entity"/>.
    ''' </summary>
    ''' <param name="entityType"></param>
    Sub New(entityType As Type)
      m_PropertiesDictionary = New Dictionary(Of String, [Property])
      m_Properties = New List(Of [Property])
      m_RelationshipNavigations = New Dictionary(Of String, RelationshipNavigation)
      Me.EntityType = entityType
      Me.TableName = entityType.Name
      Me.Schema = ""
    End Sub

    ''' <summary>
    ''' Adds metadata about property of this entity.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="propertyType"></param>
    ''' <returns></returns>
    Friend Function AddProperty(name As String, propertyType As Type) As [Property]
      Dim prop As [Property] = Nothing

      If Not m_PropertiesDictionary.TryGetValue(name, prop) Then
        prop = New [Property](name, propertyType)
        m_PropertiesDictionary.Add(name, prop)
        m_Properties.Add(prop)
      End If

      Return prop
    End Function

    ''' <summary>
    ''' Gets property of this entity.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    Public Function GetProperty(name As String) As [Property]
      Return m_PropertiesDictionary(name)
    End Function

    ''' <summary>
    ''' Gets property of this entity together with the index of the property.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    Public Function GetPropertyWithIndex(name As String) As ([Property] As [Property], Index As Int32)
      For i = 0 To m_Properties.Count - 1
        If m_Properties(i).Name = name Then
          Return (m_Properties(i), i)
        End If
      Next

      Throw New Exception($"Unknown property '{name}'.")
    End Function

    ''' <summary>
    ''' Gets all properties defined on this entity.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetProperties() As List(Of [Property])
      Return m_Properties.ToList()
    End Function

    ''' <summary>
    ''' Gets all primary key properties defined on this entity with their indexes.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetKeyProperties() As List(Of ([Property] As [Property], Index As Int32))
      Return m_Properties.Select(Function(p, i) (p, i)).Where(Function(t) t.Item1.IsKey).ToList()
    End Function

    ''' <summary>
    ''' Gets all non-primary key properties defined on this entity with their indexes.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetNonKeyProperties() As List(Of ([Property] As [Property], Index As Int32))
      Return m_Properties.Select(Function(p, i) (p, i)).Where(Function(t) Not t.Item1.IsKey).ToList()
    End Function

    ''' <summary>
    ''' Gets all properties marked as indentity or having default value defined on this entity with their indexes.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetIdentityOrDefaultValueProperties() As List(Of ([Property] As [Property], Index As Int32))
      Return m_Properties.Select(Function(p, i) (p, i)).Where(Function(t) t.Item1.IsIdentity OrElse t.Item1.HasDefaultValue).ToList()
    End Function

    ''' <summary>
    ''' Get count of properties defined on this entity.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetPropertiesCount() As Int32
      Return m_Properties.Count
    End Function

    ''' <summary>
    ''' Get count of primary key properties defined on this entity.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetKeyPropertiesCount() As Int32
      Return m_Properties.Where(Function(p) p.IsKey).Count()
    End Function

    ''' <summary>
    ''' Adds metadata about reference navigation between this and related entity.
    ''' </summary>
    ''' <param name="propertyName"></param>
    ''' <param name="relatedEntityType"></param>
    ''' <returns></returns>
    Friend Function AddReferenceNavigation(propertyName As String, relatedEntityType As Type) As ReferenceNavigation
      Dim navigation As RelationshipNavigation = Nothing

      If m_RelationshipNavigations.TryGetValue(propertyName, navigation) Then
        If TypeOf navigation IsNot ReferenceNavigation Then
          Throw New Exception($"Cannot add reference navigation. Property '{propertyName}' is already assigned for different type of navigation.")
        End If
      Else
        navigation = New ReferenceNavigation(propertyName, relatedEntityType)
        m_RelationshipNavigations.Add(propertyName, navigation)
      End If

      Return DirectCast(navigation, ReferenceNavigation)
    End Function

    ''' <summary>
    ''' Adds metadata about collection navigation between this and related entity.
    ''' </summary>
    ''' <param name="propertyName"></param>
    ''' <param name="relatedEntityType"></param>
    ''' <param name="collectionType"></param>
    ''' <returns></returns>
    Friend Function AddCollectionNavigation(propertyName As String, relatedEntityType As Type, collectionType As Type) As CollectionNavigation
      Dim navigation As RelationshipNavigation = Nothing

      If m_RelationshipNavigations.TryGetValue(propertyName, navigation) Then
        If TypeOf navigation IsNot CollectionNavigation Then
          Throw New Exception($"Cannot add collection navigation. Property '{propertyName}' is already assigned for different type of navigation.")
        End If
      Else
        navigation = New CollectionNavigation(propertyName, relatedEntityType, collectionType)
        m_RelationshipNavigations.Add(propertyName, navigation)
      End If

      Return DirectCast(navigation, CollectionNavigation)
    End Function

    ''' <summary>
    ''' Gets all relationship navigations defined on this entity with an entity of a given type.
    ''' </summary>
    ''' <param name="relatedEntityType"></param>
    ''' <returns></returns>
    Public Function GetRelationshipNavigations(relatedEntityType As Type) As IEnumerable(Of RelationshipNavigation)
      Return m_RelationshipNavigations.Values.Where(Function(r) r.RelatedEntityType = relatedEntityType).ToArray()
    End Function

    ''' <summary>
    ''' Gets the relationship navigation defined on this entity.
    ''' </summary>
    ''' <param name="propertyName"></param>
    ''' <returns></returns>
    Public Function GetRelationshipNavigation(propertyName As String) As RelationshipNavigation
      Return m_RelationshipNavigations(propertyName)
    End Function

  End Class
End Namespace
