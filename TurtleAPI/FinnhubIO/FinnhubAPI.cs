using MarketDomain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TurtleAPI.FinnhubIO
{
    public class FinnhubAPI
    {
        public PreviousClose? GetPreviousClose(string ticker)
        {
            //has a repository
            var uri = new Uri($"https://finnhub.io/api/v1/quote?symbol={ticker}");
            var client = new HttpClient()
            {
                BaseAddress = uri,
            };
            client.DefaultRequestHeaders.Add("X-Finnhub-Token", AuthData.API_KEY_FINNHUB);
            client.DefaultRequestHeaders.Add("X-Finnhub-Secret", AuthData.SECRET_FINNHUB);
            var response = client.GetAsync(uri).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            var baseData = JsonConvert.DeserializeObject<FinnhubPrevCloseResponse>(responseString) ??
                throw new Exception("could not parse Finnhub response");
            var marketDetail = new PreviousClose
            {
                Ticker = ticker,
                Date = ParseUnixTimestamp(baseData.t),
                Open = baseData?.o,
                Close = baseData?.c,
                High = baseData?.h,
                Low = baseData?.l,
                Volume = null,
            };
            return marketDetail;
        }
        private static DateTime ParseUnixTimestamp(decimal t)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dateTime = epoch.AddMilliseconds((float)t);
            return dateTime;
        }
        public RecommendedTrend? GetRecommendedTrend(string ticker)
        {
            //has a repository
            var uri = new Uri($"https://finnhub.io/api/v1/stock/recommendation?symbol={ticker}&token={AuthData.API_KEY_FINNHUB}");
            var client = new HttpClient()
            {
                BaseAddress = uri,
            };
            client.DefaultRequestHeaders.Add("X-Finnhub-Token", AuthData.API_KEY_FINNHUB);
            client.DefaultRequestHeaders.Add("X-Finnhub-Secret", AuthData.SECRET_FINNHUB);
            var response = client.GetAsync(uri).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            var baseData = JsonConvert.DeserializeObject<List<FinnhubTrendResponse>>(responseString) ??
                throw new Exception("could not parse Finnhub response");
            var recommendedTrend = baseData?.Select(r => new RecommendedTrend
            {
                Ticker = ticker,
                Buy = r.buy,
                Hold = r.hold,
                Period = r.period,
                Sell = r.sell,
                StrongBuy = r.strongBuy,
                StrongSell = r.strongSell
            });
            return recommendedTrend?.First();
        }
    }
}
