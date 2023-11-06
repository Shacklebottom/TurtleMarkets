using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.PolygonIO
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    internal class PolygonPrevCloseResponse
    {
        public PolygonPreviousClose[]? results { get; set; }
    }
}