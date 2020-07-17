''' <summary>
'''  Provides an API for configuring <see cref="DbContextOptions"/>.<br/>
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
  ''' Sets database commands timeout.
  ''' </summary>
  ''' <param name="timeout"></param>
  ''' <returns></returns>
  Public Function UseCommandTimeout(timeout As Int32) As DbContextOptionsBuilder
    Me.Options.CommandTimeout = timeout
    Return Me
  End Function

  ''' <summary>
  ''' Registers dialect specific SQL helper.
  ''' </summary>
  ''' <typeparam name="TSqlHelper"></typeparam>
  ''' <typeparam name="TDialectSqlHelper"></typeparam>
  ''' <returns></returns>
  Public Function RegisterDialectSpecificSqlHelper(Of TSqlHelper, TDialectSqlHelper)() As DbContextOptionsBuilder
    Me.Options.DialectProvider.RegisterDialectSpecificSqlHelper(Of TSqlHelper, TDialectSqlHelper)()
    Return Me
  End Function

End Class
