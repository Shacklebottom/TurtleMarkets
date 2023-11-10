﻿using MarketDomain;
using Microsoft.Data.SqlClient;
using TurtleSQL.Extensions;

namespace TurtleSQL
{
    public class ProminenceRepository : TickerRepository<Prominence>, ITickerRepository<Prominence>
    {
        //This should work, but needs to be validated!
        protected override string TableName => "Prominence";
        protected override List<string> FieldList => new() { "Ticker", "Price", "ChangeAmount", "ChangePercentage", "Volume"};
        protected override IEnumerable<SqlParameter> SqlParameters(Prominence entity)
        {
            var parms = new List<SqlParameter>
            {
                new SqlParameter("@Ticker", entity.Ticker.DBValue()),
                new SqlParameter("@Price", entity.Price.DBValue()),
                new SqlParameter("@ChangeAmount", entity.ChangeAmount.DBValue()),
                new SqlParameter("@ChangePercentage", entity.ChangePercentage.DBValue()),
                new SqlParameter("@Volume", entity.Volume.DBValue())
            };
            return parms;
        }
        protected override IEnumerable<Prominence> AllFromReader(SqlDataReader rdr)
        {
            while (rdr.Read())
            {
                yield return new Prominence
                {
                    Ticker = rdr["Ticker"].ToString() ?? string.Empty,
                    Price = rdr.ParseNullable<decimal>("Price"),
                    ChangeAmount = rdr.ParseNullable<decimal>("ChangeAmount"),
                    ChangePercentage = rdr["ChangePercentage"].ToString(),
                    Volume = rdr.ParseNullable<Int64>("Volume"),
                    Id = rdr.ParseNullable<int>("Id") ?? 0
                };
            }
        }
    }
}
