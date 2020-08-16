Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithTableSourceTests
    Inherits Yamo.Test.Tests.SelectWithTableSourceTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

    <TestMethod()>
    Public Overrides Sub SelectWithRightOuterJoinWithSpecifiedTableSourceUsingFormattableSqlString()
      Try
        MyBase.SelectWithRightOuterJoinWithSpecifiedTableSourceUsingFormattableSqlString()
        Assert.Fail()
      Catch ex As Exception
        If Not ex.Message.Contains("RIGHT and FULL OUTER JOINs are not currently supported") Then
          Assert.Fail(ex.Message)
        End If
      End Try
    End Sub

    <TestMethod()>
    Public Overrides Sub SelectWithRightOuterJoinWithSpecifiedTableNameUsingRawSqlString()
      Try
        MyBase.SelectWithRightOuterJoinWithSpecifiedTableNameUsingRawSqlString()
        Assert.Fail()
      Catch ex As Exception
        If Not ex.Message.Contains("RIGHT and FULL OUTER JOINs are not currently supported") Then
          Assert.Fail(ex.Message)
        End If
      End Try
    End Sub

    <TestMethod()>
    Public Overrides Sub SelectWithRightOuterJoinWithSpecifiedTableSourceUsingRawSqlStringWithParameters()
      Try
        MyBase.SelectWithRightOuterJoinWithSpecifiedTableSourceUsingRawSqlStringWithParameters()
        Assert.Fail()
      Catch ex As Exception
        If Not ex.Message.Contains("RIGHT and FULL OUTER JOINs are not currently supported") Then
          Assert.Fail(ex.Message)
        End If
      End Try
    End Sub

    <TestMethod()>
    Public Overrides Sub SelectWithFullOuterJoinWithSpecifiedTableSourceUsingFormattableSqlString()
      Try
        MyBase.SelectWithFullOuterJoinWithSpecifiedTableSourceUsingFormattableSqlString()
        Assert.Fail()
      Catch ex As Exception
        If Not ex.Message.Contains("RIGHT and FULL OUTER JOINs are not currently supported") Then
          Assert.Fail(ex.Message)
        End If
      End Try
    End Sub

    <TestMethod()>
    Public Overrides Sub SelectWithFullOuterJoinWithSpecifiedTableNameUsingRawSqlString()
      Try
        MyBase.SelectWithFullOuterJoinWithSpecifiedTableNameUsingRawSqlString()
        Assert.Fail()
      Catch ex As Exception
        If Not ex.Message.Contains("RIGHT and FULL OUTER JOINs are not currently supported") Then
          Assert.Fail(ex.Message)
        End If
      End Try
    End Sub

    <TestMethod()>
    Public Overrides Sub SelectWithFullOuterJoinWithSpecifiedTableSourceUsingRawSqlStringWithParameters()
      Try
        MyBase.SelectWithFullOuterJoinWithSpecifiedTableSourceUsingRawSqlStringWithParameters()
        Assert.Fail()
      Catch ex As Exception
        If Not ex.Message.Contains("RIGHT and FULL OUTER JOINs are not currently supported") Then
          Assert.Fail(ex.Message)
        End If
      End Try
    End Sub

  End Class
End Namespace