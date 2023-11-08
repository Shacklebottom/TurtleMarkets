using MarketDomain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleSQL.Extensions;

namespace TurtleSQL
{
    //This should work, but needs to be validated!
    public class RecommendedTrendRepository : Repository<RecommendedTrend>, IRepository<RecommendedTrend>
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
                    Ticker = rdr["Ticker"].ToString(),
                    Buy = rdr.Parse<int>("Buy"),
                    Hold = rdr.Parse<int>("Hold"),
                    Period = rdr.Parse < DateOnly>("Period"),
                    Sell = rdr.Parse<int>("Sell"),
                    StrongBuy = rdr.Parse<int>("StrongBuy"),
                    StrongSell = rdr.Parse<int>("StrongSell")
                };
            }
        }
    }
}
