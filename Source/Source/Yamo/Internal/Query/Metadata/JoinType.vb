Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Join type.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Enum JoinType
    Inner = 0
    LeftOuter = 1
    RightOuter = 2
    FullOuter = 3
    CrossJoin = 4
  End Enum

End Namespace