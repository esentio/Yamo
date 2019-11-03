Imports System.Data
Imports Yamo.Infrastructure
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Metadata

Namespace Internal.Query

  ''' <summary>
  ''' Base class for info used to read data from SQL result.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public MustInherit Class BaseReadInfo

    ''' <summary>
    ''' Get primary keys offsets.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <returns></returns>
    Protected Shared Function GetPKOffsets(entity As SqlEntity) As Int32()
      Dim includedColumns = entity.IncludedColumns
      Dim pks = entity.Entity.GetKeyProperties()
      Dim pkOffsets = New Int32(pks.Count - 1) {}

      If pks.Count = 0 Then
        Return pkOffsets
      End If

      Dim offset = 0
      Dim currentPkIndex = 0

      For i = 0 To pks.Last().Index
        If i = pks(currentPkIndex).Index Then
          pkOffsets(currentPkIndex) = offset
          currentPkIndex += 1
        End If

        If includedColumns(i) Then
          offset += 1
        End If
      Next

      Return pkOffsets
    End Function
  End Class
End Namespace