using MarketDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleAPI.PolygonIO
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class PolygonTickerDetailResults
    {
        public string? name { get; set; }
        public string? market { get; set; }
        public string? locale { get; set; }
        public string? primary_exchange { get; set; }
        public string? type { get; set; }
        public string? active { get; set; }
        public string? currency_name { get; set; }
        public string? phone_number { get; set; }
        public TickerAddress? address { get; set; }
        public string? description { get; set; }
        public long total_employees { get; set; }
        public DateOnly list_date { get; set; }
    }
}
