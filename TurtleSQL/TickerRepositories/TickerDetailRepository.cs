using MarketDomain.Objects;
using Microsoft.Data.SqlClient;
using TurtleSQL.BaseClasses;
using TurtleSQL.Extensions;
using TurtleSQL.Interfaces;

namespace TurtleSQL.TickerRepositories
{
    public class TickerDetailRepository : TickerRepository<TickerDetail>, ITickerRepository<TickerDetail>
    {
        protected override string TableName => "TickerDetail";

        protected override List<string> FieldList => ["Ticker", "Name", "Description", "Address", "City", "State", "TotalEmployees", "ListDate"];
        
        protected override IEnumerable<SqlParameter> SqlParameters(TickerDetail entity)
        {
            var parms = new List<SqlParameter>
            {
                new("Ticker", entity.Ticker.DBValue()),
                new("Name", entity.Name.DBValue()),
                new("Description", entity.Description.DBValue()),
                new("Address", entity.Address?.DBValue()),
                new("City", entity.City?.DBValue()),
                new("State", entity.State?.DBValue()),
                new("TotalEmployees", entity.TotalEmployees.DBValue()),
                new("ListDate", entity.ListDate.DBValue())
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
                    TotalEmployees = rdr.ParseNullable<int>("TotalEmployees"),
                    ListDate = rdr.ParseNullable<DateTime>("ListDate"),
                    Id = rdr.ParseNullable<int>("Id") ?? 0
                };
            };
        }
    }
}
