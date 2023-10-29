using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketDomain
{

    internal class AlphaVantageFormat
    {
        public const string Source = "Alpha Vantage";
        [JsonProperty("Meta Data")]
        public MetaData? MetaData { get; set; }
        [JsonProperty("Time Series (Daily)")]
        public Dictionary<DateTime, TimeDetail> TimeSeries { get; set; } = new();
    }
}
