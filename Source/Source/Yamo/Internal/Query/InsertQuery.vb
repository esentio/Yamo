Namespace Internal.Query

  ''' <summary>
  ''' Represents insert statement data.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class InsertQuery
    Inherits Query

    ''' <summary>
    ''' Gets whether database generated values should be read.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ReadDbGeneratedValues As Boolean

    ''' <summary>
    ''' Gets entity object.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Entity As Object

    ''' <summary>
    ''' Creates new instance of <see cref="InsertQuery"/><br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="readDbGeneratedValues"></param>
    ''' <param name="entity"></param>
    Sub New(sql As SqlString, readDbGeneratedValues As Boolean, entity As Object)
      MyBase.New(sql)
      Me.ReadDbGeneratedValues = readDbGeneratedValues
      Me.Entity = entity
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="InsertQuery"/><br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="parameters"></param>
    ''' <param name="readDbGeneratedValues"></param>
    ''' <param name="entity"></param>
    Sub New(sql As String, parameters As List(Of SqlParameter), readDbGeneratedValues As Boolean, entity As Object)
      MyBase.New(sql, parameters)
      Me.ReadDbGeneratedValues = readDbGeneratedValues
      Me.Entity = entity
    End Sub

  End Class
End Namespace