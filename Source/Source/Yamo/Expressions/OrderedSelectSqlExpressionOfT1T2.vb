Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents ORDER BY clause in SQL SELECT statement.
  ''' </summary>
  ''' <typeparam name="T1"></typeparam>
  ''' <typeparam name="T2"></typeparam>
  Public Class OrderedSelectSqlExpression(Of T1, T2)
    Inherits SelectSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="OrderedSelectSqlExpression(Of T1, T2)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(keySelector, {0}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(<DisallowNull> keySelector As Expression(Of Func(Of T1, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(keySelector, {0}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(keySelector, {1}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(<DisallowNull> keySelector As Expression(Of Func(Of T2, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(keySelector, {1}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of T1, T2), TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(keySelector, Nothing, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of T1, T2), FormattableString))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(keySelector, Nothing, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function ThenBy(<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object) As OrderedSelectSqlExpression(Of T1, T2)
      Me.Builder.AddOrderBy(predicate, True, parameters)
      Return Me
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(keySelector, {0}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(<DisallowNull> keySelector As Expression(Of Func(Of T1, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(keySelector, {0}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(keySelector, {1}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(<DisallowNull> keySelector As Expression(Of Func(Of T2, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(keySelector, {1}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of T1, T2), TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(keySelector, Nothing, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of T1, T2), FormattableString))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(keySelector, Nothing, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object) As OrderedSelectSqlExpression(Of T1, T2)
      Me.Builder.AddOrderBy(predicate, False, parameters)
      Return Me
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <param name="ascending"></param>
    ''' <returns></returns>
    Private Function InternalOrderBy(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of T1, T2)
      Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)
      Return Me
    End Function

    ''' <summary>
    ''' Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used.
    ''' </summary>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Public Function Limit(count As Int32) As LimitedSelectSqlExpression(Of T1, T2)
      Return InternalLimit(Nothing, count)
    End Function

    ''' <summary>
    ''' Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used.
    ''' </summary>
    ''' <param name="offset"></param>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Public Function Limit(offset As Int32, count As Int32) As LimitedSelectSqlExpression(Of T1, T2)
      Return InternalLimit(offset, count)
    End Function

    ''' <summary>
    ''' Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used.
    ''' </summary>
    ''' <param name="offset"></param>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Private Function InternalLimit(offset As Int32?, count As Int32) As LimitedSelectSqlExpression(Of T1, T2)
      Me.Builder.AddLimit(offset, count)
      Return New LimitedSelectSqlExpression(Of T1, T2)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SELECT clause with all columns of all tables (entities).
    ''' </summary>
    ''' <returns></returns>
    Public Function SelectAll() As SelectedSelectSqlExpression(Of T1, T2)
      Me.Builder.AddSelectAll(GetType(T1), GetType(T2))
      Return New SelectedSelectSqlExpression(Of T1, T2)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SELECT COUNT(*) clause, executes SQL query and returns the result.
    ''' </summary>
    ''' <returns></returns>
    Public Function SelectCount() As Int32
      Me.Builder.AddSelectCount()
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.QueryFirstOrDefault(Of Int32)(query)
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of T1, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {0})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of T2, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {1})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of T1, T2, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {0, 1})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of Join(Of T1, T2), TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, Nothing)
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalSelect(Of TResult)(selector As Expression, entityIndexHints As Int32()) As CustomSelectSqlExpression(Of TResult)
      Me.Builder.AddSelect(selector, entityIndexHints)
      Return New CustomSelectSqlExpression(Of TResult)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Conditionally builds the expression.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="condition"></param>
    ''' <param name="[then]"></param>
    ''' <param name="otherwise"></param>
    ''' <returns></returns>
    Public Function [If](Of TResult)(condition As Boolean, <DisallowNull> [then] As Func(Of OrderedSelectSqlExpression(Of T1, T2), TResult), Optional otherwise As Func(Of OrderedSelectSqlExpression(Of T1, T2), TResult) = Nothing) As TResult
      If condition Then
        Return [then].Invoke(Me)
      ElseIf otherwise Is Nothing Then
        Return CreateResultForCondition(Of TResult)()
      Else
        Return otherwise.Invoke(Me)
      End If
    End Function

    ''' <summary>
    ''' Creates result for condition if condition is not met.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <returns></returns>
    Private Function CreateResultForCondition(Of TResult)() As TResult
      Dim thisType = Me.GetType()
      Dim resultType = GetType(TResult)

      If thisType Is resultType Then
        Return DirectCast(DirectCast(Me, Object), TResult)
      End If

      If Not CanCreateResultForCondition(resultType) Then
        Throw New InvalidOperationException($"Parameter 'otherwise' in If() method is required for return type '{resultType}'.")
      End If

      Return DirectCast(Activator.CreateInstance(resultType, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance, Nothing, {Me.Builder, Me.Executor}, Nothing), TResult)
    End Function

    ''' <summary>
    ''' Checks if result can be created if condition is not met.
    ''' </summary>
    ''' <param name="resultType"></param>
    ''' <returns></returns>
    Private Function CanCreateResultForCondition(resultType As Type) As Boolean
      If Not GetType(SelectSqlExpressionBase).IsAssignableFrom(resultType) Then
        Return False
      End If

      If Not resultType.IsGenericType Then
        Return False
      End If

      Dim genericType = resultType.GetGenericTypeDefinition()

      If genericType Is GetType(OrderedSelectSqlExpression(Of ,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,)) Then Return True

      Return False
    End Function

  End Class
End Namespace
