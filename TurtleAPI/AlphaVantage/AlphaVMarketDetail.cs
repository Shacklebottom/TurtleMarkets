using MarketDomain;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.AlphaVantage
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class AlphaVMarketDetail
    {
        /*
         * {
    "Global Quote": {
        "01. symbol": "MSFT",
        "02. open": "349.6300",
        "03. high": "354.3900",
        "04. low": "347.3300",
        "05. price": "352.8000",
        "06. volume": "23637673",
        "07. latest trading day": "2023-11-03",
        "08. previous close": "348.3200",
        "09. change": "4.4800",
        "10. change percent": "1.2862%"
    }
}
         */
        [JsonProperty("01. symbol")]
        public string? symbol { get; set; }
        [JsonProperty("02. open")]
        public float open { get; set; }
        [JsonProperty("03. high")]
        public float high { get; set; }
        [JsonProperty("04. low")]
        public float low { get; set; }
        [JsonProperty("05. price")]
        public float price { get; set; }
        [JsonProperty("06. volume")]
        public float volume { get; set; }
        [JsonProperty("07. latest trading day")]
        public DateTime latest { get; set; }
        [JsonProperty("08. previous close")]
        public float previous { get; set; }
        
    }
}