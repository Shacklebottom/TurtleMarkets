using CsvHelper;
using MarketDomain;
using Newtonsoft.Json;
using System.Globalization;

namespace TurtleAPI.AlphaVantage
{
    public class AlphaVantageAPI
    {
        public static PreviousClose? GetPreviousClose(string ticker)
        {
            //has a repository : Validated!
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
        public static MarketStatus? GetMarketStatus()
        {
            //this does not have a repo!
            //this is set up to return only USA as of right now, but could be easily modified to return all market status
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
            return results?.FirstOrDefault();
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
        /// <param name="statusRequest">Activity.Active or Activity.Delisted</param>
        /// <returns>IEnumberable of all active or delisted tickers</returns>
        public static IEnumerable<ListedStatus>? GetListedStatus(string statusRequest)
        {
            //has a repository : Validated!
            var uri = new Uri($"https://www.alphavantage.co/query?function=LISTING_STATUS&state={statusRequest}&apikey={AuthData.API_KEY_ALPHAVANTAGE}");
            var client = new HttpClient
            {
                BaseAddress = uri
            };
            var response = client.GetStreamAsync(uri).Result;
            var reader = new StreamReader(response);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<AlphaVListingResponse>();
            var statusDetail = records.Select(r => new ListedStatus
            {
                Ticker = r?.symbol,
                Name = r?.name,
                Exchange = r?.exchange,
                Type = r?.assetType,
                IPOdate = r?.ipoDate,
                DelistingDate = r?.delistingDate,
                Status = r?.status,
            });
            return statusDetail;
        }
    }
}
