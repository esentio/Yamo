Namespace Metadata

  Public Class Entity

    Private m_PropertiesDictionary As Dictionary(Of String, [Property])

    Private m_Properties As List(Of [Property])

    Private m_RelationshipNavigations As Dictionary(Of String, RelationshipNavigation)

    Public ReadOnly Property EntityType As Type

    Public Property TableName As String

    Sub New(entityType As Type)
      m_PropertiesDictionary = New Dictionary(Of String, [Property])
      m_Properties = New List(Of [Property])
      m_RelationshipNavigations = New Dictionary(Of String, RelationshipNavigation)
      Me.EntityType = entityType
      Me.TableName = entityType.Name
    End Sub

    Friend Function AddProperty(name As String, propertyType As Type) As [Property]
      If m_PropertiesDictionary.ContainsKey(name) Then
        Return m_PropertiesDictionary(name)
      Else
        Dim [property] = New [Property](name, propertyType)
        m_PropertiesDictionary.Add(name, [property])
        m_Properties.Add([property])
        Return [property]
      End If
    End Function

    Public Function GetProperty(name As String) As [Property]
      Return m_PropertiesDictionary(name)
    End Function

    Public Function GetPropertyWithIndex(name As String) As ([Property] As [Property], Index As Int32)
      For i = 0 To m_Properties.Count - 1
        If m_Properties(i).Name = name Then
          Return (m_Properties(i), i)
        End If
      Next

      Throw New Exception($"Unknown property '{name}'.")
    End Function

    Public Function GetProperties() As List(Of [Property])
      Return m_Properties.ToList()
    End Function

    Public Function GetKeyProperties() As List(Of ([Property] As [Property], Index As Int32))
      Return m_Properties.Select(Function(p, i) (p, i)).Where(Function(t) t.Item1.IsKey).ToList()
    End Function

    Public Function GetNonKeyProperties() As List(Of ([Property] As [Property], Index As Int32))
      Return m_Properties.Select(Function(p, i) (p, i)).Where(Function(t) Not t.Item1.IsKey).ToList()
    End Function

    Public Function GetIdentityOrDefaultValueProperties() As List(Of ([Property] As [Property], Index As Int32))
      Return m_Properties.Select(Function(p, i) (p, i)).Where(Function(t) t.Item1.IsIdentity OrElse t.Item1.HasDefaultValue).ToList()
    End Function

    Public Function GetPropertiesCount() As Int32
      Return m_Properties.Count
    End Function

    Public Function GetKeyPropertiesCount() As Int32
      Return m_Properties.Where(Function(p) p.IsKey).Count()
    End Function

    Friend Function AddReferenceNavigation(propertyName As String, relatedEntityType As Type) As ReferenceNavigation
      If m_RelationshipNavigations.ContainsKey(propertyName) Then
        If TypeOf m_RelationshipNavigations(propertyName) Is ReferenceNavigation Then
          Return DirectCast(m_RelationshipNavigations(propertyName), ReferenceNavigation)
        Else
          Throw New Exception($"Cannot add reference navigation. Property '{propertyName}' is already assigned for different type of navigation.")
        End If
      Else
        Dim referenceNavigation = New ReferenceNavigation(propertyName, relatedEntityType)
        m_RelationshipNavigations.Add(propertyName, referenceNavigation)
        Return referenceNavigation
      End If
    End Function

    Friend Function AddCollectionNavigation(propertyName As String, relatedEntityType As Type, collectionType As Type) As CollectionNavigation
      If m_RelationshipNavigations.ContainsKey(propertyName) Then
        If TypeOf m_RelationshipNavigations(propertyName) Is CollectionNavigation Then
          Return DirectCast(m_RelationshipNavigations(propertyName), CollectionNavigation)
        Else
          Throw New Exception($"Cannot add collection navigation. Property '{propertyName}' is already assigned for different type of navigation.")
        End If
      Else
        Dim collectionNavigation = New CollectionNavigation(propertyName, relatedEntityType, collectionType)
        m_RelationshipNavigations.Add(propertyName, collectionNavigation)
        Return collectionNavigation
      End If
    End Function

    Public Function GetRelationshipNavigations(relatedEntityType As Type) As IEnumerable(Of RelationshipNavigation)
      Return m_RelationshipNavigations.Values.Where(Function(r) r.RelatedEntityType = relatedEntityType).ToArray()
    End Function

    Public Function GetRelationshipNavigation(propertyName As String) As RelationshipNavigation
      Return m_RelationshipNavigations(propertyName)
    End Function

  End Class
End Namespace
