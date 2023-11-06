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
            var baseData = JsonConvert.DeserializeObject<AlphaVPrevCloseResponse>(responseString) ??
                throw new Exception("could not parse Alpha Vantage response");
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
        public MarketStatus? GetMarketStatus()
        {
            var uri = new Uri($"https://www.alphavantage.co/query?function=MARKET_STATUS&apikey={AuthData.API_KEY_ALPHAVANTAGE}");
            var client = new HttpClient
            {
                BaseAddress = uri
            };
            var response = client.GetAsync(uri).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            var baseData = JsonConvert.DeserializeObject<AlphaVMarketStatusResponse>(responseString) ??
                throw new Exception("could not parse Alpha Vantage response");
            var results = baseData?.results?.Select(r => new MarketStatus
            {
                MarketType = r.market_type,
                Region = r.region,
                Exchange = r.primary_exchanges,
                LocalOpen = r.local_open,
                LocalClose = r.local_close,
                Status = r.current_status,
                Notes = r.notes
            });
            return results.FirstOrDefault();
        }
        public IEnumerable<Prominence>? GetPolarizedMarkets()
        {
            //returns the top and bottom 20 tickers, and the 20 most traded.
            IEnumerable<Prominence>? polarizedMarkets = new List<Prominence>();
            var uri = new Uri($"https://www.alphavantage.co/query?function=TOP_GAINERS_LOSERS&apikey={AuthData.API_KEY_ALPHAVANTAGE}");
            var client = new HttpClient
            {
                BaseAddress = uri
            };
            var response = client.GetAsync(uri).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            var baseData = JsonConvert.DeserializeObject<AlphaVProminenceResponse>(responseString) ??
                throw new Exception("could not parse Alpha Vantage response");
            polarizedMarkets = baseData?.top_gainers?.Select(tg => new Prominence
            {
                Ticker = tg.ticker,
                Price = tg.price,
                ChangeAmount = tg.change_amount,
                ChangePercentage = tg.change_percentage,
                Volume = tg.volume,
                Prestige = "Top Gainer"
            }).Concat(baseData.top_losers.Select(tl => new Prominence
            {
                Ticker = tl.ticker,
                Price = tl.price,
                ChangeAmount = tl.change_amount,
                ChangePercentage = tl.change_percentage,
                Volume = tl.volume,
                Prestige = "Top Loser"
            })).Concat(baseData.most_actively_traded.Select(m => new Prominence
            {
                Ticker = m.ticker,
                Price = m.price,
                ChangeAmount = m.change_amount,
                ChangePercentage = m.change_percentage,
                Volume = m.volume,
                Prestige = "Most Actively Traded"
            }));
            return polarizedMarkets;
        }
    }
}
