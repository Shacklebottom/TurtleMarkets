using ApiModule.BaseClasses;
using LoggerModule.Interfaces;
using MarketDomain;
using TurtleAPI.FinnhubIO.Responses;

namespace TurtleAPI.FinnhubIO
{
    public class FinnhubAPI : ApiBaseClass, IFinnhubAPI
    {
        public FinnhubAPI(ILogger logger, int msSleepTime = 1000) : base(logger, msSleepTime) { }

        //IMPORTANT! FINNHUB API HAS 60 CALLS / MINUTE
        public async Task<PreviousClose?> GetPreviousClose(string ticker)
        {
            var uri = new Uri($"https://finnhub.io/api/v1/quote?symbol={ticker}");

            var requestHeaders = new List<KeyValuePair<string, string>>
            {
                new("X-Finnhub-Token", AuthData.API_KEY_FINNHUB),
                new("X-Finnhub-Secret", AuthData.SECRET_FINNHUB)
            };

            var response = await CallAPIAsync<FinnhubPrevCloseResponse>(uri, requestHeaders);
            var results = response?.First();
            var marketDetail = new PreviousClose
            {
                Ticker = ticker,
                Date = ParseUnixTimestamp(results?.t),
                Open = results?.o,
                Close = results?.c,
                High = results?.h,
                Low = results?.l,
                Volume = null,
            };

            return marketDetail;
        }

        private static DateTime? ParseUnixTimestamp(decimal? t)
        {
            if (t != null)
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

                var dateTime = epoch.AddSeconds((double)t);

                return dateTime;
            }

            return null;
        }

        public async Task<IEnumerable<RecommendedTrend>?> GetRecommendedTrend(string ticker)
        {
            //has a repository : Validated!
            var uri = new Uri($"https://finnhub.io/api/v1/stock/recommendation?symbol={ticker}&token={AuthData.API_KEY_FINNHUB}");

            var requestHeaders = new List<KeyValuePair<string, string>>
            {
                new("X-Finnhub-Token", AuthData.API_KEY_FINNHUB),
                new("X-Finnhub-Secret", AuthData.SECRET_FINNHUB)
            };

            var response = await CallAPIAsync<List<FinnhubTrendResponse>>(uri, requestHeaders);
            var results = response?.First();
            var recommendedTrend = results?.Select(r => new RecommendedTrend
            {
                Ticker = ticker,
                Buy = r.buy,
                Hold = r.hold,
                Period = r.period,
                Sell = r.sell,
                StrongBuy = r.strongBuy,
                StrongSell = r.strongSell
            });

            return recommendedTrend;
        }
    }
}
