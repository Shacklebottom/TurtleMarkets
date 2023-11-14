using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.AlphaVantage.Responses
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class AlphaVMarketStatusResponse
    {
        [JsonProperty("markets")]
        public AlphaVMarketStatusResult[]? results { get; set; }
    }
}
