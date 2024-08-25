using MarketDomain.Objects;
using Microsoft.Data.SqlClient;
using TurtleSQL.BaseClasses;
using TurtleSQL.Extensions;
using TurtleSQL.Interfaces;

namespace TurtleSQL.TickerRepositories
{
    public class DividendDetailRepository : TickerRepository<DividendDetails>, ITickerRepository<DividendDetails>
    {
        protected override string TableName => "DividendDetails";
        
        protected override List<string> FieldList =>
        [
            "Ticker",
            "PayoutPerShare",
            "DividendType",
            "PayoutFrequency",
            "DividendDeclaration",
            "OpenBeforeDividend",
            "PayoutDate",
            "OwnBeforeDate"
        ];

        protected override IEnumerable<SqlParameter> SqlParameters(DividendDetails entity)
        {
            var parms = new List<SqlParameter>
            {
                new("@Ticker", entity.Ticker.DBValue()),
                new("@PayoutPerShare", entity.PayoutPerShare.DBValue()),
                new("@DividendType", entity.DividendType.DBValue()),
                new("@PayoutFrequency", entity.PayoutFrequency.DBValue()),
                new("@DividendDeclaration", entity.DividendDeclaration.DBValue()),
                new("@OpenBeforeDividend", entity.OpenBeforeDividend.DBValue()),
                new("@PayoutDate", entity.PayoutDate.DBValue()),
                new("@OwnBeforeDate", entity.OwnBeforeDate.DBValue())
            };
            return parms;
        }

        protected override IEnumerable<DividendDetails> AllFromReader(SqlDataReader rdr)
        {
            while (rdr.Read())
            {
                yield return new DividendDetails
                {
                    Ticker = rdr["Ticker"].ToString() ?? string.Empty,
                    PayoutPerShare = rdr.ParseNullable<double>("PayoutPerShare"),
                    DividendType = rdr["DividendType"].ToString(),
                    PayoutFrequency = rdr.ParseNullable<int>("PayoutFrequency"),
                    DividendDeclaration = rdr.ParseNullable<DateTime>("DividendDeclaration"),
                    OpenBeforeDividend = rdr.ParseNullable<DateTime>("OpenBeforeDividend"),
                    PayoutDate = rdr.ParseNullable<DateTime>("PayoutDate"),
                    OwnBeforeDate = rdr.ParseNullable<DateTime>("OwnBeforeDate")
                };
            }
        }

    }
}
