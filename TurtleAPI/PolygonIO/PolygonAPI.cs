using ApiModule.BaseClasses;
using LoggerModule.Interfaces;
using MarketDomain.Objects;
using TurtleAPI.PolygonIO.Responses;

namespace TurtleAPI.PolygonIO
{
    public class PolygonAPI(ILogger logger, int msToSleep = 12000) : ApiBaseClass(logger, msToSleep), IPolygonAPI
    {
        //IMPORTANT! POLYGON API HAS 5 CALLS / MINUTE
        public async Task<PreviousClose?> GetPreviousClose(string ticker)
        {
            //maybe unnecessary??
            //has a repository : >Validated!<
            var uri = new Uri($"https://api.polygon.io/v2/aggs/ticker/{ticker}/prev?adjusted=true&apiKey={AuthData.API_KEY_POLYGON}");
            var response = await CallAPIAsync<PolygonPrevCloseResponse>(uri);
            var results = response?.First().results?[0];

            var marketDetails = new PreviousClose
            {
                Ticker = ticker,
                Close = results?.c,
                Date = ParseUnixTimestamp(results?.t),
                High = results?.h,
                Low = results?.l,
                Open = results?.o,
                Volume = results?.v,
            };
            return marketDetails;
        }

        public async Task<TickerDetail?> GetTickerDetails(string ticker)
        {
            //has repository : >Validated!<
            var uri = new Uri($"https://api.polygon.io/v3/reference/tickers/{ticker}?apiKey={AuthData.API_KEY_POLYGON}");
            
            var response = await CallAPIAsync<PolygonTickerDetailResponse>(uri);
            var results = response?.First().results;

            var tickerDetail = new TickerDetail
            {
                Ticker = ticker,
                Name = results?.name,
                Description = results?.description,
                Address = results?.address?.Address1,
                City = results?.address?.City,
                State = results?.address?.State,
                TotalEmployees = results?.total_employees,
                ListDate = results?.list_date
            };
            return tickerDetail;
        }

        private static DateTime? ParseUnixTimestamp(decimal? t)
        {
            if (t != null)
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

                var dateTime = epoch.AddMilliseconds((double)t);

                return dateTime;
            }
            return null;
        }

        public async Task<IEnumerable<MarketHoliday>?> GetMarketHoliday()
        {
            //has a repository : >Validated!<
            var uri = new Uri($"https://api.polygon.io/v1/marketstatus/upcoming?apiKey={AuthData.API_KEY_POLYGON}");

            var response = await CallAPIAsync<List<PolygonMarketHolidayResponse>>(uri);
            var results = response?.First();

            var holidayDetail = results?.Select(r => new MarketHoliday
            {
                Exchange = r?.exchange,
                Date = r?.date,
                Holiday = r?.name,
                Status = r?.status,
                Open = r?.open,
                Close = r?.close,
            });
            return holidayDetail;
        }

        public async Task<IEnumerable<DividendDetails>?> GetDividendDetails(string ticker)
        {
            //has a repository! : Validated
            var uri = new Uri($"https://api.polygon.io/v3/reference/dividends?ticker={ticker}&apiKey={AuthData.API_KEY_POLYGON}");
            
            var response = await CallAPIAsync<PolygonDividendResponse>(uri);
            var results = response?.First();

            var dividendDetail = results?.results.Select(r =>
                new DividendDetails
                {
                    Ticker = ticker,
                    PayoutPerShare = r.cash_amount,
                    DividendType = r.dividend_type,
                    PayoutFrequency = r.frequency,
                    DividendDeclaration = r.declaration_date,
                    OpenBeforeDividend = r.ex_dividend_date,
                    PayoutDate = r.pay_date,
                    OwnBeforeDate = r.record_date
                });
            
            if (!dividendDetail.Any())
            {
                var emptyDetail = new List<DividendDetails>();
                
                var x = new DividendDetails
                {
                    Ticker = ticker,
                    PayoutPerShare = 0,
                    DividendType = "",
                    PayoutFrequency = 0,
                    DividendDeclaration = DateTime.Today,
                    OpenBeforeDividend = DateTime.Today,
                    PayoutDate = DateTime.Today,
                    OwnBeforeDate = DateTime.Today
                };
                emptyDetail.Add(x);
                
                return emptyDetail;
            }
            return dividendDetail;
        }
    }
}
