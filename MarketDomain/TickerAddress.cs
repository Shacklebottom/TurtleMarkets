using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketDomain
{
    public class TickerAddress
    {
        [JsonProperty("address1")]
        public string? Address { get; set; }
        [JsonProperty("city")]
        public string? City { get; set; }
        [JsonProperty("state")]
        public string? State { get; set; }
    }
}
