''' <summary>
'''  Provides an API for configuring <see cref="DbContextOptions"/>.
'''  Define extension methods on this object to configure options for particular database dialects.
''' </summary>
Public Class DbContextOptionsBuilder

  ''' <summary>
  ''' Gets configuration options that are being configured.
  ''' </summary>
  ''' <returns></returns>
  Friend ReadOnly Property Options As DbContextOptions

  ''' <summary>
  ''' Gets internal configuration builder.
  ''' </summary>
  ''' <returns></returns>
  Private ReadOnly Property InternalBuilder As DbContextOptionsInternalBuilder

  ''' <summary>
  ''' Creates new instance of <see cref="DbContextOptionsBuilder"/>.
  ''' </summary>
  Sub New()
    Me.Options = New DbContextOptions
    Me.InternalBuilder = New DbContextOptionsInternalBuilder(Me.Options)
  End Sub

  ''' <summary>
  ''' Gets internal configuration builder.
  ''' </summary>
  ''' <returns></returns>
  Public Function GetInternalBuilder() As DbContextOptionsInternalBuilder
    Return Me.InternalBuilder
  End Function

  ''' <summary>
  ''' Set database commands timeout.
  ''' </summary>
  ''' <param name="timeout"></param>
  Public Sub UseCommandTimeout(timeout As Int32)
    Me.Options.CommandTimeout = timeout
  End Sub

End Class
