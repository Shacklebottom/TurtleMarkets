using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketDomain
{
    public class SourceHeader
    {
        public string? Source { get; set; }
        public MetaData? MetaData { get; set; }
        public IEnumerable<MarketDetail>? MarketDetails { get; set; }
        public static SourceHeader ParseJson(string json)
        {
            var jsonobject = JsonConvert.DeserializeObject<AlphaVantageFormat>(json) ?? throw new Exception("oh no!");
            var sourceHeader = new SourceHeader();
            sourceHeader.Source = AlphaVantageFormat.Source;
            sourceHeader.MetaData = jsonobject.MetaData;
            sourceHeader.MarketDetails = MarketDetail.Parse(jsonobject.TimeSeries);
            return sourceHeader;
        }
    }
}
