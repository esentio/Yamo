Imports System.Data

Namespace Internal.Query

  ''' <summary>
  ''' Represents SQL parameter.
  ''' </summary>
  Public Structure SqlParameter

    ''' <summary>
    ''' Gets parameter name.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Name As String

    ''' <summary>
    ''' Gets parameter value.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Value As Object

    ''' <summary>
    ''' Gets parameter database type.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property DbType As DbType?

    ''' <summary>
    ''' Creates new instance of <see cref="SqlParameter"/>.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="value"></param>
    Sub New(name As String, value As Object)
      Me.Name = name
      Me.Value = value
      Me.DbType = Nothing
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="SqlParameter"/>.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="value"></param>
    ''' <param name="dbType"></param>
    Sub New(name As String, value As Object, dbType As DbType)
      Me.Name = name
      Me.Value = value
      Me.DbType = dbType
    End Sub

  End Structure
End Namespace