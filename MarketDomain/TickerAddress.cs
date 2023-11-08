using Newtonsoft.Json;

namespace MarketDomain
{
    public class TickerAddress
    {
        //not sure if this belongs here tbh.
        [JsonProperty("address1")]
        public string? Address1 { get; set; }
        [JsonProperty("city")]
        public string? City { get; set; }
        [JsonProperty("state")]
        public string? State { get; set; }
    }
}
