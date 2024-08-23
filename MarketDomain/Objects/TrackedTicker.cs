using MarketDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketDomain.Objects
{
    public class TrackedTicker : ITicker
    {
        public string Ticker { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}
