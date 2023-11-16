

using MarketDomain.Interfaces;

namespace MarketDomain
{
    public class MarketStatus : IEntity
    {
        public override string ToString()
        {
            return $"Market Type: {MarketType}\tRegion: {Region}\tExchange: {Exchange}\tLocal Open: {LocalOpen}\tLocal Close: {LocalClose}\tStatus: {Status}";
        }
        public string? MarketType { get; set; }
        public string? Region { get; set; }
        public string? Exchange { get; set; }
        public DateTime? LocalOpen { get; set; }
        public DateTime? LocalClose { get; set; }
        public int? TimeOffset { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public int Id { get; set; }
    }
}
