﻿using System.Diagnostics.CodeAnalysis;

namespace TurtleAPI.PolygonIO
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
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
