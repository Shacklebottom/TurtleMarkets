using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleAPI.FinnhubIO
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class FinnhubMarketDetail
    {
        public decimal c { get; set; }
        public decimal d { get; set; }
        public decimal dp { get; set; }
        public decimal h { get; set; }
        public decimal l { get; set; }
        public decimal o { get; set; }
        public decimal pc { get; set; }
        public decimal t { get; set; }
    }
}
