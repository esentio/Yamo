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
    public class ExpressionTests : BaseUnitTests
    {
        // It probably doesn't make sense to repeat all the tests from VB.NET here, since C# variant will result
        // in the same expression tree. It probably make sense to test only differences and C# specific stuff.
        // Something like:
        //    x => x.BitColumnNull is null
        //    x => x.BitColumnNull is not null
        //    ...
        // But "new" pattern matching operators (is, is not, or, and, ...) are not supported in expression trees (yet?).
        // If needed, add C# specific tests here in the future.

    }
}
