using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using MarketDomain;
using Microsoft.Data.SqlClient;
using TurtleSQL.Extensions;

namespace TurtleSQL
{
    //This should work, but needs to be validated! FURTHER, wut do with AddressDetail? Separate repo?
    public class TickerDetailRepository : Repository<TickerDetail>, IRepository<TickerDetail>
    {
        protected override string TableName => "TickerDetail";
        protected override List<string> FieldList => new() { "Ticker", "Name", "Description", "Address", "TotalEmployees", "ListDate" };
        protected override IEnumerable<SqlParameter> SqlParameters(TickerDetail entity)
        {
            var parms = new List<SqlParameter>
            {
                new SqlParameter("Ticker", entity.Ticker.DBValue()),
                new SqlParameter("Name", entity.Name.DBValue()),
                new SqlParameter("Description", entity.Description.DBValue()),
                new SqlParameter("Address", entity.Address.DBValue()),
                new SqlParameter("TotalEmployees", entity.TotalEmployees.DBValue()),
                new SqlParameter("ListDate", entity.ListDate.DBValue())
            };
            return parms;
        }
        protected virtual IEnumerable<TickerDetail> AllFromReader(SqlDataReader rdr)
        {
            while (rdr.Read())
            {
                yield return new TickerDetail
                {
                    Ticker = rdr["Ticker"].ToString(),
                    Name = rdr["Name"].ToString(),
                    Description = rdr["Description"].ToString(),
                    //Address = rdr. Literal wut do
                    TotalEmployees = rdr.Parse<Int64>("TotalEmployees"),
                    ListDate = rdr.Parse<DateOnly>("ListDate"),
                    Id = rdr.Parse<int>("Id") ?? 0
                };
            };
        }
    }
}
