using MarketDomain.Interfaces;
using Microsoft.Data.SqlClient;
using TurtleSQL.Extensions;
using TurtleSQL.Interfaces;

namespace TurtleSQL.BaseClasses
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        internal SqlConnection _sqlConnection;

        #region OVERRIDE THESE METHODS
        protected virtual string TableName => "";
        protected virtual List<string> FieldList => [];
        protected virtual IEnumerable<SqlParameter> SqlParameters(T entity) => [];
        protected virtual IEnumerable<T> AllFromReader(SqlDataReader rdr) => [];
        #endregion

        //constructor
        internal Repository()
        {
            var cnString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MarketData;Data Source=.;TrustServerCertificate=true";
            _sqlConnection = new SqlConnection(cnString);
        }

        #region GET FROM REPO
        public T? GetById(int id)
        {
            var cmd = _sqlConnection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM {TableName} WHERE Id = {id}";

            _sqlConnection.Open();
            var rdr = cmd.ExecuteReader();
            T? result = AllFromReader(rdr).FirstOrDefault();
            _sqlConnection.Close();

            return result;
        }

        public IEnumerable<T> GetAll()
        {
            var cmd = _sqlConnection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM {TableName}";

            _sqlConnection.Open();
            var rdr = cmd.ExecuteReader();
            IEnumerable<T> result = AllFromReader(rdr).ToList();
            _sqlConnection.Close();

            return result;
        }
        #endregion

        #region PUT INTO REPO
        public void Save(T entity)
        {
            var cmd = _sqlConnection.CreateCommand();

            // does it already exist?
            if (GetById(entity.Id) == null)
            {
                Insert(entity, cmd);
            }
            else
            {
                Update(entity, cmd);
            }
        }

        private void Insert(T entity, SqlCommand cmd)
        {
            var fieldNames = string.Join(',', FieldList.Select(fn => $"[{fn}]"));
            var parameterNames = string.Join(',', FieldList.Select(fn => $"@{fn}"));

            cmd.CommandText = $"INSERT INTO {TableName}({fieldNames}) VALUES({parameterNames})";
            cmd.Parameters.AddRange(SqlParameters(entity).ToArray());

            _sqlConnection.Open();
            cmd.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        private void Update(T entity, SqlCommand cmd)
        {
            var fields = FieldList;
            fields.Add("Id");

            var updateClause = $"UPDATE {TableName} SET";
            var values = string.Join(',', fields.Where(fn => fn != "Id").Select(fn => $"[{fn}]=@{fn}"));
            var whereClause = "WHERE Id = @Id";
            cmd.CommandText = $"{updateClause} {values} {whereClause}";

            var parms = SqlParameters(entity).Where(p => p.ParameterName != "@Id").ToArray();
            cmd.Parameters.AddRange(parms);
            cmd.Parameters.AddWithValue("@Id", entity.Id.DBValue());

            _sqlConnection.Open();
            cmd.ExecuteNonQuery();
            _sqlConnection.Close();
        }
        #endregion

        #region MODIFY REPO
        public void TruncateTable()
        {
            var cmd = _sqlConnection.CreateCommand();
            cmd.CommandText = $"TRUNCATE TABLE {TableName}";

            _sqlConnection.Open();
            cmd.ExecuteNonQuery();
            _sqlConnection.Close();
        }
        #endregion
    }
}
