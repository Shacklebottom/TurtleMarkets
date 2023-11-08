using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.FinnhubIO
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class FinnhubPrevCloseResponse
    {
        //Response isn't nested like Polygon and AlphaVantage are, so we only need the Response
        public decimal c { get; set; } //close
        public decimal d { get; set; } // (!)investigate
        public decimal dp { get; set; } // (!)investigate
        public decimal h { get; set; } //high
        public decimal l { get; set; } //low
        public decimal o { get; set; } //open
        public decimal pc { get; set; } //percent changed
        public decimal t { get; set; } //unix timestamp
    }
}
