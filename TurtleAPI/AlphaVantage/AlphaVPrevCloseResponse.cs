using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.AlphaVantage
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class AlphaVPrevCloseResponse
    {
        [JsonProperty("Global Quote")]
        public AlphaVPreviousCloseResult? results { get; set; }
    }
}
