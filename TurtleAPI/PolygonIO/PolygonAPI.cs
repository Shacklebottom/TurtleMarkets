using MarketDomain;
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

        public IEnumerable<MarketDetail> GetMarketDetails(string ticker, DateTime startDate, DateTime endDate)
        {
            var uri = new Uri($"https://api.polygon.io/v2/aggs/ticker/{ticker}/range/1/day/{endDate:yyyy-MM-dd}/{startDate:yyyy-MM-dd}?apiKey={AuthData.API_KEY_POLYGON}");

            var client = new HttpClient
            {
                BaseAddress = uri
            };

            var response = client.GetAsync(uri).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine($"{responseString}\n\n-=> END OF LINE <=-");
            Console.ReadKey();

            return new List<MarketDetail>();
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
