

namespace MarketDomain
{

    public class WorkTicker : IEntity
    {
        public override string ToString()
        {
            return $"Ticker: {Ticker}";
        }
        public string? Ticker { get; set; }
        public int Id { get; set; }

    }
}
