using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleSQL.Extensions
{
    internal static class ObjectExtensions
    {
        public static object DBValue(this object? obj)
        {
            return obj ?? DBNull.Value;
        }
    }
}
