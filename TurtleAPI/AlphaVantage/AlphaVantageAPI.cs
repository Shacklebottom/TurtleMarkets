using ApiModule.BaseClasses;
using LoggerModule.Interfaces;
using MarketDomain;
using MarketDomain.Enums;
using TurtleAPI.AlphaVantage.Responses;

namespace TurtleAPI.AlphaVantage
{
    public class AlphaVantageAPI(ILogger logger, int msToSleep = 0) : ApiBaseClass(logger, msToSleep), IAlphaVantageAPI
    {
        //IMPORTANT! ALPHA VANTAGE API HAS 25 CALLS ===>PER DAY<===
        public async Task<PreviousClose> GetPreviousClose(string ticker)
        {
            //has a repository : Validated! (we'll probably never use this?)
            var uri = new Uri($"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={ticker}&apikey={AuthData.API_KEY_ALPHAVANTAGE}");
            
            var response = await CallAPIAsync<AlphaVPrevCloseResponse>(uri);
            var results = response?.First().results;

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

        public async Task<IEnumerable<MarketStatus>?> GetMarketStatus()
        {
            //has a repository! : Validated!
            var uri = new Uri($"https://www.alphavantage.co/query?function=MARKET_STATUS&apikey={AuthData.API_KEY_ALPHAVANTAGE}");
            var response = await CallAPIAsync<AlphaVMarketStatusResponse>(uri);

            var results = response?.First().results;

            var marketStatus = results?.Select(r => new MarketStatus
            {
                MarketType = r.market_type,
                Region = r.region,
                Exchange = r.primary_exchanges,
                LocalOpen = r.local_open,
                LocalClose = r.local_close,
                Status = r.current_status,
                Notes = r.notes
            });

            return marketStatus;
        }

        public async Task<Dictionary<PrestigeType, IEnumerable<Prominence>>> GetPolarizedMarkets()
        {
            //has a repository : Validated!
            //returns the top and bottom 20 tickers, and the 20 most traded.
            Dictionary<PrestigeType, IEnumerable<Prominence>> prominenceDetail = new();

            var uri = new Uri($"https://www.alphavantage.co/query?function=TOP_GAINERS_LOSERS&apikey={AuthData.API_KEY_ALPHAVANTAGE}");
            
            var response = await CallAPIAsync<AlphaVProminenceResponse>(uri);
            var results = response?.First();


            prominenceDetail.Add(PrestigeType.TopGainer, results.top_gainers.Select(tg => BuildProminence(tg, PrestigeType.TopGainer)));
            
            prominenceDetail.Add(PrestigeType.TopLoser, results.top_losers.Select(tl => BuildProminence(tl, PrestigeType.TopLoser)));
            
            prominenceDetail.Add(PrestigeType.MostTraded, results.most_actively_traded.Select(m => BuildProminence(m, PrestigeType.MostTraded)));

            return prominenceDetail;
        }
        private static Prominence BuildProminence(AlphaVProminenceResult r, PrestigeType pt)
        {
            return new Prominence
            {
                PrestigeType = pt.ToString(),
                Date = DateTime.Now,
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
        public async Task<IEnumerable<ListedStatus>?> GetListedStatus(ListedStatusTypes listingType = ListedStatusTypes.Active)
        {
            //has a repository : Validated!
            var uri = new Uri($"https://www.alphavantage.co/query?function=LISTING_STATUS&state={listingType.ToString().ToLower()}&apikey={AuthData.API_KEY_ALPHAVANTAGE}");
            
            var results = await CallAPIAsync(uri, parser: ParseListedStatus);
            
            return results;
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
