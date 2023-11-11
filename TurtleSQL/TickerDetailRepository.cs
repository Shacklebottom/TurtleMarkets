using MarketDomain;
using Microsoft.Data.SqlClient;
using TurtleSQL.Extensions;

namespace TurtleSQL
{
    //This should work, but needs to be validated! FURTHER, I think I did the 'AllFromReader' TickerDetail properly.
    public class TickerDetailRepository : TickerRepository<TickerDetail>, ITickerRepository<TickerDetail>
    {
        protected override string TableName => "TickerDetail";
        protected override List<string> FieldList => new() { "Ticker", "Name", "Description", "Address", "City", "State", "TotalEmployees", "ListDate" };
        protected override IEnumerable<SqlParameter> SqlParameters(TickerDetail entity)
        {
            var parms = new List<SqlParameter>
            {
                new SqlParameter("Ticker", entity.Ticker.DBValue()),
                new SqlParameter("Name", entity.Name.DBValue()),
                new SqlParameter("Description", entity.Description.DBValue()),
                new SqlParameter("Address", entity.Address?.DBValue()),
                new SqlParameter("City", entity.City?.DBValue()),
                new SqlParameter("State", entity.State?.DBValue()),
                new SqlParameter("TotalEmployees", entity.TotalEmployees.DBValue()),
                new SqlParameter("ListDate", entity.ListDate.DBValue())
            };
            return parms;
        }
        protected override IEnumerable<TickerDetail> AllFromReader(SqlDataReader rdr)
        {
            while (rdr.Read())
            {
                yield return new TickerDetail
                {
                    Ticker = rdr["Ticker"].ToString() ?? string.Empty,
                    Name = rdr["Name"].ToString(),
                    Description = rdr["Description"].ToString(),
                    Address = rdr["Address"].ToString(),
                    City = rdr["City"].ToString(),
                    State = rdr["State"].ToString(),
                    TotalEmployees = rdr.ParseNullable<Int32>("TotalEmployees"),
                    ListDate = rdr.ParseNullable<DateTime>("ListDate"),
                    Id = rdr.ParseNullable<int>("Id") ?? 0
                };
            };
        }
    }
}
