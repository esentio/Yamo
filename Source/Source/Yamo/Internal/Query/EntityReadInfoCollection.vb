Imports System.Data.Common
Imports Yamo.Infrastructure
Imports Yamo.Internal.Query.Metadata

Namespace Internal.Query

  Public Class EntityReadInfoCollection

    Public ReadOnly Property Items As EntityReadInfo()

    Public ReadOnly Property Count As Int32

    Public ReadOnly Property HasCollectionNavigation As Boolean

    ' probably change to something like BitArray in the future (list is used just for convenience in GetChainKey method - once better hashing API is avaliable, refactor this!)
    Private m_ChainIndexes As List(Of Int32)()

    Private Sub New()
    End Sub

    Public Shared Function Create(dialectProvider As SqlDialectProvider, model As SqlModel) As EntityReadInfoCollection
      Dim entityInfos = EntityReadInfo.Create(dialectProvider, model)
      Dim hasCollectionNavigation = entityInfos.Any(Function(o) o.HasCollectionNavigation)

      Dim result = New EntityReadInfoCollection

      result._Items = entityInfos
      result._Count = entityInfos.Length
      result._HasCollectionNavigation = hasCollectionNavigation

      result.SetChainIndexes()

      Return result
    End Function

    ''' <summary>
    ''' Set chain indexes for all entities for later computation of chain key.
    ''' 
    ''' Chain index is list of entity indexes of primary keys (hashes of pks) that should be used to build chain key (which is again hash).
    ''' We need following entities (more precisely their indexes) to be part of the chain:
    ''' - Current entity + all entities that are in the path to the root of the query tree (to the first entity). These are marked with MarkMainPath method.
    ''' - All entities that are on a following path: from current entity to any entity on the main path, there are only 1:1 relationships. These are marked with Mark1To1Paths method.
    ''' 
    ''' For example, let's have following tree of entities and their relationships:
    ''' 
    '''          T5
    '''         /
    '''        / 1:1
    '''       /
    '''      T2--1:N--T6
    '''     /
    '''    / 1:1
    '''   / 
    ''' T1--1:N--T3--1:1--T7
    '''   \
    '''    \ 1:N
    '''     \
    '''      T4
    ''' 
    ''' Entity indexes: T1 = 0, T1 = 1, ..., T7 = 6
    ''' 
    ''' Chain indexes will be:
    ''' T1 = {0}
    ''' T2 = {0, 1, 4}
    ''' T3 = {0, 1, 2, 4}
    ''' T4 = {0, 1, 3, 4}
    ''' T5 = {0, 1, 4}
    ''' T6 = {0, 1, 4, 5}
    ''' T7 = {0, 1, 2, 4, 6}
    ''' 
    ''' This is necessary to properly group 1:N records.
    ''' </summary>
    Private Sub SetChainIndexes()
      m_ChainIndexes = New List(Of Integer)(Me.Count - 1) {}

      For i = 0 To Me.Count - 1
        Dim flags = New BitArray(Me.Count, False)

        MarkMainPath(flags, i)

        For j = Me.Count - 1 To 0 Step -1
          If Not flags(j) Then
            Mark1To1Paths(flags, j)
          End If
        Next

        m_ChainIndexes(i) = New List(Of Int32)

        For j = 0 To Me.Count - 1
          If flags(j) Then
            m_ChainIndexes(i).Add(j)
          End If
        Next
      Next
    End Sub

    Private Sub MarkMainPath(flags As BitArray, entityIndex As Int32)
      flags(entityIndex) = True

      If Me.Items(entityIndex).Entity.Relationship IsNot Nothing Then
        MarkMainPath(flags, Me.Items(entityIndex).Entity.Relationship.DeclaringEntity.Index)
      End If
    End Sub

    Private Function Mark1To1Paths(flags As BitArray, entityIndex As Int32) As Boolean
      If entityIndex = 0 Then
        flags(0) = True ' should be already true anyway
        Return True
      End If

      If flags(entityIndex) Then
        Return True
      End If

      If (Me.Items(entityIndex).Entity.Relationship?.IsReferenceNavigation).GetValueOrDefault(False) Then
        Dim mark = Mark1To1Paths(flags, Me.Items(entityIndex).Entity.Relationship.DeclaringEntity.Index)

        If mark Then
          flags(entityIndex) = True
        End If

        Return mark
      End If

      Return False
    End Function

    Public Sub FillPks(dataReader As DbDataReader, pks As Int32?())
      For i = 0 To Me.Count - 1
        Dim entityInfo = Me.Items(i)

        If entityInfo.Entity.IsExcluded Then
          pks(i) = Nothing
        ElseIf entityInfo.ContainsPKReader(dataReader, entityInfo.ReaderIndex, entityInfo.PKOffsets) Then
          pks(i) = entityInfo.PKReader(dataReader, entityInfo.ReaderIndex, entityInfo.PKOffsets)
        Else
          pks(i) = Nothing
        End If
      Next
    End Sub

    Public Function GetChainKey(entityIndex As Int32, pks As Int32?()) As Int32
      ' using ValueTuple is just simple workaround until .NET becomes System.HashCode

      Dim chainIndex = m_ChainIndexes(entityIndex)

      Select Case chainIndex.Count
        Case 1
          Return GetHashCodeOfPk(pks, chainIndex, 0)
        Case 2
          Return (GetHashCodeOfPk(pks, chainIndex, 0), GetHashCodeOfPk(pks, chainIndex, 1)).GetHashCode()
        Case 3
          Return (GetHashCodeOfPk(pks, chainIndex, 0), GetHashCodeOfPk(pks, chainIndex, 1), GetHashCodeOfPk(pks, chainIndex, 2)).GetHashCode()
        Case 4
          Return (GetHashCodeOfPk(pks, chainIndex, 0), GetHashCodeOfPk(pks, chainIndex, 1), GetHashCodeOfPk(pks, chainIndex, 2), GetHashCodeOfPk(pks, chainIndex, 3)).GetHashCode()
        Case 5
          Return (GetHashCodeOfPk(pks, chainIndex, 0), GetHashCodeOfPk(pks, chainIndex, 1), GetHashCodeOfPk(pks, chainIndex, 2), GetHashCodeOfPk(pks, chainIndex, 3), GetHashCodeOfPk(pks, chainIndex, 4)).GetHashCode()
        Case 6
          Return (GetHashCodeOfPk(pks, chainIndex, 0), GetHashCodeOfPk(pks, chainIndex, 1), GetHashCodeOfPk(pks, chainIndex, 2), GetHashCodeOfPk(pks, chainIndex, 3), GetHashCodeOfPk(pks, chainIndex, 4), GetHashCodeOfPk(pks, chainIndex, 5)).GetHashCode()
        Case 7
          Return (GetHashCodeOfPk(pks, chainIndex, 0), GetHashCodeOfPk(pks, chainIndex, 1), GetHashCodeOfPk(pks, chainIndex, 2), GetHashCodeOfPk(pks, chainIndex, 3), GetHashCodeOfPk(pks, chainIndex, 4), GetHashCodeOfPk(pks, chainIndex, 5), GetHashCodeOfPk(pks, chainIndex, 6)).GetHashCode()
        Case Else
          Throw New Exception("Too much joins.")
      End Select
    End Function

    Private Function GetHashCodeOfPk(pks As Int32?(), chainIndex As List(Of Int32), index As Int32) As Int32
      ' using ValueTuple is just simple workaround until .NET becomes System.HashCode
      ' we need to differenciate Nothing from 0
      Return (pks(chainIndex(index)).HasValue, pks(chainIndex(index))).GetHashCode()
    End Function

  End Class
End Namespace