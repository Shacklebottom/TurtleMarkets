using MarketDomain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleAPI.AlphaVantage
{
    public class AlphaVantageAPI
    {
        public MarketDetail? GetPreviousClose(string ticker)
        {
            var uri = new Uri($"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol=IBM&apikey={AuthData.API_KEY_ALPHAVANTAGE}");
            var client = new HttpClient
            {
                BaseAddress = uri
            };

            var response = client.GetAsync(uri).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(responseString);
            return null;
        }


    }
}
