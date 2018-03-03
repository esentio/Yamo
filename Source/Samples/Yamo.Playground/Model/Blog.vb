Public Class Blog

  Public Property Id As Guid

  Public Property Title As String

  Public Property Content As String

  Public Property PublishDate As DateTime

  Public Property Rating As Int32

  Public Property ExtraRating As Int32?


  Public Property Comments As List(Of Comment)

End Class
