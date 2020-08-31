Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class InsertTests
    Inherits Yamo.Test.Tests.InsertTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

    <TestMethod()>
    Public Overrides Sub InsertRecordWithDefaultValueIdAndUseDbIdentityAndDefaults()
      Assert.ThrowsException(Of NotSupportedException)(AddressOf MyBase.InsertRecordWithDefaultValueIdAndUseDbIdentityAndDefaults)
    End Sub

    <TestMethod()>
    Public Overrides Sub InsertRecordWithDefaultValueIdAndUseDbIdentityAndDefaultsWithSpecifiedTableName()
      Assert.ThrowsException(Of NotSupportedException)(AddressOf MyBase.InsertRecordWithDefaultValueIdAndUseDbIdentityAndDefaultsWithSpecifiedTableName)
    End Sub

    <TestMethod()>
    Public Overrides Sub InsertRecordWithIdentityIdAndDefaultValuesAndUseDbIdentityAndDefaults()
      Assert.ThrowsException(Of NotSupportedException)(AddressOf MyBase.InsertRecordWithIdentityIdAndDefaultValuesAndUseDbIdentityAndDefaults)
    End Sub

    <TestMethod()>
    Public Overrides Sub InsertRecordWithIdentityIdAndDefaultValuesAndUseDbIdentityAndDefaultsWithSpecifiedTableName()
      Assert.ThrowsException(Of NotSupportedException)(AddressOf MyBase.InsertRecordWithIdentityIdAndDefaultValuesAndUseDbIdentityAndDefaultsWithSpecifiedTableName)
    End Sub

  End Class
End Namespace