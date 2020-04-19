using System;
using System.Collections.Generic;
using System.Linq;
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

        public static new string GetSqlFormat(MethodInfo method, SqlDialectProvider dialectProvider)
        {
            switch (method.Name)
            {
                case nameof(DateTime.SameYear):
                    return "DATEDIFF(year, {0}, {1}) = 0";
                case nameof(DateTime.SameQuarter):
                    return "DATEDIFF(quarter, {0}, {1}) = 0";
                // ...
                default:
                    throw new NotSupportedException($"Method '{method.Name}' is not supported.");
            }
        }
    }
}
