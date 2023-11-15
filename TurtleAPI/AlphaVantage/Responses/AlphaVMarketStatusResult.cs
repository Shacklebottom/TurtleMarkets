using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.AlphaVantage.Responses
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    internal class AlphaVMarketStatusResult
    {
        public string? market_type { get; set; }
        public string? region { get; set; }
        public string? primary_exchanges { get; set; }
        public DateTime? local_open { get; set; }
        public DateTime? local_close { get; set; }
        public string? current_status { get; set; }
        public string? notes { get; set; }
    }
}
