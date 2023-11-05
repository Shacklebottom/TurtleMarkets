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
            var uri = new Uri($"https://finnhub.io/api/v1/quote?symbol={ticker}");

            var client = new HttpClient()
            {
                BaseAddress = uri,

            };
            client.DefaultRequestHeaders.Add("X-Finnhub-Token", AuthData.API_KEY_FINNHUB);
            client.DefaultRequestHeaders.Add("X-Finnhub-Secret", AuthData.SECRET_FINNHUB);
            var response = client.GetAsync(uri).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            var baseData = JsonConvert.DeserializeObject<FinnhubMarketDetail>(responseString);
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
            //if (t == null) return DateTime.Now;
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dateTime = epoch.AddMilliseconds((float)t);
            return dateTime;
        }
    }
}
