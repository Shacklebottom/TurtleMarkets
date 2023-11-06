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
    internal class PolygonPreviousClose
    {
        public string? T { get; set; } //Ticker
        public decimal o { get; set; } //open
        public decimal c { get; set; } //close
        public decimal h { get; set; } //high
        public decimal l { get; set; } //low
        public decimal n { get; set; } // (!)investigate
        public decimal t { get; set; } //unix timestamp
        public long v { get; set; } //volume

    }
}
