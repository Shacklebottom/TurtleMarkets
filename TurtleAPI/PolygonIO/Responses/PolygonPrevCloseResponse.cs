using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.PolygonIO.Responses
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    internal class PolygonPrevCloseResponse
    {
        public PolygonPreviousCloseResult[]? results { get; set; }
    }
}