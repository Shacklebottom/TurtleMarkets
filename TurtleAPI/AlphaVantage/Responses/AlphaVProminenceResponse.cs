using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.AlphaVantage.Responses
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    internal class AlphaVProminenceResponse
    {
        public AlphaVProminenceResult[] top_gainers { get; set; } = Array.Empty<AlphaVProminenceResult>();
        public AlphaVProminenceResult[] top_losers { get; set; } = Array.Empty<AlphaVProminenceResult>();
        public AlphaVProminenceResult[] most_actively_traded { get; set; } = Array.Empty<AlphaVProminenceResult>();
    }
}
