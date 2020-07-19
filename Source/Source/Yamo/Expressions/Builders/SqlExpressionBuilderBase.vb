Imports System.Text
Imports Yamo.Infrastructure
Imports Yamo.Internal.Query
Imports Yamo.Sql

Namespace Expressions.Builders

  ''' <summary>
  ''' Base class for SQL expression builders.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public MustInherit Class SqlExpressionBuilderBase

    ''' <summary>
    ''' Gets dialect provider.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    Public ReadOnly DialectProvider As SqlDialectProvider

    ''' <summary>
    ''' Gets context.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    Public ReadOnly DbContext As DbContext

    ''' <summary>
    ''' Creates new instance of <see cref="SqlExpressionBuilderBase"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="context"></param>
    Public Sub New(context As DbContext)
      Me.DialectProvider = context.Options.DialectProvider
      Me.DbContext = context
    End Sub

    ''' <summary>
    ''' Creates SQL parameter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Public Function CreateParameter(index As Int32) As String
      Return Me.DialectProvider.Formatter.CreateParameter("p" & index.ToString(Globalization.CultureInfo.InvariantCulture))
    End Function

    ''' <summary>
    ''' Converts <see cref="FormattableString"/> to <see cref="SqlString"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="parameterIndex"></param>
    ''' <returns></returns>
    Public Function ConvertToSqlString(sql As FormattableString, parameterIndex As Int32) As SqlString
      Dim args = sql.GetArguments()

      If args.Length = 0 Then
        Return New SqlString(sql.Format)
      Else
        Return ConvertToSqlString(sql.Format, args, parameterIndex)
      End If
    End Function

    ''' <summary>
    ''' Converts string format to <see cref="SqlString"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="format"></param>
    ''' <param name="args"></param>
    ''' <param name="parameterIndex"></param>
    ''' <returns></returns>
    Public Function ConvertToSqlString(format As String, args() As Object, parameterIndex As Int32) As SqlString
      Dim formatArgs = New String(args.Length - 1) {}
      Dim parameters = New List(Of SqlParameter)(args.Length)

      For i = 0 To args.Length - 1
        Dim value = args(i)

        If TypeOf value Is ModelInfo Then
          Dim mi = DirectCast(value, ModelInfo)
          formatArgs(i) = CreateColumnsString(mi.Model, mi.TableAlias)
        ElseIf TypeOf value Is RawSqlString Then
          Dim s = DirectCast(value, RawSqlString)
          formatArgs(i) = s.Value
        Else
          Dim paramName = CreateParameter(parameterIndex + i)
          formatArgs(i) = paramName
          parameters.Add(New SqlParameter(paramName, value))
        End If
      Next

      Dim sqlString = String.Format(format, formatArgs)

      Return New SqlString(sqlString, parameters)
    End Function

    ''' <summary>
    ''' Creates string containing list of columns.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="tableAlias"></param>
    ''' <returns></returns>
    Private Function CreateColumnsString(model As Type, tableAlias As String) As String
      Dim entity = Me.DbContext.Model.GetEntity(model)
      Dim properties = entity.GetProperties()

      Dim sql = New StringBuilder()
      Dim first = True
      Dim hasAlias = Not String.IsNullOrWhiteSpace(tableAlias)

      For i = 0 To properties.Count - 1
        If first Then
          first = False
        Else
          sql.Append(", ")
        End If

        If hasAlias Then
          sql.Append(tableAlias)
          sql.Append(".")
        End If

        Me.DialectProvider.Formatter.AppendIdentifier(sql, properties(i).ColumnName)
      Next

      Return sql.ToString()
    End Function

  End Class
End Namespace