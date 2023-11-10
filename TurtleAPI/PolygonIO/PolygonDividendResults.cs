using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleAPI.PolygonIO
{
    public class PolygonDividendResults
    {
        public string? ticker { get; set; }
        public decimal? cash_amount { get; set; }
        public DateTime? declaration_date { get; set; }
        public string? dividend_type { get; set; }
        public DateTime? ex_dividend_date { get; set; }
        public int? frequency { get; set; }
        public DateTime? pay_date { get; set; }
        public DateTime? record_date { get; set; }
    }
}
