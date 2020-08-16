Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithConditionTests
    Inherits Yamo.Test.Tests.SelectWithConditionTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

    <TestMethod()>
    Public Overrides Sub SelectWithConditionalFullOuterJoin()
      Try
        MyBase.SelectWithConditionalFullOuterJoin()
        Assert.Fail()
      Catch ex As Exception
        If Not ex.Message.Contains("RIGHT and FULL OUTER JOINs are not currently supported") Then
          Assert.Fail(ex.Message)
        End If
      End Try
    End Sub

    <TestMethod()>
    Public Overrides Sub SelectWithConditionalRightOuterJoin()
      Try
        MyBase.SelectWithConditionalRightOuterJoin()
        Assert.Fail()
      Catch ex As Exception
        If Not ex.Message.Contains("RIGHT and FULL OUTER JOINs are not currently supported") Then
          Assert.Fail(ex.Message)
        End If
      End Try
    End Sub

    <TestMethod()>
    Public Overrides Sub SelectWithConditionalFullOuterJoinWithSpecifiedTableName()
      Try
        MyBase.SelectWithConditionalFullOuterJoinWithSpecifiedTableName()
        Assert.Fail()
      Catch ex As Exception
        If Not ex.Message.Contains("RIGHT and FULL OUTER JOINs are not currently supported") Then
          Assert.Fail(ex.Message)
        End If
      End Try
    End Sub

    <TestMethod()>
    Public Overrides Sub SelectWithConditionalRightOuterJoinWithSpecifiedTableName()
      Try
        MyBase.SelectWithConditionalRightOuterJoinWithSpecifiedTableName()
        Assert.Fail()
      Catch ex As Exception
        If Not ex.Message.Contains("RIGHT and FULL OUTER JOINs are not currently supported") Then
          Assert.Fail(ex.Message)
        End If
      End Try
    End Sub

  End Class
End Namespace