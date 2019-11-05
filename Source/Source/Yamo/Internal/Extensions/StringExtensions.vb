Imports System.Linq.Expressions
Imports System.Runtime.CompilerServices

Namespace Internal.Extensions

  ''' <summary>
  ''' <see cref="String"/> related extension methods.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  <Extension()>
  Public Module StringExtensions

    ''' <summary>
    ''' Retrieves the value of the current string object, or empty string.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="source"></param>
    ''' <returns>The value of the string if it is not null; otherwise, an empty string.</returns>
    <Extension>
    Public Function GetValueOrDefault(source As String) As String
      If source Is Nothing Then
        Return ""
      Else
        Return source
      End If
    End Function

    ''' <summary>
    ''' Retrieves the value of the current string object, or the specified default value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="defaultValue"></param>
    ''' <returns>The value of the string if it is not null; otherwise, the defaultValue parameter.</returns>
    <Extension>
    Public Function GetValueOrDefault(source As String, defaultValue As String) As String
      If source Is Nothing Then
        Return defaultValue
      Else
        Return source
      End If
    End Function

    ''' <summary>
    ''' Retrieves the value of the current string object, or the specified default value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="defaultValue"></param>
    ''' <returns>The value of the string if it is not null and not empty; otherwise, the defaultValue parameter.</returns>
    <Extension>
    Public Function GetValueOrDefaultIfIsNullOrEmpty(source As String, defaultValue As String) As String
      If String.IsNullOrEmpty(source) Then
        Return defaultValue
      Else
        Return source
      End If
    End Function

    ''' <summary>
    ''' Retrieves the value of the current string object, or the specified default value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="defaultValue"></param>
    ''' <returns>The value of the string if it is not null, not empty and doesn't consist only of white-space characters; otherwise, the defaultValue parameter.</returns>
    <Extension>
    Public Function GetValueOrDefaultIfIsNullOrWhiteSpace(source As String, defaultValue As String) As String
      If String.IsNullOrWhiteSpace(source) Then
        Return defaultValue
      Else
        Return source
      End If
    End Function

  End Module
End Namespace