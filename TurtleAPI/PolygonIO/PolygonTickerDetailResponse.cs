﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleAPI.PolygonIO
{
    public class PolygonTickerDetailResponse
    {
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public PolygonTickerDetailResults? results { get; set; }
    }
}
