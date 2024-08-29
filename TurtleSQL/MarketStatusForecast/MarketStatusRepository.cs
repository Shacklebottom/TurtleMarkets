using MarketDomain.Objects;
using Microsoft.Data.SqlClient;
using TurtleSQL.BaseClasses;
using TurtleSQL.Extensions;
using TurtleSQL.Interfaces;

namespace TurtleSQL.MarketStatusForecast
{
    public class MarketStatusRepository : Repository<MarketStatus>, IRepository<MarketStatus>
    {
        protected override string TableName => "MarketStatus";

        protected override List<string> FieldList => ["MarketType", "Region", "Exchange", "LocalOpen", "LocalClose", "Status", "Notes"];
        
        protected override IEnumerable<SqlParameter> SqlParameters(MarketStatus entity)
        {
            var parms = new List<SqlParameter>
            {
                new("MarketType", entity.MarketType.DBValue()),
                new("Region", entity.Region.DBValue()),
                new("Exchange", entity.Exchange.DBValue()),
                new("LocalOpen", entity.LocalOpen.DBValue()),
                new("LocalClose", entity.LocalClose.DBValue()),
                new("Status", entity.Status.DBValue()),
                new("Notes", entity.Notes.DBValue())
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
