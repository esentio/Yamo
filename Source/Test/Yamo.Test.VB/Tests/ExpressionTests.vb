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

    Public FieldNullableValue As Int32? = 402

    Public FieldNullableValueNull As Int32? = Nothing

    Public Property PropertyValue As Int32 = 403

    Public Property PropertyNullableValue As Int32? = 404

    Public Property PropertyNullableValueNull As Int32? = Nothing

    Public Property IndexerPropertyValue(index As Int32) As Int32
      Get
        Return 405 + index
      End Get
      Set(ByVal value As Int32)
      End Set
    End Property

    Public Property IndexerPropertyNullableValue(index As Int32) As Int32?
      Get
        Return 406 + index
      End Get
      Set(ByVal value As Int32?)
      End Set
    End Property

    Public Property IndexerPropertyNullableValueNull(index As Int32) As Int32?
      Get
        Return Nothing
      End Get
      Set(ByVal value As Int32?)
      End Set
    End Property

    Public Function GetValue() As Int32
      Return 407
    End Function

    Public Function GetNullableValue() As Int32?
      Return 408
    End Function

    Public Function GetNullableValueNull() As Int32?
      Return Nothing
    End Function

    Public Function GetValue(param1 As Int32, param2 As Int32) As Int32
      Return 409 + param1 + param2
    End Function

    Public Function GetNullableValue(param1 As Int32, param2 As Int32) As Int32?
      Return 410 + param1 + param2
    End Function

    Public Function GetNullableValueNull(param1 As Int32, param2 As Int32) As Int32?
      Return Nothing
    End Function

  End Module

  <TestClass()>
  Public Class ExpressionTests
    Inherits BaseUnitTests

    Private Const ConstantValue As Int32 = 200

    Private m_FieldValue As Int32 = 201

    Private m_FieldNullableValue As Int32? = 202

    Private m_FieldNullableValueNull As Int32? = Nothing

    Private Shared m_StaticFieldValue As Int32 = 203

    Private Shared m_StaticFieldNullableValue As Int32? = 204

    Private Shared m_StaticFieldNullableValueNull As Int32? = Nothing

    Private Property PropertyValue As Int32 = 205

    Private Property PropertyNullableValue As Int32? = 206

    Private Property PropertyNullableValueNull As Int32? = Nothing

    Private Shared Property StaticPropertyValue As Int32 = 207

    Private Shared Property StaticPropertyNullableValue As Int32? = 208

    Private Shared Property StaticPropertyNullableValueNull As Int32? = Nothing

    Public Property IndexerPropertyValue(index As Int32) As Int32
      Get
        Return 209 + index
      End Get
      Set(ByVal value As Int32)
      End Set
    End Property

    Public Property IndexerPropertyNullableValue(index As Int32) As Int32?
      Get
        Return 210 + index
      End Get
      Set(ByVal value As Int32?)
      End Set
    End Property

    Public Property IndexerPropertyNullableValueNull(index As Int32) As Int32?
      Get
        Return Nothing
      End Get
      Set(ByVal value As Int32?)
      End Set
    End Property

    Public Shared Property StaticIndexerPropertyValue(index As Int32) As Int32
      Get
        Return 211 + index
      End Get
      Set(ByVal value As Int32)
      End Set
    End Property

    Public Shared Property StaticIndexerPropertyNullableValue(index As Int32) As Int32?
      Get
        Return 212 + index
      End Get
      Set(ByVal value As Int32?)
      End Set
    End Property

    Public Shared Property StaticIndexerPropertyNullableValueNull(index As Int32) As Int32?
      Get
        Return Nothing
      End Get
      Set(ByVal value As Int32?)
      End Set
    End Property

    Private Function GetValue() As Int32
      Return 213
    End Function

    Private Function GetNullableValue() As Int32?
      Return 214
    End Function

    Private Function GetNullableValueNull() As Int32?
      Return Nothing
    End Function

    Private Function GetValue(param1 As Int32, param2 As Int32) As Int32
      Return 215 + param1 + param2
    End Function

    Private Function GetNullableValue(param1 As Int32, param2 As Int32) As Int32?
      Return 216 + param1 + param2
    End Function

    Private Function GetNullableValueNull(param1 As Int32, param2 As Int32) As Int32?
      Return Nothing
    End Function

    Private Shared Function GetStaticValue() As Int32
      Return 217
    End Function

    Private Shared Function GetStaticNullableValue() As Int32?
      Return 218
    End Function

    Private Shared Function GetStaticNullableValueNull() As Int32?
      Return Nothing
    End Function

    Private Shared Function GetStaticValue(param1 As Int32, param2 As Int32) As Int32
      Return 219 + param1 + param2
    End Function

    Private Shared Function GetStaticNullableValue(param1 As Int32, param2 As Int32) As Int32?
      Return 220 + param1 + param2
    End Function

    Private Shared Function GetStaticNullableValueNull(param1 As Int32, param2 As Int32) As Int32?
      Return Nothing
    End Function

    Private Class Class1

      Public Const ConstantValue As Int32 = 300

      Public FieldValue As Int32 = 301

      Public FieldNullableValue As Int32? = 302

      Public FieldNullableValueNull As Int32? = Nothing

      Public Shared StaticFieldValue As Int32 = 303

      Public Shared StaticFieldNullableValue As Int32? = 304

      Public Shared StaticFieldNullableValueNull As Int32? = Nothing

      Public Property PropertyValue As Int32 = 305

      Public Property PropertyNullableValue As Int32? = 306

      Public Property PropertyNullableValueNull As Int32? = Nothing

      Public Shared Property StaticPropertyValue As Int32 = 307

      Public Shared Property StaticPropertyNullableValue As Int32? = 308

      Public Shared Property StaticPropertyNullableValueNull As Int32? = Nothing

      Public Property IndexerPropertyValue(index As Int32) As Int32
        Get
          Return 309 + index
        End Get
        Set(ByVal value As Int32)
        End Set
      End Property

      Public Property IndexerPropertyNullableValue(index As Int32) As Int32?
        Get
          Return 310 + index
        End Get
        Set(ByVal value As Int32?)
        End Set
      End Property

      Public Property IndexerPropertyNullableValueNull(index As Int32) As Int32?
        Get
          Return Nothing
        End Get
        Set(ByVal value As Int32?)
        End Set
      End Property

      Public Shared Property StaticIndexerPropertyValue(index As Int32) As Int32
        Get
          Return 311 + index
        End Get
        Set(ByVal value As Int32)
        End Set
      End Property

      Public Shared Property StaticIndexerPropertyNullableValue(index As Int32) As Int32?
        Get
          Return 312 + index
        End Get
        Set(ByVal value As Int32?)
        End Set
      End Property

      Public Shared Property StaticIndexerPropertyNullableValueNull(index As Int32) As Int32?
        Get
          Return Nothing
        End Get
        Set(ByVal value As Int32?)
        End Set
      End Property

      Public Function GetValue() As Int32
        Return 313
      End Function

      Public Function GetNullableValue() As Int32?
        Return 314
      End Function

      Public Function GetNullableValueNull() As Int32?
        Return Nothing
      End Function

      Public Function GetValue(param1 As Int32, param2 As Int32) As Int32
        Return 315 + param1 + param2
      End Function

      Public Function GetNullableValue(param1 As Int32, param2 As Int32) As Int32?
        Return 316 + param1 + param2
      End Function

      Public Function GetNullableValueNull(param1 As Int32, param2 As Int32) As Int32?
        Return Nothing
      End Function

      Public Shared Function GetStaticValue() As Int32
        Return 317
      End Function

      Public Shared Function GetStaticNullableValue() As Int32?
        Return 318
      End Function

      Public Shared Function GetStaticNullableValueNull() As Int32?
        Return Nothing
      End Function

      Public Shared Function GetStaticValue(param1 As Int32, param2 As Int32) As Int32
        Return 319 + param1 + param2
      End Function

      Public Shared Function GetStaticNullableValue(param1 As Int32, param2 As Int32) As Int32?
        Return 320 + param1 + param2
      End Function

      Public Shared Function GetStaticNullableValueNull(param1 As Int32, param2 As Int32) As Int32?
        Return Nothing
      End Function

    End Class

    Private Class Class2

      Public Function GetTrueValue() As Boolean
        Return True
      End Function

      Public Function GetFalseValue() As Boolean
        Return False
      End Function

      Public Function GetNullableValueTrue() As Boolean?
        Return True
      End Function

      Public Function GetNullableValueFalse() As Boolean?
        Return False
      End Function

      Public Function GetNullableValueNull() As Boolean?
        Return Nothing
      End Function

    End Class

    <TestMethod()>
    Public Sub EvaluateExpression()
      ' tests evaluation of various values

      Using db = CreateDbContext()
        Dim visitor = CreateSqlExpressionVisitor(db)
        Dim result As SqlString

        Dim localValue As Int32
        Dim localNullableValue As Int32?

        Dim localParam = 10000

        ' constant
        result = TranslateCondition(visitor, Function(x) x.IntColumn = 100)
        Assert.AreEqual("[T0].[IntColumn] = 100", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' local value (captured closure)
        localValue = 101
        result = TranslateCondition(visitor, Function(x) x.IntColumn = localValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        localNullableValue = 102
        result = TranslateCondition(visitor, Function(x) x.IntColumn = localNullableValue.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localNullableValue, result.Parameters(0).Value)

        localNullableValue = Nothing
        result = TranslateCondition(visitor, Function(x) x.IntColumn = localNullableValue.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localNullableValue, result.Parameters(0).Value)

        ' --------------------------------------------------------------------------------------

        ' constant
        result = TranslateCondition(visitor, Function(x) x.IntColumn = ConstantValue)
        Assert.AreEqual("[T0].[IntColumn] = 200", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' field
        result = TranslateCondition(visitor, Function(x) x.IntColumn = m_FieldValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(m_FieldValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = m_FieldNullableValue.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(m_FieldNullableValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = m_FieldNullableValueNull.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(m_FieldNullableValueNull, result.Parameters(0).Value)

        ' static field
        result = TranslateCondition(visitor, Function(x) x.IntColumn = m_StaticFieldValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(m_StaticFieldValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = m_StaticFieldNullableValue.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(m_StaticFieldNullableValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = m_StaticFieldNullableValueNull.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(m_StaticFieldNullableValueNull, result.Parameters(0).Value)

        ' property
        result = TranslateCondition(visitor, Function(x) x.IntColumn = Me.PropertyValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Me.PropertyValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Me.PropertyNullableValue.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Me.PropertyNullableValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Me.PropertyNullableValueNull.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Me.PropertyNullableValueNull, result.Parameters(0).Value)

        ' static property
        result = TranslateCondition(visitor, Function(x) x.IntColumn = StaticPropertyValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(StaticPropertyValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = StaticPropertyNullableValue.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(StaticPropertyNullableValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = StaticPropertyNullableValueNull.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(StaticPropertyNullableValueNull, result.Parameters(0).Value)

        ' indexer property
        result = TranslateCondition(visitor, Function(x) x.IntColumn = Me.IndexerPropertyValue(10000))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Me.IndexerPropertyValue(10000), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Me.IndexerPropertyNullableValue(10000).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Me.IndexerPropertyNullableValue(10000), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Me.IndexerPropertyNullableValueNull(10000).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Me.IndexerPropertyNullableValueNull(10000), result.Parameters(0).Value)

        ' static indexer property
        result = TranslateCondition(visitor, Function(x) x.IntColumn = StaticIndexerPropertyValue(10000))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(StaticIndexerPropertyValue(10000), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = StaticIndexerPropertyNullableValue(10000).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(StaticIndexerPropertyNullableValue(10000), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = StaticIndexerPropertyNullableValueNull(10000).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(StaticIndexerPropertyNullableValueNull(10000), result.Parameters(0).Value)

        ' function
        result = TranslateCondition(visitor, Function(x) x.IntColumn = GetValue())
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = GetNullableValue().Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetNullableValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = GetNullableValueNull().Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetNullableValueNull(), result.Parameters(0).Value)

        ' function with parameters
        result = TranslateCondition(visitor, Function(x) x.IntColumn = GetValue(10000, localParam))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetValue(10000, localParam), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = GetNullableValue(10000, localParam).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetNullableValue(10000, localParam), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = GetNullableValueNull(10000, localParam).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetNullableValueNull(10000, localParam), result.Parameters(0).Value)

        ' static function
        result = TranslateCondition(visitor, Function(x) x.IntColumn = GetStaticValue())
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetStaticValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = GetStaticNullableValue().Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetStaticNullableValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = GetStaticNullableValueNull().Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetStaticNullableValueNull(), result.Parameters(0).Value)

        ' static function with parameters
        result = TranslateCondition(visitor, Function(x) x.IntColumn = GetStaticValue(10000, localParam))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetStaticValue(10000, localParam), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = GetStaticNullableValue(10000, localParam).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetStaticNullableValue(10000, localParam), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = GetStaticNullableValueNull(10000, localParam).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(GetStaticNullableValueNull(10000, localParam), result.Parameters(0).Value)

        ' --------------------------------------------------------------------------------------

        Dim c = New Class1

        ' constant
        result = TranslateCondition(visitor, Function(x) x.IntColumn = Class1.ConstantValue)
        Assert.AreEqual("[T0].[IntColumn] = 300", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' field
        result = TranslateCondition(visitor, Function(x) x.IntColumn = c.FieldValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.FieldValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = c.FieldNullableValue.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.FieldNullableValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = c.FieldNullableValueNull.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.FieldNullableValueNull, result.Parameters(0).Value)

        ' static field
        result = TranslateCondition(visitor, Function(x) x.IntColumn = Class1.StaticFieldValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.StaticFieldValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Class1.StaticFieldNullableValue.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.StaticFieldNullableValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Class1.StaticFieldNullableValueNull.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.StaticFieldNullableValueNull, result.Parameters(0).Value)

        ' property
        result = TranslateCondition(visitor, Function(x) x.IntColumn = c.PropertyValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.PropertyValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = c.PropertyNullableValue.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.PropertyNullableValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = c.PropertyNullableValueNull.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.PropertyNullableValueNull, result.Parameters(0).Value)

        ' static property
        result = TranslateCondition(visitor, Function(x) x.IntColumn = Class1.StaticPropertyValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.StaticPropertyValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Class1.StaticPropertyNullableValue.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.StaticPropertyNullableValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Class1.StaticPropertyNullableValueNull.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.StaticPropertyNullableValueNull, result.Parameters(0).Value)

        ' indexer property
        result = TranslateCondition(visitor, Function(x) x.IntColumn = c.IndexerPropertyValue(10000))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.IndexerPropertyValue(10000), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = c.IndexerPropertyNullableValue(10000).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.IndexerPropertyNullableValue(10000), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = c.IndexerPropertyNullableValueNull(10000).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.IndexerPropertyNullableValueNull(10000), result.Parameters(0).Value)

        ' static indexer property
        result = TranslateCondition(visitor, Function(x) x.IntColumn = Class1.StaticIndexerPropertyValue(10000))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.StaticIndexerPropertyValue(10000), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Class1.StaticIndexerPropertyNullableValue(10000).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.StaticIndexerPropertyNullableValue(10000), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Class1.StaticIndexerPropertyNullableValueNull(10000).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.StaticIndexerPropertyNullableValueNull(10000), result.Parameters(0).Value)

        ' function
        result = TranslateCondition(visitor, Function(x) x.IntColumn = c.GetValue())
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = c.GetNullableValue().Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = c.GetNullableValueNull().Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueNull(), result.Parameters(0).Value)

        ' function with parameters
        result = TranslateCondition(visitor, Function(x) x.IntColumn = c.GetValue(10000, localParam))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetValue(10000, localParam), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = c.GetNullableValue(10000, localParam).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValue(10000, localParam), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = c.GetNullableValueNull(10000, localParam).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueNull(10000, localParam), result.Parameters(0).Value)

        ' static function
        result = TranslateCondition(visitor, Function(x) x.IntColumn = Class1.GetStaticValue())
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.GetStaticValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Class1.GetStaticNullableValue().Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.GetStaticNullableValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Class1.GetStaticNullableValueNull().Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.GetStaticNullableValueNull(), result.Parameters(0).Value)

        ' static function with parameters
        result = TranslateCondition(visitor, Function(x) x.IntColumn = Class1.GetStaticValue(10000, localParam))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.GetStaticValue(10000, localParam), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Class1.GetStaticNullableValue(10000, localParam).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.GetStaticNullableValue(10000, localParam), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Class1.GetStaticNullableValueNull(10000, localParam).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Class1.GetStaticNullableValueNull(10000, localParam), result.Parameters(0).Value)

        ' --------------------------------------------------------------------------------------

        ' constant
        result = TranslateCondition(visitor, Function(x) x.IntColumn = Module1.ConstantValue)
        Assert.AreEqual("[T0].[IntColumn] = 400", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' field
        result = TranslateCondition(visitor, Function(x) x.IntColumn = Module1.FieldValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.FieldValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Module1.FieldNullableValue.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.FieldNullableValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Module1.FieldNullableValueNull.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.FieldNullableValueNull, result.Parameters(0).Value)

        ' property
        result = TranslateCondition(visitor, Function(x) x.IntColumn = Module1.PropertyValue)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.PropertyValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Module1.PropertyNullableValue.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.PropertyNullableValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Module1.PropertyNullableValueNull.Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.PropertyNullableValueNull, result.Parameters(0).Value)

        ' indexer property
        result = TranslateCondition(visitor, Function(x) x.IntColumn = Module1.IndexerPropertyValue(10000))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.IndexerPropertyValue(10000), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Module1.IndexerPropertyNullableValue(10000).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.IndexerPropertyNullableValue(10000), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Module1.IndexerPropertyNullableValueNull(10000).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.IndexerPropertyNullableValueNull(10000), result.Parameters(0).Value)

        ' function
        result = TranslateCondition(visitor, Function(x) x.IntColumn = Module1.GetValue())
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.GetValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Module1.GetNullableValue().Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.GetNullableValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Module1.GetNullableValueNull().Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.GetNullableValueNull(), result.Parameters(0).Value)

        ' function with parameters
        result = TranslateCondition(visitor, Function(x) x.IntColumn = Module1.GetValue(10000, localParam))
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.GetValue(10000, localParam), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Module1.GetNullableValue(10000, localParam).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.GetNullableValue(10000, localParam), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.IntColumn = Module1.GetNullableValueNull(10000, localParam).Value)
        Assert.AreEqual("[T0].[IntColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(Module1.GetNullableValueNull(10000, localParam), result.Parameters(0).Value)

        ' --------------------------------------------------------------------------------------

        ' SQL helper
        result = TranslateCondition(visitor, Function(x) Sql.Exp.Raw(Of Int32)("IntColumn") = 100)
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

        result = TranslateCondition(visitor, Function(x) x.BitColumn = True)
        Assert.AreEqual("[T0].[BitColumn] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) j.T1.BitColumn = True)
        Assert.AreEqual("[T0].[BitColumn] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = TranslateCondition(visitor, Function(x) x.BitColumn = localValue)
        Assert.AreEqual("[T0].[BitColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.BitColumn = c.GetTrueValue())
        Assert.AreEqual("[T0].[BitColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetTrueValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.BitColumn = Sql.Exp.Raw(Of Boolean)("1"))
        Assert.AreEqual("[T0].[BitColumn] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) x.BitColumn = False)
        Assert.AreEqual("[T0].[BitColumn] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) j.T1.BitColumn = False)
        Assert.AreEqual("[T0].[BitColumn] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = False
        result = TranslateCondition(visitor, Function(x) x.BitColumn = localValue)
        Assert.AreEqual("[T0].[BitColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.BitColumn = c.GetFalseValue())
        Assert.AreEqual("[T0].[BitColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetFalseValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.BitColumn = Sql.Exp.Raw(Of Boolean)("0"))
        Assert.AreEqual("[T0].[BitColumn] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) Not x.BitColumn = True)
        Assert.AreEqual("NOT [T0].[BitColumn] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) Not j.T1.BitColumn = True)
        Assert.AreEqual("NOT [T0].[BitColumn] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = TranslateCondition(visitor, Function(x) Not x.BitColumn = localValue)
        Assert.AreEqual("NOT [T0].[BitColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not x.BitColumn = c.GetTrueValue())
        Assert.AreEqual("NOT [T0].[BitColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetTrueValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not x.BitColumn = Sql.Exp.Raw(Of Boolean)("1"))
        Assert.AreEqual("NOT [T0].[BitColumn] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) Not x.BitColumn = False)
        Assert.AreEqual("NOT [T0].[BitColumn] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) Not j.T1.BitColumn = False)
        Assert.AreEqual("NOT [T0].[BitColumn] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = False
        result = TranslateCondition(visitor, Function(x) Not x.BitColumn = localValue)
        Assert.AreEqual("NOT [T0].[BitColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not x.BitColumn = c.GetFalseValue())
        Assert.AreEqual("NOT [T0].[BitColumn] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetFalseValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not x.BitColumn = Sql.Exp.Raw(Of Boolean)("0"))
        Assert.AreEqual("NOT [T0].[BitColumn] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) x.BitColumn <> True)
        Assert.AreEqual("[T0].[BitColumn] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) j.T1.BitColumn <> True)
        Assert.AreEqual("[T0].[BitColumn] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = TranslateCondition(visitor, Function(x) x.BitColumn <> localValue)
        Assert.AreEqual("[T0].[BitColumn] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.BitColumn <> c.GetTrueValue())
        Assert.AreEqual("[T0].[BitColumn] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetTrueValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.BitColumn <> Sql.Exp.Raw(Of Boolean)("1"))
        Assert.AreEqual("[T0].[BitColumn] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) x.BitColumn <> False)
        Assert.AreEqual("[T0].[BitColumn] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) j.T1.BitColumn <> False)
        Assert.AreEqual("[T0].[BitColumn] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = False
        result = TranslateCondition(visitor, Function(x) x.BitColumn <> localValue)
        Assert.AreEqual("[T0].[BitColumn] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.BitColumn <> c.GetFalseValue())
        Assert.AreEqual("[T0].[BitColumn] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetFalseValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.BitColumn <> Sql.Exp.Raw(Of Boolean)("0"))
        Assert.AreEqual("[T0].[BitColumn] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) Not x.BitColumn <> True)
        Assert.AreEqual("NOT [T0].[BitColumn] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) Not j.T1.BitColumn <> True)
        Assert.AreEqual("NOT [T0].[BitColumn] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = TranslateCondition(visitor, Function(x) Not x.BitColumn <> localValue)
        Assert.AreEqual("NOT [T0].[BitColumn] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not x.BitColumn <> c.GetTrueValue())
        Assert.AreEqual("NOT [T0].[BitColumn] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetTrueValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not x.BitColumn <> Sql.Exp.Raw(Of Boolean)("1"))
        Assert.AreEqual("NOT [T0].[BitColumn] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) Not x.BitColumn <> False)
        Assert.AreEqual("NOT [T0].[BitColumn] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) Not j.T1.BitColumn <> False)
        Assert.AreEqual("NOT [T0].[BitColumn] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = False
        result = TranslateCondition(visitor, Function(x) Not x.BitColumn <> localValue)
        Assert.AreEqual("NOT [T0].[BitColumn] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not x.BitColumn <> c.GetFalseValue())
        Assert.AreEqual("NOT [T0].[BitColumn] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetFalseValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not x.BitColumn <> Sql.Exp.Raw(Of Boolean)("0"))
        Assert.AreEqual("NOT [T0].[BitColumn] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) x.BitColumn)
        Assert.AreEqual("[T0].[BitColumn] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) j.T1.BitColumn)
        Assert.AreEqual("[T0].[BitColumn] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = TranslateCondition(visitor, Function(x) localValue)
        Assert.AreEqual("@p0 = 1", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) c.GetTrueValue())
        Assert.AreEqual("@p0 = 1", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetTrueValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Sql.Exp.Raw(Of Boolean)("BitColumn = 1"))
        Assert.AreEqual("BitColumn = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) Not x.BitColumn)
        Assert.AreEqual("[T0].[BitColumn] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) Not j.T1.BitColumn)
        Assert.AreEqual("[T0].[BitColumn] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = TranslateCondition(visitor, Function(x) Not localValue)
        Assert.AreEqual("NOT @p0 = 1", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not c.GetTrueValue())
        Assert.AreEqual("NOT @p0 = 1", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetTrueValue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not Sql.Exp.Raw(Of Boolean)("BitColumn = 1"))
        Assert.AreEqual("NOT BitColumn = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) x.BitColumn = True AndAlso Not x.BitColumn = True And x.BitColumn OrElse Not x.BitColumn Or x.BitColumn)
        Assert.AreEqual("(((([T0].[BitColumn] = 1 AND NOT [T0].[BitColumn] = 1) AND [T0].[BitColumn] = 1) OR [T0].[BitColumn] = 0) OR [T0].[BitColumn] = 1)", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) j.T1.BitColumn = True AndAlso Not j.T1.BitColumn = True And j.T1.BitColumn OrElse Not j.T1.BitColumn Or j.T1.BitColumn)
        Assert.AreEqual("(((([T0].[BitColumn] = 1 AND NOT [T0].[BitColumn] = 1) AND [T0].[BitColumn] = 1) OR [T0].[BitColumn] = 0) OR [T0].[BitColumn] = 1)", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        localValue = True
        result = TranslateCondition(visitor, Function(x) x.BitColumn = localValue AndAlso c.GetTrueValue() OrElse Not Sql.Exp.Raw(Of Boolean)("BitColumn = 1"))
        Assert.AreEqual("(([T0].[BitColumn] = @p0 AND @p1 = 1) OR NOT BitColumn = 1)", result.Sql)
        Assert.AreEqual(2, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)
        Assert.AreEqual(c.GetTrueValue(), result.Parameters(1).Value)

      End Using
    End Sub

    <TestMethod()>
    Public Sub TranslateNullableBooleanValue()
      Using db = CreateDbContext()
        Dim visitor = CreateSqlExpressionVisitor(db)
        Dim result As SqlString

        Dim localValue As Boolean?
        Dim c = New Class2

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value = True)
        Assert.AreEqual("[T0].[BitColumnNull] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) j.T1.BitColumnNull.Value = True)
        Assert.AreEqual("[T0].[BitColumnNull] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value = localValue.Value)
        Assert.AreEqual("[T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value = c.GetNullableValueTrue().Value)
        Assert.AreEqual("[T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueTrue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value = c.GetNullableValueTrue().Value)
        Assert.AreEqual("[T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueTrue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value = Sql.Exp.Raw(Of Boolean?)("1").Value)
        Assert.AreEqual("[T0].[BitColumnNull] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value = False)
        Assert.AreEqual("[T0].[BitColumnNull] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) j.T1.BitColumnNull.Value = False)
        Assert.AreEqual("[T0].[BitColumnNull] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = False
        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value = localValue.Value)
        Assert.AreEqual("[T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value = c.GetNullableValueFalse().Value)
        Assert.AreEqual("[T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueFalse(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value = Sql.Exp.Raw(Of Boolean?)("0").Value)
        Assert.AreEqual("[T0].[BitColumnNull] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) Not x.BitColumnNull.Value = True)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) Not j.T1.BitColumnNull.Value = True)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = TranslateCondition(visitor, Function(x) Not x.BitColumnNull.Value = localValue.Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not x.BitColumnNull.Value = c.GetNullableValueTrue().Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueTrue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not x.BitColumnNull.Value = Sql.Exp.Raw(Of Boolean?)("1").Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) Not x.BitColumnNull.Value = False)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) Not j.T1.BitColumnNull.Value = False)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = False
        result = TranslateCondition(visitor, Function(x) Not x.BitColumnNull.Value = localValue.Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not x.BitColumnNull.Value = c.GetNullableValueFalse().Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueFalse(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not x.BitColumnNull.Value = Sql.Exp.Raw(Of Boolean?)("0").Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value <> True)
        Assert.AreEqual("[T0].[BitColumnNull] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) j.T1.BitColumnNull.Value <> True)
        Assert.AreEqual("[T0].[BitColumnNull] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value <> localValue.Value)
        Assert.AreEqual("[T0].[BitColumnNull] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value <> c.GetNullableValueTrue().Value)
        Assert.AreEqual("[T0].[BitColumnNull] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueTrue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value <> Sql.Exp.Raw(Of Boolean?)("1").Value)
        Assert.AreEqual("[T0].[BitColumnNull] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value <> False)
        Assert.AreEqual("[T0].[BitColumnNull] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) j.T1.BitColumnNull.Value <> False)
        Assert.AreEqual("[T0].[BitColumnNull] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = False
        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value <> localValue.Value)
        Assert.AreEqual("[T0].[BitColumnNull] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value <> c.GetNullableValueFalse().Value)
        Assert.AreEqual("[T0].[BitColumnNull] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueFalse(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value <> Sql.Exp.Raw(Of Boolean?)("0").Value)
        Assert.AreEqual("[T0].[BitColumnNull] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) Not x.BitColumnNull.Value <> True)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) Not j.T1.BitColumnNull.Value <> True)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = TranslateCondition(visitor, Function(x) Not x.BitColumnNull.Value <> localValue.Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not x.BitColumnNull.Value <> c.GetNullableValueTrue().Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueTrue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not x.BitColumnNull.Value <> Sql.Exp.Raw(Of Boolean?)("1").Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) Not x.BitColumnNull.Value <> False)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) Not j.T1.BitColumnNull.Value <> False)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = False
        result = TranslateCondition(visitor, Function(x) Not x.BitColumnNull.Value <> localValue.Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not x.BitColumnNull.Value <> c.GetNullableValueFalse().Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueFalse(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not x.BitColumnNull.Value <> Sql.Exp.Raw(Of Boolean?)("0").Value)
        Assert.AreEqual("NOT [T0].[BitColumnNull] <> 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value)
        Assert.AreEqual("[T0].[BitColumnNull] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) j.T1.BitColumnNull.Value)
        Assert.AreEqual("[T0].[BitColumnNull] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = TranslateCondition(visitor, Function(x) localValue.Value)
        Assert.AreEqual("@p0 = 1", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) c.GetNullableValueTrue().Value)
        Assert.AreEqual("@p0 = 1", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueTrue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Sql.Exp.Raw(Of Boolean?)("BitColumnNull = 1").Value)
        Assert.AreEqual("BitColumnNull = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) Not x.BitColumnNull.Value)
        Assert.AreEqual("[T0].[BitColumnNull] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) Not j.T1.BitColumnNull.Value)
        Assert.AreEqual("[T0].[BitColumnNull] = 0", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = TranslateCondition(visitor, Function(x) Not localValue.Value)
        Assert.AreEqual("NOT @p0 = 1", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not c.GetNullableValueTrue().Value)
        Assert.AreEqual("NOT @p0 = 1", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueTrue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not Sql.Exp.Raw(Of Boolean?)("BitColumnNull = 1").Value)
        Assert.AreEqual("NOT BitColumnNull = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        localValue = Nothing
        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value = localValue.Value)
        Assert.AreEqual("[T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value = c.GetNullableValueNull().Value)
        Assert.AreEqual("[T0].[BitColumnNull] = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueNull(), result.Parameters(0).Value)

        localValue = Nothing
        result = TranslateCondition(visitor, Function(x) localValue.Value)
        Assert.AreEqual("@p0 = 1", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) c.GetNullableValueNull().Value)
        Assert.AreEqual("@p0 = 1", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueNull(), result.Parameters(0).Value)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value = True AndAlso Not x.BitColumnNull.Value = True And x.BitColumnNull.Value OrElse Not x.BitColumnNull.Value Or x.BitColumnNull.Value)
        Assert.AreEqual("(((([T0].[BitColumnNull] = 1 AND NOT [T0].[BitColumnNull] = 1) AND [T0].[BitColumnNull] = 1) OR [T0].[BitColumnNull] = 0) OR [T0].[BitColumnNull] = 1)", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) j.T1.BitColumnNull.Value = True AndAlso Not j.T1.BitColumnNull.Value = True And j.T1.BitColumnNull.Value OrElse Not j.T1.BitColumnNull.Value Or j.T1.BitColumnNull.Value)
        Assert.AreEqual("(((([T0].[BitColumnNull] = 1 AND NOT [T0].[BitColumnNull] = 1) AND [T0].[BitColumnNull] = 1) OR [T0].[BitColumnNull] = 0) OR [T0].[BitColumnNull] = 1)", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        localValue = True
        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.Value = localValue.Value AndAlso c.GetNullableValueTrue().Value OrElse Not Sql.Exp.Raw(Of Boolean?)("BitColumnNull = 1").Value)
        Assert.AreEqual("(([T0].[BitColumnNull] = @p0 AND @p1 = 1) OR NOT BitColumnNull = 1)", result.Sql)
        Assert.AreEqual(2, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)
        Assert.AreEqual(c.GetNullableValueTrue(), result.Parameters(1).Value)

      End Using
    End Sub

    <TestMethod()>
    Public Sub TranslateWithPossibleExpandedBooleanComparison()
      Using db = CreateDbContext()
        Dim visitor = CreateSqlExpressionVisitor(db)
        Dim result As SqlString

        result = Translate(visitor, Function(x) x.BitColumn, ExpressionTranslateMode.Condition)
        Assert.AreEqual("[T0].[BitColumn] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = Translate(visitor, Function(x) x.BitColumn, ExpressionTranslateMode.GroupBy)
        Assert.AreEqual("[T0].[BitColumn]", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = Translate(visitor, Function(x) x.BitColumn, ExpressionTranslateMode.OrderBy)
        Assert.AreEqual("[T0].[BitColumn]", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = Translate(visitor, Function(x) x.BitColumn, ExpressionTranslateMode.CustomSelect)
        Assert.AreEqual("[T0].[BitColumn]", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = Translate(visitor, Function(x) x.BitColumn, ExpressionTranslateMode.Set)
        Assert.AreEqual("[T0].[BitColumn]", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

      End Using
    End Sub


    <TestMethod()>
    Public Sub TranslateNullableHasValue()
      Using db = CreateDbContext()
        Dim visitor = CreateSqlExpressionVisitor(db)
        Dim result As SqlString

        Dim localValue As Boolean?
        Dim c = New Class2

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.HasValue)
        Assert.AreEqual("[T0].[BitColumnNull] IS NOT NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) j.T1.BitColumnNull.HasValue)
        Assert.AreEqual("[T0].[BitColumnNull] IS NOT NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = TranslateCondition(visitor, Function(x) localValue.HasValue)
        Assert.AreEqual("@p0 IS NOT NULL", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) c.GetNullableValueTrue().HasValue)
        Assert.AreEqual("@p0 IS NOT NULL", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueTrue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Sql.Exp.Raw(Of Boolean?)("BitColumnNull").HasValue)
        Assert.AreEqual("BitColumnNull IS NOT NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) Not x.BitColumnNull.HasValue)
        Assert.AreEqual("[T0].[BitColumnNull] IS NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) Not j.T1.BitColumnNull.HasValue)
        Assert.AreEqual("[T0].[BitColumnNull] IS NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = TranslateCondition(visitor, Function(x) Not localValue.HasValue)
        Assert.AreEqual("@p0 IS NULL", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not c.GetNullableValueTrue().HasValue)
        Assert.AreEqual("@p0 IS NULL", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueTrue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Not Sql.Exp.Raw(Of Boolean?)("BitColumnNull").HasValue)
        Assert.AreEqual("BitColumnNull IS NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        localValue = True
        result = TranslateCondition(visitor, Function(x) x.BitColumnNull.HasValue AndAlso Not x.BitColumnNull.HasValue And localValue.HasValue OrElse Not c.GetNullableValueTrue().HasValue Or Sql.Exp.Raw(Of Boolean?)("BitColumnNull").HasValue)
        Assert.AreEqual("(((([T0].[BitColumnNull] IS NOT NULL AND [T0].[BitColumnNull] IS NULL) AND @p0 IS NOT NULL) OR @p1 IS NULL) OR BitColumnNull IS NOT NULL)", result.Sql)
        Assert.AreEqual(2, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)
        Assert.AreEqual(c.GetNullableValueTrue(), result.Parameters(1).Value)

        ' TODO: SIP - fix: "nullable.HasValue = True"

      End Using
    End Sub

    <TestMethod()>
    Public Sub TranslateNullableIsOrIsNotNothing()
      Using db = CreateDbContext()
        Dim visitor = CreateSqlExpressionVisitor(db)
        Dim result As SqlString

        Dim localValue As Boolean?
        Dim c = New Class2

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull Is Nothing)
        Assert.AreEqual("[T0].[BitColumnNull] IS NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) j.T1.BitColumnNull Is Nothing)
        Assert.AreEqual("[T0].[BitColumnNull] IS NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = TranslateCondition(visitor, Function(x) localValue Is Nothing)
        Assert.AreEqual("@p0 IS NULL", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) c.GetNullableValueTrue() Is Nothing)
        Assert.AreEqual("@p0 IS NULL", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueTrue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Sql.Exp.Raw(Of Boolean?)("BitColumnNull") Is Nothing)
        Assert.AreEqual("BitColumnNull IS NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        result = TranslateCondition(visitor, Function(x) x.BitColumnNull IsNot Nothing)
        Assert.AreEqual("[T0].[BitColumnNull] IS NOT NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateConditionWithJoin(visitor, Function(j) j.T1.BitColumnNull IsNot Nothing)
        Assert.AreEqual("[T0].[BitColumnNull] IS NOT NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        localValue = True
        result = TranslateCondition(visitor, Function(x) localValue IsNot Nothing)
        Assert.AreEqual("@p0 IS NOT NULL", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) c.GetNullableValueTrue() IsNot Nothing)
        Assert.AreEqual("@p0 IS NOT NULL", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(c.GetNullableValueTrue(), result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) Sql.Exp.Raw(Of Boolean?)("BitColumnNull") IsNot Nothing)
        Assert.AreEqual("BitColumnNull IS NOT NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' --------------------------------------------------------------------------------------

        localValue = True
        result = TranslateCondition(visitor, Function(x) x.BitColumnNull Is Nothing AndAlso x.BitColumnNull IsNot Nothing And localValue Is Nothing OrElse c.GetNullableValueTrue() IsNot Nothing Or Sql.Exp.Raw(Of Boolean?)("BitColumnNull") Is Nothing)
        Assert.AreEqual("(((([T0].[BitColumnNull] IS NULL AND [T0].[BitColumnNull] IS NOT NULL) AND @p0 IS NULL) OR @p1 IS NOT NULL) OR BitColumnNull IS NULL)", result.Sql)
        Assert.AreEqual(2, result.Parameters.Count)
        Assert.AreEqual(localValue, result.Parameters(0).Value)
        Assert.AreEqual(c.GetNullableValueTrue(), result.Parameters(1).Value)

      End Using
    End Sub

    <TestMethod()>
    Public Sub TranslateTernary()
      Using db = CreateDbContext()
        Dim visitor = CreateSqlExpressionVisitor(db)
        Dim result As SqlString

        Dim value1 = True
        Dim value2 = 42
        Dim value3 = 6

        result = TranslateCondition(visitor, Function(x) x.IntColumn = If(value1, value2, value3))
        Assert.AreEqual("[T0].[IntColumn] = CASE WHEN @p0 = 1 THEN @p1 ELSE @p2 END", result.Sql)
        Assert.AreEqual(3, result.Parameters.Count)
        Assert.AreEqual(value1, result.Parameters(0).Value)
        Assert.AreEqual(value2, result.Parameters(1).Value)
        Assert.AreEqual(value3, result.Parameters(2).Value)

        result = TranslateCondition(visitor, Function(x) If(x.BitColumn = True, x.IntColumnNull.Value, x.IntColumn) = value2)
        Assert.AreEqual("CASE WHEN [T0].[BitColumn] = 1 THEN [T0].[IntColumnNull] ELSE [T0].[IntColumn] END = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(value2, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) If(x.BitColumn, x.IntColumnNull.Value, x.IntColumn) = value2)
        Assert.AreEqual("CASE WHEN [T0].[BitColumn] = 1 THEN [T0].[IntColumnNull] ELSE [T0].[IntColumn] END = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(value2, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) If(x.IntColumnNull.HasValue, x.IntColumnNull.Value, x.IntColumn) = value2)
        Assert.AreEqual("CASE WHEN [T0].[IntColumnNull] IS NOT NULL THEN [T0].[IntColumnNull] ELSE [T0].[IntColumn] END = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(value2, result.Parameters(0).Value)

        ' boolen and nullable boolean have special handling

        ' boolean

        result = TranslateCondition(visitor, Function(x) If(x.BitColumnNull.HasValue, x.BitColumnNull.Value, x.BitColumn) = value1)
        Assert.AreEqual("CASE WHEN [T0].[BitColumnNull] IS NOT NULL THEN [T0].[BitColumnNull] ELSE [T0].[BitColumn] END = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(value1, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) If(x.BitColumnNull.HasValue, x.BitColumnNull.Value, x.BitColumn) = True)
        Assert.AreEqual("CASE WHEN [T0].[BitColumnNull] IS NOT NULL THEN [T0].[BitColumnNull] ELSE [T0].[BitColumn] END = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateCondition(visitor, Function(x) If(x.BitColumnNull.HasValue, x.BitColumnNull.Value, x.BitColumn))
        Assert.AreEqual("CASE WHEN [T0].[BitColumnNull] IS NOT NULL THEN [T0].[BitColumnNull] ELSE [T0].[BitColumn] END = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' nullable boolean (following examples maybe don't make sence, but they work fine as a test)

        result = TranslateCondition(visitor, Function(x) If(x.BitColumnNull.HasValue, x.BitColumnNull, x.BitColumnNull).Value = value1)
        Assert.AreEqual("CASE WHEN [T0].[BitColumnNull] IS NOT NULL THEN [T0].[BitColumnNull] ELSE [T0].[BitColumnNull] END = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(value1, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) If(x.BitColumnNull.HasValue, x.BitColumnNull, x.BitColumnNull).Value = True)
        Assert.AreEqual("CASE WHEN [T0].[BitColumnNull] IS NOT NULL THEN [T0].[BitColumnNull] ELSE [T0].[BitColumnNull] END = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateCondition(visitor, Function(x) If(x.BitColumnNull.HasValue, x.BitColumnNull, x.BitColumnNull).Value)
        Assert.AreEqual("CASE WHEN [T0].[BitColumnNull] IS NOT NULL THEN [T0].[BitColumnNull] ELSE [T0].[BitColumnNull] END = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateCondition(visitor, Function(x) If(x.BitColumnNull.HasValue, x.BitColumnNull, x.BitColumnNull).HasValue)
        Assert.AreEqual("CASE WHEN [T0].[BitColumnNull] IS NOT NULL THEN [T0].[BitColumnNull] ELSE [T0].[BitColumnNull] END IS NOT NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Sub TranslateCoalesce()
      Using db = CreateDbContext()
        Dim visitor = CreateSqlExpressionVisitor(db)
        Dim result As SqlString

        Dim value1 = True
        Dim value2 = 42
        Dim value3 = 6

        result = TranslateCondition(visitor, Function(x) If(x.IntColumnNull, x.IntColumn) = value2)
        Assert.AreEqual("COALESCE([T0].[IntColumnNull], [T0].[IntColumn]) = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(value2, result.Parameters(0).Value)

        ' boolen and nullable boolean have special handling

        ' boolean

        result = TranslateCondition(visitor, Function(x) If(x.BitColumnNull, x.BitColumn) = value1)
        Assert.AreEqual("COALESCE([T0].[BitColumnNull], [T0].[BitColumn]) = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(value1, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) If(x.BitColumnNull, x.BitColumn) = True)
        Assert.AreEqual("COALESCE([T0].[BitColumnNull], [T0].[BitColumn]) = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateCondition(visitor, Function(x) If(x.BitColumnNull, x.BitColumn))
        Assert.AreEqual("COALESCE([T0].[BitColumnNull], [T0].[BitColumn]) = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        ' nullable boolean (following examples maybe don't make sence, but they work fine as a test)

        result = TranslateCondition(visitor, Function(x) If(x.BitColumnNull, x.BitColumnNull).Value = value1)
        Assert.AreEqual("COALESCE([T0].[BitColumnNull], [T0].[BitColumnNull]) = @p0", result.Sql)
        Assert.AreEqual(1, result.Parameters.Count)
        Assert.AreEqual(value1, result.Parameters(0).Value)

        result = TranslateCondition(visitor, Function(x) If(x.BitColumnNull, x.BitColumnNull).Value = True)
        Assert.AreEqual("COALESCE([T0].[BitColumnNull], [T0].[BitColumnNull]) = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateCondition(visitor, Function(x) If(x.BitColumnNull, x.BitColumnNull).Value)
        Assert.AreEqual("COALESCE([T0].[BitColumnNull], [T0].[BitColumnNull]) = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateCondition(visitor, Function(x) If(x.BitColumnNull, x.BitColumnNull).HasValue)
        Assert.AreEqual("COALESCE([T0].[BitColumnNull], [T0].[BitColumnNull]) IS NOT NULL", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Sub TranslateBitwiseOperators()
      Using db = CreateDbContext()
        Dim visitor = CreateSqlExpressionVisitor(db)
        Dim result As SqlString

        result = TranslateCondition(visitor, Function(x) (x.IntColumn And 3) = 1)
        Assert.AreEqual("([T0].[IntColumn] & 3) = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateCondition(visitor, Function(x) (x.IntColumn Or 3) = 1)
        Assert.AreEqual("([T0].[IntColumn] | 3) = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateCondition(visitor, Function(x) (x.IntColumn Xor 3) = 1)
        Assert.AreEqual("([T0].[IntColumn] ^ 3) = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateCondition(visitor, Function(x) (Not x.IntColumn) = 1)
        Assert.AreEqual("~ [T0].[IntColumn] = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Sub TranslateShiftOperators()
      Using db = CreateDbContext()
        Dim visitor = CreateSqlExpressionVisitor(db)
        Dim result As SqlString

        ' NOTE: "& 31" is added by compiler(?)

        result = TranslateCondition(visitor, Function(x) (x.IntColumn << 3) = 1)
        Assert.AreEqual("([T0].[IntColumn] << (3 & 31)) = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

        result = TranslateCondition(visitor, Function(x) (x.IntColumn >> 3) = 1)
        Assert.AreEqual("([T0].[IntColumn] >> (3 & 31)) = 1", result.Sql)
        Assert.AreEqual(0, result.Parameters.Count)

      End Using
    End Sub

    Private Function CreateSqlExpressionVisitor(db As DbContext) As SqlExpressionVisitor
      Dim builder = New SelectSqlExpressionBuilder(db, GetType(ItemWithAllSupportedValues))

      Dim fi = builder.GetType().GetField("m_Visitor", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
      Return DirectCast(fi.GetValue(builder), SqlExpressionVisitor)
    End Function

    Private Function TranslateCondition(visitor As SqlExpressionVisitor, predicate As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean))) As SqlString
      Return Translate(visitor, predicate, ExpressionTranslateMode.Condition)
    End Function

    Private Function TranslateConditionWithJoin(visitor As SqlExpressionVisitor, predicate As Expression(Of Func(Of Join(Of ItemWithAllSupportedValues, Article), Boolean))) As SqlString
      Return TranslateWithJoin(visitor, predicate, ExpressionTranslateMode.Condition)
    End Function

    Private Function Translate(visitor As SqlExpressionVisitor, predicate As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)), mode As ExpressionTranslateMode) As SqlString
      Return visitor.Translate(predicate, mode, {0}, 0, True, True)
    End Function

    Private Function TranslateWithJoin(visitor As SqlExpressionVisitor, predicate As Expression(Of Func(Of Join(Of ItemWithAllSupportedValues, Article), Boolean)), mode As ExpressionTranslateMode) As SqlString
      Return visitor.Translate(predicate, mode, Nothing, 0, True, True)
    End Function

  End Class
End Namespace
