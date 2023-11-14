using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.FinnhubIO.Responses
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    internal class FinnhubPrevCloseResponse
    {
        //Response isn't nested like Polygon and AlphaVantage are, so we only need the Response
        public double? c { get; set; } //close
        public double? d { get; set; } // (!)investigate
        public double? dp { get; set; } // (!)investigate
        public double? h { get; set; } //high
        public double? l { get; set; } //low
        public double? o { get; set; } //open
        public decimal? pc { get; set; } //percent changed
        public decimal? t { get; set; } //unix timestamp
    }
}
