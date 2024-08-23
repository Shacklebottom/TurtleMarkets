using MarketDomain.Interfaces;

namespace MarketDomain.Objects
{
    public class TickerDetail : ITicker
    {
        //has a repository
        public override string ToString()
        {
            return $"Id: {Id}\tTicker: {Ticker}\tListDate: {ListDate}\tName: {Name}\tDescription: {Description}";
        }
        public string Ticker { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public long? TotalEmployees { get; set; }
        public DateTime? ListDate { get; set; }
        public int Id { get; set; }
    }
}
