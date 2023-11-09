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
        protected override List<string> FieldList => new() { "Ticker" };
        protected override IEnumerable<SqlParameter> SqlParameters(WorkTicker entity)
        {
            var parms = new List<SqlParameter>
            {
                new SqlParameter(@"Ticker", entity.Ticker.DBValue()),
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
                    Id = rdr.Parse<int>("Id") ?? 0
                };
            }
        }
    }
}
