using ApiModule.BaseClasses;
using LoggerModule.Interfaces;
using MarketDomain.Objects;
using TurtleAPI.PolygonIO.Responses;

namespace TurtleAPI.PolygonIO
{
    public class PolygonAPI : ApiBaseClass, IPolygonAPI
    {
        private readonly HttpClient _httpClient;

        //constructor
        public PolygonAPI(ILogger logger, int msToSleep = 12000) : base(logger, msToSleep)
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://api.polygon.io/")
            };

            var requestHeaders = new List<KeyValuePair<string, string>>
            {
                new("Authorization", $"Bearer {AuthData.API_KEY_POLYGON}")
            };
            requestHeaders.ForEach(h => _httpClient.DefaultRequestHeaders.Add(h.Key, h.Value));
        }

        //IMPORTANT! POLYGON API HAS 5 CALLS / MINUTE
        #region API CALLS
        public async Task<TickerDetail?> GetTickerDetails(string ticker)
        {
            Thread.Sleep(SleepDuration);
            //has repository : >Validated!<

            var response = await CallAPIAsync<PolygonTickerDetailResponse>(_httpClient, $"v3/reference/tickers/{ticker}");
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

        public async Task<IEnumerable<MarketHoliday>?> GetMarketHoliday()
        {
            Thread.Sleep(SleepDuration);
            //has a repository : >Validated!<

            var response = await CallAPIAsync<List<PolygonMarketHolidayResponse>>(_httpClient, "v1/marketstatus/upcoming");
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
            Thread.Sleep(SleepDuration);
            //has a repository! : Validated

            var response = await CallAPIAsync<PolygonDividendResponse>(_httpClient, $"v3/reference/dividends?ticker={ticker}");
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

            if (dividendDetail?.Count() == 0)
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
        #endregion
    }
}
