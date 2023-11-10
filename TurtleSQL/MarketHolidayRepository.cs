using MarketDomain;
using Microsoft.Data.SqlClient;
using TurtleSQL.Extensions;

namespace TurtleSQL
{
    //This should work, but needs to be validated!
    public class MarketHolidayRepository : Repository<MarketHoliday>, IRepository<MarketHoliday>
    {
        protected override string TableName => "MarketHoliday";
        protected override List<string> FieldList => new() { "Exchange", "Date", "Holiday", "Status", "Open", "Close" };
        protected override IEnumerable<SqlParameter> SqlParameters(MarketHoliday entity)
        {
            var parms = new List<SqlParameter>
            {
                new SqlParameter("Exchange", entity.Exchange.DBValue()),
                new SqlParameter("Date", entity.Date.DBValue()),
                new SqlParameter("Holiday", entity.Holiday.DBValue()),
                new SqlParameter("Status", entity.Status.DBValue()),
                new SqlParameter("Open", entity.Open.DBValue()),
                new SqlParameter("Close", entity.Close.DBValue())
            };
            return parms;
        }
        protected override IEnumerable<MarketHoliday> AllFromReader(SqlDataReader rdr)
        {
            while (rdr.Read())
            {
                yield return new MarketHoliday()
                {
                    Exchange = rdr["Exchange"].ToString(),
                    Date = rdr.ParseNullable<DateTime>("Date"),
                    Holiday = rdr["Holiday"].ToString(),
                    Status = rdr["Status"].ToString(),
                    Open = rdr.ParseNullable<DateTime>("Open"),
                    Close = rdr.ParseNullable<DateTime>("Close")
                };
            }
        }
    }
}
