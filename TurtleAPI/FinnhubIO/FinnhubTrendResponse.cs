using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.FinnhubIO
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class FinnhubTrendResponse
    {
        public int? buy { get; set; }
        public int? hold { get; set; }
        public DateTime? period { get; set; }
        public int? sell { get; set; }
        public int? strongBuy { get; set; }
        public int? strongSell { get; set; }

    }
}
