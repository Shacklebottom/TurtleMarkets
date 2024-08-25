using MarketDomain.Objects;
using Microsoft.Data.SqlClient;
using TurtleSQL.BaseClasses;
using TurtleSQL.Extensions;
using TurtleSQL.Interfaces;

namespace TurtleSQL.TickerRepositories
{
    public class ListedStatusRepository : TickerRepository<ListedStatus>, ITickerRepository<ListedStatus>
    {
        protected override string TableName => "ListedStatus";

        protected override List<string> FieldList => ["Ticker", "Name", "Exchange", "Type", "IPOdate", "DelistingDate", "Status"];

        protected override IEnumerable<SqlParameter> SqlParameters(ListedStatus entity)
        {
            var parms = new List<SqlParameter>
            {
                new("@Ticker", entity.Ticker.DBValue()),
                new("@Name", entity.Name.DBValue()),
                new("@Exchange", entity.Exchange.DBValue()),
                new("@Type", entity.Type.DBValue()),
                new("@IPOdate", entity.IPOdate.DBValue()),
                new("@DelistingDate", entity.DelistingDate.DBValue()),
                new("@Status", entity.Status.DBValue())
            };
            return parms;
        }

        protected override IEnumerable<ListedStatus> AllFromReader(SqlDataReader rdr)
        {
            while (rdr.Read())
            {
                yield return new ListedStatus
                {
                    Ticker = rdr["Ticker"].ToString() ?? string.Empty,
                    Name = rdr["Name"].ToString(),
                    Exchange = rdr["Exchange"].ToString(),
                    Type = rdr["Type"].ToString(),
                    IPOdate = rdr.ParseNullable<DateTime>("IPOdate"),
                    DelistingDate = rdr.ParseNullable<DateTime>("DelistingDate"),
                    Status = rdr["Status"].ToString(),
                    Id = rdr.ParseNullable<int>("Id") ?? 0
                };
            }
        }
    }
}
