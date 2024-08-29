using MarketDomain.Objects;
using Microsoft.Data.SqlClient;
using TurtleSQL.BaseClasses;
using TurtleSQL.Extensions;
using TurtleSQL.Interfaces;

namespace TurtleSQL.TickerRepositories
{
    public class TrackedTickerRepository : TickerRepository<TrackedTicker>, ITickerRepository<TrackedTicker>
    {
        protected override string TableName => "TrackedTicker";

        protected override List<string> FieldList => ["Ticker"];

        protected override IEnumerable<SqlParameter> SqlParameters(TrackedTicker entity)
        {
            var parms = new List<SqlParameter>
            {
                new("@Ticker", entity.Ticker.DBValue()),
            };

            return parms;
        }

        protected override IEnumerable<TrackedTicker> AllFromReader(SqlDataReader rdr)
        {
            while (rdr.Read())
            {
                yield return new TrackedTicker
                {
                    Ticker = rdr["Ticker"].ToString() ?? string.Empty,
                };
            }
        }
    }
}
