using MarketDomain;
using Microsoft.Data.SqlClient;
using TurtleSQL.Extensions;
using TurtleSQL.Interfaces;

namespace TurtleSQL.TickerRepositories
{

    public class DividendDetailRepository : TickerRepository<DividendDetails>, ITickerRepository<DividendDetails>
    {
        protected override string TableName => "DividendDetails";
        protected override List<string> FieldList => new()
        {
            "Ticker",
            "PayoutPerShare",
            "DividendType",
            "PayoutFrequency",
            "DividendDeclaration",
            "OpenBeforeDividend",
            "PayoutDate",
            "OwnBeforeDate"
        };
        protected override IEnumerable<SqlParameter> SqlParameters(DividendDetails entity)
        {
            var parms = new List<SqlParameter>
            {
                new SqlParameter("@Ticker", entity.Ticker.DBValue()),
                new SqlParameter("@PayoutPerShare", entity.PayoutPerShare.DBValue()),
                new SqlParameter("@DividendType", entity.DividendType.DBValue()),
                new SqlParameter("@PayoutFrequency", entity.PayoutFrequency.DBValue()),
                new SqlParameter("@DividendDeclaration", entity.DividendDeclaration.DBValue()),
                new SqlParameter("@OpenBeforeDividend", entity.OpenBeforeDividend.DBValue()),
                new SqlParameter("@PayoutDate", entity.PayoutDate.DBValue()),
                new SqlParameter("@OwnBeforeDate", entity.OwnBeforeDate.DBValue())
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
