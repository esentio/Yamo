using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Yamo.Expressions.Builders;
using Yamo.Internal;
using Yamo.Internal.Query;
using Yamo.Test.Model;
using Yamo.Test.Tests;

namespace Yamo.Test.CS
{
    [TestClass]
    public class ExpressionTests : BaseExpressionTests
    {
        // It probably doesn't make sense to repeat all the tests from VB.NET here, since C# variant will result
        // in the same expression tree. It probably make sense to test only differences and C# specific stuff.
        // Something like:
        //    x => x.BitColumnNull is null
        //    x => x.BitColumnNull is not null
        //    ...
        // But "new" pattern matching operators (is, is not, or, and, ...) are not supported in expression trees (yet?).
        // If needed, add C# specific tests here in the future.

        [TestMethod()]
        public void TranslateEnumerableContains()
        {
            // see https://github.com/esentio/Yamo/issues/112

            using (var db = CreateDbContext())
            {
                var visitor = CreateSqlExpressionVisitor(db);
                SqlString result;

                // Enumerable

                result = TranslateCondition(visitor, x => new int[] { }.Contains(x.IntColumn));
                Assert.AreEqual("(0 = 1)", result.Sql);
                Assert.AreEqual(0, result.Parameters.Count);

                int[] emptyArray = new int[] { };
                result = TranslateCondition(visitor, x => emptyArray.Contains(x.IntColumn));
                Assert.AreEqual("(0 = 1)", result.Sql);
                Assert.AreEqual(0, result.Parameters.Count);

                var value1 = 1;
                var value2 = 2;
                result = TranslateCondition(visitor, x => new int[] { value1, value2 }.Contains(x.IntColumn));
                Assert.AreEqual("([T0].[IntColumn] IN (@p0, @p1))", result.Sql);
                Assert.AreEqual(2, result.Parameters.Count);
                Assert.AreEqual(value1, result.Parameters[0].Value);
                Assert.AreEqual(value2, result.Parameters[1].Value);

                int[] array = new int[] { 3, 4 };
                result = TranslateCondition(visitor, x => array.Contains(x.IntColumn));
                Assert.AreEqual("([T0].[IntColumn] IN (@p0, @p1))", result.Sql);
                Assert.AreEqual(2, result.Parameters.Count);
                Assert.AreEqual(array[0], result.Parameters[0].Value);
                Assert.AreEqual(array[1], result.Parameters[1].Value);

                // IEnumerable

                var emptyList = new List<int>();
                result = TranslateCondition(visitor, x => emptyList.Contains(x.IntColumn));
                Assert.AreEqual("(0 = 1)", result.Sql);
                Assert.AreEqual(0, result.Parameters.Count);

                var list = new List<int>() { 5, 6 };
                result = TranslateCondition(visitor, x => list.Contains(x.IntColumn));
                Assert.AreEqual("([T0].[IntColumn] IN (@p0, @p1))", result.Sql);
                Assert.AreEqual(2, result.Parameters.Count);
                Assert.AreEqual(list[0], result.Parameters[0].Value);
                Assert.AreEqual(list[1], result.Parameters[1].Value);
            }
        }

        [TestMethod()]
        public void TranslateStringStartsWith()
        {
            using (var db = CreateDbContext())
            {
                var visitor = CreateSqlExpressionVisitor(db);
                SqlString result;

                result = TranslateCondition(visitor, x => x.Nvarchar50Column.StartsWith("lorem"));
                Assert.AreEqual("[T0].[Nvarchar50Column] LIKE @p0 + '%'", result.Sql);
                Assert.AreEqual(1, result.Parameters.Count);
                Assert.AreEqual("lorem", result.Parameters[0].Value);

                var value = "ipsum";
                result = TranslateCondition(visitor, x => x.Nvarchar50Column.StartsWith(value));
                Assert.AreEqual("[T0].[Nvarchar50Column] LIKE @p0 + '%'", result.Sql);
                Assert.AreEqual(1, result.Parameters.Count);
                Assert.AreEqual(value, result.Parameters[0].Value);
            }
        }

        [TestMethod()]
        public void TranslateStringEndsWith()
        {
            using (var db = CreateDbContext())
            {
                var visitor = CreateSqlExpressionVisitor(db);
                SqlString result;

                result = TranslateCondition(visitor, x => x.Nvarchar50Column.EndsWith("lorem"));
                Assert.AreEqual("[T0].[Nvarchar50Column] LIKE '%' + @p0", result.Sql);
                Assert.AreEqual(1, result.Parameters.Count);
                Assert.AreEqual("lorem", result.Parameters[0].Value);

                var value = "ipsum";
                result = TranslateCondition(visitor, x => x.Nvarchar50Column.EndsWith(value));
                Assert.AreEqual("[T0].[Nvarchar50Column] LIKE '%' + @p0", result.Sql);
                Assert.AreEqual(1, result.Parameters.Count);
                Assert.AreEqual(value, result.Parameters[0].Value);
            }
        }

        [TestMethod()]
        public void TranslateStringContains()
        {
            using (var db = CreateDbContext())
            {
                var visitor = CreateSqlExpressionVisitor(db);
                SqlString result;

                result = TranslateCondition(visitor, x => x.Nvarchar50Column.Contains("lorem"));
                Assert.AreEqual("[T0].[Nvarchar50Column] LIKE '%' + @p0 + '%'", result.Sql);
                Assert.AreEqual(1, result.Parameters.Count);
                Assert.AreEqual("lorem", result.Parameters[0].Value);

                var value = "ipsum";
                result = TranslateCondition(visitor, x => x.Nvarchar50Column.Contains(value));
                Assert.AreEqual("[T0].[Nvarchar50Column] LIKE '%' + @p0 + '%'", result.Sql);
                Assert.AreEqual(1, result.Parameters.Count);
                Assert.AreEqual(value, result.Parameters[0].Value);
            }
        }

        [TestMethod()]
        public void TranslateStringConcat()
        {
            using (var db = CreateDbContext())
            {
                var visitor = CreateSqlExpressionVisitor(db);
                SqlString result;

                result = TranslateCondition(visitor, x => x.Nvarchar50Column == String.Concat("lorem", "ipsum"));
                Assert.AreEqual("[T0].[Nvarchar50Column] = @p0 + @p1", result.Sql);
                Assert.AreEqual(2, result.Parameters.Count);
                Assert.AreEqual("lorem", result.Parameters[0].Value);
                Assert.AreEqual("ipsum", result.Parameters[1].Value);

                var value1 = "dolor";
                var value2 = "sit";
                result = TranslateCondition(visitor, x => x.Nvarchar50Column == String.Concat(new string?[] { "lorem", "ipsum", value1, value2, "amet" }));
                Assert.AreEqual("[T0].[Nvarchar50Column] = @p0 + @p1 + @p2 + @p3 + @p4", result.Sql);
                Assert.AreEqual(5, result.Parameters.Count);
                Assert.AreEqual("lorem", result.Parameters[0].Value);
                Assert.AreEqual("ipsum", result.Parameters[1].Value);
                Assert.AreEqual(value1, result.Parameters[2].Value);
                Assert.AreEqual(value2, result.Parameters[3].Value);
                Assert.AreEqual("amet", result.Parameters[4].Value);
            }
        }
    }
}
