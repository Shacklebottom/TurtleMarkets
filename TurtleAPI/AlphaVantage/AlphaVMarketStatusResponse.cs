using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleAPI.AlphaVantage
{
    public class AlphaVMarketStatusResponse
    {
        [JsonProperty("markets")]
        public AlphaVMarketStatusResult[]? results { get; set; }
    }
}
