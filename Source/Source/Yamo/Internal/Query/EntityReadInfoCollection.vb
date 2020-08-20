Imports System.Data.Common
Imports Yamo.Infrastructure
Imports Yamo.Internal.Query.Metadata

Namespace Internal.Query

  ''' <summary>
  ''' Represents info used to read multiple entities from SQL result.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class EntityReadInfoCollection

    ''' <summary>
    ''' Gets entity read info items.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Items As EntityReadInfo()

    ''' <summary>
    ''' Gets count of entity read infos.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Count As Int32

    ''' <summary>
    ''' Gets whether collection navigation is used.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HasCollectionNavigation As Boolean

    ''' <summary>
    ''' Stores chain indexes
    ''' </summary>
    Private m_ChainIndexes As List(Of Int32)() ' probably change to something like Boolean() in the future (list is used just for convenience in GetChainKey method - once better hashing API is avaliable, refactor this!)

    ''' <summary>
    ''' Creates new instance of <see cref="EntityReadInfoCollection"/>.
    ''' </summary>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="EntityReadInfoCollection"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <returns></returns>
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
    ''' Sets chain indexes for all entities for later computation of chain key.
    ''' </summary>
    Private Sub SetChainIndexes()
      ' Chain index is list of entity indexes of primary keys (hashes of pks) that should be used to build chain key (which is again hash).
      ' We need following entities (more precisely their indexes) to be part of the chain:
      ' - Current entity + all entities that are in the path to the root of the query tree (to the first entity). These are marked with MarkMainPath method.
      ' - All entities that are on a following path: from current entity to any entity on the main path, there are only 1:1 relationships. These are marked with Mark1To1Paths method.
      ' 
      ' For example, let's have following tree of entities and their relationships:
      ' 
      '          T5
      '         /
      '        / 1:1
      '       /
      '      T2--1:N--T6
      '     /
      '    / 1:1
      '   / 
      ' T1--1:N--T3--1:1--T7
      '   \
      '    \ 1:N
      '     \
      '      T4
      ' 
      ' Entity indexes: T1 = 0, T1 = 1, ..., T7 = 6
      ' 
      ' Chain indexes will be:
      ' T1 = {0}
      ' T2 = {0, 1, 4}
      ' T3 = {0, 1, 2, 4}
      ' T4 = {0, 1, 3, 4}
      ' T5 = {0, 1, 4}
      ' T6 = {0, 1, 4, 5}
      ' T7 = {0, 1, 2, 4, 6}
      ' 
      ' This is necessary to properly group 1:N records.

      m_ChainIndexes = New List(Of Int32)(Me.Count - 1) {}

      For i = 0 To Me.Count - 1
        Dim flags = New Boolean(Me.Count - 1) {}

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

    ''' <summary>
    ''' Marsk main path.
    ''' </summary>
    ''' <param name="flags"></param>
    ''' <param name="entityIndex"></param>
    Private Sub MarkMainPath(flags As Boolean(), entityIndex As Int32)
      flags(entityIndex) = True

      If Me.Items(entityIndex).Entity.Relationship IsNot Nothing Then
        MarkMainPath(flags, Me.Items(entityIndex).Entity.Relationship.DeclaringEntity.Index)
      End If
    End Sub

    ''' <summary>
    ''' Marks 1:1 paths.
    ''' </summary>
    ''' <param name="flags"></param>
    ''' <param name="entityIndex"></param>
    ''' <returns></returns>
    Private Function Mark1To1Paths(flags As Boolean(), entityIndex As Int32) As Boolean
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

    ''' <summary>
    ''' Fill primary keys.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReader"></param>
    ''' <param name="pks"></param>
    Public Sub FillPks(dataReader As DbDataReader, pks As Object())
      For i = 0 To Me.Count - 1
        Dim entityInfo = Me.Items(i)

        If entityInfo.Entity.IsExcludedOrIgnored Then
          pks(i) = Nothing
        ElseIf entityInfo.ContainsPKReader(dataReader, entityInfo.ReaderIndex, entityInfo.PKOffsets) Then
          pks(i) = entityInfo.PKReader(dataReader, entityInfo.ReaderIndex, entityInfo.PKOffsets)
        Else
          pks(i) = Nothing
        End If
      Next
    End Sub

    ''' <summary>
    ''' Get chain key.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entityIndex"></param>
    ''' <param name="pks"></param>
    ''' <returns></returns>
    Public Function GetChainKey(entityIndex As Int32, pks As Object()) As ChainKey
      Dim chainIndex = m_ChainIndexes(entityIndex)

      Select Case chainIndex.Count
        Case 1
          Return New ChainKey({pks(chainIndex(0))})
        Case 2
          Return New ChainKey({pks(chainIndex(0)), pks(chainIndex(1))})
        Case 3
          Return New ChainKey({pks(chainIndex(0)), pks(chainIndex(1)), pks(chainIndex(2))})
        Case 4
          Return New ChainKey({pks(chainIndex(0)), pks(chainIndex(1)), pks(chainIndex(2)), pks(chainIndex(3))})
        Case 5
          Return New ChainKey({pks(chainIndex(0)), pks(chainIndex(1)), pks(chainIndex(2)), pks(chainIndex(3)), pks(chainIndex(4))})
        Case 6
          Return New ChainKey({pks(chainIndex(0)), pks(chainIndex(1)), pks(chainIndex(2)), pks(chainIndex(3)), pks(chainIndex(4)), pks(chainIndex(5))})
        Case 7
          Return New ChainKey({pks(chainIndex(0)), pks(chainIndex(1)), pks(chainIndex(2)), pks(chainIndex(3)), pks(chainIndex(4)), pks(chainIndex(5)), pks(chainIndex(6))})
        Case 8
          Return New ChainKey({pks(chainIndex(0)), pks(chainIndex(1)), pks(chainIndex(2)), pks(chainIndex(3)), pks(chainIndex(4)), pks(chainIndex(5)), pks(chainIndex(6)), pks(chainIndex(7))})
        Case 9
          Return New ChainKey({pks(chainIndex(0)), pks(chainIndex(1)), pks(chainIndex(2)), pks(chainIndex(3)), pks(chainIndex(4)), pks(chainIndex(5)), pks(chainIndex(6)), pks(chainIndex(7)), pks(chainIndex(8))})
        Case 10
          Return New ChainKey({pks(chainIndex(0)), pks(chainIndex(1)), pks(chainIndex(2)), pks(chainIndex(3)), pks(chainIndex(4)), pks(chainIndex(5)), pks(chainIndex(6)), pks(chainIndex(7)), pks(chainIndex(8)), pks(chainIndex(9))})
        Case 11
          Return New ChainKey({pks(chainIndex(0)), pks(chainIndex(1)), pks(chainIndex(2)), pks(chainIndex(3)), pks(chainIndex(4)), pks(chainIndex(5)), pks(chainIndex(6)), pks(chainIndex(7)), pks(chainIndex(8)), pks(chainIndex(9)), pks(chainIndex(10))})
        Case 12
          Return New ChainKey({pks(chainIndex(0)), pks(chainIndex(1)), pks(chainIndex(2)), pks(chainIndex(3)), pks(chainIndex(4)), pks(chainIndex(5)), pks(chainIndex(6)), pks(chainIndex(7)), pks(chainIndex(8)), pks(chainIndex(9)), pks(chainIndex(10)), pks(chainIndex(11))})
        Case 13
          Return New ChainKey({pks(chainIndex(0)), pks(chainIndex(1)), pks(chainIndex(2)), pks(chainIndex(3)), pks(chainIndex(4)), pks(chainIndex(5)), pks(chainIndex(6)), pks(chainIndex(7)), pks(chainIndex(8)), pks(chainIndex(9)), pks(chainIndex(10)), pks(chainIndex(11)), pks(chainIndex(12))})
        Case 14
          Return New ChainKey({pks(chainIndex(0)), pks(chainIndex(1)), pks(chainIndex(2)), pks(chainIndex(3)), pks(chainIndex(4)), pks(chainIndex(5)), pks(chainIndex(6)), pks(chainIndex(7)), pks(chainIndex(8)), pks(chainIndex(9)), pks(chainIndex(10)), pks(chainIndex(11)), pks(chainIndex(12)), pks(chainIndex(13))})
        Case 15
          Return New ChainKey({pks(chainIndex(0)), pks(chainIndex(1)), pks(chainIndex(2)), pks(chainIndex(3)), pks(chainIndex(4)), pks(chainIndex(5)), pks(chainIndex(6)), pks(chainIndex(7)), pks(chainIndex(8)), pks(chainIndex(9)), pks(chainIndex(10)), pks(chainIndex(11)), pks(chainIndex(12)), pks(chainIndex(13)), pks(chainIndex(14))})
        Case Else
          Throw New Exception("Too much joins.")
      End Select
    End Function

  End Class
End Namespace