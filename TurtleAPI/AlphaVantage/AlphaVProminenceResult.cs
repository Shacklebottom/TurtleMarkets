using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.AlphaVantage
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class AlphaVProminenceResult
    {
        public string ticker { get; set; } = string.Empty;
        public decimal? price { get; set; }
        public decimal? change_amount { get; set; }
        public string? change_percentage { get; set; }
        public long? volume { get; set; }
    }
}
