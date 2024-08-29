using MarketDomain.Objects;
using Microsoft.Data.SqlClient;
using TurtleSQL.BaseClasses;
using TurtleSQL.Extensions;
using TurtleSQL.Interfaces;

namespace TurtleSQL.TickerRepositories
{
    public class SnapshotRepository : TickerRepository<PreviousClose>, ITickerRepository<PreviousClose>
    {
        protected override string TableName => "Snapshot";

        protected override List<string> FieldList => ["Close", "Date", "High", "Low", "Open", "Ticker", "Volume"];

        protected override IEnumerable<SqlParameter> SqlParameters(PreviousClose entity)
        {
            var parms = new List<SqlParameter>
            {
                new("@Close", entity.Close.DBValue()),
                new("@Date", entity.Date.DBValue()),
                new("@High", entity.High.DBValue()),
                new("@Low", entity.Low.DBValue()),
                new("@Open", entity.Open.DBValue()),
                new("@Ticker", entity.Ticker.DBValue()),
                new("@Volume", entity.Volume.DBValue())
            };

            return parms;
        }

        protected override IEnumerable<PreviousClose> AllFromReader(SqlDataReader rdr)
        {
            while (rdr.Read())
            {
                yield return new PreviousClose
                {
                    Close = rdr.ParseNullable<double>("Close"),
                    Date = rdr.ParseNullable<DateTime>("Date"),
                    High = rdr.ParseNullable<double>("High"),
                    Low = rdr.ParseNullable<double>("Low"),
                    Open = rdr.ParseNullable<double>("Open"),
                    Ticker = rdr["Ticker"].ToString() ?? string.Empty,
                    Volume = rdr.ParseNullable<long>("Volume"),
                    Id = rdr.ParseNullable<int>("Id") ?? 0
                };
            }
        }
    }
}
