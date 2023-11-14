using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.PolygonIO.Responses
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    internal class PolygonTickerDetailResults
    {
        public string? name { get; set; }
        public string? market { get; set; }
        public string? locale { get; set; }
        public string? primary_exchange { get; set; }
        public string? type { get; set; }
        public string? active { get; set; }
        public string? currency_name { get; set; }
        public string? phone_number { get; set; }
        public PolygonTickerAddress? address { get; set; }
        public string? description { get; set; }
        public long total_employees { get; set; }
        public DateTime list_date { get; set; }
    }
}
