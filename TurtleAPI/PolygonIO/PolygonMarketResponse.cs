using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.PolygonIO
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    internal class PolygonMarketResponse
    {
        public PolygonMarketDetail[]? results { get; set; }
        public string? status { get; set; }
        public string? ticker { get; set; }
    }
}