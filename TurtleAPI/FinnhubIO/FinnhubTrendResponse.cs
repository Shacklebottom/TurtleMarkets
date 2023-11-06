using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleAPI.FinnhubIO
{
    public class FinnhubTrendResponse
    {
        public int? buy { get; set; }
        public int? hold { get; set; }
        public DateOnly? period { get; set; }
        public int? sell { get; set; }
        public int? strongBuy { get; set; }
        public int? strongSell { get; set; }

    }
}
