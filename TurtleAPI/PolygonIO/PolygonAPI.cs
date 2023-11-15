﻿using MarketDomain;
using Newtonsoft.Json;
using System.Net;
using TurtleAPI.BaseClasses;
using TurtleAPI.Exceptions;
using TurtleAPI.PolygonIO.Responses;

namespace TurtleAPI.PolygonIO
{
    public class PolygonAPI : BaseTurtleAPI, IPolygonAPI
    {
        public PolygonAPI(int msToSleep = 12000) : base(msToSleep) { }

        //IMPORTANT! POLYGON API HAS 5 CALLS / MINUTE
        public PreviousClose GetPreviousClose(string ticker)
        {
            //maybe unnecessary??
            //has a repository : >Validated!<
            var uri = new Uri($"https://api.polygon.io/v2/aggs/ticker/{ticker}/prev?adjusted=true&apiKey={AuthData.API_KEY_POLYGON}");
            var baseData = CallAPI<PolygonPrevCloseResponse>(uri).First().results?[0];

            var marketDetails = new PreviousClose
            {
                Ticker = ticker,
                Close = baseData?.c,
                Date = ParseUnixTimestamp(baseData?.t),
                High = baseData?.h,
                Low = baseData?.l,
                Open = baseData?.o,
                Volume = baseData?.v,
            };
            return marketDetails;
        }

        public TickerDetail GetTickerDetails(string ticker)
        {
            //has repository : >Validated!<
            var uri = new Uri($"https://api.polygon.io/v3/reference/tickers/{ticker}?apiKey={AuthData.API_KEY_POLYGON}");
            
            var baseData = CallAPI<PolygonTickerDetailResponse>(uri).First().results;

            var tickerDetail = new TickerDetail
            {
                Ticker = ticker,
                Name = baseData?.name,
                Description = baseData?.description,
                Address = baseData?.address?.Address1,
                City = baseData?.address?.City,
                State = baseData?.address?.State,
                TotalEmployees = baseData?.total_employees,
                ListDate = baseData?.list_date
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

        public IEnumerable<MarketHoliday> GetMarketHoliday()
        {
            //has a repository : >Validated!<
            var uri = new Uri($"https://api.polygon.io/v1/marketstatus/upcoming?apiKey={AuthData.API_KEY_POLYGON}");
            
            var baseData = CallAPI<List<PolygonMarketHolidayResponse>>(uri).First();
            
            var holidayDetail = baseData.Select(r => new MarketHoliday
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

        public IEnumerable<DividendDetails> GetDividendDetails(string ticker)
        {
            //has a repository! : Validated
            var uri = new Uri($"https://api.polygon.io/v3/reference/dividends?ticker={ticker}&apiKey={AuthData.API_KEY_POLYGON}");
            
            var baseData = CallAPI<PolygonDividendResponse>(uri).First();
            
            var dividendDetail = baseData.results.Select(r =>
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
