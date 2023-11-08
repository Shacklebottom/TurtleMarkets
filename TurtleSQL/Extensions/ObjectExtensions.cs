

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
