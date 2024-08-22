

using MarketDomain.Interfaces;

namespace MarketDomain
{
    public class ListedStatus : ITicker
    {
        //has a repository
        public override string ToString()
        {
            return $"Ticker: {Ticker}\tName {Name}\tExchange {Exchange}\tStatus {Status}\tDelistingDate {DelistingDate?.ToString() ?? "(null)"}";
        }
        public string Ticker { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? Exchange { get; set; } 
        public string? Type { get; set; }
        public DateTime? IPOdate { get; set; }
        public DateTime? DelistingDate { get; set; }
        public string? Status { get; set; }
        public int Id { get; set; }

    }
}
