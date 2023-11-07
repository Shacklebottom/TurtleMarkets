using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleAPI.PolygonIO
{
    public class PolygonMarketHolidayResponse
    {
        public string? exchange { get; set; }
        public DateTime? date { get; set; }
        public string? name { get; set; }
        public string? status { get; set; }
        public DateTime? open { get; set; }
        public DateTime? close { get; set; }
    }
}
