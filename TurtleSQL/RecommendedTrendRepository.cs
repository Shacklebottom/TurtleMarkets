using MarketDomain;
using Microsoft.Data.SqlClient;
using TurtleSQL.Extensions;

namespace TurtleSQL
{
    //This should work, but needs to be validated!
    public class RecommendedTrendRepository : TickerRepository<RecommendedTrend>, ITickerRepository<RecommendedTrend>
    {
        protected override string TableName => "RecommendedTrend";
        protected override List<string> FieldList => new() { "Ticker", "Buy", "Hold", "Period", "Sell", "StrongBuy", "StrongSell"};
        protected override IEnumerable<SqlParameter> SqlParameters(RecommendedTrend entity)
        {
            var parms = new List<SqlParameter>
            {
                new SqlParameter("Ticker", entity.Ticker.DBValue()),
                new SqlParameter("Buy", entity.Buy.DBValue()),
                new SqlParameter("Hold", entity.Hold.DBValue()),
                new SqlParameter("Period", entity.Period.DBValue()),
                new SqlParameter("Sell", entity.Sell.DBValue()),
                new SqlParameter("StrongBuy", entity.StrongBuy.DBValue()),
                new SqlParameter("StrongSell", entity.StrongSell.DBValue())
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
                    Period = rdr.ParseNullable <DateTime>("Period"),
                    Sell = rdr.ParseNullable<int>("Sell"),
                    StrongBuy = rdr.ParseNullable<int>("StrongBuy"),
                    StrongSell = rdr.ParseNullable<int>("StrongSell")
                };
            }
        }
    }
}
