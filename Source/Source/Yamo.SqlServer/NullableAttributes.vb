#If NETSTANDARD2_0 Then
Namespace Global.System.Diagnostics.CodeAnalysis

  <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field Or AttributeTargets.Parameter Or AttributeTargets.ReturnValue, Inherited:=False)>
  Friend Class MaybeNullAttribute
    Inherits Attribute
  End Class

  <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field Or AttributeTargets.Parameter, Inherited:=False)>
  Friend Class DisallowNullAttribute
    Inherits Attribute
  End Class
End Namespace
#End If
