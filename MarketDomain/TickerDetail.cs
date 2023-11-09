

namespace MarketDomain
{
    public class TickerDetail : IEntity
    {
        //has a repository
        public override string ToString()
        {
            return $"Ticker: {Ticker}\tListDate: {ListDate}\tName: {Name}\tDescription: {Description}";
        }
        public string? Ticker { get; set; }
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
