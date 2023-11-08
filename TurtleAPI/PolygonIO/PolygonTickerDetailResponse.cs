using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.PolygonIO
{
    public class PolygonTickerDetailResponse
    {
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public PolygonTickerDetailResults? results { get; set; }
    }
}
