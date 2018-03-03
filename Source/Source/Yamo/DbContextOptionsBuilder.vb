Public Class DbContextOptionsBuilder

  Friend ReadOnly Property Options As DbContextOptions

  Private ReadOnly Property InternalBuilder As DbContextOptionsInternalBuilder

  Sub New()
    Me.Options = New DbContextOptions
    Me.InternalBuilder = New DbContextOptionsInternalBuilder(Me.Options)
  End Sub

  Public Function GetInternalBuilder() As DbContextOptionsInternalBuilder
    Return Me.InternalBuilder
  End Function

  Public Sub UseCommandTimeout(timeout As Int32)
    Me.Options.CommandTimeout = timeout
  End Sub

End Class
