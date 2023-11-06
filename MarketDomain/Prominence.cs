using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketDomain
{
    public class Prominence : IEntity
    {
        public string? Ticker { get; set; }
        public decimal? Price { get; set; }
        public decimal? ChangeAmount { get; set; }
        public string? ChangePercentage { get; set; }
        public long? Volume { get; set; }
        public string? Prestige { get; set; }
        public int Id { get; set; }
    }
}
