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
    public class Repository
    {
        private SqlConnection _connection;

        #region Constructor Overloads
        public Repository()
        {
            _connection = new SqlConnection("TrustServerCertificate=True;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MarketData;Data Source=.");
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

        public void SaveAll(Dictionary<string, IEnumerable<MarketDetail>> data)
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
                    adjustedCloseParm.Value = marketData.AdjustedClose;
                    closeParm.Value = marketData.Close;
                    dateParm.Value = marketData.Date;
                    dividendAmountParm.Value = marketData.DividendAmount;
                    highParm.Value = marketData.High;
                    lowParm.Value = marketData.Low;
                    openParm.Value = marketData.Open;
                    splitCoefficientParm.Value = marketData.SplitCoefficient;
                    volumeParm.Value = marketData.Volume;
                    volumeWeightedParm.Value = marketData.VolumeWeighted;

                    command.ExecuteNonQuery();
                }
            }
            _connection.Close();
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
