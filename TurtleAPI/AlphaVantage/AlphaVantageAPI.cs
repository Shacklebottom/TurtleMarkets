using MarketDomain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleAPI.PolygonIO;

namespace TurtleAPI.AlphaVantage
{
    public class AlphaVantageAPI
    {
        public PreviousClose? GetPreviousClose(string ticker)
        {
            var uri = new Uri($"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={ticker}&apikey={AuthData.API_KEY_ALPHAVANTAGE}");
            var client = new HttpClient
            {
                BaseAddress = uri
            };

            var response = client.GetAsync(uri).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            var baseData = JsonConvert.DeserializeObject<AlphaVMarketResponse>(responseString);
            var results = baseData?.results;
            var marketDetails = new PreviousClose
            {
                Ticker = ticker,
                Date = results?.latest,
                Open = results?.open,
                Close = results?.price,
                High = results?.high,
                Low = results?.low,
                Volume = results?.volume
            };
            return marketDetails;
        }
    }
}
