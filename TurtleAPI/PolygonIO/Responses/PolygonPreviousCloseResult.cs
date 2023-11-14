using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.PolygonIO.Responses
{

    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    internal class PolygonPreviousCloseResult
    {
        public string T { get; set; } = string.Empty; //Ticker
        public double o { get; set; } //open
        public double c { get; set; } //close
        public double h { get; set; } //high
        public double l { get; set; } //low
        public double n { get; set; } // (!)investigate
        public decimal t { get; set; } //unix timestamp
        public long v { get; set; } //volume

    }
}
