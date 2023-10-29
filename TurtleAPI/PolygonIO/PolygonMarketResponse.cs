using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.PolygonIO
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    internal class PolygonMarketResponse
    {
        public string? adjusted { get; set; }
        public int count { get; set; }
        public int queryCount { get; set; }
        public string? request_id { get; set; }
        public PolygonMarketDetail[]? results { get; set; }
        public int resultsCount { get; set; }
        public string? status { get; set; }
        public string? ticker { get; set; }
    }
}