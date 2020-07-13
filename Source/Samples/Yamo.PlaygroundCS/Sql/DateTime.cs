using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yamo.Infrastructure;
using Yamo.Sql;

namespace Yamo.PlaygroundCS.Sql
{
    public class DateTime : SqlHelper
    {

        public static bool SameYear(System.DateTime date1, System.DateTime date2)
        {
            throw new Exception("This method is not intended to be called directly.");
        }

        public static bool SameQuarter(System.DateTime date1, System.DateTime date2)
        {
            throw new Exception("This method is not intended to be called directly.");
        }

        // ...

        public static new SqlFormat GetSqlFormat(MethodCallExpression method, SqlDialectProvider dialectProvider)
        {
            switch (method.Method.Name)
            {
                case nameof(DateTime.SameYear):
                    return new SqlFormat("DATEDIFF(year, {0}, {1}) = 0", method.Arguments);
                case nameof(DateTime.SameQuarter):
                    return new SqlFormat("DATEDIFF(quarter, {0}, {1}) = 0", method.Arguments);
                // ...
                default:
                    throw new NotSupportedException($"Method '{method.Method.Name}' is not supported.");
            }
        }
    }
}
