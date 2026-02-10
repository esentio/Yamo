Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal
Imports Yamo.Internal.Query
Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class BaseExpressionTests
    Inherits BaseTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return UnitTestEnvironment.Create()
    End Function

    Protected Function CreateSqlExpressionVisitor(db As DbContext) As SqlExpressionVisitor
      Dim builder = New SelectSqlExpressionBuilder(db, GetType(ItemWithAllSupportedValues))

      Dim fi = builder.GetType().GetField("m_Visitor", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
      Return DirectCast(fi.GetValue(builder), SqlExpressionVisitor)
    End Function

    Protected Function TranslateCondition(visitor As SqlExpressionVisitor, predicate As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean))) As SqlString
      Return Translate(visitor, predicate, ExpressionTranslateMode.Condition)
    End Function

    Protected Function TranslateConditionWithJoin(visitor As SqlExpressionVisitor, predicate As Expression(Of Func(Of Join(Of ItemWithAllSupportedValues, Article), Boolean))) As SqlString
      Return TranslateWithJoin(visitor, predicate, ExpressionTranslateMode.Condition)
    End Function

    Protected Function Translate(visitor As SqlExpressionVisitor, predicate As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)), mode As ExpressionTranslateMode) As SqlString
      Return visitor.Translate(predicate, mode, {0}, 0, True, True)
    End Function

    Protected Function TranslateWithJoin(visitor As SqlExpressionVisitor, predicate As Expression(Of Func(Of Join(Of ItemWithAllSupportedValues, Article), Boolean)), mode As ExpressionTranslateMode) As SqlString
      Return visitor.Translate(predicate, mode, Nothing, 0, True, True)
    End Function

  End Class
End Namespace