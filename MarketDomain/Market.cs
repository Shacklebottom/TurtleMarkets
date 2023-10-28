using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketObjects
{
    public class Market
    {
        public string? Name { get; set; }
        public string? Ticker { get; set; }
        public MarketEnum.TradeType TradeType { get; set; }
        public string? Description { get; set; }
        public long NetWorth { get; set; }
    }
}
