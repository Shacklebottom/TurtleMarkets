using MarketDomain.Interfaces;

namespace MarketDomain.Objects
{
    public class Prominence : ITicker
    {
        //has a repository
        public override string ToString()
        {
            return $"Ticker: {Ticker}\tPrice {Price}\tChange Amount {ChangeAmount}";
        }
        public string? PrestigeType { get; set; }
        public DateTime? Date { get; set; }
        public string Ticker { get; set; } = string.Empty;
        public double? Price { get; set; }
        public double? ChangeAmount { get; set; }
        public string? ChangePercentage { get; set; }
        public long? Volume { get; set; }
        public int Id { get; set; }
    }
}
