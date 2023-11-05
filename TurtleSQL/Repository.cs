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
        public Repository()
        {
            var cnString = "data source=.;integrated security=sspi;initial catalog=MarketData;";
            _sqlConnection = new SqlConnection(cnString);
        }

        public T? Get(int id)
        {
            var cmd = _sqlConnection.CreateCommand();
            cmd.CommandText = this.GetIdSql;

            var rdr = cmd.ExecuteReader();
            T? result = this.AllFromReader(rdr)?.FirstOrDefault();

            return result;
        }

        protected virtual string GetIdSql => "";
        protected virtual string GetAllSql => "";
        protected virtual IEnumerable<T>? AllFromReader(SqlDataReader rdr) => null;
        
        public IEnumerable<T>? GetAll()
        {
            var cmd = _sqlConnection.CreateCommand();
            cmd.CommandText = this.GetAllSql;

            var rdr = cmd.ExecuteReader();
            IEnumerable<T>? result = this.AllFromReader(rdr);

            return result;
        }
    }
}
