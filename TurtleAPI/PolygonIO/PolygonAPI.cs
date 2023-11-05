using MarketDomain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TurtleAPI.PolygonIO
{
    public class PolygonAPI
    {

        public PreviousClose? GetPreviousClose(string ticker)
        {
            var uri = new Uri($"https://api.polygon.io/v2/aggs/ticker/{ticker}/prev?adjusted=true&apiKey={AuthData.API_KEY_POLYGON}");

            var client = new HttpClient
            {
                BaseAddress = uri
            };

            var response = client.GetAsync(uri).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            var baseData = JsonConvert.DeserializeObject<PolygonMarketResponse>(responseString);
            var marketDetails = baseData?.results?.Select(r => new PreviousClose
            {
                Ticker = ticker,
                Close = r.c,
                Date = ParseUnixTimestamp(r.t),
                High = r.h,
                Low = r.l,
                Open = r.o,
                Volume = r.v,
            });

            return marketDetails?.First();

        }
        private static DateTime ParseUnixTimestamp(decimal t)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dateTime = epoch.AddMilliseconds((float)t);

            return dateTime;
        }
    }
}
