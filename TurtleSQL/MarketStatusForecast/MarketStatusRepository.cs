using MarketDomain.Objects;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleSQL.BaseClasses;
using TurtleSQL.Extensions;
using TurtleSQL.Interfaces;

namespace TurtleSQL.MarketStatusForecast
{
    public class MarketStatusRepository : Repository<MarketStatus>, IRepository<MarketStatus>
    {
        protected override string TableName => "MarketStatus";
        protected override List<string> FieldList => new() { "MarketType", "Region", "Exchange", "LocalOpen", "LocalClose", "Status", "Notes"};
        protected override IEnumerable<SqlParameter> SqlParameters(MarketStatus entity)
        {
            var parms = new List<SqlParameter>
            {
                new SqlParameter("MarketType", entity.MarketType.DBValue()),
                new SqlParameter("Region", entity.Region.DBValue()),
                new SqlParameter("Exchange", entity.Exchange.DBValue()),
                new SqlParameter("LocalOpen", entity.LocalOpen.DBValue()),
                new SqlParameter("LocalClose", entity.LocalClose.DBValue()),
                new SqlParameter("Status", entity.Status.DBValue()),
                new SqlParameter("Notes", entity.Notes.DBValue())
            };
            return parms;
        }
        protected override IEnumerable<MarketStatus> AllFromReader(SqlDataReader rdr)
        {
            while (rdr.Read())
            {
                yield return new MarketStatus()
                {
                    MarketType = rdr["MarketType"].ToString(),
                    Region = rdr["Region"].ToString(),
                    Exchange = rdr["Exchange"].ToString(),
                    LocalOpen = rdr.ParseNullable<DateTime>("LocalOpen"),
                    LocalClose = rdr.ParseNullable<DateTime>("LocalClose"),
                    TimeOffset = rdr.ParseNullable<int>("TimeOffset"),
                    Status = rdr["Status"].ToString(),
                    Notes = rdr["Notes"].ToString()
                }; 
            }
        }
    }
}
