

namespace MarketDomain
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

        public decimal? Open { get; set; }

        public decimal? Close { get; set; }
        public decimal? High { get; set; }

        public decimal? Low { get; set; }

        public long? Volume { get; set; }
        
        public int Id { get; set; }
    }
}
