using MarketDomain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleAPI.Interfaces;

namespace TurtleAPI.PolygonIO
{
    public class PolygonAPI : IMarketAPI
    {

        public IEnumerable<MarketDetail>? GetMarketDetails(string ticker, DateTime startDate, DateTime endDate)
        {
            var uri = new Uri($"https://api.polygon.io/v2/aggs/ticker/{ticker}/range/1/day/{endDate:yyyy-MM-dd}/{startDate:yyyy-MM-dd}?apiKey={AuthData.API_KEY_POLYGON}");

            var client = new HttpClient
            {
                BaseAddress = uri
            };

            var response = client.GetAsync(uri).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            var baseData = JsonConvert.DeserializeObject<PolygonMarketResponse>(responseString);

            var marketDetails = baseData?.results?.Select(r => new MarketDetail
            {
                AdjustedClose = null,
                Close = r.c,
                Date = ParseUnixTimestamp(r.t),
                DividendAmount = null,
                High = r.h,
                Low = r.l,
                Open = r.o,
                SplitCoefficient = null,
                Volume = r.v,
                VolumeWeighted = r.vw
            });

            return marketDetails;
        }

        private static DateTime ParseUnixTimestamp(float t)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dateTime = epoch.AddMilliseconds(t);

            return dateTime;
        }

        public IEnumerable<MarketDetail> ParseJson(string json)
        {
            return Parse(new Dictionary<DateTime, TimeDetail>());
        }

        private IEnumerable<MarketDetail> Parse(Dictionary<DateTime, TimeDetail> data)
        {
            return new List<MarketDetail>();
        }

    }
}
