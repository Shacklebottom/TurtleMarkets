using Microsoft.Data.SqlClient;

namespace TurtleSQL.Extensions
{
    public static class DataReaderExtensions
    {
        public static T? ParseNullable<T>(this SqlDataReader reader, string fieldName) where T : struct
        {
            var colIndex = reader.GetOrdinal(fieldName);
            
            if (reader.IsDBNull(colIndex)) return null;
            
            return (T)reader[colIndex];
        }
    }
}
