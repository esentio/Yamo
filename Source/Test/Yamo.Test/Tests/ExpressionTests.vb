Imports System.Linq.Expressions
Imports Yamo.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Test.Model

Namespace Tests

  Module Module1

    Public Const ConstantValue As Int32 = 400

    Public FieldValue As Int32 = 401

    Public Property PropertyValue As Int32 = 402

    Public Property IndexerPropertyValue(index As Int32) As Int32
      Get
        Return 403 + index
      End Get
      Set(ByVal value As Int32)
      End Set
    End Property

    Public Function GetValue() As Int32
      Return 404
    End Function

    Public Function GetValue(param1 As Int32, param2 As Int32) As Int32
      Return 405 + param1 + param2
    End Function

  End Module

  <TestClass()>
  Public Class ExpressionTests
    Inherits BaseUnitTests

    Private Const ConstantValue As Int32 = 200

    Private m_FieldValue As Int32 = 201

    Private Shared m_StaticFieldValue As Int32 = 202

    Private Property PropertyValue As Int32 = 203

    Private Shared Property StaticPropertyValue As Int32 = 204

    Public Property IndexerPropertyValue(index As Int32) As Int32
      Get
        Return 205 + index
      End Get
      Set(ByVal value As Int32)
      End Set
    End Property

    Public Shared Property StaticIndexerPropertyValue(index As Int32) As Int32
      Get
        Return 206 + index
      End Get
      Set(ByVal value As Int32)
      End Set
    End Property

    Private Function GetValue() As Int32
      Return 207
    End Function

    Private Function GetValue(param1 As Int32, param2 As Int32) As Int32
      Return 208 + param1 + param2
    End Function

    Private Shared Function GetStaticValue() As Int32
      Return 209
    End Function

    Private Shared Function GetStaticValue(param1 As Int32, param2 As Int32) As Int32
      Return 210 + param1 + param2
    End Function

    Private Class Class1

      Public Const ConstantValue As Int32 = 300

      Public FieldValue As Int32 = 301

      Public Shared StaticFieldValue As Int32 = 302

      Public Property PropertyValue As Int32 = 303

      Public Shared Property StaticPropertyValue As Int32 = 304

      Public Property IndexerPropertyValue(index As Int32) As Int32
        Get
          Return 305 + index
        End Get
        Set(ByVal value As Int32)
        End Set
      End Property

      Public Shared Property StaticIndexerPropertyValue(index As Int32) As Int32
        Get
          Return 306 + index
        End Get
        Set(ByVal value As Int32)
        End Set
      End Property

      Public Function GetValue() As Int32
        Return 307
      End Function

      Public Function GetValue(param1 As Int32, param2 As Int32) As Int32
        Return 308 + param1 + param2
      End Function

      Public Shared Function GetStaticValue() As Int32
        Return 309
      End Function

      Public Shared Function GetStaticValue(param1 As Int32, param2 As Int32) As Int32
        Return 310 + param1 + param2
      End Function

    End Class

    <TestMethod()>
    Public Sub EvaluateExpression()
      ' tests evaluation of various values

      Using db = CreateDbContext()
        Dim visitor = CreateSqlExpressionVisitor(db)
        Dim result As SqlString

        ' constant
        result = Translate(visitor, Function(x) x.IntColumn = 100)
        Assert.AreEqual(0, result.Parameters.Count)

        ' local value (captured closure)
        Dim localValue = 101
        result = Translate(visitor, Function(x) x.IntColumn = localValue)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        ' --------------------------------------------------------------------------------------

        ' constant
        result = Translate(visitor, Function(x) x.IntColumn = ConstantValue)
        Assert.AreEqual(0, result.Parameters.Count)

        ' field
        result = Translate(visitor, Function(x) x.IntColumn = m_FieldValue)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(m_FieldValue, result.Parameters(0).Value)

        ' static field
        result = Translate(visitor, Function(x) x.IntColumn = m_StaticFieldValue)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(m_StaticFieldValue, result.Parameters(0).Value)

        ' property
        result = Translate(visitor, Function(x) x.IntColumn = Me.PropertyValue)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Me.PropertyValue, result.Parameters(0).Value)

        ' static property
        result = Translate(visitor, Function(x) x.IntColumn = StaticPropertyValue)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(StaticPropertyValue, result.Parameters(0).Value)

        ' indexer property
        result = Translate(visitor, Function(x) x.IntColumn = Me.IndexerPropertyValue(10000))
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Me.IndexerPropertyValue(10000), result.Parameters(0).Value)

        ' static indexer property
        result = Translate(visitor, Function(x) x.IntColumn = StaticIndexerPropertyValue(10000))
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(StaticIndexerPropertyValue(10000), result.Parameters(0).Value)

        ' function
        result = Translate(visitor, Function(x) x.IntColumn = GetValue())
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetValue(), result.Parameters(0).Value)

        ' function with parameters
        Dim localParam = 10000
        result = Translate(visitor, Function(x) x.IntColumn = GetValue(10000, localParam))
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetValue(10000, localParam), result.Parameters(0).Value)

        ' static function
        result = Translate(visitor, Function(x) x.IntColumn = GetStaticValue())
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetStaticValue(), result.Parameters(0).Value)

        ' static function with parameters
        result = Translate(visitor, Function(x) x.IntColumn = GetStaticValue(10000, localParam))
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetStaticValue(10000, localParam), result.Parameters(0).Value)

        ' --------------------------------------------------------------------------------------

        Dim c = New Class1

        ' constant
        result = Translate(visitor, Function(x) x.IntColumn = Class1.ConstantValue)
        Assert.AreEqual(0, result.Parameters.Count)

        ' field
        result = Translate(visitor, Function(x) x.IntColumn = c.FieldValue)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.FieldValue, result.Parameters(0).Value)

        ' static field
        result = Translate(visitor, Function(x) x.IntColumn = Class1.StaticFieldValue)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.StaticFieldValue, result.Parameters(0).Value)

        ' property
        result = Translate(visitor, Function(x) x.IntColumn = c.PropertyValue)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.PropertyValue, result.Parameters(0).Value)

        ' static property
        result = Translate(visitor, Function(x) x.IntColumn = Class1.StaticPropertyValue)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.StaticPropertyValue, result.Parameters(0).Value)

        ' indexer property
        result = Translate(visitor, Function(x) x.IntColumn = c.IndexerPropertyValue(10000))
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.IndexerPropertyValue(10000), result.Parameters(0).Value)

        ' static indexer property
        result = Translate(visitor, Function(x) x.IntColumn = Class1.StaticIndexerPropertyValue(10000))
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.StaticIndexerPropertyValue(10000), result.Parameters(0).Value)

        ' function
        result = Translate(visitor, Function(x) x.IntColumn = c.GetValue())
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetValue(), result.Parameters(0).Value)

        ' function with parameters
        result = Translate(visitor, Function(x) x.IntColumn = c.GetValue(10000, localParam))
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetValue(10000, localParam), result.Parameters(0).Value)

        ' static function
        result = Translate(visitor, Function(x) x.IntColumn = Class1.GetStaticValue())
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.GetStaticValue(), result.Parameters(0).Value)

        ' static function with parameters
        result = Translate(visitor, Function(x) x.IntColumn = Class1.GetStaticValue(10000, localParam))
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.GetStaticValue(10000, localParam), result.Parameters(0).Value)

        ' --------------------------------------------------------------------------------------

        ' constant
        result = Translate(visitor, Function(x) x.IntColumn = Module1.ConstantValue)
        Assert.AreEqual(0, result.Parameters.Count)

        ' field
        result = Translate(visitor, Function(x) x.IntColumn = Module1.FieldValue)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.FieldValue, result.Parameters(0).Value)

        ' property
        result = Translate(visitor, Function(x) x.IntColumn = Module1.PropertyValue)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.PropertyValue, result.Parameters(0).Value)

        ' indexer property
        result = Translate(visitor, Function(x) x.IntColumn = Module1.IndexerPropertyValue(10000))
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.IndexerPropertyValue(10000), result.Parameters(0).Value)

        ' function
        result = Translate(visitor, Function(x) x.IntColumn = Module1.GetValue())
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.GetValue(), result.Parameters(0).Value)

        ' function with parameters
        result = Translate(visitor, Function(x) x.IntColumn = Module1.GetValue(10000, localParam))
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.GetValue(10000, localParam), result.Parameters(0).Value)

      End Using
    End Sub

    Private Function CreateSqlExpressionVisitor(db As DbContext) As SqlExpressionVisitor
      Dim builder = New SelectSqlExpressionBuilder(db)
      builder.SetMainTable(Of ItemWithAllSupportedValues)()

      Dim fi = builder.GetType().GetField("m_Visitor", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
      Return DirectCast(fi.GetValue(builder), SqlExpressionVisitor)
    End Function

    Public Function Translate(visitor As SqlExpressionVisitor, predicate As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean))) As SqlString
      Return visitor.Translate(predicate, ExpressionParametersType.Entities, {0}, 0, True, True)
    End Function

  End Class
End Namespace
