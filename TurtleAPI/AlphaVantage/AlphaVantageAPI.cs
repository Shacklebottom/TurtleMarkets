using MarketDomain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleAPI.Interfaces;

namespace TurtleAPI.AlphaVantage
{
    public class AlphaVantageAPI : IMarketAPI
    {
        public IEnumerable<MarketDetail>? GetMarketDetails(string ticker, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MarketDetail> ParseJson(string json)
        {
            return Parse(new Dictionary<DateTime, TimeDetail>());
        }

        private IEnumerable<MarketDetail> Parse(Dictionary<DateTime, TimeDetail> data)
        {
            return new List<MarketDetail>();
        }
        //private SourceHeader ParseSourceHeader(string json)
        //{
        //    var jsonobject = JsonConvert.DeserializeObject<AlphaVantageFormat>(json) ?? throw new Exception("oh no!");
        //    var sourceHeader = new SourceHeader
        //    {
        //        Source = AlphaVantageFormat.Source,
        //        MetaData = jsonobject.MetaData,
        //        MarketDetails = AlphaVantageResult.Parse(jsonobject.TimeSeries)
        //    };
        //    return sourceHeader;
        //}

    }
}
