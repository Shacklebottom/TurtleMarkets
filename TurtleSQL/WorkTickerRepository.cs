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
    public class WorkTickerRepository : Repository<WorkTicker>, IRepository<WorkTicker>
    {
        protected override string TableName => "WorkTicker";
        protected override List<string> FieldList => new() { "Ticker", "Open", "Close", "High", "Low", "Buy", "Sell", "Hold" };
        protected override IEnumerable<SqlParameter> SqlParameters(WorkTicker entity)
        {

            var parms = new List<SqlParameter>
            {
                new SqlParameter(@"Ticker", entity.Ticker.DBValue()),
                new SqlParameter(@"Open", entity.Open.DBValue()),
                new SqlParameter(@"Close", entity.Close.DBValue()),
                new SqlParameter(@"High", entity.High.DBValue()),
                new SqlParameter(@"Low", entity.Low.DBValue()),
                new SqlParameter(@"Buy", entity.Buy.DBValue()),
                new SqlParameter(@"Sell", entity.Sell.DBValue()),
                new SqlParameter(@"Hold", entity.Hold.DBValue())
            };
            return parms;
        }
        protected override IEnumerable<WorkTicker> AllFromReader(SqlDataReader rdr)
        {
            while (rdr.Read())
            {
                yield return new WorkTicker
                {
                    Ticker = rdr["Ticker"].ToString(),
                    Open = rdr.ParseNullable<decimal>("Open"),
                    Close = rdr.ParseNullable<decimal>("Close"),
                    High = rdr.ParseNullable<decimal>("High"),
                    Low = rdr.ParseNullable<decimal>("Low"),
                    Buy = rdr.ParseNullable<int>("Buy"),
                    Sell = rdr.ParseNullable<int>("Sell"),
                    Hold = rdr.ParseNullable<int>("Hold"),
                    Id = rdr.ParseNullable<int>("Id") ?? 0
                };
            }
        }
    }
}
