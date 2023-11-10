﻿using MarketDomain;
using Newtonsoft.Json;


namespace TurtleAPI.PolygonIO
{
    public class PolygonAPI
    {
        public static PreviousClose? GetPreviousClose(string ticker)
        {
            //has a repository : Validated!
            var uri = new Uri($"https://api.polygon.io/v2/aggs/ticker/{ticker}/prev?adjusted=true&apiKey={AuthData.API_KEY_POLYGON}");
            var client = new HttpClient
            {
                BaseAddress = uri
            };
            var response = client.GetAsync(uri).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            var baseData = JsonConvert.DeserializeObject<PolygonPrevCloseResponse>(responseString) ??
                throw new Exception("could not parse Polygon response");
            var marketDetails = baseData?.results?.Select(r => new PreviousClose
            {
                Ticker = ticker,
                Close = r.c,
                Date = ParseUnixTimestamp(r.t),
                High = r.h,
                Low = r.l,
                Open = r.o,
                Volume = r.v,
            });
            return marketDetails?.First();
        }
        public static TickerDetail? GetTickerDetails(string ticker)
        {
            //has repository : Validated!
            var uri = new Uri($"https://api.polygon.io/v3/reference/tickers/{ticker}?apiKey={AuthData.API_KEY_POLYGON}");
            var client = new HttpClient
            {
                BaseAddress = uri
            };
            var response = client.GetAsync(uri).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            var baseData = JsonConvert.DeserializeObject<PolygonTickerDetailResponse>(responseString) ??
                throw new Exception("could not parse Polygon response");
            var tickerDetail = new TickerDetail
            {
                Ticker = ticker,
                Name = baseData?.results?.name,
                Description = baseData?.results?.description,
                Address = baseData?.results?.address?.Address1,
                City = baseData?.results?.address?.City,
                State = baseData?.results?.address?.State,
                TotalEmployees = baseData?.results?.total_employees,
                ListDate = baseData?.results?.list_date
            };
               return tickerDetail;
        }
        private static DateTime ParseUnixTimestamp(decimal t)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dateTime = epoch.AddMilliseconds((float)t);
            return dateTime;
        }
        public static IEnumerable<MarketHoliday>? GetMarketHoliday()
        {
            //has a repository : Validated!
            var uri = new Uri($"https://api.polygon.io/v1/marketstatus/upcoming?apiKey={AuthData.API_KEY_POLYGON}");
            var client = new HttpClient
            {
                BaseAddress = uri
            };
            var response = client.GetAsync(uri).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            var baseData = JsonConvert.DeserializeObject<List<PolygonMarketHolidayResponse>>(responseString) ??
                throw new Exception("could not parse Polygon response");
            var holidayDetail = baseData?.Select(r => new MarketHoliday
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
        public static IEnumerable<DividendDetails>? GetDividendDetails(string ticker)
        {
            //has a repository! : PayPerShare isn't being recorded as a decimal for some reason!
            var uri = new Uri($"https://api.polygon.io/v3/reference/dividends?ticker={ticker}&apiKey={AuthData.API_KEY_POLYGON}");
            var client = new HttpClient
            {
                BaseAddress = uri
            };
            var response = client.GetAsync(uri).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            var baseData = JsonConvert.DeserializeObject<PolygonDividendResponse>(responseString) ??
                throw new Exception("could not parse Polygon response");
            var dividendDetail = baseData?.results?.Select(r =>
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
            return dividendDetail;
        }
    }
}
