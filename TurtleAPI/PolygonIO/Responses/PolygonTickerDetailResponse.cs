using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.PolygonIO.Responses
{
    internal class PolygonTickerDetailResponse
    {
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public PolygonTickerDetailResults? results { get; set; }
    }
}
