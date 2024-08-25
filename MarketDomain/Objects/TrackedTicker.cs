using MarketDomain.Interfaces;

namespace MarketDomain.Objects
{
    public class TrackedTicker : ITicker
    {
        public string Ticker { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}
