using MarketDomain.Interfaces;

namespace MarketDomain.Objects
{
    public class PreviousClose : ITicker
    {
        //has a repository
        public override string ToString()
        {
            return $"Ticker: {Ticker}\tDate: {Date}\tOpen: {Open}\tClose: {Close}";
        }
        public string Ticker { get; set; } = string.Empty;
        public DateTime? Date { get; set; }
        public double? Open { get; set; }
        public double? Close { get; set; }
        public double? High { get; set; }
        public double? Low { get; set; }
        public long? Volume { get; set; }
        public int Id { get; set; }
    }
}
