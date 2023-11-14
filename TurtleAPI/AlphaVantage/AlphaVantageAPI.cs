using CsvHelper;
using MarketDomain;
using MarketDomain.Enums;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using TurtleAPI.BaseClasses;
using TurtleAPI.Exceptions;
using TurtleAPI.FinnhubIO;

namespace TurtleAPI.AlphaVantage
{
    public class AlphaVantageAPI : BaseTurtleAPI
    {
        //IMPORTANT! ALPHA VANTAGE API HAS 25 CALLS ===>PER DAY<===
        public AlphaVantageAPI(int msToSleep = 2400) : base(msToSleep) { }

        public PreviousClose? GetPreviousClose(string ticker)
        {
            //has a repository : Validated!
            var uri = new Uri($"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={ticker}&apikey={AuthData.API_KEY_ALPHAVANTAGE}");
            var results = CallAPI<AlphaVPrevCloseResponse>(uri).results;

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
        public static IEnumerable<MarketStatus>? GetMarketStatus()
        {
            //has a repository! : Validated!
            var uri = new Uri($"https://www.alphavantage.co/query?function=MARKET_STATUS&apikey={AuthData.API_KEY_ALPHAVANTAGE}");
            var client = new HttpClient
            {
                BaseAddress = uri
            };

            var response = client.GetAsync(uri).Result;
            if (response.StatusCode != HttpStatusCode.OK) // 200 == OK
            {
                throw new ApiException(response);
            }

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

            return results;
        }
        public static Dictionary<PrestigeType, IEnumerable<Prominence>?> GetPolarizedMarkets()
        {
            //has a repository : Validated!
            //returns the top and bottom 20 tickers, and the 20 most traded.
            Dictionary<PrestigeType, IEnumerable<Prominence>?>? prominenceDetail = new();

            var uri = new Uri($"https://www.alphavantage.co/query?function=TOP_GAINERS_LOSERS&apikey={AuthData.API_KEY_ALPHAVANTAGE}");
            var client = new HttpClient
            {
                BaseAddress = uri
            };

            var response = client.GetAsync(uri).Result;
            if (response.StatusCode != HttpStatusCode.OK) // 200 == OK
            {
                throw new ApiException(response);
            }

            var responseString = response.Content.ReadAsStringAsync().Result;
            var baseData = JsonConvert.DeserializeObject<AlphaVProminenceResponse>(responseString) ??
                throw new Exception("could not parse Alpha Vantage response");

            prominenceDetail.Add(PrestigeType.TopGainer, baseData.top_gainers?.Select(tg => BuildProminence(tg)));
            prominenceDetail.Add(PrestigeType.TopLoser, baseData.top_losers?.Select(tl => BuildProminence(tl)));
            prominenceDetail.Add(PrestigeType.MostTraded, baseData.most_actively_traded?.Select(m => BuildProminence(m)));

            return prominenceDetail;
        }
        private static Prominence BuildProminence(AlphaVProminenceResult r)
        {
            return new Prominence
            {
                Ticker = r.ticker,
                Price = r.price,
                ChangeAmount = r.change_amount,
                ChangePercentage = r.change_percentage,
                Volume = r.volume,
            };
        }
        /// <summary>
        /// Provides a list of all active or delisted tickers as of the latest trading day.
        /// </summary>
        /// <param name="statusRequest">ListedStatusType.Active or ListedStatusType.Delisted</param>
        /// <returns>IEnumberable of all active or delisted tickers</returns>
        public static IEnumerable<ListedStatus> GetListedStatus(ListedStatusTypes listingType = ListedStatusTypes.Active)
        {
            //has a repository : Validated!
            var uri = new Uri($"https://www.alphavantage.co/query?function=LISTING_STATUS&state={listingType.ToString().ToLower()}&apikey={AuthData.API_KEY_ALPHAVANTAGE}");
            var client = new HttpClient
            {
                BaseAddress = uri
            };

            var response = client.GetAsync(uri).Result;
            if (response.StatusCode != HttpStatusCode.OK) // 200 == OK
            {
                throw new ApiException(response);
            }

            var responseString = response.Content.ReadAsStringAsync().Result; // this "should" be a string of CSV data
            var result = ParseListedStatus(responseString);

            return result;
        }

        private static IEnumerable<ListedStatus> ParseListedStatus(string csvData)
        {
            var result = new List<ListedStatus>();

            var lines = csvData.Split("\r\n");
            //var header = lines[0];
            var data = lines.Skip(1);

            foreach (var line in data)
            {
                if (string.IsNullOrEmpty(line)) continue;

                var elements = line.Split(',');

                var ls = new ListedStatus
                {
                    Ticker = elements[0],
                    Name = elements[1],
                    Exchange = elements[2],
                    Type = elements[3],
                    IPOdate = elements[4] == "null" ? null : DateTime.Parse(elements[4]),
                    DelistingDate = elements[5] == "null" ? null : DateTime.Parse(elements[5]),
                    Status = elements[6]
                };

                result.Add(ls);
            }
            return result;
        }
    }
}
