using MarketDomain.Objects;
using Microsoft.Data.SqlClient;
using TurtleSQL.BaseClasses;
using TurtleSQL.Extensions;
using TurtleSQL.Interfaces;

namespace TurtleSQL.TickerRepositories
{
    public class ProminenceRepository : TickerRepository<Prominence>, ITickerRepository<Prominence>
    {
        protected override string TableName => "Prominence";
     
        protected override List<string> FieldList => ["PrestigeType", "Date", "Ticker", "Price", "ChangeAmount", "ChangePercentage", "Volume"];
        
        protected override IEnumerable<SqlParameter> SqlParameters(Prominence entity)
        {
            var parms = new List<SqlParameter>
            {
                new("PrestigeType", entity.PrestigeType.DBValue()),
                new("Date", entity.Date.DBValue()),
                new("@Ticker", entity.Ticker.DBValue()),
                new("@Price", entity.Price.DBValue()),
                new("@ChangeAmount", entity.ChangeAmount.DBValue()),
                new("@ChangePercentage", entity.ChangePercentage.DBValue()),
                new("@Volume", entity.Volume.DBValue())
            };
            return parms;
        }

        protected override IEnumerable<Prominence> AllFromReader(SqlDataReader rdr)
        {
            while (rdr.Read())
            {
                yield return new Prominence
                {
                    PrestigeType = rdr["PrestigeType"].ToString(),
                    Date = rdr.ParseNullable<DateTime>("Date"),
                    Ticker = rdr["Ticker"].ToString() ?? string.Empty,
                    Price = rdr.ParseNullable<double>("Price"),
                    ChangeAmount = rdr.ParseNullable<double>("ChangeAmount"),
                    ChangePercentage = rdr["ChangePercentage"].ToString(),
                    Volume = rdr.ParseNullable<long>("Volume"),
                    Id = rdr.ParseNullable<int>("Id") ?? 0
                };
            }
        }
    }
}
