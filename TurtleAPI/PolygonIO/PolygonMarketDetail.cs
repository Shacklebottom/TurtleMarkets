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
        public float o { get; set; }
        public float c { get; set; }
        public float h { get; set; }
        public float l { get; set; }
        public float n { get; set; }
        public float t { get; set; } //unix timestamp
        public float v { get; set; }

    }
}
