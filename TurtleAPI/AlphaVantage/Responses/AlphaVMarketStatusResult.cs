using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.AlphaVantage.Responses
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class AlphaVMarketStatusResult
    {
        public string? market_type { get; set; }
        public string? region { get; set; }
        public string? primary_exchanges { get; set; }
        public string? local_open { get; set; }
        public string? local_close { get; set; }
        public string? current_status { get; set; }
        public string? notes { get; set; }
    }
}
