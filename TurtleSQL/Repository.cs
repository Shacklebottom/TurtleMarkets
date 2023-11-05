using MarketDomain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleSQL
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        internal SqlConnection _sqlConnection;

        // constructor
        internal Repository()
        {
            var cnString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MarketData;Data Source=.;TrustServerCertificate=true";
            _sqlConnection = new SqlConnection(cnString);
        }

        public T? Get(int id)
        {
            var cmd = _sqlConnection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM {TableName} WHERE Id = {id}";

            _sqlConnection.Open();
            var rdr = cmd.ExecuteReader();
            T? result = this.AllFromReader(rdr).FirstOrDefault();
            _sqlConnection.Close();

            return result;
        }

        protected virtual string TableName => "";
        protected virtual IEnumerable<T> AllFromReader(SqlDataReader rdr) => new List<T>();
        
        public IEnumerable<T>? GetAll()
        {
            var cmd = _sqlConnection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM {TableName}";

            _sqlConnection.Open();
            var rdr = cmd.ExecuteReader();
            IEnumerable<T>? result = this.AllFromReader(rdr).ToList();

            return result;
        }
    }
}
