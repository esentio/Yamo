Imports System.Linq.Expressions
Imports Yamo.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Test.Model
Imports Yamo.Test.Tests

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

    Public Function GetNullableValue() As Int32?
      Return 406
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

    Private Function GetNullableValue() As Int32?
      Return 209
    End Function

    Private Shared Function GetStaticValue() As Int32
      Return 210
    End Function

    Private Shared Function GetStaticValue(param1 As Int32, param2 As Int32) As Int32
      Return 211 + param1 + param2
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

      Public Function GetNullableValue() As Int32?
        Return 309
      End Function

      Public Shared Function GetStaticValue() As Int32
        Return 310
      End Function

      Public Shared Function GetStaticValue(param1 As Int32, param2 As Int32) As Int32
        Return 311 + param1 + param2
      End Function

    End Class

    Private Class Class2

      Public Function GetTrueValue() As Boolean
        Return True
      End Function

      Public Function GetFalseValue() As Boolean
        Return False
      End Function

      Public Function GetNullableTrueValue() As Boolean?
        Return True
      End Function

      Public Function GetNullableFalseValue() As Boolean?
        Return False
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
        Assert.AreEqual("[T0].[IntColumn] = 100", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' local value (captured closure)
        Dim localValue = 101
        result = Translate(visitor, Function(x) x.IntColumn = localValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        ' --------------------------------------------------------------------------------------

        ' constant
        result = Translate(visitor, Function(x) x.IntColumn = ConstantValue)
        Assert.AreEqual("[T0].[IntColumn] = 200", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' field
        result = Translate(visitor, Function(x) x.IntColumn = m_FieldValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(m_FieldValue, result.Parameters(0).Value)

        ' static field
        result = Translate(visitor, Function(x) x.IntColumn = m_StaticFieldValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(m_StaticFieldValue, result.Parameters(0).Value)

        ' property
        result = Translate(visitor, Function(x) x.IntColumn = Me.PropertyValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Me.PropertyValue, result.Parameters(0).Value)

        ' static property
        result = Translate(visitor, Function(x) x.IntColumn = StaticPropertyValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(StaticPropertyValue, result.Parameters(0).Value)

        ' indexer property
        result = Translate(visitor, Function(x) x.IntColumn = Me.IndexerPropertyValue(10000))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Me.IndexerPropertyValue(10000), result.Parameters(0).Value)

        ' static indexer property
        result = Translate(visitor, Function(x) x.IntColumn = StaticIndexerPropertyValue(10000))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(StaticIndexerPropertyValue(10000), result.Parameters(0).Value)

        ' function
        result = Translate(visitor, Function(x) x.IntColumn = GetValue())
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetValue(), result.Parameters(0).Value)

        ' function with parameters
        Dim localParam = 10000
        result = Translate(visitor, Function(x) x.IntColumn = GetValue(10000, localParam))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetValue(10000, localParam), result.Parameters(0).Value)

        ' function with nullable return value
        result = Translate(visitor, Function(x) x.IntColumn = GetNullableValue().Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetNullableValue(), result.Parameters(0).Value)

        ' static function
        result = Translate(visitor, Function(x) x.IntColumn = GetStaticValue())
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetStaticValue(), result.Parameters(0).Value)

        ' static function with parameters
        result = Translate(visitor, Function(x) x.IntColumn = GetStaticValue(10000, localParam))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetStaticValue(10000, localParam), result.Parameters(0).Value)

        ' --------------------------------------------------------------------------------------

        Dim c = New Class1

        ' constant
        result = Translate(visitor, Function(x) x.IntColumn = Class1.ConstantValue)
        Assert.AreEqual("[T0].[IntColumn] = 300", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' field
        result = Translate(visitor, Function(x) x.IntColumn = c.FieldValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.FieldValue, result.Parameters(0).Value)

        ' static field
        result = Translate(visitor, Function(x) x.IntColumn = Class1.StaticFieldValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.StaticFieldValue, result.Parameters(0).Value)

        ' property
        result = Translate(visitor, Function(x) x.IntColumn = c.PropertyValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.PropertyValue, result.Parameters(0).Value)

        ' static property
        result = Translate(visitor, Function(x) x.IntColumn = Class1.StaticPropertyValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.StaticPropertyValue, result.Parameters(0).Value)

        ' indexer property
        result = Translate(visitor, Function(x) x.IntColumn = c.IndexerPropertyValue(10000))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.IndexerPropertyValue(10000), result.Parameters(0).Value)

        ' static indexer property
        result = Translate(visitor, Function(x) x.IntColumn = Class1.StaticIndexerPropertyValue(10000))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.StaticIndexerPropertyValue(10000), result.Parameters(0).Value)

        ' function
        result = Translate(visitor, Function(x) x.IntColumn = c.GetValue())
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetValue(), result.Parameters(0).Value)

        ' function with parameters
        result = Translate(visitor, Function(x) x.IntColumn = c.GetValue(10000, localParam))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetValue(10000, localParam), result.Parameters(0).Value)

        ' function with nullable return value
        result = Translate(visitor, Function(x) x.IntColumn = c.GetNullableValue().Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValue(), result.Parameters(0).Value)

        ' static function
        result = Translate(visitor, Function(x) x.IntColumn = Class1.GetStaticValue())
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.GetStaticValue(), result.Parameters(0).Value)

        ' static function with parameters
        result = Translate(visitor, Function(x) x.IntColumn = Class1.GetStaticValue(10000, localParam))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.GetStaticValue(10000, localParam), result.Parameters(0).Value)

        ' --------------------------------------------------------------------------------------

        ' constant
        result = Translate(visitor, Function(x) x.IntColumn = Module1.ConstantValue)
        Assert.AreEqual("[T0].[IntColumn] = 400", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' field
        result = Translate(visitor, Function(x) x.IntColumn = Module1.FieldValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.FieldValue, result.Parameters(0).Value)

        ' property
        result = Translate(visitor, Function(x) x.IntColumn = Module1.PropertyValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.PropertyValue, result.Parameters(0).Value)

        ' indexer property
        result = Translate(visitor, Function(x) x.IntColumn = Module1.IndexerPropertyValue(10000))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.IndexerPropertyValue(10000), result.Parameters(0).Value)

        ' function
        result = Translate(visitor, Function(x) x.IntColumn = Module1.GetValue())
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.GetValue(), result.Parameters(0).Value)

        ' function with parameters
        result = Translate(visitor, Function(x) x.IntColumn = Module1.GetValue(10000, localParam))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.GetValue(10000, localParam), result.Parameters(0).Value)

        ' function with nullable return value
        result = Translate(visitor, Function(x) x.IntColumn = Module1.GetNullableValue().Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.GetNullableValue(), result.Parameters(0).Value)

        ' --------------------------------------------------------------------------------------

        ' SQL helper
        result = Translate(visitor, Function(x) Sql.Exp.Raw(Of Int32)("IntColumn") = 100)
        Assert.AreEqual("IntColumn = 100", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

      End Using
    End Sub

    <TestMethod()>
    Public Sub TranslateBoolean()
      Using db = CreateDbContext()
        Dim visitor = CreateSqlExpressionVisitor(db)
        Dim result As SqlString

        Dim localValue As Boolean
        Dim c = New Class2

        result = Translate(visitor, Function(x) x.BitColumn = True)
        Assert.AreEqual("[T0].[BitColumn] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) j.T1.BitColumn = True)
        Assert.AreEqual("[T0].[BitColumn] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = Translate(visitor, Function(x) x.BitColumn = localValue)
        Assert.AreEqual("[T0].[BitColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) x.BitColumn = c.GetTrueValue())
        Assert.AreEqual("[T0].[BitColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetTrueValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) x.BitColumn = Sql.Exp.Raw(Of Boolean)("1"))
        Assert.AreEqual("[T0].[BitColumn] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) x.BitColumn = False)
        Assert.AreEqual("[T0].[BitColumn] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) j.T1.BitColumn = False)
        Assert.AreEqual("[T0].[BitColumn] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = False
        result = Translate(visitor, Function(x) x.BitColumn = localValue)
        Assert.AreEqual("[T0].[BitColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) x.BitColumn = c.GetFalseValue())
        Assert.AreEqual("[T0].[BitColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetFalseValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) x.BitColumn = Sql.Exp.Raw(Of Boolean)("0"))
        Assert.AreEqual("[T0].[BitColumn] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) Not x.BitColumn = True)
        Assert.AreEqual("NOT [T0].[BitColumn] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) Not j.T1.BitColumn = True)
        Assert.AreEqual("NOT [T0].[BitColumn] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = Translate(visitor, Function(x) Not x.BitColumn = localValue)
        Assert.AreEqual("NOT [T0].[BitColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not x.BitColumn = c.GetTrueValue())
        Assert.AreEqual("NOT [T0].[BitColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetTrueValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not x.BitColumn = Sql.Exp.Raw(Of Boolean)("1"))
        Assert.AreEqual("NOT [T0].[BitColumn] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) Not x.BitColumn = False)
        Assert.AreEqual("NOT [T0].[BitColumn] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) Not j.T1.BitColumn = False)
        Assert.AreEqual("NOT [T0].[BitColumn] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = False
        result = Translate(visitor, Function(x) Not x.BitColumn = localValue)
        Assert.AreEqual("NOT [T0].[BitColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not x.BitColumn = c.GetFalseValue())
        Assert.AreEqual("NOT [T0].[BitColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetFalseValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not x.BitColumn = Sql.Exp.Raw(Of Boolean)("0"))
        Assert.AreEqual("NOT [T0].[BitColumn] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) x.BitColumn <> True)
        Assert.AreEqual("[T0].[BitColumn] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) j.T1.BitColumn <> True)
        Assert.AreEqual("[T0].[BitColumn] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = Translate(visitor, Function(x) x.BitColumn <> localValue)
        Assert.AreEqual("[T0].[BitColumn] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) x.BitColumn <> c.GetTrueValue())
        Assert.AreEqual("[T0].[BitColumn] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetTrueValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) x.BitColumn <> Sql.Exp.Raw(Of Boolean)("1"))
        Assert.AreEqual("[T0].[BitColumn] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) x.BitColumn <> False)
        Assert.AreEqual("[T0].[BitColumn] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) j.T1.BitColumn <> False)
        Assert.AreEqual("[T0].[BitColumn] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = False
        result = Translate(visitor, Function(x) x.BitColumn <> localValue)
        Assert.AreEqual("[T0].[BitColumn] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) x.BitColumn <> c.GetFalseValue())
        Assert.AreEqual("[T0].[BitColumn] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetFalseValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) x.BitColumn <> Sql.Exp.Raw(Of Boolean)("0"))
        Assert.AreEqual("[T0].[BitColumn] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) Not x.BitColumn <> True)
        Assert.AreEqual("NOT [T0].[BitColumn] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) Not j.T1.BitColumn <> True)
        Assert.AreEqual("NOT [T0].[BitColumn] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = Translate(visitor, Function(x) Not x.BitColumn <> localValue)
        Assert.AreEqual("NOT [T0].[BitColumn] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not x.BitColumn <> c.GetTrueValue())
        Assert.AreEqual("NOT [T0].[BitColumn] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetTrueValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not x.BitColumn <> Sql.Exp.Raw(Of Boolean)("1"))
        Assert.AreEqual("NOT [T0].[BitColumn] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) Not x.BitColumn <> False)
        Assert.AreEqual("NOT [T0].[BitColumn] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) Not j.T1.BitColumn <> False)
        Assert.AreEqual("NOT [T0].[BitColumn] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = False
        result = Translate(visitor, Function(x) Not x.BitColumn <> localValue)
        Assert.AreEqual("NOT [T0].[BitColumn] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not x.BitColumn <> c.GetFalseValue())
        Assert.AreEqual("NOT [T0].[BitColumn] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetFalseValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not x.BitColumn <> Sql.Exp.Raw(Of Boolean)("0"))
        Assert.AreEqual("NOT [T0].[BitColumn] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) x.BitColumn)
        Assert.AreEqual("[T0].[BitColumn] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) j.T1.BitColumn)
        Assert.AreEqual("[T0].[BitColumn] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = Translate(visitor, Function(x) localValue)
        Assert.AreEqual("@p0 = 1", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) c.GetTrueValue())
        Assert.AreEqual("@p0 = 1", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetTrueValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Sql.Exp.Raw(Of Boolean)("BitColumn = 1"))
        Assert.AreEqual("BitColumn = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) Not x.BitColumn)
        Assert.AreEqual("[T0].[BitColumn] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) Not j.T1.BitColumn)
        Assert.AreEqual("[T0].[BitColumn] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = Translate(visitor, Function(x) Not localValue)
        Assert.AreEqual("NOT @p0 = 1", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not c.GetTrueValue())
        Assert.AreEqual("NOT @p0 = 1", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetTrueValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not Sql.Exp.Raw(Of Boolean)("BitColumn = 1"))
        Assert.AreEqual("NOT BitColumn = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) x.BitColumn = True AndAlso Not x.BitColumn = True And x.BitColumn OrElse Not x.BitColumn Or x.BitColumn)
        Assert.AreEqual("(((([T0].[BitColumn] = 1 AND NOT [T0].[BitColumn] = 1) AND [T0].[BitColumn] = 1) OR [T0].[BitColumn] = 0) OR [T0].[BitColumn] = 1)", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) j.T1.BitColumn = True AndAlso Not j.T1.BitColumn = True And j.T1.BitColumn OrElse Not j.T1.BitColumn Or j.T1.BitColumn)
        Assert.AreEqual("(((([T0].[BitColumn] = 1 AND NOT [T0].[BitColumn] = 1) AND [T0].[BitColumn] = 1) OR [T0].[BitColumn] = 0) OR [T0].[BitColumn] = 1)", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        localValue = True
        result = Translate(visitor, Function(x) x.BitColumn = localValue AndAlso c.GetTrueValue() OrElse Not Sql.Exp.Raw(Of Boolean)("BitColumn = 1"))
        Assert.AreEqual("(([T0].[BitColumn] = @p0 AND @p1 = 1) OR NOT BitColumn = 1)", result.Sql)
        Assert.AreEqual(2, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)
        Assert.AreEqual(c.GetTrueValue(), result.Parameters(1).Value)

      End Using
    End Sub

    <TestMethod()>
    Public Sub TranslateNullableValue()
      Using db = CreateDbContext()
        Dim visitor = CreateSqlExpressionVisitor(db)
        Dim result As SqlString

        Dim localValue As Boolean?
        Dim c = New Class2

        result = Translate(visitor, Function(x) x.BitColumnNull.Value = True)
        Assert.AreEqual("[T0].[BitColumnNull] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) j.T1.BitColumnNull.Value = True)
        Assert.AreEqual("[T0].[BitColumnNull] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = Translate(visitor, Function(x) x.BitColumnNull.Value = localValue.Value)
        Assert.AreEqual("[T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) x.BitColumnNull.Value = c.GetNullableTrueValue().Value)
        Assert.AreEqual("[T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableTrueValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) x.BitColumnNull.Value = Sql.Exp.Raw(Of Boolean?)("1").Value)
        Assert.AreEqual("[T0].[BitColumnNull] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) x.BitColumnNull.Value = False)
        Assert.AreEqual("[T0].[BitColumnNull] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) j.T1.BitColumnNull.Value = False)
        Assert.AreEqual("[T0].[BitColumnNull] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = False
        result = Translate(visitor, Function(x) x.BitColumnNull.Value = localValue.Value)
        Assert.AreEqual("[T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) x.BitColumnNull.Value = c.GetNullableFalseValue().Value)
        Assert.AreEqual("[T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableFalseValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) x.BitColumnNull.Value = Sql.Exp.Raw(Of Boolean?)("0").Value)
        Assert.AreEqual("[T0].[BitColumnNull] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) Not x.BitColumnNull.Value = True)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) Not j.T1.BitColumnNull.Value = True)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = Translate(visitor, Function(x) Not x.BitColumnNull.Value = localValue.Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not x.BitColumnNull.Value = c.GetNullableTrueValue().Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableTrueValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not x.BitColumnNull.Value = Sql.Exp.Raw(Of Boolean?)("1").Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) Not x.BitColumnNull.Value = False)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) Not j.T1.BitColumnNull.Value = False)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = False
        result = Translate(visitor, Function(x) Not x.BitColumnNull.Value = localValue.Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not x.BitColumnNull.Value = c.GetNullableFalseValue().Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableFalseValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not x.BitColumnNull.Value = Sql.Exp.Raw(Of Boolean?)("0").Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) x.BitColumnNull.Value <> True)
        Assert.AreEqual("[T0].[BitColumnNull] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) j.T1.BitColumnNull.Value <> True)
        Assert.AreEqual("[T0].[BitColumnNull] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = Translate(visitor, Function(x) x.BitColumnNull.Value <> localValue.Value)
        Assert.AreEqual("[T0].[BitColumnNull] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) x.BitColumnNull.Value <> c.GetNullableTrueValue().Value)
        Assert.AreEqual("[T0].[BitColumnNull] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableTrueValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) x.BitColumnNull.Value <> Sql.Exp.Raw(Of Boolean?)("1").Value)
        Assert.AreEqual("[T0].[BitColumnNull] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) x.BitColumnNull.Value <> False)
        Assert.AreEqual("[T0].[BitColumnNull] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) j.T1.BitColumnNull.Value <> False)
        Assert.AreEqual("[T0].[BitColumnNull] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = False
        result = Translate(visitor, Function(x) x.BitColumnNull.Value <> localValue.Value)
        Assert.AreEqual("[T0].[BitColumnNull] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) x.BitColumnNull.Value <> c.GetNullableFalseValue().Value)
        Assert.AreEqual("[T0].[BitColumnNull] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableFalseValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) x.BitColumnNull.Value <> Sql.Exp.Raw(Of Boolean?)("0").Value)
        Assert.AreEqual("[T0].[BitColumnNull] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) Not x.BitColumnNull.Value <> True)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) Not j.T1.BitColumnNull.Value <> True)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = Translate(visitor, Function(x) Not x.BitColumnNull.Value <> localValue.Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not x.BitColumnNull.Value <> c.GetNullableTrueValue().Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableTrueValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not x.BitColumnNull.Value <> Sql.Exp.Raw(Of Boolean?)("1").Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) Not x.BitColumnNull.Value <> False)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) Not j.T1.BitColumnNull.Value <> False)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = False
        result = Translate(visitor, Function(x) Not x.BitColumnNull.Value <> localValue.Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not x.BitColumnNull.Value <> c.GetNullableFalseValue().Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableFalseValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not x.BitColumnNull.Value <> Sql.Exp.Raw(Of Boolean?)("0").Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) x.BitColumnNull.Value)
        Assert.AreEqual("[T0].[BitColumnNull] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) j.T1.BitColumnNull.Value)
        Assert.AreEqual("[T0].[BitColumnNull] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = Translate(visitor, Function(x) localValue.Value)
        Assert.AreEqual("@p0 = 1", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) c.GetNullableTrueValue().Value)
        Assert.AreEqual("@p0 = 1", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableTrueValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Sql.Exp.Raw(Of Boolean?)("BitColumnNull = 1").Value)
        Assert.AreEqual("BitColumnNull = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) Not x.BitColumnNull.Value)
        Assert.AreEqual("[T0].[BitColumnNull] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) Not j.T1.BitColumnNull.Value)
        Assert.AreEqual("[T0].[BitColumnNull] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = Translate(visitor, Function(x) Not localValue.Value)
        Assert.AreEqual("NOT @p0 = 1", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not c.GetNullableTrueValue().Value)
        Assert.AreEqual("NOT @p0 = 1", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableTrueValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not Sql.Exp.Raw(Of Boolean?)("BitColumnNull = 1").Value)
        Assert.AreEqual("NOT BitColumnNull = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) x.BitColumnNull.Value = True AndAlso Not x.BitColumnNull.Value = True And x.BitColumnNull.Value OrElse Not x.BitColumnNull.Value Or x.BitColumnNull.Value)
        Assert.AreEqual("(((([T0].[BitColumnNull] = 1 AND NOT [T0].[BitColumnNull] = 1) AND [T0].[BitColumnNull] = 1) OR [T0].[BitColumnNull] = 0) OR [T0].[BitColumnNull] = 1)", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) j.T1.BitColumnNull.Value = True AndAlso Not j.T1.BitColumnNull.Value = True And j.T1.BitColumnNull.Value OrElse Not j.T1.BitColumnNull.Value Or j.T1.BitColumnNull.Value)
        Assert.AreEqual("(((([T0].[BitColumnNull] = 1 AND NOT [T0].[BitColumnNull] = 1) AND [T0].[BitColumnNull] = 1) OR [T0].[BitColumnNull] = 0) OR [T0].[BitColumnNull] = 1)", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        localValue = True
        result = Translate(visitor, Function(x) x.BitColumnNull.Value = localValue.Value AndAlso c.GetNullableTrueValue().Value OrElse Not Sql.Exp.Raw(Of Boolean?)("BitColumnNull = 1").Value)
        Assert.AreEqual("(([T0].[BitColumnNull] = @p0 AND @p1 = 1) OR NOT BitColumnNull = 1)", result.Sql)
        Assert.AreEqual(2, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)
        Assert.AreEqual(c.GetNullableTrueValue(), result.Parameters(1).Value)

      End Using
    End Sub

    <TestMethod()>
    Public Sub TranslateNullableHasValue()
      Using db = CreateDbContext()
        Dim visitor = CreateSqlExpressionVisitor(db)
        Dim result As SqlString

        Dim localValue As Boolean?
        Dim c = New Class2

        result = Translate(visitor, Function(x) x.BitColumnNull.HasValue)
        Assert.AreEqual("[T0].[BitColumnNull] IS NOT NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) j.T1.BitColumnNull.HasValue)
        Assert.AreEqual("[T0].[BitColumnNull] IS NOT NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = Translate(visitor, Function(x) localValue.HasValue)
        Assert.AreEqual("@p0 IS NOT NULL", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) c.GetNullableTrueValue().HasValue)
        Assert.AreEqual("@p0 IS NOT NULL", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableTrueValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Sql.Exp.Raw(Of Boolean?)("BitColumnNull").HasValue)
        Assert.AreEqual("BitColumnNull IS NOT NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) Not x.BitColumnNull.HasValue)
        Assert.AreEqual("[T0].[BitColumnNull] IS NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) Not j.T1.BitColumnNull.HasValue)
        Assert.AreEqual("[T0].[BitColumnNull] IS NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = Translate(visitor, Function(x) Not localValue.HasValue)
        Assert.AreEqual("@p0 IS NULL", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not c.GetNullableTrueValue().HasValue)
        Assert.AreEqual("@p0 IS NULL", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableTrueValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Not Sql.Exp.Raw(Of Boolean?)("BitColumnNull").HasValue)
        Assert.AreEqual("BitColumnNull IS NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        localValue = True
        result = Translate(visitor, Function(x) x.BitColumnNull.HasValue AndAlso Not x.BitColumnNull.HasValue And localValue.HasValue OrElse Not c.GetNullableTrueValue().HasValue Or Sql.Exp.Raw(Of Boolean?)("BitColumnNull").HasValue)
        Assert.AreEqual("(((([T0].[BitColumnNull] IS NOT NULL AND [T0].[BitColumnNull] IS NULL) AND @p0 IS NOT NULL) OR @p1 IS NULL) OR BitColumnNull IS NOT NULL)", result.Sql)
        Assert.AreEqual(2, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)
        Assert.AreEqual(c.GetNullableTrueValue(), result.Parameters(1).Value)

      End Using
    End Sub

    <TestMethod()>
    Public Sub TranslateNullableIsOrIsNotNothing()
      Using db = CreateDbContext()
        Dim visitor = CreateSqlExpressionVisitor(db)
        Dim result As SqlString

        Dim localValue As Boolean?
        Dim c = New Class2

        result = Translate(visitor, Function(x) x.BitColumnNull Is Nothing)
        Assert.AreEqual("[T0].[BitColumnNull] IS NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) j.T1.BitColumnNull Is Nothing)
        Assert.AreEqual("[T0].[BitColumnNull] IS NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = Translate(visitor, Function(x) localValue Is Nothing)
        Assert.AreEqual("@p0 IS NULL", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) c.GetNullableTrueValue() Is Nothing)
        Assert.AreEqual("@p0 IS NULL", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableTrueValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Sql.Exp.Raw(Of Boolean?)("BitColumnNull") Is Nothing)
        Assert.AreEqual("BitColumnNull IS NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = Translate(visitor, Function(x) x.BitColumnNull IsNot Nothing)
        Assert.AreEqual("[T0].[BitColumnNull] IS NOT NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateJoin(visitor, Function(j) j.T1.BitColumnNull IsNot Nothing)
        Assert.AreEqual("[T0].[BitColumnNull] IS NOT NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = Translate(visitor, Function(x) localValue IsNot Nothing)
        Assert.AreEqual("@p0 IS NOT NULL", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = Translate(visitor, Function(x) c.GetNullableTrueValue() IsNot Nothing)
        Assert.AreEqual("@p0 IS NOT NULL", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableTrueValue(), result.Parameters(0).Value)

        result = Translate(visitor, Function(x) Sql.Exp.Raw(Of Boolean?)("BitColumnNull") IsNot Nothing)
        Assert.AreEqual("BitColumnNull IS NOT NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        localValue = True
        result = Translate(visitor, Function(x) x.BitColumnNull Is Nothing AndAlso x.BitColumnNull IsNot Nothing And localValue Is Nothing OrElse c.GetNullableTrueValue() IsNot Nothing Or Sql.Exp.Raw(Of Boolean?)("BitColumnNull") Is Nothing)
        Assert.AreEqual("(((([T0].[BitColumnNull] IS NULL AND [T0].[BitColumnNull] IS NOT NULL) AND @p0 IS NULL) OR @p1 IS NOT NULL) OR BitColumnNull IS NULL)", result.Sql)
        Assert.AreEqual(2, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)
        Assert.AreEqual(c.GetNullableTrueValue(), result.Parameters(1).Value)

      End Using
    End Sub

    Private Function CreateSqlExpressionVisitor(db As DbContext) As SqlExpressionVisitor
      Dim builder = New SelectSqlExpressionBuilder(db)
      builder.SetMainTable(Of ItemWithAllSupportedValues)()

      Dim fi = builder.GetType().GetField("m_Visitor", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
      Return DirectCast(fi.GetValue(builder), SqlExpressionVisitor)
    End Function

    Public Function Translate(visitor As SqlExpressionVisitor, predicate As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean))) As SqlString
      Return visitor.Translate(predicate, {0}, 0, True, True)
    End Function

    Public Function TranslateJoin(visitor As SqlExpressionVisitor, predicate As Expression(Of Func(Of Join(Of ItemWithAllSupportedValues, Article), Boolean))) As SqlString
      Return visitor.Translate(predicate, Nothing, 0, True, True)
    End Function

  End Class
End Namespace
