using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MarketDomain
{
    public class MarketStatus : IEntity
    {
        //this will probably be used as a variable for logic gates, and not placed in a repo
        public override string ToString()
        {
            return $"Market Type: {MarketType}\tLocal Open: {LocalOpen}\tLocal Close: {LocalClose}\tStatus: {Status}";
        }
        public string? MarketType { get; set; }
        public string? Region { get; set; }
        public string? Exchange { get; set; }
        public string? LocalOpen { get; set; }
        public string? LocalClose { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public int Id { get; set; }
    }
}
