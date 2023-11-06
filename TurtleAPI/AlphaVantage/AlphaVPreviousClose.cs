using MarketDomain;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.AlphaVantage
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class AlphaVPreviousClose
    {
        [JsonProperty("02. open")]
        public decimal open { get; set; }
        [JsonProperty("03. high")]
        public decimal high { get; set; }
        [JsonProperty("04. low")]
        public decimal low { get; set; }
        [JsonProperty("05. price")] //we have this set as "previous close" when there is a "previous close," below.
        public decimal price { get; set; }
        [JsonProperty("06. volume")]
        public long volume { get; set; }
        [JsonProperty("07. latest trading day")]
        public DateTime latest { get; set; }
        [JsonProperty("08. previous close")]
        public decimal previous { get; set; }
        
    }
}