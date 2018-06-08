Imports Yamo.Infrastructure
Imports Yamo.Internal.Query

Namespace Expressions.Builders

  Public MustInherit Class SqlExpressionBuilderBase

    Public ReadOnly DialectProvider As SqlDialectProvider

    Public ReadOnly DbContext As DbContext

    Public Sub New(context As DbContext)
      Me.DialectProvider = context.Options.DialectProvider
      Me.DbContext = context
    End Sub

    Public Function CreateParameter(index As Int32) As String
      Return Me.DialectProvider.Formatter.CreateParameter("p" & index.ToString(Globalization.CultureInfo.InvariantCulture))
    End Function

    Public Function ConvertToSqlString(sql As FormattableString, parameterIndex As Int32) As SqlString
      Dim args = sql.GetArguments()

      Dim paramNames = New String(args.Length - 1) {}
      Dim parameters = New List(Of SqlParameter)(args.Length)

      For i = 0 To args.Length - 1
        Dim paramName = CreateParameter(parameterIndex + i)
        paramNames(i) = paramName
        parameters.Add(New SqlParameter(paramName, args(i)))
      Next

      Dim sqlString = String.Format(sql.Format, paramNames)

      Return New SqlString(sqlString, parameters)
    End Function

  End Class
End Namespace