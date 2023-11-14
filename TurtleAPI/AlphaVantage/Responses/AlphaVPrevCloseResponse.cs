using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.AlphaVantage.Responses
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    internal class AlphaVPrevCloseResponse
    {
        [JsonProperty("Global Quote")]
        public AlphaVPreviousCloseResult? results { get; set; }
    }
}
