Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal
Imports Yamo.Internal.Query

Namespace Expressions

  Public Class DeleteSqlExpression(Of T)
    Inherits DeleteSqlExpressionBase

    Friend Sub New(context As DbContext, softDelete As Boolean)
      MyBase.New(context, New DeleteSqlExpressionBuilder(context, softDelete), New QueryExecutor(context))
      Me.Builder.SetMainTable(Of T)()
    End Sub

    Public Function Where(predicate As Expression(Of Func(Of T, Boolean))) As FilteredDeleteSqlExpression(Of T)
      Me.Builder.AddWhere(predicate)
      Return New FilteredDeleteSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    Public Function Where(predicate As Expression(Of Func(Of T, FormattableString))) As FilteredDeleteSqlExpression(Of T)
      Me.Builder.AddWhere(predicate)
      Return New FilteredDeleteSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    Public Function Where(predicate As String) As FilteredDeleteSqlExpression(Of T)
      Me.Builder.AddWhere(predicate)
      Return New FilteredDeleteSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    Public Function Execute() As Int32
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.Execute(query)
    End Function

    Friend Function Delete(obj As T) As Int32
      Dim query = Me.Builder.CreateDeleteQuery(obj)
      Return Me.Executor.Execute(query)
    End Function

    Friend Function SoftDelete(obj As T) As Int32
      ' NOTE: this doesn't reset property modified tracking!
      Dim setter = EntityAutoFieldsSetterCache.GetOnDeleteSetter(Me.DbContext.Model, GetEntityType(obj))
      setter(obj, Me.DbContext)

      Dim query = Me.Builder.CreateSoftDeleteQuery(obj)
      Return Me.Executor.Execute(query)
    End Function

  End Class
End Namespace