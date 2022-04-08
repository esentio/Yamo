Imports System.Diagnostics.CodeAnalysis
Imports System.Reflection

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL result of an excluded type with no additional details.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class ExcludedUnknownSqlResult
    Inherits SqlResultBase

    ''' <summary>
    ''' Creates new instance of <see cref="ExcludedUnknownSqlResult"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="resultType"></param>
    Public Sub New(<DisallowNull> resultType As Type)
      MyBase.New(resultType)
    End Sub

    ''' <summary>
    ''' Gets count of columns in the resultset.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function GetColumnCount() As Int32
      Return 0
    End Function

  End Class
End Namespace