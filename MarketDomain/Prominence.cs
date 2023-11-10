

namespace MarketDomain
{
    public class Prominence : ITicker
    {
        //has a repository
        public override string ToString()
        {
            return $"Ticker: {Ticker}\tPrice {Price}\tChange Amount {ChangeAmount}";
        }
        public string Ticker { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public decimal? ChangeAmount { get; set; }
        public string? ChangePercentage { get; set; }
        public long? Volume { get; set; }
        public int Id { get; set; }
    }
}
