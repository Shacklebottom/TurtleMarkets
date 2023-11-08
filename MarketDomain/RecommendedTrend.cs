

namespace MarketDomain
{
    public class RecommendedTrend : IEntity
    {
        //this has a repository
        public override string ToString()
        {
            return $"Ticker: {Ticker}\tPeriod: {Period}\tBuy: {Buy}\tHold: {Hold}\tSell {Sell}";
        }
        public string? Ticker { get; set; }
        public int? Buy { get; set; }
        public int? Hold { get; set; }
        public DateOnly? Period { get; set; }
        public int? Sell { get; set; }
        public int? StrongBuy { get; set; }
        public int? StrongSell { get; set; }
        public int Id { get; set; }
    }
}
