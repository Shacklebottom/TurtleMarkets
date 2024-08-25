using ApiModule.BaseClasses;
using LoggerModule.Interfaces;
using MarketDomain.Enums;
using MarketDomain.Objects;
using TurtleAPI.AlphaVantage.Responses;

namespace TurtleAPI.AlphaVantage
{
    public class AlphaVantageAPI : ApiBaseClass, IAlphaVantageAPI
    {
        private readonly HttpClient _httpClient;

        //constructor
        public AlphaVantageAPI(ILogger logger, int msToSleep = 0) : base(logger, msToSleep)
        {
            _httpClient = new HttpClient() 
            { 
                BaseAddress = new Uri("https://www.alphavantage.co/")
            };
        }

        //IMPORTANT! ALPHA VANTAGE API HAS 25 CALLS ===>PER DAY<===
        #region API CALLS
        public async Task<IEnumerable<MarketStatus>?> GetMarketStatus()
        {
            //has a repository! : Validated!
            var response = await CallAPIAsync<AlphaVMarketStatusResponse>(_httpClient, $"query?function=MARKET_STATUS");

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

        public async Task<Dictionary<EnumPrestigeType, IEnumerable<Prominence>>?> GetPolarizedMarkets()
        {
            //has a repository : Validated!
            //returns the top and bottom 20 tickers, and the 20 most traded.
            Dictionary<EnumPrestigeType, IEnumerable<Prominence>> prominenceDetail = [];

            var response = await CallAPIAsync<AlphaVProminenceResponse>(_httpClient, $"query?function=TOP_GAINERS_LOSERS&apikey={AuthData.API_KEY_ALPHAVANTAGE}");
            var results = response?.First();

            var topGainers = results?.top_gainers.Select(tg => BuildProminence(tg, EnumPrestigeType.TopGainer));
            var topLosers = results?.top_losers.Select(tl => BuildProminence(tl, EnumPrestigeType.TopLoser));
            var mostActivelyTraded = results?.most_actively_traded.Select(m => BuildProminence(m, EnumPrestigeType.MostTraded));

            prominenceDetail.Add(EnumPrestigeType.TopGainer, topGainers == null ? [] : new List<Prominence>(topGainers));
            prominenceDetail.Add(EnumPrestigeType.TopLoser, topLosers == null ? [] : new List<Prominence>(topLosers));
            prominenceDetail.Add(EnumPrestigeType.MostTraded, mostActivelyTraded == null ? [] : new List<Prominence>(mostActivelyTraded));

            return prominenceDetail;
        }

        /// <summary>
        /// Provides a list of all active or delisted tickers as of the latest trading day.
        /// </summary>
        /// <param name="statusRequest">ListedStatusType.Active or ListedStatusType.Delisted</param>
        /// <returns>IEnumberable of all active or delisted tickers</returns>
        public async Task<IEnumerable<ListedStatus>?> GetListedStatus(EnumListedStatusTypes listingType = EnumListedStatusTypes.Active)
        {
            //has a repository : Validated!
            var results = await CallAPIAsync(_httpClient, $"query?function=LISTING_STATUS&state={listingType.ToString().ToLower()}&apikey={AuthData.API_KEY_ALPHAVANTAGE}", parser: ParseListedStatus);

            return results;
        }
        #endregion

        #region HELPER FUNCTIONS
        private static Prominence BuildProminence(AlphaVProminenceResult r, EnumPrestigeType pt)
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
        #endregion
    }
}
