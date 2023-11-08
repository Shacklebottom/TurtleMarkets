using MarketDomain;
using Microsoft.Data.SqlClient;
using TurtleSQL.Extensions;

namespace TurtleSQL
{
    public class PreviousCloseRepository : Repository<PreviousClose>, IRepository<PreviousClose>
    {
        protected override string TableName => "PreviousClose";
        protected override IEnumerable<SqlParameter> SqlParameters(PreviousClose entity)
        {
            var parms = new List<SqlParameter>
            {
                new SqlParameter("@Close", entity.Close.DBValue()),
                new SqlParameter("@Date", entity.Date.DBValue()),
                new SqlParameter("@High", entity.High.DBValue()),
                new SqlParameter("@Low", entity.Low.DBValue()),
                new SqlParameter("@Open", entity.Open.DBValue()),
                new SqlParameter("@Ticker", entity.Ticker.DBValue()),
                new SqlParameter("@Volume", entity.Volume.DBValue())
            };

            return parms;
        }
        protected override List<string> FieldList => new() { "Close", "Date", "High", "Low", "Open", "Ticker", "Volume" };

        protected override IEnumerable<PreviousClose> AllFromReader(SqlDataReader rdr)
        {
            while (rdr.Read())
            {
                yield return new PreviousClose
                {
                    Close = rdr.Parse<decimal>("Close"),
                    Date = rdr.Parse<DateTime>("Date"),
                    High = rdr.Parse<decimal>("High"),
                    Low = rdr.Parse<decimal>("Low"),
                    Open = rdr.Parse<decimal>("Open"),
                    Ticker = rdr["Ticker"].ToString(),
                    Volume = rdr.Parse<long>("Volume"),
                    Id = rdr.Parse<int>("Id") ?? 0
                };
            }
        }

    }
}
