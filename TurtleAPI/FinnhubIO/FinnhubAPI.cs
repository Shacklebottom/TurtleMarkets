using MarketDomain;
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
        public IEnumerable<MarketDetail>? GetMarketDetails(string ticker, DateTime startDate, DateTime endDate)
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
            Console.WriteLine(responseString);
            return null;
        }

    }
}
