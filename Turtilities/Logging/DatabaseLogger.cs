using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logging
{
    public class DatabaseLogger : ILogger
    {
        private readonly SqlConnection _connection;

        public DatabaseLogger(string? cnString = null)
        {
            _connection = new SqlConnection(cnString ?? "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MarketData;Data Source=.;TrustServerCertificate=true");
        }

        public void Log(string message)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Logs(message) VALUES(@message)";
            cmd.Parameters.AddWithValue("@message", message);

            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }
}
