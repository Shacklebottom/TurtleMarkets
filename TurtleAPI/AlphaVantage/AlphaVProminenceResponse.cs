using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.AlphaVantage
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class AlphaVProminenceResponse
    {
        public AlphaVProminenceResult[]? top_gainers { get; set; }
        public AlphaVProminenceResult[]? top_losers { get; set; }
        public AlphaVProminenceResult[]? most_actively_traded { get; set; }
    }
}
