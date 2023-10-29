using MarketDomain;

namespace TurtleAPI.AlphaVantage
{
    public class AlphaVantageResult : MarketDetail
    {
        public static IEnumerable<MarketDetail> ParseJson(string json)
        {
            return AlphaVantageResult.Parse(new Dictionary<DateTime, TimeDetail>());
        }
        private static IEnumerable<MarketDetail> Parse(Dictionary<DateTime, TimeDetail> data)
        {
            foreach (var item in data)
            {
                MarketDetail marketDetail = new();
                marketDetail.Date = item.Key;
                marketDetail.Open = item.Value.Open;
                marketDetail.High = item.Value.High;
                marketDetail.Low = item.Value.Low;
                marketDetail.Close = item.Value.Close;
                marketDetail.AdjustedClose = item.Value.AdjustedClose;
                marketDetail.Volume = item.Value.Volume;
                marketDetail.DividendAmount = item.Value.DividendAmount;
                marketDetail.SplitCoefficient = item.Value.SplitCoefficient;
                yield return marketDetail;
            }
        }

    }
}