using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketDomain
{
    public class ListedStatus
    {
        public override string ToString()
        {
            return $"Ticker: {Ticker}\tName {Name}\tExchange {Exchange}\tStatus {Status}";
        }
        public string? Ticker { get; set; }
        public string? Name { get; set; }
        public string? Exchange { get; set; }
        public string? Type { get; set; }
        public DateOnly? IPOdate { get; set; }
        public string? DelistingDate { get; set; }
        public string? Status { get; set; }


    }
}
