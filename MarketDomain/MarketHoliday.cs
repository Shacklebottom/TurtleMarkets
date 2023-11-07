using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketDomain
{
    public class MarketHoliday
    {
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
    }
}
