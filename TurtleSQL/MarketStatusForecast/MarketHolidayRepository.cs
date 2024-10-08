﻿using MarketDomain.Objects;
using Microsoft.Data.SqlClient;
using TurtleSQL.BaseClasses;
using TurtleSQL.Extensions;
using TurtleSQL.Interfaces;

namespace TurtleSQL.MarketStatusForecast
{
    public class MarketHolidayRepository : Repository<MarketHoliday>, IRepository<MarketHoliday>
    {
        protected override string TableName => "MarketHoliday";
        
        protected override List<string> FieldList => ["Exchange", "Date", "Holiday", "Status", "Open", "Close"];

        protected override IEnumerable<SqlParameter> SqlParameters(MarketHoliday entity)
        {
            var parms = new List<SqlParameter>
            {
                new("Exchange", entity.Exchange.DBValue()),
                new("Date", entity.Date.DBValue()),
                new("Holiday", entity.Holiday.DBValue()),
                new("Status", entity.Status.DBValue()),
                new("Open", entity.Open.DBValue()),
                new("Close", entity.Close.DBValue())
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
