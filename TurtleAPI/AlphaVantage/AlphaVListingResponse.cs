using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.AlphaVantage
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class AlphaVListingResponse
    {
        public string? symbol { get; set; }
        public string? name { get; set; }
        public string? exchange { get; set; }
        public string? assetType { get; set; }
        public DateOnly? ipoDate { get; set; }
        public string? delistingDate { get; set; }
        public string? status { get; set; }
    }
}
