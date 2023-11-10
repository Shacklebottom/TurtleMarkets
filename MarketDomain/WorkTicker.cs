

namespace MarketDomain
{

    public class WorkTicker : IEntity
    {
        public override string ToString()
        {
            return $"Ticker: {Ticker}";
        }
        public string? Ticker { get; set; }
        public decimal? Open { get; set; }
        public decimal? Close { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public int? Buy { get; set; }
        public int? Sell { get; set; }
        public int? Hold { get; set; }
        public int Id { get; set; }

    }
}
