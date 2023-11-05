using MarketDomain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleSQL.Extensions;

namespace TurtleSQL
{
    public class RepositoryOld
    {
        private readonly SqlConnection _connection;

        #region Constructor Overloads
        public RepositoryOld()
        {
            _connection = new SqlConnection("TrustServerCertificate=True;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MarketData;Data Source=.");
        }
        public RepositoryOld(string cnString)
        {
            _connection = new SqlConnection(cnString);
        }
        #endregion

        public IEnumerable<PreviousClose> GetAll(string ticker)
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

        public void SaveAll(Dictionary<string, IEnumerable<PreviousClose>> data)
        {
            var command = _connection.CreateCommand();
            command.CommandText = @"
INSERT INTO MarketDetails(
    Ticker, AdjustedClose, [Close], [Date], DividendAmount, [High], [Low], [Open],
    SplitCoefficient, Volume, VolumeWeighted)
VALUES (
    @ticker, @adjustedClose, @close, @date, @dividendAmount, @high, @low, @open,
    @splitCoefficient, @volume, @volumeWeighted)
";
            var tickerParm = command.Parameters.Add("ticker", SqlDbType.VarChar);
            var adjustedCloseParm = command.Parameters.Add("adjustedClose", SqlDbType.Float);
            var closeParm = command.Parameters.Add("close", SqlDbType.Float);
            var dateParm = command.Parameters.Add("date", SqlDbType.DateTime);
            var dividendAmountParm = command.Parameters.Add("dividendAmount", SqlDbType.Float);
            var highParm = command.Parameters.Add("high", SqlDbType.Float);
            var lowParm = command.Parameters.Add("low", SqlDbType.Float);
            var openParm = command.Parameters.Add("open", SqlDbType.Float);
            var splitCoefficientParm = command.Parameters.Add("splitCoefficient", SqlDbType.Float);
            var volumeParm = command.Parameters.Add("volume", SqlDbType.Float);
            var volumeWeightedParm = command.Parameters.Add("volumeWeighted", SqlDbType.Float);

            _connection.Open();
            foreach(var kvp in data)
            {
                foreach (var marketData in kvp.Value)
                {
                    tickerParm.Value = kvp.Key;
                    closeParm.Value = marketData.Close;
                    dateParm.Value = marketData.Date;
                    highParm.Value = marketData.High;
                    lowParm.Value = marketData.Low;
                    openParm.Value = marketData.Open;
                    volumeParm.Value = marketData.Volume;

                    command.ExecuteNonQuery();
                }
            }
            _connection.Close();
        }
        private static IEnumerable<PreviousClose> MarketDetailsFromReader(SqlDataReader reader)
        {
            while (reader.Read())
            {
                var detail = new PreviousClose
                {
                    Close = reader.Parse<decimal>("Close"),
                    Date = reader.Parse<DateTime>("Date"),
                    High = reader.Parse<decimal>("High"),
                    Low = reader.Parse<decimal>("Low"),
                    Open = reader.Parse<decimal>("Open"),
                    Volume = reader.Parse<long>("Volume"),
                };

                yield return detail;
            }
        }
    }
}
