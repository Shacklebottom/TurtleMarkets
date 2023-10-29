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
    public class Repository
    {
        private SqlConnection _connection;

        #region Constructor Overloads
        public Repository()
        {
            _connection = new SqlConnection("data provider=.; initial catalog=MarketData; integrated security=SSPI");
        }
        public Repository(string cnString)
        {
            _connection = new SqlConnection(cnString);
        }
        #endregion

        public IEnumerable<MarketDetail> GetAll(string ticker)
        {
            var command = _connection.CreateCommand();

            // sanitize input by using SqlParameter objects
            command.Parameters.AddWithValue("tickerParameter", ticker);
            command.CommandText = "SELECT * FROM MarketDetail WHERE Ticker=@tickerParameter";

            _connection.Open();
            var reader = command.ExecuteReader();
            var details = MarketDetailsFromReader(reader).ToList();
            _connection.Close();

            return details;
        }

        private static IEnumerable<MarketDetail> MarketDetailsFromReader(SqlDataReader reader)
        {
            while (reader.Read())
            {
                var detail = new MarketDetail
                {
                    AdjustedClose = reader.Parse<float>("AdjustedClose"),
                    Close = reader.Parse<float>("Close"),
                    Date = reader.Parse<DateTime>("Date"),
                    DividendAmount = reader.Parse<float>("DividendAmount"),
                    High = reader.Parse<float>("High"),
                    Low = reader.Parse<float>("Low"),
                    Open = reader.Parse<float>("Open"),
                    SplitCoefficient = reader.Parse<float>("SplitCoefficient"),
                    Volume = reader.Parse<float>("Volume"),
                    VolumeWeighted = reader.Parse<float>("VolumeWeighted")
                };

                yield return detail;
            }
        }
    }
}
