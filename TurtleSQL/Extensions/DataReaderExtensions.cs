using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleSQL.Extensions
{
    public static class DataReaderExtensions
    {
        public static T? Parse<T>(this SqlDataReader reader, string fieldName) where T : struct
        {
            var colIndex = reader.GetOrdinal(fieldName);
            
            if (reader.IsDBNull(colIndex)) return null;
            
            return (T)reader[colIndex];
        }
    }
}
