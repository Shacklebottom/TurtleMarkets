using MarketDomain;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleAPI.PolygonIO
{

    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    internal class PolygonMarketDetail
    {
        public string? T { get; set; }
        public decimal o { get; set; }
        public decimal c { get; set; }
        public decimal h { get; set; }
        public decimal l { get; set; }
        public decimal n { get; set; }
        public decimal t { get; set; } //unix timestamp
        public long v { get; set; }

    }
}
