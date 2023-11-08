using MarketDomain;
using Microsoft.Data.SqlClient;
using TurtleSQL.Extensions;

namespace TurtleSQL
{
    //This should work, but needs to be validated!
    public class ListedStatusRepository : Repository<ListedStatus>, IRepository<ListedStatus>
    {
        protected override string TableName => "ListedStatus";
        protected override IEnumerable<SqlParameter> SqlParameters(ListedStatus entity)
        {
            var parms = new List<SqlParameter>
            {
                new SqlParameter("@Ticker", entity.Ticker.DBValue()),
                new SqlParameter("@Name", entity.Name.DBValue()),
                new SqlParameter("@Exchange", entity.Exchange.DBValue()),
                new SqlParameter("@Type", entity.Type.DBValue()),
                new SqlParameter("@IPOdate", entity.IPOdate.DBValue()),
                new SqlParameter("@DelistingDate", entity.DelistingDate.DBValue()),
                new SqlParameter("@Status", entity.Status.DBValue())
            };
            return parms;
        }
        protected override List<string> FieldList => new() { "Ticker", "Name", "Exchange", "Type", "IPOdate", "DelistingDate", "Status" };
        protected override IEnumerable<ListedStatus> AllFromReader(SqlDataReader rdr)
        {
            while (rdr.Read())
            {
                yield return new ListedStatus
                {
                    Ticker = rdr["Ticker"].ToString(),
                    Name = rdr["Name"].ToString(),
                    Exchange = rdr["Exchange"].ToString(),
                    Type = rdr["Type"].ToString(),
                    IPOdate = rdr.Parse<DateOnly>("IPOdate"),
                    DelistingDate = rdr.Parse<DateOnly>("DelistingDate"),
                    Status = rdr["Status"].ToString(),
                    Id = rdr.Parse<int>("Id") ?? 0
                };
            }
        }
    }
}
