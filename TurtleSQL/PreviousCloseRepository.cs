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
    public class PreviousCloseRepository : Repository<PreviousClose>, IRepository<PreviousClose>
    {
        protected override string TableName => "PreviousClose";
        protected override IEnumerable<PreviousClose> AllFromReader(SqlDataReader rdr) 
        {
            while(rdr.Read())
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
