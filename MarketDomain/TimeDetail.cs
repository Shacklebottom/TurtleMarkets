using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MarketDomain
{
    public class TimeDetail
    {
        [JsonProperty("1. open")]
        public float Open { get; set; }
        [JsonProperty("2. high")]
        public float High { get; set; }
        [JsonProperty("3. low")]
        public float Low { get; set; }
        [JsonProperty("4. close")]
        public float Close { get; set; }
        [JsonProperty("5. adjusted close")]
        public float AdjustedClose { get; set; }
        [JsonProperty("6. volume")]
        public float Volume { get; set; }
        [JsonProperty("7. dividend amount")]
        public float DividendAmount { get; set; }
        [JsonProperty("8. split coefficient")]
        public float SplitCoefficient { get; set; }
    }
}
