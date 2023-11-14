using Newtonsoft.Json;

namespace TurtleAPI.PolygonIO.Responses
{
    internal class PolygonTickerAddress
    {
        [JsonProperty("address1")]
        public string? Address1 { get; set; }
        [JsonProperty("city")]
        public string? City { get; set; }
        [JsonProperty("state")]
        public string? State { get; set; }
    }
}
