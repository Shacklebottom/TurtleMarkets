using MarketDomain.Objects;
using Microsoft.Data.SqlClient;
using TurtleSQL.BaseClasses;
using TurtleSQL.Extensions;
using TurtleSQL.Interfaces;

namespace TurtleSQL.TickerRepositories
{
    public class RecommendedTrendRepository : TickerRepository<RecommendedTrend>, ITickerRepository<RecommendedTrend>
    {
        protected override string TableName => "RecommendedTrend";
        
        protected override List<string> FieldList => ["Ticker", "Buy", "Hold", "Period", "Sell", "StrongBuy", "StrongSell"];

        protected override IEnumerable<SqlParameter> SqlParameters(RecommendedTrend entity)
        {
            var parms = new List<SqlParameter>
            {
                new("Ticker", entity.Ticker.DBValue()),
                new("Buy", entity.Buy.DBValue()),
                new("Hold", entity.Hold.DBValue()),
                new("Period", entity.Period.DBValue()),
                new("Sell", entity.Sell.DBValue()),
                new("StrongBuy", entity.StrongBuy.DBValue()),
                new("StrongSell", entity.StrongSell.DBValue())
            };

            return parms;
        }

        protected override IEnumerable<RecommendedTrend> AllFromReader(SqlDataReader rdr)
        {
            while (rdr.Read())
            {
                yield return new RecommendedTrend
                {
                    Ticker = rdr["Ticker"].ToString() ?? string.Empty,
                    Buy = rdr.ParseNullable<int>("Buy"),
                    Hold = rdr.ParseNullable<int>("Hold"),
                    Period = rdr.ParseNullable<DateTime>("Period"),
                    Sell = rdr.ParseNullable<int>("Sell"),
                    StrongBuy = rdr.ParseNullable<int>("StrongBuy"),
                    StrongSell = rdr.ParseNullable<int>("StrongSell")
                };
            }
        }
    }
}
