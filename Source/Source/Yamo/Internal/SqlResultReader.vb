Imports System.Data.Common
Imports System.Diagnostics.CodeAnalysis

Namespace Internal

  ''' <summary>
  ''' Sql result reader.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SqlResultReader

    ''' <summary>
    ''' Creates new instance of <see cref="SqlResultReader"/>.
    ''' </summary>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Checks whether there is at least one column that doesn't contain <see cref="DBNull"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReader"></param>
    ''' <param name="readerIndex"></param>
    ''' <param name="columnCount"></param>
    ''' <returns></returns>
    Public Shared Function ContainsNonNullColumn(<DisallowNull> dataReader As DbDataReader, readerIndex As Int32, columnCount As Int32) As Boolean
      For i = readerIndex To readerIndex + columnCount - 1
        If Not dataReader.IsDBNull(i) Then
          Return True
        End If
      Next

      Return False
    End Function

  End Class
End Namespace