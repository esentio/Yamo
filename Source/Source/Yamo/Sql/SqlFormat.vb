Imports System.Linq.Expressions

Namespace Sql

  ''' <summary>
  ''' Stores info about SQL format string and objects to format.<br/>
  ''' This is a result of <see cref="SqlHelper.GetSqlFormat"/> method.
  ''' </summary>
  Public Structure SqlFormat

    ''' <summary>
    ''' SQL format string.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Format As String

    ''' <summary>
    ''' Objects to format.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Arguments As IReadOnlyList(Of Expression)

    ''' <summary>
    ''' Creates new instance of <see cref="SqlFormat"/>.
    ''' </summary>
    ''' <param name="format"></param>
    ''' <param name="arguments"></param>
    Public Sub New(format As String, arguments As IReadOnlyList(Of Expression))
      Me.Format = format
      Me.Arguments = arguments
    End Sub

  End Structure
End Namespace
