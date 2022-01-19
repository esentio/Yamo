Imports System.Diagnostics.CodeAnalysis

Namespace Internal.Query

  ''' <summary>
  ''' Create insert SQL string result.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Structure CreateInsertSqlStringResult

    ''' <summary>
    ''' Gets SQL string.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SqlString As SqlString

    ''' <summary>
    ''' Gets whether database generated values should be read.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ReadDbGeneratedValues As Boolean

    ''' <summary>
    ''' Creates new instance of <see cref="CreateInsertSqlStringResult"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sqlString"></param>
    ''' <param name="readDbGeneratedValues"></param>
    Sub New(<DisallowNull> sqlString As SqlString, readDbGeneratedValues As Boolean)
      Me.SqlString = sqlString
      Me.ReadDbGeneratedValues = readDbGeneratedValues
    End Sub

  End Structure
End Namespace