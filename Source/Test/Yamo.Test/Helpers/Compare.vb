Namespace Helpers

  ''' <summary>
  ''' Compare helper functions
  ''' </summary>
  ''' <remarks></remarks>
  Public Class Compare

    ' NOTE: rewrite or make obsolete when System.HashCode is available: https://github.com/dotnet/corefx/issues/14354

    Private Const Int32Range = 4294967296L ' Int32.MaxValue - Int32.MinValue + 1

    ''' <summary>
    ''' Private constructor
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Compute hashcode from multiple values.
    ''' Via http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
    ''' </summary>
    ''' <typeparam name="T1"></typeparam>
    ''' <typeparam name="T2"></typeparam>
    ''' <param name="arg1"></param>
    ''' <param name="arg2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function GetHashCode(Of T1, T2)(arg1 As T1, arg2 As T2) As Int32
      Dim hash = 17L
      hash = CombineHashCode(Of T1)(hash, arg1)
      hash = CombineHashCode(Of T2)(hash, arg2)
      Return Convert.ToInt32(hash)
    End Function

    ''' <summary>
    ''' Compute hashcode from multiple values
    ''' </summary>
    ''' <typeparam name="T1"></typeparam>
    ''' <typeparam name="T2"></typeparam>
    ''' <typeparam name="T3"></typeparam>
    ''' <param name="arg1"></param>
    ''' <param name="arg2"></param>
    ''' <param name="arg3"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function GetHashCode(Of T1, T2, T3)(arg1 As T1, arg2 As T2, arg3 As T3) As Int32
      Dim hash = 17L
      hash = CombineHashCode(Of T1)(hash, arg1)
      hash = CombineHashCode(Of T2)(hash, arg2)
      hash = CombineHashCode(Of T3)(hash, arg3)
      Return Convert.ToInt32(hash)
    End Function

    ''' <summary>
    ''' Compute hashcode from multiple values
    ''' </summary>
    ''' <typeparam name="T1"></typeparam>
    ''' <typeparam name="T2"></typeparam>
    ''' <typeparam name="T3"></typeparam>
    ''' <typeparam name="T4"></typeparam>
    ''' <param name="arg1"></param>
    ''' <param name="arg2"></param>
    ''' <param name="arg3"></param>
    ''' <param name="arg4"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function GetHashCode(Of T1, T2, T3, T4)(arg1 As T1, arg2 As T2, arg3 As T3, arg4 As T4) As Int32
      Dim hash = 17L
      hash = CombineHashCode(Of T1)(hash, arg1)
      hash = CombineHashCode(Of T2)(hash, arg2)
      hash = CombineHashCode(Of T3)(hash, arg3)
      hash = CombineHashCode(Of T4)(hash, arg4)
      Return Convert.ToInt32(hash)
    End Function

    ''' <summary>
    ''' Compute hashcode from multiple values
    ''' </summary>
    ''' <typeparam name="T1"></typeparam>
    ''' <typeparam name="T2"></typeparam>
    ''' <typeparam name="T3"></typeparam>
    ''' <typeparam name="T4"></typeparam>
    ''' <typeparam name="T5"></typeparam>
    ''' <param name="arg1"></param>
    ''' <param name="arg2"></param>
    ''' <param name="arg3"></param>
    ''' <param name="arg4"></param>
    ''' <param name="arg5"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function GetHashCode(Of T1, T2, T3, T4, T5)(arg1 As T1, arg2 As T2, arg3 As T3, arg4 As T4, arg5 As T5) As Int32
      Dim hash = 17L
      hash = CombineHashCode(Of T1)(hash, arg1)
      hash = CombineHashCode(Of T2)(hash, arg2)
      hash = CombineHashCode(Of T3)(hash, arg3)
      hash = CombineHashCode(Of T4)(hash, arg4)
      hash = CombineHashCode(Of T5)(hash, arg5)
      Return Convert.ToInt32(hash)
    End Function

    ''' <summary>
    ''' Compute hashcode from multiple values
    ''' </summary>
    ''' <param name="args"></param>
    ''' <returns></returns>
    Public Overloads Shared Function GetHashCode(ParamArray args As Object()) As Int32
      Dim hash = 17L

      For i = 0 To args.Length - 1
        hash = CombineHashCode(Of Object)(hash, args(i))
      Next

      Return Convert.ToInt32(hash)
    End Function

    ''' <summary>
    ''' Combine hash code
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="hash"></param>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CombineHashCode(Of T)(hash As Int64, obj As T) As Int64
      If obj Is Nothing Then
        Return UncheckedInt32Add(UncheckedTimes23(hash), 0)
      Else
        Return UncheckedInt32Add(UncheckedTimes23(hash), obj.GetHashCode())
      End If
    End Function

    ''' <summary>
    ''' Simulate multiplication by 23 inside unchecked block as in C#
    ''' </summary>
    ''' <param name="value">Should be in Int32 range</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function UncheckedTimes23(value As Int64) As Int64
      value *= 23

      If value > Int32.MaxValue Then
        value = value Mod Int32Range

        If value > Int32.MaxValue Then
          value = Int32.MinValue + (value - Int32.MaxValue) - 1
        End If
      ElseIf value < Int32.MinValue Then
        value = value Mod Int32Range

        If value < Int32.MinValue Then
          value = Int32.MaxValue + (value - Int32.MinValue) + 1
        End If
      End If

      Return value
    End Function

    ''' <summary>
    ''' Simulate addition of 2 numbers inside unchecked block as in C#
    ''' </summary>
    ''' <param name="value1">Should be in Int32 range</param>
    ''' <param name="value2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function UncheckedInt32Add(value1 As Int64, value2 As Int32) As Int64
      value1 += value2

      If value1 > Int32.MaxValue Then
        value1 = Int32.MinValue + (value1 - Int32.MaxValue) - 1
      ElseIf value1 < Int32.MinValue Then
        value1 = Int32.MaxValue + (value1 - Int32.MinValue)
      End If

      Return value1
    End Function

    ''' <summary>
    ''' Determine if two byte arrays are equal
    ''' </summary>
    ''' <param name="array1"></param>
    ''' <param name="array2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function AreByteArraysEqual(array1() As Byte, array2() As Byte) As Boolean
      If array1 Is Nothing AndAlso array2 Is Nothing Then Return True
      If array1 Is Nothing OrElse array2 Is Nothing Then Return False
      If Not array1.Length = array2.Length Then Return False

      For i = 0 To array1.Length - 1
        If Not array1(i) = array2(i) Then Return False
      Next

      Return True
    End Function

  End Class
End Namespace