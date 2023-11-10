using MarketDomain;
using Microsoft.Data.SqlClient;
using TurtleSQL.Extensions;

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

        public T? GetById(int id)
        {
            var cmd = _sqlConnection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM {TableName} WHERE Id = {id}";

            _sqlConnection.Open();
            var rdr = cmd.ExecuteReader();
            T? result = this.AllFromReader(rdr).FirstOrDefault();
            _sqlConnection.Close();

            return result;
        }

        #region OVERRIDE THESE METHODS
        protected virtual string TableName => "";
        protected virtual List<string> FieldList => new();
        protected virtual IEnumerable<SqlParameter> SqlParameters(T entity) => new List<SqlParameter>();
        protected virtual IEnumerable<T> AllFromReader(SqlDataReader rdr) => new List<T>();
        #endregion

        public IEnumerable<T>? GetAll()
        {
            var cmd = _sqlConnection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM {TableName}";

            _sqlConnection.Open();
            var rdr = cmd.ExecuteReader();
            IEnumerable<T>? result = this.AllFromReader(rdr).ToList();
            _sqlConnection.Close();

            return result;
        }

        public int Save(T entity)
        {
            var cmd = _sqlConnection.CreateCommand();

            // does it already exist?
            if (GetById(entity.Id) == null)
            {
                return Insert(entity, cmd);
            }
            else
            {
                Update(entity, cmd);
                return (entity.Id);
            }
        }

        private int Insert(T entity, SqlCommand cmd)
        {
            var fields = FieldList;

            var fieldNames = string.Join(',', FieldList.Select(fn => $"[{fn}]"));
            var parameterNames = string.Join(',', FieldList.Select(fn => $"@{fn}"));

            cmd.CommandText = $"INSERT INTO {TableName}({fieldNames}) VALUES({parameterNames})";
            cmd.Parameters.AddRange(this.SqlParameters(entity).ToArray());

            _sqlConnection.Open();
            cmd.ExecuteNonQuery();
            var identity = GetLastIdentity();
            _sqlConnection.Close();

            return identity;
        }

        private int GetLastIdentity()
        {
            var cmd = _sqlConnection.CreateCommand();
            cmd.CommandText = "SELECT CAST(@@IDENTITY AS int)";
            var identity = (int)cmd.ExecuteScalar();
            return identity;
        }
        private void Update(T entity, SqlCommand cmd)
        {
            var fields = FieldList;
            fields.Add("Id");

            var updateClause = $"UPDATE {TableName} SET";
            var values = string.Join(',', fields.Where(fn => fn != "Id").Select(fn => $"[{fn}]=@{fn}"));
            var whereClause = "WHERE Id = @Id";
            cmd.CommandText = $"{updateClause} {values} {whereClause}";

            var parms = this.SqlParameters(entity).Where(p => p.ParameterName != "@Id").ToArray();
            cmd.Parameters.AddRange(parms);
            cmd.Parameters.AddWithValue("@Id", entity.Id.DBValue());

            _sqlConnection.Open();
            cmd.ExecuteNonQuery();
            _sqlConnection.Close();

            return;
        }
    }
}
