Namespace Model

  Public Class Blog

    Public Property Id As Int32

    Public Property Title As String

    Public Property Content As String

    Public Property Created As DateTime

    Public Property CreatedUserId As Int32

    Public Property Modified As DateTime?

    Public Property ModifiedUserId As Int32?

    Public Property Deleted As DateTime?

    Public Property DeletedUserId As Int32?

    Public Property Author As User

    Public Property Comments As List(Of Comment)

  End Class
End Namespace
