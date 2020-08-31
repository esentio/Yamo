Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class SelectWithJoinTests
    Inherits Yamo.Test.Tests.SelectWithJoinTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

    <TestMethod()>
    Public Overrides Sub SelectWith1To1RelationshipUsingRightOuterJoin()
      Try
        MyBase.SelectWith1To1RelationshipUsingRightOuterJoin()
        Assert.Fail()
      Catch ex As Exception
        If Not ex.Message.Contains("RIGHT and FULL OUTER JOINs are not currently supported") Then
          Assert.Fail(ex.Message)
        End If
      End Try
    End Sub

    <TestMethod()>
    Public Overrides Sub SelectWith1To1RelationshipUsingFullOuterJoin()
      Try
        MyBase.SelectWith1To1RelationshipUsingFullOuterJoin()
        Assert.Fail()
      Catch ex As Exception
        If Not ex.Message.Contains("RIGHT and FULL OUTER JOINs are not currently supported") Then
          Assert.Fail(ex.Message)
        End If
      End Try
    End Sub

    <TestMethod()>
    Public Overrides Sub SelectWith1ToNRelationshipUsingRightOuterJoin()
      Try
        MyBase.SelectWith1ToNRelationshipUsingRightOuterJoin()
        Assert.Fail()
      Catch ex As Exception
        If Not ex.Message.Contains("RIGHT and FULL OUTER JOINs are not currently supported") Then
          Assert.Fail(ex.Message)
        End If
      End Try
    End Sub

    <TestMethod()>
    Public Overrides Sub SelectWith1ToNRelationshipUsingFullOuterJoin()
      Try
        MyBase.SelectWith1ToNRelationshipUsingFullOuterJoin()
        Assert.Fail()
      Catch ex As Exception
        If Not ex.Message.Contains("RIGHT and FULL OUTER JOINs are not currently supported") Then
          Assert.Fail(ex.Message)
        End If
      End Try
    End Sub

  End Class
End Namespace