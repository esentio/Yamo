Imports System.Linq.Expressions
Imports Yamo.Internal

''' <summary>
''' Provides the functionality to dynamically build predicates.
''' </summary>
Public Class PredicateBuilder

  ''' <summary>
  ''' Creates new instance of <see cref="PredicateBuilder"/>.
  ''' </summary>
  Private Sub New()
  End Sub

  ''' <summary>
  ''' Creates predicate that represents logical AND between two predicates.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="left"></param>
  ''' <param name="right"></param>
  ''' <returns></returns>
  Public Shared Function [And](Of T)(left As Expression(Of Func(Of T, Boolean)), right As Expression(Of Func(Of T, Boolean))) As Expression(Of Func(Of T, Boolean))
    Dim visitor = New ReplaceParameterVisitor()

    Dim newLeft = left.Body
    Dim newRight = visitor.Replace(right.Body, right.Parameters(0), left.Parameters(0))
    Dim newParameters = left.Parameters

    Return Expression.Lambda(Of Func(Of T, Boolean))(Expression.AndAlso(newLeft, newRight), newParameters)
  End Function

  ''' <summary>
  ''' Creates predicate that represents logical AND between multiple predicates.<br/>
  ''' If parameter array contains 0 predicates, result will be a predicate that always returns <see langword="True"/>.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="predicates"></param>
  ''' <returns></returns>
  Public Shared Function [And](Of T)(ParamArray predicates As Expression(Of Func(Of T, Boolean))()) As Expression(Of Func(Of T, Boolean))
    If predicates Is Nothing Then
      Throw New ArgumentNullException(NameOf(predicates))
    ElseIf predicates.Length = 0 Then
      Return [True](Of T)()
    ElseIf predicates.Length = 1 Then
      Return predicates(0)
    End If

    Dim visitor = New ReplaceParameterVisitor()

    Dim newPredicate = predicates(0).Body
    Dim newParameters = predicates(0).Parameters

    For i = 1 To predicates.Length - 1
      Dim exp = predicates(i)
      Dim right = visitor.Replace(exp.Body, exp.Parameters(0), newParameters(0))
      newPredicate = Expression.AndAlso(newPredicate, right)
    Next

    Return Expression.Lambda(Of Func(Of T, Boolean))(newPredicate, newParameters)
  End Function

  ''' <summary>
  ''' Creates predicate that represents logical AND between multiple predicates.<br/>
  ''' If parameter collection contains 0 predicates, result will be a predicate that always returns <see langword="True"/>.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="predicates"></param>
  ''' <returns></returns>
  Public Shared Function [And](Of T)(predicates As IEnumerable(Of Expression(Of Func(Of T, Boolean)))) As Expression(Of Func(Of T, Boolean))
    If predicates Is Nothing Then
      Throw New ArgumentNullException(NameOf(predicates))
    End If

    Dim count = predicates.Count()

    If count = 0 Then
      Return [True](Of T)()
    ElseIf predicates.Count = 1 Then
      Return predicates(0)
    End If

    Dim visitor = New ReplaceParameterVisitor()

    Dim newPredicate = predicates(0).Body
    Dim newParameters = predicates(0).Parameters
    Dim skipped = False

    For Each predicate In predicates
      If skipped Then
        Dim right = visitor.Replace(predicate.Body, predicate.Parameters(0), newParameters(0))
        newPredicate = Expression.AndAlso(newPredicate, right)
      Else
        skipped = True
      End If
    Next

    Return Expression.Lambda(Of Func(Of T, Boolean))(newPredicate, newParameters)
  End Function

  ''' <summary>
  ''' Creates predicate that represents logical OR between two predicates.<br/>
  ''' If parameter array contains 0 predicates, result will be a predicate that always returns <see langword="True"/>.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="left"></param>
  ''' <param name="right"></param>
  ''' <returns></returns>
  Public Shared Function [Or](Of T)(left As Expression(Of Func(Of T, Boolean)), right As Expression(Of Func(Of T, Boolean))) As Expression(Of Func(Of T, Boolean))
    Dim visitor = New ReplaceParameterVisitor()

    Dim newLeft = left.Body
    Dim newRight = visitor.Replace(right.Body, right.Parameters(0), left.Parameters(0))
    Dim newParameters = left.Parameters

    Return Expression.Lambda(Of Func(Of T, Boolean))(Expression.OrElse(newLeft, newRight), newParameters)
  End Function

  ''' <summary>
  ''' Creates predicate that represents logical OR between multiple predicates.<br/>
  ''' If parameter collection contains 0 predicates, result will be a predicate that always returns <see langword="True"/>.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="predicates"></param>
  ''' <returns></returns>
  Public Shared Function [Or](Of T)(ParamArray predicates As Expression(Of Func(Of T, Boolean))()) As Expression(Of Func(Of T, Boolean))
    If predicates Is Nothing Then
      Throw New ArgumentNullException(NameOf(predicates))
    ElseIf predicates.Length = 0 Then
      Return [True](Of T)()
    ElseIf predicates.Length = 1 Then
      Return predicates(0)
    End If

    Dim visitor = New ReplaceParameterVisitor()

    Dim newPredicate = predicates(0).Body
    Dim newParameters = predicates(0).Parameters

    For i = 1 To predicates.Length - 1
      Dim exp = predicates(i)
      Dim right = visitor.Replace(exp.Body, exp.Parameters(0), newParameters(0))
      newPredicate = Expression.OrElse(newPredicate, right)
    Next

    Return Expression.Lambda(Of Func(Of T, Boolean))(newPredicate, newParameters)
  End Function

  ''' <summary>
  ''' Creates predicate that represents logical OR between multiple predicates.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="predicates"></param>
  ''' <returns></returns>
  Public Shared Function [Or](Of T)(predicates As IEnumerable(Of Expression(Of Func(Of T, Boolean)))) As Expression(Of Func(Of T, Boolean))
    If predicates Is Nothing Then
      Throw New ArgumentNullException(NameOf(predicates))
    End If

    Dim count = predicates.Count()

    If count = 0 Then
      Return [True](Of T)()
    ElseIf predicates.Count = 1 Then
      Return predicates(0)
    End If

    Dim visitor = New ReplaceParameterVisitor()

    Dim newPredicate = predicates(0).Body
    Dim newParameters = predicates(0).Parameters
    Dim skipped = False

    For Each predicate In predicates
      If skipped Then
        Dim right = visitor.Replace(predicate.Body, predicate.Parameters(0), newParameters(0))
        newPredicate = Expression.OrElse(newPredicate, right)
      Else
        skipped = True
      End If
    Next

    Return Expression.Lambda(Of Func(Of T, Boolean))(newPredicate, newParameters)
  End Function

  ''' <summary>
  ''' Creates predicate that represents logical negation of a predicate.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="predicate"></param>
  ''' <returns></returns>
  Public Shared Function [Not](Of T)(predicate As Expression(Of Func(Of T, Boolean))) As Expression(Of Func(Of T, Boolean))
    Return Expression.Lambda(Of Func(Of T, Boolean))(Expression.Not(predicate.Body), predicate.Parameters)
  End Function

  ''' <summary>
  ''' Creates predicate that always returns <see langword="True"/>.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <returns></returns>
  Public Shared Function [True](Of T)() As Expression(Of Func(Of T, Boolean))
    Dim param = Expression.Parameter(GetType(T), "x")
    Return Expression.Lambda(Of Func(Of T, Boolean))(Expression.Constant(True), {param})
  End Function

  ''' <summary>
  ''' Creates predicate that always returns <see langword="False"/>.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <returns></returns>
  Public Shared Function [False](Of T)() As Expression(Of Func(Of T, Boolean))
    Dim param = Expression.Parameter(GetType(T), "x")
    Return Expression.Lambda(Of Func(Of T, Boolean))(Expression.Constant(False), {param})
  End Function

End Class
