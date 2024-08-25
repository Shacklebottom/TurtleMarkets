﻿using ApiModule.BaseClasses;
using LoggerModule.Interfaces;
using MarketDomain.Objects;
using TurtleAPI.FinnhubIO.Responses;

namespace TurtleAPI.FinnhubIO
{
    public class FinnhubAPI : ApiBaseClass, IFinnhubAPI
    {
        private HttpClient _httpClient;
        public FinnhubAPI(ILogger logger, int msSleepTime = 1000) : base(logger, msSleepTime)
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://finnhub.io/api/v1/")
            };

            var requestHeaders = new List<KeyValuePair<string, string>>
            {
                new("X-Finnhub-Token", AuthData.API_KEY_FINNHUB),
                new("X-Finnhub-Secret", AuthData.SECRET_FINNHUB)
            };

            requestHeaders.ForEach(h => _httpClient.DefaultRequestHeaders.Add(h.Key, h.Value));
        }

        //IMPORTANT! FINNHUB API HAS 60 CALLS / MINUTE
        public async Task<PreviousClose?> GetPreviousClose(string ticker)
        {
            Thread.Sleep(SleepDuration);

            var response = await CallAPIAsync<FinnhubPrevCloseResponse>(_httpClient, $"quote?symbol={ticker}");
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
            return [];
            //Thread.Sleep(SleepDuration);

            //var uri = new Uri($"https://finnhub.io/api/v1/stock/recommendation?symbol={ticker}&token={AuthData.API_KEY_FINNHUB}");

            //var requestHeaders = new List<KeyValuePair<string, string>>
            //{
            //    new("X-Finnhub-Token", AuthData.API_KEY_FINNHUB),
            //    new("X-Finnhub-Secret", AuthData.SECRET_FINNHUB)
            //};

            //var response = await CallAPIAsync<List<FinnhubTrendResponse>>(uri, requestHeaders);
            //var results = response?.First();
            //var recommendedTrend = results?.Select(r => new RecommendedTrend
            //{
            //    Ticker = ticker,
            //    Buy = r.buy,
            //    Hold = r.hold,
            //    Period = r.period,
            //    Sell = r.sell,
            //    StrongBuy = r.strongBuy,
            //    StrongSell = r.strongSell
            //});

            //return recommendedTrend;
        }
    }
}
