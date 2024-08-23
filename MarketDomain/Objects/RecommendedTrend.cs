using MarketDomain.Interfaces;

namespace MarketDomain.Objects
{
    public class RecommendedTrend : ITicker
    {
        //this has a repository
        public override string ToString()
        {
            return $"Ticker: {Ticker}\tPeriod: {Period}\tBuy: {Buy}\tHold: {Hold}\tSell {Sell}";
        }
        public string Ticker { get; set; } = string.Empty;
        public int? Buy { get; set; }
        public int? Hold { get; set; }
        public DateTime? Period { get; set; }
        public int? Sell { get; set; }
        public int? StrongBuy { get; set; }
        public int? StrongSell { get; set; }
        public int Id { get; set; }
    }
}
