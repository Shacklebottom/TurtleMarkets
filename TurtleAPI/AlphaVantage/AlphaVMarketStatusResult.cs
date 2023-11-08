﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleAPI.AlphaVantage
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class AlphaVMarketStatusResult
    {
        public string? market_type { get; set; }
        public string? region { get; set; }
        public string? primary_exchanges { get; set; }
        public string? local_open { get; set; }
        public string? local_close { get; set; }
        public string? current_status { get; set; }
        public string? notes { get; set; }
    }
}
