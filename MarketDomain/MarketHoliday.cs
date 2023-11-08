

namespace MarketDomain
{
    public class MarketHoliday : IEntity
    {
        //has a repository
        public override string ToString()
        {
            return $"Exchange: {Exchange}\tDate: {Date}\tHoliday: {Holiday}\tStatus: {Status}";
        }
        public string? Exchange { get; set; }
        public DateTime? Date { get; set; }
        public string? Holiday { get; set; }
        public string? Status { get; set; }
        public DateTime? Open { get; set; }
        public DateTime? Close { get; set; }
        public int Id { get; set; }
    }
}
