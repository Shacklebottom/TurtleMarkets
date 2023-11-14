using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.AlphaVantage.Responses
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class AlphaVProminenceResult
    {
        public string ticker { get; set; } = string.Empty;
        public double? price { get; set; }
        public double? change_amount { get; set; }
        public string? change_percentage { get; set; }
        public long? volume { get; set; }
    }
}
